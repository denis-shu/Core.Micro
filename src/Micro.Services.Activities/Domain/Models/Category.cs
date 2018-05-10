using System;

namespace Micro.Services.Activities.Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string NAme { get; set; }

        public Category(string name)
        {
            Id = Guid.NewGuid();
            NAme = name;
        }
        
        protected Category()
        {

        }
    }
}