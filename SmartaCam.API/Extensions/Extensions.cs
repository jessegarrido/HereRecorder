using SmartaCam.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartaCam
{
    internal static class Extensions
    {
        public static bool IsFileReady(this string filename)
        {
            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            try
            {
                using (FileStream inputStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                    return inputStream.Length > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string? TranslateMp3TagString(this string untranslatedstring)
        {
            try
            {
                int take = Settings.Default.Takes++;
                var translatedstring = untranslatedstring.Replace("[Date]", DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd"), StringComparison.CurrentCultureIgnoreCase);
                translatedstring = translatedstring.Replace("[#]", take.ToString());
                return translatedstring;
            }
            catch (Exception)
            {
                return untranslatedstring;
            }
        }
    }
}
