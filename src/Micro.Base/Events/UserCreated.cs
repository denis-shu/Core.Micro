namespace Micro.Base.Events
{
    public class UserCreated : IEvent
    {
        public string Email { get; }
        public string Name { get; }

        public UserCreated(string email, string name)
        {
            Email = email;
            Name = name;
        }

        protected UserCreated()
        {

        }
    }
}