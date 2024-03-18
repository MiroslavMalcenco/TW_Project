using Gamma.BusinessLogic.Core;
using Gamma.BusinessLogic.DBModel;
using Gamma.BusinessLogic.Interfaces;
using Gamma.Domain.Entities.Post;
using Gamma.Domain.Entities.Response;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Gamma.BusinessLogic
{
    public class PostBL : PostApi, IPost
    {
        private readonly PostContext _context;
        public PostBL()
        {
            _context = new PostContext();
        }
        public PostBL(PostContext context)
        {
            _context = context;
        }
        public ServiceResponse AddPostAction(PDbModel Name)
        {
            return AddPost(Name);
        }
        public PDbModel GetById(int PostID)
        {
            return _context.Posts.Find(PostID);
        }
        public IEnumerable<PDbModel> GetAll()
        {
            return _context.Posts.ToList();
        }
        public IEnumerable<PostMinimal> GetBySearchWrapData(PSearchWrapData searchWrapData)
        { 
            return ReturnPostsBySearchWrapData(searchWrapData);
        }
        public IEnumerable<PostMinimal> GetPostsByIngredientsOrCuisine(string Ingredients)
        {
            return ReturnPostsByIngredientsOrCuisine(Ingredients);
        }
        public void Update(PDbModel Name)
        {
            _context.Entry(Name).State = EntityState.Modified;
        }
        public void Delete(int PostID)
        {
            PDbModel Name = _context.Posts.Find(PostID);
            if (Name != null)
            {
                _context.Posts.Remove(Name);
            }
        }
        public IEnumerable<PostMinimal> GetLatestPosts()
        {
            return ReturnLatestPosts();
        }
        public IEnumerable<PostMinimal> GetPostsByAuthor(string author)
        {
            return ReturnPostsByAuthor(author);
        }
        public IEnumerable<PostMinimal> GetPostsByListingFilter(PListingFilterData data)
        {
            return ReturnPostsByListingFilter(data);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}