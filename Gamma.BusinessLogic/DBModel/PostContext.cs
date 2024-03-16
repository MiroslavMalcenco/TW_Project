using Gamma.Domain.Entities.Post;
using System.Data.Entity;

namespace Gamma.BusinessLogic.DBModel
{
    public class PostContext : DbContext
    {
        public PostContext() : base("name=Gamma") { }
        public virtual DbSet<PDbModel> Posts { get; set; }
    }
}