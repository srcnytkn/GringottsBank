using GringottsBank.Service.Abstract;

namespace GringottsBank.Service.Concrete.Account
{
    public partial class AccountService : IAccountService
    {
        private readonly ICacheHelper _cacheHelper;
        public AccountService(ICacheHelper cacheHelper)
        {
            _cacheHelper = cacheHelper;
        }
    }
}
