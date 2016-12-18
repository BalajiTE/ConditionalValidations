﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConditionalValidations.Controllers;
using ConditionalValidations.Models;
using Moq;
using System.Net.Http;
using System.Web.Http;

namespace ConditionalValidations.Tests
{
    [TestClass]
    public class ConditionalValidationTest
    {
        private ConditionalValidationsController conditionalValidationsController = new ConditionalValidationsController();
        private Mock<ConditionalValidationsContext> context = new Mock<ConditionalValidationsContext>();
        private ConditionalValidation conditionalValidation;

        public void InitiateRequiredData()
        {
            conditionalValidationsController.Request = new HttpRequestMessage();
            conditionalValidationsController.Request.Properties["MS_HttpConfiguration"] = new HttpConfiguration();

            conditionalValidation = new ConditionalValidation
            {
                FieldOne = "Field One",
                FieldTwo = "Field Two",
                FieldThree = "Field Three",
                FieldFour = "Field Four",
                FieldFive = "Field Five",
                FieldSix = "Field Six",
                FieldSeven = DateTime.Now,
                FieldEight = 0
            };
        }

        #region Required Validator Test Methods
        [TestMethod]
        public void TestRequiredFieldOne()
        {
            InitiateRequiredData();
            try
            {
                conditionalValidation.FieldOne = "";
                var response = conditionalValidationsController.PostConditionalValidation(conditionalValidation);

                Assert.IsFalse(conditionalValidationsController.ModelState.IsValid);
                Assert.IsTrue(conditionalValidationsController.ModelState.Count == 1, "Field One is Required");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Assert.AreEqual("Field One is Required", validationError.ErrorMessage);
                    }
                }
            }
        }

        #endregion

        #region Required If Not Validator Test Methods

        [TestMethod]
        public void TestNotRequiredFieldTwoWhenFieldOneIsDelete()
        {
            InitiateRequiredData();
            conditionalValidation.FieldOne = "Test";
            conditionalValidationsController.PostConditionalValidation(conditionalValidation);
        }

        [TestMethod]
        public void TestRequiredFieldTwoWhenFieldOneIsNotTest()
        {
            InitiateRequiredData();
            conditionalValidation.FieldOne = "Test One";
            
            try
            {
                var result = conditionalValidationsController.PostConditionalValidation(conditionalValidation);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Assert.AreEqual("Field Two is Required", validationError.ErrorMessage);
                    }
                }
            }
        }

        #endregion

        #region Required If Validator Test Methods
        [TestMethod]
        public void TestRequiredFieldFourWhenFieldThreeIsTest()
        {
            InitiateRequiredData();
            conditionalValidation.FieldThree = "Test";
            try
            {
                conditionalValidationsController.PostConditionalValidation(conditionalValidation);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Assert.AreEqual("Field Four is Required", validationError.ErrorMessage);
                    }
                }
            }
        }

        [TestMethod]
        public void TestNotRequiredFieldFourWhenFieldThreeIsNotTest()
        {
            InitiateRequiredData();
            try
            {
                conditionalValidationsController.PostConditionalValidation(conditionalValidation);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                // Should not throw any catch as everything is as expected
            }
        }
        #endregion
        
        #region Required If Dep Field is Not Null Test Methods

        [TestMethod]
        public void TestFieldEightRequiredWhenFieldSevenIsNotNull()
        {
            InitiateRequiredData();
            conditionalValidation.FieldEight = null;

            try
            {
                conditionalValidationsController.PostConditionalValidation(conditionalValidation);

                Assert.IsFalse(conditionalValidationsController.ModelState.IsValid);
                Assert.IsTrue(conditionalValidationsController.ModelState.Count == 1, "Field Eight is Required");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Assert.AreEqual("Field Eight is Required", validationError.ErrorMessage);
                    }
                }
            }
        }

        [TestMethod]
        public void TestFieldEightNotRequiredWhenFieldSevenIsNull()
        {
            InitiateRequiredData();
            conditionalValidation.FieldSeven = null;
            conditionalValidation.FieldEight = null;

            try
            {
                conditionalValidationsController.PostConditionalValidation(conditionalValidation);

                Assert.IsTrue(conditionalValidationsController.ModelState.IsValid);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                // Should not through any error to catch as it is matching the requirement
            }
        }

        #endregion

        #region Required If Dep Field is Null Test Methods
        [TestMethod]
        public void TestFieldSixRequiredWhenFieldFiveIsNull()
        {
            InitiateRequiredData();
            conditionalValidation.FieldFive = null;
            conditionalValidation.FieldSix = null;
            try
            {
               var response = conditionalValidationsController.PostConditionalValidation(conditionalValidation);

                Assert.IsFalse(conditionalValidationsController.ModelState.IsValid);
                Assert.IsTrue(conditionalValidationsController.ModelState.Count == 1, "Field Six is Required");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Assert.AreEqual("Field Six is Required", validationError.ErrorMessage);
                    }
                }
            }
        }

        [TestMethod]
        public void TestFieldSixNotRequiredWhenFieldFiveIsNotNull()
        {
            InitiateRequiredData();
            //conditionalValidation.FieldFive = null;
            conditionalValidation.FieldSix = null;
            
            try
            {
               var response = conditionalValidationsController.PostConditionalValidation(conditionalValidation);

                Assert.IsTrue(conditionalValidationsController.ModelState.IsValid);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                // Should  not throw any exception to catch 
            }
        }

        #endregion

    }
}
