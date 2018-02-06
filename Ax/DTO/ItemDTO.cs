using System.Collections.Generic;

namespace ErpConnector.Ax.DTO
{
    public class ItemDTO : BaseComDTO
    {
        public int id { get; set; }

        public ProductMasterWriteDTO productMaster { get; set; }

        public ReleasedProductMasterWriteDTO releasedProductMaster { get; set; }
        public List<ReleasedProductVariantDTO> variants { get; set; }

        public DistinctProductDTO distinctProduct { get; set; }

    }
}
