using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConditionalValidations.Controllers;
using ConditionalValidations.Models;
using Moq;

namespace ConditionalValidations.Tests
{
    [TestClass]
    public class ConditionalValidationTest
    {
        private ConditionalValidationsController conditionalValidationsController = new ConditionalValidationsController();
        private Mock<ConditionalValidationsContext> context = new Mock<ConditionalValidationsContext>();

        private ConditionalValidation conditionalValidation = new ConditionalValidation
        {
            FieldOne = "Field One", FieldTwo = "Field Two",
            FieldThree = "Field Three", FieldFour = "Field Four",
            FieldFive = "Field Five", FieldSix = "Field Six",
            FieldSeven = DateTime.Now, FieldEight = 0
        };

        #region Required Validator Test Methods
        [TestMethod]
        public void TestRequiredFieldOne()
        {
            try
            {
                conditionalValidation.FieldOne = "";
                conditionalValidationsController.PostConditionalValidation(conditionalValidation);
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
            conditionalValidation.FieldOne = "Test";
            conditionalValidationsController.PostConditionalValidation(conditionalValidation);
        }

        [TestMethod]
        public void TestRequiredFieldTwoWhenFieldOneIsNotTest()
        {
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
            conditionalValidation.FieldEight = null;

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
                        Assert.AreEqual("Field Eight is Required", validationError.ErrorMessage);
                    }
                }
            }
        }

        [TestMethod]
        public void TestFieldEightNotRequiredWhenFieldSevenIsNull()
        {            
            conditionalValidation.FieldSeven = null;
            conditionalValidation.FieldEight = null;

            try
            {
                conditionalValidationsController.PostConditionalValidation(conditionalValidation);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                // Should not through any error to catch as it is matching the requirement
            }
        }

        #endregion

        #region Required If Dep Field is Null Test Methods
        [TestMethod]
        public void TestFieldSixRequiredWhenFieldFiveIsNotNull()
        {
            conditionalValidation.FieldSix = null;
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
                        Assert.AreEqual("Field Six is Required", validationError.ErrorMessage);
                    }
                }
            }
        }

        [TestMethod]
        public void TestFieldSixNotRequiredWhenFieldFiveIsNull()
        {
            conditionalValidation.FieldFive = null;
            conditionalValidation.FieldSix = null;
            
            try
            {
                conditionalValidationsController.PostConditionalValidation(conditionalValidation);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                // Should  not throw any exception to catch 
            }
        }

        #endregion

    }
}
