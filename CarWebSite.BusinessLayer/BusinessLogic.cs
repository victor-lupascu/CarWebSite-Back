using CarWebSite.BusinessLayer.Interfaces;
using CarWebSite.BusinessLayer.Structure;

namespace CarWebSite.BusinessLayer
{
    public class BusinessLogic
    {
        public BusinessLogic() { }

        public IBrandAction BrandAction()
        {
            return new BrandExecution();
        }

        public IFavoriteAction FavoriteAction()
        {
            return new FavoriteExecution();
        }

        public ICarImageAction CarImageAction()
        {
            return new CarImageExecution();
        }

        public ICarAction CarAction()
        {
            return new CarExecution();
        }

        public IContactMessageAction ContactMessageAction()
        {
            return new ContactMessageExecution();
        }
    }
}
