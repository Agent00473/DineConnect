using MediatR;

namespace DineConnect.OrderManagementService.Application.Common
{
    public class NotificationModel<TDomainEvent>: INotification where TDomainEvent : class
    {
        private readonly TDomainEvent _domainEvent;
		private NotificationModel(TDomainEvent data) {
            _domainEvent = data;
        } 

        public TDomainEvent DomainEvent { get { return _domainEvent; } }

        public static NotificationModel<TDomainEvent> Create(TDomainEvent data)
        {
            return new NotificationModel<TDomainEvent>(data);
        }
    }
}
