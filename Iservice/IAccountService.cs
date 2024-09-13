using crud_operation.Models;
using System.Security.Principal;

namespace crud_operation.Iservice
{
    public interface IAccountService
    {

        Task<List<acountsInfo>> GetAllAccountsInfo();
    }
}
