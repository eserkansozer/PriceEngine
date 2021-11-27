﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Engines;
using ConsoleApp1.Models;
using NUnit.Framework;

namespace ConsoleApp1.Tests
{
    public class PriceEngineTests
    {
        [Test]
        public void PriceEngineShouldRaiseExceptionWhenRiskDataIsNull()
        {
            var tax = 0m;
            var insurerName = "testInsurer";
            
            var engine = new PriceEngine();
            var ex = Assert.Throws<ApplicationException>(() =>
                engine.GetPrice(new PriceRequest(null), out tax, out insurerName)
            );

            Assert.That(ex.Message, Is.EqualTo("Risk Data is missing"));
        }

        [Test]
        public void PriceEngineShouldRaiseExceptionWhenRiskDataFirstNameIsNull()
        {
            var tax = 0m;
            var insurerName = "testInsurer";

            var engine = new PriceEngine();
            var ex = Assert.Throws<ApplicationException>(() =>
                engine.GetPrice(new PriceRequest(new RiskData(null, "test", 100, "test", null)), out tax, out insurerName)
            );
            Assert.That(ex.Message, Is.EqualTo("First name is required"));
        }

        [Test]
        public void PriceEngineShouldRaiseExceptionWhenRiskDataLastNameIsNull()
        {
            var tax = 0m;
            var insurerName = "testInsurer";

            var engine = new PriceEngine();
            var ex = Assert.Throws<ApplicationException>(() =>
                engine.GetPrice(new PriceRequest(new RiskData("test", null, 100, "test", null)), out tax, out insurerName)
            );

            Assert.That(ex.Message, Is.EqualTo("Surname is required"));
        }

        [Test]
        public void PriceEngineShouldRaiseExceptionWhenRiskDataValueIsZero()
        {
            var tax = 0m;
            var insurerName = "testInsurer";

            var engine = new PriceEngine();
            var ex = Assert.Throws<ApplicationException>(() =>
                engine.GetPrice(new PriceRequest(new RiskData("test", "test", 0, "test", null)), out tax, out insurerName)
            );

            Assert.That(ex.Message, Is.EqualTo("Value is required"));
        }
    }
}