using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MyApp.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboCompanyTypes();
    }
}
