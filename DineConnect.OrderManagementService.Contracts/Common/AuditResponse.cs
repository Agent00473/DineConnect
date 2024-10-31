
namespace DineConnect.OrderManagementService.Contracts.Common
{
    public record AuditResponse(
            DateTimeOffset Created,
            string? CreatedBy,
           DateTimeOffset LastModified,  string? LastModifiedBy);
}
