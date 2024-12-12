using Infrastructure.IntegrationEvents;

namespace DineConnect.OrderManagementService.Infrastructure.EventHandlers
{
    internal class IntegrationEventPublisher
    {
        #region Constants and Static Fields
        // Add constants and static fields here
        #endregion

        #region Private & Protected Fields
        private readonly IAddIntegrationEventCommandHandler _handler;
        #endregion

        #region Private & Protected Methods
        #endregion

        #region Constructors
        private IntegrationEventPublisher(IAddIntegrationEventCommandHandler handler)
        {
            _handler = handler; 
        }
        #endregion

        #region Public Properties
        #endregion

        #region Public Methods
        #endregion

        #region Interface Implementations
        // Implement interface members here
        #endregion

        #region Factory Methods
        //public static IntegrationEventPublisher Create(Icon)
        //{
        //    return new IntegrationEventPublisher();
        //}
        #endregion
    }
}
