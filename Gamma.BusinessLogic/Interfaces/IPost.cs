using Gamma.Domain.Entities.Post;
using Gamma.Domain.Entities.Response;
using System.Collections.Generic;

namespace Gamma.BusinessLogic.Interfaces
{
    public interface IPost
    {
        ServiceResponse AddPostAction(PDbModel Name);
        PDbModel GetById(int postID);
        IEnumerable<PDbModel> GetAll();
        IEnumerable<PostMinimal> GetBySearchWrapData(PSearchWrapData searchWrapData);
        IEnumerable<PostMinimal> GetPostsByIngredientsOrCuisine(string Ingredients);
        IEnumerable<PostMinimal> GetPostsByAuthor(string author);
        IEnumerable<PostMinimal> GetLatestPosts();
        IEnumerable<PostMinimal> GetPostsByListingFilter(PListingFilterData data);
        void Update(PDbModel Name);
        void Delete(int PostID);
        void Save();
    }
}