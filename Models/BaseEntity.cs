using System;
using System.ComponentModel.DataAnnotations;

namespace DojoActivity.Models
{
    public abstract class BaseEntity 
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt {get; set;}
    }
}

