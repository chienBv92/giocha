//------------------------------------------------------------------------
// Version		: 001
// Designer		: ChienBV
// Date			: 2018/01/23
// Comment		: Create new
//-------------------------------
//------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WebDuhoc.Models;
using System.Web;
using System.Text.RegularExpressions;

namespace Web_Gio_Cha.UtilityService
{
	public static partial class StringUtil
	{
        public static string GetHtmlTitle(string title)
        {
            return string.Format("{0} : {1}", "Giò Chả Vân Bảo", title);
        }

        public static string GetScreenTitle(string title)
        {
            return title;
        }

        public static string RTrim(string stringValue)
		{
			if ( String.IsNullOrEmpty( stringValue ) )
			{
				return stringValue;
			}
			else
			{
				return stringValue.TrimEnd();
			}
		}

		public static string TrimString(string inputString, int lenghRequire)
		{
			if (inputString.Length > lenghRequire)
				return inputString.Remove(lenghRequire) + "...";
			else
				return inputString;
		}

        private const uint LOCALE_SYSTEM_DEFAULT = 0x0800;
        private const uint LCMAP_HALFWIDTH = 0x00400000;
	    public static string ConvertToHalfSize(string inputString)
	    {
            StringBuilder sb = new StringBuilder(256);
            LCMapString(LOCALE_SYSTEM_DEFAULT, LCMAP_HALFWIDTH, inputString, -1, sb, sb.Capacity);
            return sb.ToString();
	    }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int LCMapString(uint Locale, uint dwMapFlags, string lpSrcStr, int cchSrc, StringBuilder lpDestStr, int cchDest);
	}
}
