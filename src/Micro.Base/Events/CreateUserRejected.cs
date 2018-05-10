namespace Micro.Base.Events
{
    public class CreateUserRejected : IRejectEvent
    {
        public CreateUserRejected(string reason, string code, string email)
        {
            this.Reason = reason;
            this.Code = code;
            this.Email = email;

        }
        protected CreateUserRejected(){
        }
        public string Reason { get; }
        public string Code { get; }
        public string Email { get; }
       
    }
}