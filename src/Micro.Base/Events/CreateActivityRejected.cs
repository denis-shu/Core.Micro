using System;

namespace Micro.Base.Events
{
    public class CreateActivityRejected : IRejectEvent
    {
        public string Reason { get; }

        public string Code { get; }
        public Guid Id { get; set; }

        protected CreateActivityRejected()
        {

        }
        public CreateActivityRejected(Guid id, string code, string reason)
        {
            Id = id;
            Code = code;
            Reason = reason;
        }
    }
}