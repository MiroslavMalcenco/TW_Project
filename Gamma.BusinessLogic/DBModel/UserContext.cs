using Gamma.Domain.Entities.User;
using System.Data.Entity;

namespace Gamma.BusinessLogic.DBModel
{
    public class UserContext : DbContext
    {
        public UserContext(string connectionString)
        {
            Database.Connection.ConnectionString = connectionString;
        }
        public UserContext() : base("name=Gamma") { }
        public virtual DbSet<UDBModel> Users { get; set; }
    }
}