using POS.DataContext;
using POS.Entity;

namespace POS.BusinessLogic
{
    public class UserBL:BaseBL
    {
        private readonly UserDataContext _userDataContext;
        public UserBL(UserDataContext userDataContext)
        {
            _userDataContext = userDataContext;
        }

        public bool RegisterUser(UserEntity user)
        {
            return _userDataContext.RegisterUser(user);
        }

        public UserEntity GetUserByEmailId(string emailId)
        {
            return _userDataContext.GetUserByEmailId(emailId);
        }

        public UserEntity GetUserByUserId(string userId)
        {
            return _userDataContext.GetUserByUserId(userId);
        }


        public bool IsUserAvailableByEmailId(string emailId)
        {
            return GetUserByEmailId(emailId) != null;
        }

        public bool IsUserAvailableByUserId(string userId)
        {
            return GetUserByUserId(userId) != null;
        }

    }
}
