using Microsoft.EntityFrameworkCore.Storage;

namespace DineConnect.OrderManagementService.Infrastructure.DataAccess.Repositories
{
    public interface ITransaction
    {
        IDbContextTransaction GetCurrentTransaction();

    }
}
