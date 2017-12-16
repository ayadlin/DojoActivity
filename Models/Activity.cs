using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DojoActivity.Models;


namespace DojoActivity.Models
{
    public class Activity : BaseEntity
    {
        public int ActivityId {get;set;}
        public string Title {get;set;}
        public DateTime Date {get;set;}
        public string Time {get;set;}
        public int Duration {get;set;}
        public string Units {get;set;}
        public string Description {get;set;}
        public int CoordinatorId {get;set;} 
        public User Coordinator {get;set;}
        public List<RSVP> Guests {get;set;}
        
        public Activity()
        {
            Guests = new List<RSVP>();
        }

        // Your code Here
    }
}


