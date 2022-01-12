using DataAccess;
using POS.Entity;
using POS.Utility;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace POS.DataContext
{
    public class UserDataContext : BaseContext
    {
        private readonly MsSqlConnect _connect;
        public UserDataContext(MsSqlConnect connect) : base(connect)
        {
            _connect = connect;
        }

        #region Register New User

        public bool RegisterUser(UserEntity user)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EmailId", user.EmailId));
            parameters.Add(new SqlParameter("@Password", user.Password));
            parameters.Add(new SqlParameter("@PasswordHash", AESCryptography.Encrypt(ApplicationConstants.SYMMETRIC_KEY, user.Password)));
            _connect.Execute("Save_ApplicationUsers", CommandType.StoredProcedure, parameters);
            return true;
        }

        #endregion

        #region Get User Info
        private UserEntity GetUser(string userId, string emailId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId",userId.DefaultDBNullValue()));
            parameters.Add(new SqlParameter("@EmailId",emailId.DefaultDBNullValue()));
            return _connect.Execute<UserEntity>("Get_UserInfo", CommandType.StoredProcedure, parameters)?.SingleOrDefault();
        }

        #endregion

        #region Get User Info by Email Id
        public UserEntity GetUserByEmailId(string emailId)
        {
            return GetUser(null,emailId);
        }

        #endregion


        #region Get User Info By User Id
        public UserEntity GetUserByUserId(string userId)
        {
            return GetUser(userId,null);
        }

        #endregion




    }
}
