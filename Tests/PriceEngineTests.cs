using System;
using System.Collections.Generic;
using System.Dynamic;
using ConsoleApp1.Engines;
using ConsoleApp1.Models;
using ConsoleApp1.QuotationSystems;
using Moq;
using NUnit.Framework;

namespace ConsoleApp1.Tests
{
    public class PriceEngineTests
    {
        private List<BaseQuotationSystem> _quotationSytems;
        private Mock<BaseQuotationSystem> mockSystem1, mockSystem2, mockSystem3;

        [SetUp]
        public void Setup()
        {
            mockSystem1 = new Mock<BaseQuotationSystem>();
            dynamic response1 = new ExpandoObject();
            response1.Price = 123.45M;
            response1.IsSuccess = true;
            response1.Name = "Test Name";
            response1.Tax = 123.45M * 0.12M;
            mockSystem1.Setup(s => s.GetPrice(It.IsAny<PriceRequest>())).Returns((PriceRequest priceRequest) => response1);

            mockSystem2 = new Mock<BaseQuotationSystem>();
            dynamic response2 = new ExpandoObject();
            response2.Price = 234.56M;
            response2.IsSuccess = true;
            response2.Name = "qewtrywrh";
            response2.Tax = 234.56M * 0.12M;
            mockSystem2.Setup(s => s.GetPrice(It.IsAny<PriceRequest>())).Returns((PriceRequest priceRequest) => response2);

            mockSystem3 = new Mock<BaseQuotationSystem>();
            dynamic response3 = new ExpandoObject();
            response3.Price = 92.67M;
            response3.IsSuccess = true;
            response3.Name = "zxcvbnm";
            response3.Tax = 92.67M * 0.12M;
            mockSystem3.Setup(s => s.GetPrice(It.IsAny<PriceRequest>())).Returns((PriceRequest priceRequest) => response3);

            _quotationSytems = new List<BaseQuotationSystem> { 
                mockSystem1.Object,
                mockSystem2.Object,
                mockSystem3.Object
            };
        }

        [Test]
        public void PriceEngineShouldRaiseExceptionWhenRiskDataIsNull()
        {          
            var engine = new PriceEngine(_quotationSytems);
            var ex = Assert.Throws<ApplicationException>(() =>
                engine.GetPrice(new PriceRequest(null))
            );

            Assert.That(ex.Message, Is.EqualTo("Risk Data is missing"));
        }

        [Test]
        public void PriceEngineShouldRaiseExceptionWhenRiskDataFirstNameIsNull()
        {
            var engine = new PriceEngine(_quotationSytems);
            var ex = Assert.Throws<ApplicationException>(() =>
                engine.GetPrice(new PriceRequest(new RiskData(null, "test", 100, "test", null)))
            );
            Assert.That(ex.Message, Is.EqualTo("First name is required"));
        }

        [Test]
        public void PriceEngineShouldRaiseExceptionWhenRiskDataLastNameIsNull()
        {
            var engine = new PriceEngine(_quotationSytems);
            var ex = Assert.Throws<ApplicationException>(() =>
                engine.GetPrice(new PriceRequest(new RiskData("test", null, 100, "test", null)))
            );

            Assert.That(ex.Message, Is.EqualTo("Surname is required"));
        }

        [Test]
        public void PriceEngineShouldRaiseExceptionWhenRiskDataValueIsZero()
        {
            var engine = new PriceEngine(_quotationSytems);
            var ex = Assert.Throws<ApplicationException>(() =>
                engine.GetPrice(new PriceRequest(new RiskData("test", "test", 0, "test", null)))
            );

            Assert.That(ex.Message, Is.EqualTo("Value is required"));
        }

        [Test]
        public void PriceEngineShouldReturnTheBestPrice()
        {
            var riskData = new RiskData("John", "Smith", 500, "Cool New Phone", DateTime.Parse("1980-01-01"));
            var engine = new PriceEngine(_quotationSytems);
            var result = engine.GetPrice(new PriceRequest(riskData));
            Assert.That(result.InsurerName, Is.EqualTo("zxcvbnm"));
            Assert.That(result.Price, Is.EqualTo(92.67M));
            Assert.That(result.Tax, Is.EqualTo(92.67M * 0.12M));
        }

        [Test]
        public void IfRiskDataHasDOBNullThenDontCallQuotationSystem1()
        {
            var riskData = new RiskData("John", "Smith", 500, "Cool New Phone", null);
            var engine = new PriceEngine(_quotationSytems);
            var result = engine.GetPrice(new PriceRequest(riskData));

            mockSystem1.Verify(m=> m.GetPrice(It.IsAny<PriceRequest>()), Times.Never);
        }

        [Test]
        public void IfRiskDataHasCertainMakeDoCallQuotationSystem2()
        {
            var riskData = new RiskData("John", "Smith", 500, "examplemake1", null);
            var engine = new PriceEngine(_quotationSytems);
            var result = engine.GetPrice(new PriceRequest(riskData));

            mockSystem2.Verify(m => m.GetPrice(It.IsAny<PriceRequest>()), Times.Once);
        }

        [Test]
        public void IfRiskDataHasNotCertainMakeDontCallQuotationSystem2()
        {
            var riskData = new RiskData("John", "Smith", 500, "examplemake101", null);
            var engine = new PriceEngine(_quotationSytems);
            var result = engine.GetPrice(new PriceRequest(riskData));

            mockSystem2.Verify(m => m.GetPrice(It.IsAny<PriceRequest>()), Times.Never);
        }
    }
}
