using DineConnect.OrderManagementService.Domain.Interfaces;
using MediatR;

namespace DineConnect.OrderManagementService.Infrastructure.EventHandlers
{
    public interface IDomainEventPublisher
    {
        bool Publish(IEnumerable<IDomainEvent> events);
    }

    internal class DomainEventPublisher : IDomainEventPublisher
    {
        #region Constants and Static Fields
        // Add constants and static fields here
        #endregion

        #region Private Fields
        private readonly ISender _mediator;

        #endregion

        #region Protected Fields
        // Add protected fields here
        #endregion

        #region Protected Methods
        // Add protected methods here
        #endregion

        #region Private Methods
        // Add private methods here
        #endregion

        #region Constructors

        public DomainEventPublisher(ISender mediator)
        {
            _mediator = mediator;
        }
        #endregion


        #region Public Methods
        public bool Publish(IEnumerable<IDomainEvent> events)
        {
            try
            {
                foreach (var devt in events)
                {
                    _mediator.Send(devt);
                }
                return true;
            }
            catch
            {
                throw;
            }

        }
        #endregion


    }
}
