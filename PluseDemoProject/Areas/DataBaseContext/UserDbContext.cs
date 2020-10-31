using Microsoft.EntityFrameworkCore;
using PluseDemoProject.Areas.DbQueryModel;
using PluseDemoProject.Areas.Entities;
using PluseDemoProject.Areas.ViewModel;

namespace PluseDemoProject.Areas.DataBaseContext
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {

        }
        public DbSet<tblUsers> tblUsers { get; set; }
        public DbQuery<UserListDbQueryModel> tblUserList { get; set; }
        public DbQuery<UserModel> moUserDetail { get; set; }

        #region old
        //public DbSet<Users> Users { get; set; }
        //public DbQuery<UserResponseModel> aUserList { get; set; }
        #endregion
    }

}
