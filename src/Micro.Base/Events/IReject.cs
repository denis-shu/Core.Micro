namespace Micro.Base.Events
{
    public interface IRejectEvent : IEvent
    {
        string Reason { get; }
        string Code {get;}
    }
}