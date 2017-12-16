using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DojoActivity.Models;

namespace DojoActivity.Models
{
    public class User : BaseEntity
    {
        public int UserId {get;set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<RSVP> RSVPs {get;set;}
    
    public User()
    {
        RSVPs = new List<RSVP>();
    }
    }
}
