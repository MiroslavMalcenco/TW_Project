using Gamma.Domain.Entities.Session;
using System.Data.Entity;

namespace Gamma.BusinessLogic.DBModel
{
    public class SessionContext : DbContext
    {
        public SessionContext() : base("name=Gamma") { }
        public virtual DbSet<SDBModel> Sessions { get; set; }
    }
}