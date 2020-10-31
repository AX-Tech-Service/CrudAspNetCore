using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PluseDemoProject.Areas.DataBaseContext;
using PluseDemoProject.Areas.DbQueryModel;
using PluseDemoProject.Areas.Services.ServiceInterface;
using PluseDemoProject.Areas.ViewModel;
using PluseDemoProject.PagingComponent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PluseDemoProject.Areas.Services.ServiceImplementation
{
    public class UserService : IUserService
    {
        private UserDbContext moDataContext;
        private readonly IMapper moAutoMapper;
        public UserService(UserDbContext foDataContext,IMapper foAutoMapper)
        {
            moDataContext = foDataContext;
            moAutoMapper=foAutoMapper;
        }

        public PagingList<UserModel> getUsers(IndexModel foIndexModel)
        {
            SqlParameter[] laParam = new SqlParameter[] {
                  new SqlParameter("@inTotalRecordFound", SqlDbType.Int, 8, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, 0)};

            List<UserListDbQueryModel> tblUserlist = moDataContext.tblUserList.FromSqlRaw("getUsersList {0},{1},{2},{3},@inTotalRecordFound OUTPUT",
               foIndexModel.PageNumber,foIndexModel.PageSize,foIndexModel.stFilterKeyword,foIndexModel.stSortKeyword,laParam[0]).ToList();

            PagingList<UserModel> loUserList = new PagingList<UserModel>(moAutoMapper.Map<List<UserListDbQueryModel>, List<UserModel>>(tblUserlist),foIndexModel.PageNumber,foIndexModel.PageSize, Convert.ToInt32(laParam[0].Value));
            return loUserList;
        }

        [Obsolete]
        public bool insertUser(UserModel foUserModel)
        {
            string lsReturnMsg = ""; int liReturnCode = 0;
            SqlParameter[] laParam = new SqlParameter[] {
            new SqlParameter("@stFirstName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.FirstName),
            new SqlParameter("@stLastName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.LastName),
            new SqlParameter("@dtBirthDate", SqlDbType.DateTime, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.BirthDate),
            new SqlParameter("@stEmailAddress", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.EmailAddress),
            new SqlParameter("@stPassword", SqlDbType.Text, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.Password),
            new SqlParameter("@stReturnMsg", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default,lsReturnMsg),
            new SqlParameter("@inReturnCode", SqlDbType.Int, 8, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default,liReturnCode) };

            moDataContext.Database.ExecuteSqlCommand("addUsers @stFirstName,@stLastName,@dtBirthDate,@stEmailAddress,@stPassword,@stReturnMsg output,@inReturnCode output", laParam);
            if (liReturnCode == Convert.ToInt32(laParam[6].Value))
            {
                return true;
            }
            return false;
        }
        [Obsolete]
        public void deleteUser(int fiId)
        {
            SqlParameter[] laParam = new SqlParameter[] {
            new SqlParameter("@inId", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, fiId) };

            moDataContext.Database.ExecuteSqlCommand("deleteUser @inId", laParam);
        }

        [Obsolete]
        public UserModel getUser(int fiId)
        {
            UserModel loUserModel = moDataContext.moUserDetail.FromSqlRaw("getUser {0}", fiId,0).ToList().First();
            return loUserModel;
        }

        [Obsolete]
        public bool updateUser(UserModel foUserModel)
        {
            string lsReturnMsg = ""; int liReturnCode = 0;
            SqlParameter[] laParam = new SqlParameter[] {
            new SqlParameter("@inId", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.Id) ,
            new SqlParameter("@stFirstName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.FirstName),
            new SqlParameter("@stLastName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.LastName),
            new SqlParameter("@dtBirthDate", SqlDbType.DateTime, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.BirthDate),
            new SqlParameter("@stEmailAddress", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.EmailAddress),
            new SqlParameter("@stPassword", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, foUserModel.Password),
            new SqlParameter("@stReturnMsg", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, lsReturnMsg),
            new SqlParameter("@inReturnCode", SqlDbType.Int, 8, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, liReturnCode) };

            moDataContext.Database.ExecuteSqlCommand("updateUsers @inId, @stFirstName,@stLastName,@dtBirthDate,@stEmailAddress,@stPassword,@stReturnMsg OUTPUT,@inReturnCode OUTPUT", laParam);
            if (liReturnCode == Convert.ToInt32(laParam[7].Value))
            {
                return true;
            }
            return false;
        }

        #region Old Code

        //public List<UserModel> getUsersnew()
        //{
        //    List<UserModel> loUserList = new List<UserModel>();
        //    var items = moDataContext.aUserList.FromSqlRaw(@"select inUserId,stFirstName,stLastName,stEmailAddress,dtCreationDate from tblUsers").ToList();
        //    foreach (var loUser in items)
        //    {
        //        loUserList.Add(new UserModel { Id = loUser.inUserId, FirstName = loUser.stFirstName, LastName = loUser.stLastName, EmailAddress = loUser.stEmailAddress, CreationDate = loUser.dtCreationDate });
        //    }
        //    return loUserList;
        //}
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

        //public List<UserModel> sortAndSearch(List<UserModel> foUserList, string fsSearchKeyword = "", string stSortKeyword = "")
        //{
        //    if (!string.IsNullOrWhiteSpace(fsSearchKeyword))
        //    {
        //        foUserList = foUserList.Where(u => u.FirstName.Contains(fsSearchKeyword) ||
        //        u.LastName.Contains(fsSearchKeyword) || u.EmailAddress.Contains(fsSearchKeyword)).ToList();
        //    }
        //    if (!string.IsNullOrWhiteSpace(stSortKeyword))
        //    {
        //        switch (stSortKeyword)
        //        {
        //            case "name_desc":
        //                foUserList = foUserList.OrderByDescending(s => s.FirstName + " " + s.LastName).ToList();
        //                break;
        //            case "name_asc":
        //                foUserList = foUserList.OrderBy(s => s.FirstName + " " + s.LastName).ToList();
        //                break;
        //            case "date_desc":
        //                foUserList = foUserList.OrderByDescending(s => s.CreationDate).ToList();
        //                break;
        //            case "date_asc":
        //                foUserList = foUserList.OrderBy(s => s.CreationDate).ToList();
        //                break;
        //            case "email_desc":
        //                foUserList = foUserList.OrderByDescending(s => s.EmailAddress).ToList();
        //                break;
        //            case "email_asc":
        //                foUserList = foUserList.OrderBy(s => s.EmailAddress).ToList();
        //                break;
        //            default:
        //                break;

        //        }
        //    }
        //    return foUserList;
        //}
        //[Obsolete]
        //public List<UserModel> getUsersOLD(IndexModel foIndexModel)
        //{

        //    string lsUsers = "";
        //    SqlParameter[] laParam = new SqlParameter[] {
        //    new SqlParameter("@stResult", SqlDbType.VarChar,int.MaxValue, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, lsUsers) };
        //    moDataContext.Database.ExecuteSqlCommand("getUserList @stResult OUTPUT", laParam);

        //    List<UserModel> loUsers = JsonConvert.DeserializeObject<List<UserModel>>(Convert.ToString(laParam[0].Value));
        //    return loUsers;
        //}

        //----this code is getuser method code
        //SqlParameter[] laParam = new SqlParameter[] {
        //new SqlParameter("@inId", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default,fiId) ,
        //new SqlParameter("@inUserId", SqlDbType.Int, 8, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default,loUserModel.Id) ,
        //new SqlParameter("@stFirstName", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, loUserModel.FirstName),
        //new SqlParameter("@stLastName", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, loUserModel.LastName),
        //new SqlParameter("@dtBirthDate", SqlDbType.DateTime, 100, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, loUserModel.BirthDate),
        //new SqlParameter("@stEmailAddress", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, loUserModel.EmailAddress),
        //new SqlParameter("@stPassword", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, loUserModel.Password) };

        //moDataContext.Database.ExecuteSqlCommand("getUser @inId,@inUserId OUTPUT, @stFirstName OUTPUT,@stLastName OUTPUT,@dtBirthDate OUTPUT,@stEmailAddress OUTPUT,@stPAssword OUTPUT", laParam);

        //if (Convert.ToInt32(laParam[1].Value) > 0)
        //{
        //    loUserModel.Id = Convert.ToInt32(laParam[1].Value);
        //    loUserModel.FirstName = Convert.ToString(laParam[2].Value);
        //    loUserModel.LastName = Convert.ToString(laParam[3].Value);
        //    loUserModel.BirthDate = (DateTime)laParam[4].Value;
        //    loUserModel.EmailAddress = Convert.ToString(laParam[5].Value);
        //    loUserModel.Password = Convert.ToString(laParam[6].Value);
        //    return loUserModel;
        //}
        //return null;
        #endregion
    }
}
