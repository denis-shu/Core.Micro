using System;

namespace Micro.Base.Events
{
    public class ActivityCreated : IAuthenticatedEvent
    {

        public Guid UserId { get; set; }
        public Guid Id { get; set; }

        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        protected ActivityCreated()
        {

        }
        public ActivityCreated(Guid userId, Guid id, string category, string name)
        {
            this.UserId = userId;
            this.Id = id;
            this.Category = category;
            this.Name = name;
           

        }


    }
}