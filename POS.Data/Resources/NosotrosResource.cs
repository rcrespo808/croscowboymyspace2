using POS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data
{
    public class NosotrosResource : ResourceParameters
    {
        public NosotrosResource() : base("Location")
        {

        }
        public string Location { get; set; }
    }
}
