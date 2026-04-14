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
    }
}
