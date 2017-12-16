using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DojoActivity.Models
{
    public class Dashboard
    {
        public List<Activity> Activities {get;set;}
        public User User {get;set;}
        //public Activity Activity {get;set;}
    }
    public class ActivityViewModels
    {
        [Required]
        [MinLength(2)]
        [Display(Name="Title")]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string Title {get;set;}

        [Required]
        [FutureDate]
        [Display(Name="Date")]
        [DataType(DataType.Date)]
        public DateTime Date {get;set;}
        
        [Required]  
        [DataType(DataType.Time)]
        [Display(Name="Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{HH:mm}")]
        public string Time { get; set; }

        [Required]
        [Display(Name="Duration")]
        public int Duration {get;set;}

        [Required]
        public string Units {get;set;}

        [Required]
        [MinLength(10)]
        [Display(Name="Description")]

        public string Description {get;set;}



    }
    public class FutureDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateOnly = (DateTime)value;
            DateTime timeOnly = (DateTime)value;

            DateTime combined = dateOnly.Date.Add(timeOnly.TimeOfDay);
            


            return dateOnly < DateTime.Now ? new ValidationResult("Date must be in future.") : ValidationResult.Success;
        }
    }
    
}