using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.ViewModels.Category
{
    public class CreateCategoryRequestViewModel
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public bool IsIncome { get; set; }
    }
}
