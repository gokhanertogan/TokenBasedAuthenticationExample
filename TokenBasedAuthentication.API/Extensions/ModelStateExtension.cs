using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TokenBasedAuthentication.API.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary modelState)
        {
            return modelState.SelectMany(m=>m.Value.Errors).Select(x=>x.ErrorMessage).ToList();
        }
    }
}