namespace My.DDD.MediatrDomainEventHandlers.Tests.Examples
{
    public class SpyableEvent : IEvent
    {
        public SpyableEvent()
        {
        }

        virtual public void ToBeCalledByHandler() { }
    }
}
