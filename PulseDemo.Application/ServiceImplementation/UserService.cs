using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PulseDemo.Application.ServiceInterface;
using PulseDemo.Application.ViewModel;
using PulseDemo.Infra.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace PulseDemo.Application.ServiceImplementation
{
    public class UserService : IUserService
    {
        private UserDbContext _dbContext;
        public UserService(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }        
        public List<UserModel> GetUserList()
        {
            List<UserModel> usersModel = new List<UserModel>();

            var items = _dbContext.UserList.FromSqlRaw(@"select inUserId,stFirstName,stLastName,stEmailAddress,dtCreationDate from tblUsers").ToList();
            foreach (var user in items)
            {
                usersModel.Add(new UserModel { Id = user.inUserId, FirstName = user.stFirstName, LastName = user.stLastName, EmailAddress = user.stEmailAddress, CreationDate = user.dtCreationDate });
            }
            return usersModel;
        }
        public bool insertUser(UserModel foUserModel)
        {
            string lsReturnMsg = ""; int liReturnCode = 0;
            SqlParameter[] aParam = new SqlParameter[] {
            new SqlParameter("@stFirstName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.FirstName),
            new SqlParameter("@stLastName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.LastName),
            new SqlParameter("@dtBirthDate", SqlDbType.DateTime, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.BirthDate),
            new SqlParameter("@stEmailAddress", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.EmailAddress),
            new SqlParameter("@stPassword", SqlDbType.Text, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.Password),
            new SqlParameter("@stReturnMsg", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default,lsReturnMsg),
            new SqlParameter("@inReturnCode", SqlDbType.Int, 8, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default,liReturnCode) };

            _dbContext.Database.ExecuteSqlCommand("addUsers @stFirstName,@stLastName,@dtBirthDate,@stEmailAddress,@stPassword,@stReturnMsg output,@inReturnCode output", aParam);
            if (liReturnCode == Convert.ToInt32(aParam[6].Value))
            {
                return true;
            }
            return false;
        }
        public void deleteUser(int fiId)
        {
            SqlParameter[] aParam = new SqlParameter[] {
            new SqlParameter("@inId", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, fiId) };

            _dbContext.Database.ExecuteSqlCommand("deleteUser @inId", aParam);
        }
        public UserModel getUser(int fiId)
        {
            UserModel loUserModel = new UserModel();
            SqlParameter[] aParam = new SqlParameter[] {
            new SqlParameter("@inId", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default,fiId) ,
            new SqlParameter("@inUserId", SqlDbType.Int, 8, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default,loUserModel.Id) ,
            new SqlParameter("@stFirstName", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, loUserModel.FirstName),
            new SqlParameter("@stLastName", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, loUserModel.LastName),
            new SqlParameter("@dtBirthDate", SqlDbType.DateTime, 100, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, loUserModel.BirthDate),
            new SqlParameter("@stEmailAddress", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, loUserModel.EmailAddress),
            new SqlParameter("@stPassword", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, loUserModel.Password) };

            _dbContext.Database.ExecuteSqlCommand("getUser @inId,@inUserId OUTPUT, @stFirstName OUTPUT,@stLastName OUTPUT,@dtBirthDate OUTPUT,@stEmailAddress OUTPUT,@stPAssword OUTPUT", aParam);

            if (Convert.ToInt32(aParam[1].Value) > 0)
            {
                loUserModel.Id = Convert.ToInt32(aParam[1].Value);
                loUserModel.FirstName = Convert.ToString(aParam[2].Value);
                loUserModel.LastName = Convert.ToString(aParam[3].Value);
                loUserModel.BirthDate = (DateTime)aParam[4].Value;
                loUserModel.EmailAddress = Convert.ToString(aParam[5].Value);
                loUserModel.Password = Convert.ToString(aParam[6].Value);
                return loUserModel;
            }
            return null;
        }
        public bool updateUser(UserModel foUserModel)
        {
            string lsReturnMsg = ""; int liReturnCode = 0;
            SqlParameter[] aParam = new SqlParameter[] {
            new SqlParameter("@inId", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.Id) ,
            new SqlParameter("@stFirstName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.FirstName),
            new SqlParameter("@stLastName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.LastName),
            new SqlParameter("@dtBirthDate", SqlDbType.DateTime, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.BirthDate),
            new SqlParameter("@stEmailAddress", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.EmailAddress),
            new SqlParameter("@stPassword", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.Password),
            new SqlParameter("@stReturnMsg", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, lsReturnMsg),
            new SqlParameter("@inReturnCode", SqlDbType.Int, 8, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, liReturnCode) };

            _dbContext.Database.ExecuteSqlCommand("updateUsers @inId, @stFirstName,@stLastName,@dtBirthDate,@stEmailAddress,@stPassword,@stReturnMsg OUTPUT,@inReturnCode OUTPUT", aParam);
            if (liReturnCode == Convert.ToInt32(aParam[7].Value))
            {
                return true;
            }
            return false;
        }
        public List<UserModel> sortAndSearch(List<UserModel> users, string searchKeyword = "", string sortKeyword = "")
        {
            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                users = users.Where(u => u.FirstName.Contains(searchKeyword) ||
                u.LastName.Contains(searchKeyword) || u.EmailAddress.Contains(searchKeyword)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(sortKeyword))
            {
                switch (sortKeyword)
                {
                    case "name_desc":
                        users = users.OrderByDescending(s => s.FirstName + " " + s.LastName).ToList();
                        break;
                    case "name_asc":
                        users = users.OrderBy(s => s.FirstName + " " + s.LastName).ToList();
                        break;
                    case "date_desc":
                        users = users.OrderByDescending(s => s.CreationDate).ToList();
                        break;
                    case "date_asc":
                        users = users.OrderBy(s => s.CreationDate).ToList();
                        break;
                    case "email_desc":
                        users = users.OrderByDescending(s => s.EmailAddress).ToList();
                        break;
                    case "email_asc":
                        users = users.OrderBy(s => s.EmailAddress).ToList();
                        break;
                    default:
                        break;

                }
            }
            return users;
        }

        #region Old Code
        //public void DeleteUserSP(int Id)
        //{
        //    SqlParameter[] param1 = new SqlParameter[] {
        //    new SqlParameter("@Id", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, Id) };

        //    _dbContext.Database.ExecuteSqlCommand("Sp_DeleteUser @Id", param1);
        //}

        //public UserModel GetSingleUser(int Id)
        //{
        //    var data = _dbContext.Users.FromSqlRaw(@"select * from Users where Id={0}", Id).FirstOrDefault();
        //    if (data != null)
        //    {
        //        UserModel user = new UserModel { Id = data.Id, FirstName = data.FirstName, LastName = data.LastName, BirthDate = DateTime.Parse(data.BirthDate), EmailAddress = data.EmailAddress, Password = data.Password };
        //        return user;
        //    }
        //    return null;
        //}

        //public bool EditUserSp(UserModel usersModel)
        //{
        //    int? userId = _dbContext.Users.FirstOrDefault(u => u.EmailAddress.Equals(usersModel.EmailAddress))?.Id;
        //    bool isEmailExist = ((userId != null) && (userId != usersModel.Id)) ? true : false;
        //    if (isEmailExist)
        //    {
        //        return isEmailExist;
        //    }

        //    SqlParameter[] param1 = new SqlParameter[] {
        //    new SqlParameter("@Id", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, usersModel.Id) ,
        //    new SqlParameter("@FirstName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, usersModel.FirstName),
        //    new SqlParameter("@LastName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, usersModel.LastName),
        //    new SqlParameter("@BirthDate", SqlDbType.DateTime, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, usersModel.BirthDate),
        //    new SqlParameter("@EmailAddress", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, usersModel.EmailAddress),
        //    new SqlParameter("@Password", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, usersModel.Password) };

        //    _dbContext.Database.ExecuteSqlCommand("Sp_EditProcedure @Id, @FirstName,@LastName,@BirthDate,@EmailAddress,@Password", param1);

        //    return false;
        //}
        //public bool IsEmailExist(string emailAddress)
        //{
        //    bool isEmailExist = _dbContext.Users.FirstOrDefault(u => u.EmailAddress.Equals(emailAddress)) != null ? true : false;
        //    return isEmailExist;
        //}

        //public bool AddUserSP(UserModel usersModel)
        //{
        //    string ReturnMsg = ""; int ReturnCode = 0;
        //    SqlParameter[] aParam = new SqlParameter[] {
        //    new SqlParameter("@FirstName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, usersModel.FirstName),
        //    new SqlParameter("@LastName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, usersModel.LastName),
        //    new SqlParameter("@BirthDate", SqlDbType.DateTime, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, usersModel.BirthDate),
        //    new SqlParameter("@EmailAddress", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, usersModel.EmailAddress),
        //    new SqlParameter("@Password", SqlDbType.Text, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, usersModel.Password),
        //    new SqlParameter("@ReturnMsg", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default,ReturnMsg),
        //    new SqlParameter("@ReturnCode", SqlDbType.Int, 8, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default,ReturnCode) };

        //    _dbContext.Database.ExecuteSqlCommand("addUsers @FirstName,@LastName,@BirthDate,@EmailAddress,@Password,@ReturnMsg output,@ReturnCode output", aParam);
        //    if (ReturnCode == Convert.ToInt32(aParam[6].Value))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public List<UserModel> GetUserList()
        //{
        //    List<UserModel> usersModel = new List<UserModel>();

        //    var items = _dbContext.UserList.FromSqlRaw(@"select Id,FirstName,LastName,EmailAddress,CreationDate from Users").ToList();
        //    foreach (var user in items)
        //    {
        //        usersModel.Add(new UserModel { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, EmailAddress = user.EmailAddress, CreationDate = user.CreationDate });
        //    }
        //    return usersModel;
        //}
        #endregion
    }
}
