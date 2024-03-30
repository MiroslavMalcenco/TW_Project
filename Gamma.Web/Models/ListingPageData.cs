using Gamma.Domain.Entities.Post;
using System.Collections.Generic;

namespace Gamma.Web.Models
{
    public class ListingPageData
    {
        public List<PostMinimal> ListingItems { get; set; }
        public SideBarData SideBar { get; set; }
    }
}