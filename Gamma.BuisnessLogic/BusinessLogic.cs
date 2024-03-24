using Gamma.BuisnessLogic.Interfaces;

namespace Gamma.BuisnessLogic
{
    public class BusinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }
    }
}
