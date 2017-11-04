using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZGCMS.Model.Admin.ViewModel.System
{
    public class BakFileVM
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FilePath { get; set; }
        public double FileSize { get; set; }
    }
}
