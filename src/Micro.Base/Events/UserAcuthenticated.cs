namespace Micro.Base.Events
{
    public class UserAuthenticated : IEvent
    {
        public string Email { get; set; }
        public UserAuthenticated(string email)
        {
            this.Email = email;

        }        
        protected UserAuthenticated()
        {

        }       
    }
}