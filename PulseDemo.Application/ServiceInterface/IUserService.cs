using System.Collections.Generic;

namespace PulseDemo.Application.ServiceInterface
{
    public interface IUserService
    {
        //bool AddUserSP(UserModel users);
        bool insertUser(UserModel foUserModel);
        //void DeleteUserSP(int Id);
        void deleteUser(int fiId);
        //UserModel GetSingleUser(int Id);
        UserModel getUser(int fiId);
        List<UserModel> GetUserList();
        //bool EditUserSp(UserModel usersModel);
        bool updateUser(UserModel foUserModel);
        List<UserModel> sortAndSearch(List<UserModel> users, string searchKeyword = "", string sortKeyword = "");
    }
}
