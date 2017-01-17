using ConditionalValidations.ValidationHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace ConditionalValidations.Models
{
    public class ConditionalValidation
    {
        public int ConditionalValidationId { get; set; }

        // START: Required validator
        // START: Required If Not validator
        [Required(ErrorMessage = "Field One is Required")]
        public string FieldOne { get; set; }
        // END: Required validator

        [RequiredIfNotValidator("FieldOne", "Test", ErrorMessage = "Field Two is Required")]
        public string FieldTwo { get; set; }
        // END: Required If Not validator

        // START: Required If Validator
        public string FieldThree { get; set; }

        [RequiredIfValidator("FieldThree", "Test", ErrorMessage = "Field Four is Required")]
        public string FieldFour { get; set; }
        // END: Required If Validator

        // START: Required If Dep Field Is Null validator
        public string FieldFive { get; set; }

        [RequiredIfDepFieldIsNullValidator("FieldFive", ErrorMessage = "Field Six is Required")]
        public string FieldSix { get; set; }
        // END: Required If Dep Field Is Null validator

        // START: Required If Dep Field Is Not Null validator
        public DateTime? FieldSeven { get; set; }

        [RequiredIfDepFieldIsNotNullValidator("FieldSeven", ErrorMessage = "Field Eight is Required")]
        public int? FieldEight { get; set; }
        // END: Required If Dep Field Is Not Null validator

        // START: Required RegEx If Dep Field Is Not Null validator
        public string FieldNine { get; set; }
                
        [RequiredIfRegExValidator(@"^\d{10}$", "FieldNine", "Delete", ErrorMessage = "Field Ten Requires Numbers between 1-10")]
        public string FieldTen { get; set; }

        [RequiredIfNotRegExValidator(@"^\d{10}$", "FieldNine", "Delete", ErrorMessage = "Field Elevan Requires Numbers between 1-10")]
        public string FieldElevan { get; set; }
        // END: Required RegEx If Dep Field Is Not Null validator
    }
}