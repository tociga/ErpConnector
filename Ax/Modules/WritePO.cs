using System.Linq;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;

namespace ErpConnector.Ax.Modules
{
    public class WritePO
    {
        Resources _context;
        public WritePO(Resources context)
        {
            _context = context;
        }

        public void WriteTestPO()
        {
           

            var rpOld = _context.ReleasedDistinctProducts.Take(1).First();
            //var rp = new ReleasedDistinctProduct();

            var rp = PropertyCopy<ReleasedDistinctProduct>.CopyFrom<ReleasedDistinctProduct>(rpOld);
            rp.ProductNumber = rpOld.ProductNumber + "Test";
            rp.SalesGSTReliefCategoryCode = null;

            _context.AddToReleasedDistinctProducts(rp);
            _context.SaveChanges();
        }
    }
}
