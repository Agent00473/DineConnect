using DineConnect.PaymentManagementService.Domain.Common;


namespace DineConnect.PaymentManagementService.Domain.Invoice.ValueObjects
{
    public class RefundId: BaseEntityId<Guid>
    {
        private RefundId(Guid id)
        {
            Id = id;
        }

        private RefundId() { }
        public override Guid Id { get; protected set ; }

        public static RefundId Create(Guid id)
        {
            return new RefundId(id);
        }

        public static RefundId Create()
        {
            return new RefundId(Guid.NewGuid());
        }
    }
}
