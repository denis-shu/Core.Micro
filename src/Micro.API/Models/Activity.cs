using System;

namespace Micro.API.Models
{
    public class Activity
    {
         public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public string NAme { get; set; }

        public string Category { get; set; }

        public string Desciptions { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}