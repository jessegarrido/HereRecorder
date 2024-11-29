using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartaCam
{
    internal static class Extensions
    {
        public static string? TranslateString(this string untranslatedstring)
        {
            try
            {
                var translatedstring = untranslatedstring.Replace("[Date]", DateOnly.FromDateTime(DateTime.Now).ToString(), StringComparison.CurrentCultureIgnoreCase);
                translatedstring = translatedstring.Replace("[#]", "X");
                return translatedstring;
            }
            catch (Exception)
            {
                return untranslatedstring;
            }
        }
      
    }
}
