using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.DTO
{
    public class RetailProductTranslationDTO
    {
        public string LanguageId { get; set; }
        public long Product { get; set; }
        public string EcoResProduct_DisplayProductNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
