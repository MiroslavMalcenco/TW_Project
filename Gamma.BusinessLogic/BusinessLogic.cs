using Gamma.BusinessLogic.Interfaces;

namespace Gamma.BusinessLogic
{
    public class BusinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }
        public IPost GetPostBL()
        {
            return new PostBL();
        }
    }
}