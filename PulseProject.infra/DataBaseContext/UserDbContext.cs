using Microsoft.EntityFrameworkCore;
using PulseDemo.core.DbQueryModel;

namespace PulseDemo.Infra.DataBaseContext
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<tblUsers> tblUsers { get; set; }
        public DbQuery<UserResponseModel> UserList { get; set; }
    }

}
