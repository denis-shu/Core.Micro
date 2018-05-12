using System;
using Micro.Base.Exceptions;

namespace Micro.Services.Activities.Domain.Models
{
    public class Activity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public string NAme { get; set; }

        public string Category { get; set; }

        public string Desciptions { get; set; }

        public DateTime CreatedAt { get; set; }

        public Activity(Guid id, Guid userId, Category category, string name,
        string description, DateTime created)
        {
            Id = id;
            Category = category.NAme;
            NAme = name;
            UserId = userId;
            Desciptions = description;
            CreatedAt = created;
        }

         public Activity(Guid id, Guid userId, string category, string name,
        string description, DateTime created)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new MicroException("activity_empty", $"Name cant be ampty or wss");

            Id = id;
            Category = category;
            NAme = name;
            UserId = userId;
            Desciptions = description;
            CreatedAt = created;
        }

        protected Activity()
        {

        }
    }
}