using PluseDemoProject.Areas.ViewModel;
using PluseDemoProject.PagingComponent;
using System.Collections.Generic;

namespace PluseDemoProject.Areas.Services.ServiceInterface
{
    public interface IUserService
    {
        bool insertUser(UserModel foUserModel);
        void deleteUser(int fiId);
        UserModel getUser(int fiId);
        PagingList<UserModel> getUsers(IndexModel foIndexModel);
        bool updateUser(UserModel foUserModel);


        #region Old
        //List<UserModel> getUsers();
        //List<UserModel> sortAndSearch(List<UserModel> foUserList, string fsSearchKeyword = "", string stSortKeyword = "");
        //bool EditUserSp(UserModel usersModel);
        //bool AddUserSP(UserModel users);
        //void DeleteUserSP(int Id);
        //UserModel GetSingleUser(int Id);
        #endregion
    }
}
