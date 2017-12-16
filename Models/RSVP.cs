using System;
using System.Collections.Generic;
using DojoActivity.Models;

namespace DojoActivity.Models
{ 
public class RSVP
    {
        public int RSVPId {get;set;}

        public int UserId {get;set;}
        public User Guest {get;set;}
       
        public int ActivityId {get;set;}

        public Activity Event {get;set;} 
    }
}

//@Model.Guests.FirstName(g => g.UserId == Model.CoordinatorId).SingleOrDefault()