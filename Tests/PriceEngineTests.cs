using System;
using System.Collections.Generic;
using ConsoleApp1.Engines;
using ConsoleApp1.Models;
using ConsoleApp1.QuotationSystems;
using NUnit.Framework;

namespace ConsoleApp1.Tests
{
    public class PriceEngineTests
    {
        private List<BaseQuotationSystem> _quotationSytems;

        [SetUp]
        public void Setup()
        {
            _quotationSytems = new List<BaseQuotationSystem> { 
                new QuotationSystem1("http://quote-system-1.com", "1234"),
                new QuotationSystem2("http://quote-system-2.com", "1235"),
                new QuotationSystem3("http://quote-system-3.com", "100")
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
    }
}
