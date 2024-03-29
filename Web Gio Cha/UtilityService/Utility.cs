﻿//------------------------------------------------------------------------
// Version		: 001
// Designer		: ChienBV
// Date			: 2018/01/23
// Comment		: Create new
//------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Resources;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Net.Mail;
using Web_Gio_Cha.UtilityService;
using Web_Gio_Cha.Resources;

namespace Web_Gio_Cha.UtilityServices
{
    public class UtilityServices
	{
		#region Static paramater

		/// <summary>
		/// List time stamp
		/// </summary>
		private Dictionary<string, Int64> listTimeStamp = new Dictionary<string, Int64>();		

		#endregion

        public string GeneratePassword()
        {
            string strPwdchar = "abcdefghijklmnopqrstuvwxyz0123456789#+@&$ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string strPwd = "";
            Random rnd = new Random();
            for (int i = 0; i <= 7; i++)
            {
                int iRandom = rnd.Next(0, strPwdchar.Length - 1);
                strPwd += strPwdchar.Substring(iRandom, 1);
            }
            return strPwd;
        }

		private bool IsContentMatch(string ListItems, string Item)
		{
			int ListItemsLength = ListItems.Length;
			int ItemLength = Item.Length;
			System.Globalization.CompareInfo ci = System.Globalization.CompareInfo.GetCompareInfo("ja-JP");

			if (ListItemsLength < ItemLength)
				return false;
			else
			{
				string index = string.Empty;
				for (int i = 0; i <= ListItemsLength - ItemLength; i++)
				{
					index = ListItems.Substring(i, ItemLength);
					if (ci.Compare(index, Item, System.Globalization.CompareOptions.IgnoreWidth | System.Globalization.CompareOptions.IgnoreKanaType | System.Globalization.CompareOptions.IgnoreCase) == 0)
					{
						return true;
					}
				}
				return false;
			}
		}


        /// <summary>
        /// Round money by unit
        /// </summary>
        /// <param name="roundUnit">round unit</param>
        /// <param name="value">value need be round</param>
        /// <returns></returns>
        public static decimal RoundMoneyByUnit(string roundUnit, decimal value)
        {
            int round = 0;

            if (int.TryParse(roundUnit, out round))
            {
                decimal lastRoundUnit = Convert.ToDecimal(Math.Pow(10, round));
                var tempValue = value / lastRoundUnit;

                value = Math.Floor(tempValue) * lastRoundUnit;
            }

            return value;
        }

        /// <summary>
        /// Create default Year/Month for some screen to search
        /// </summary>
        /// <returns></returns>
        public static string CreateDefaultYearMonthToSearch()
        {
            DateTime currentDate = Utility.GetCurrentDateOnly();
            int date = currentDate.Day;
            string defaultDate = currentDate.ToString("yyyy/MM");

            if (16 > date)
            {
                defaultDate = currentDate.AddMonths(-1).ToString("yyyy/MM");
            }

            return defaultDate;
        }

        /// <summary>
        /// Remove wildcard characters in SQL
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string replaceWildcardCharacters(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Replace("|", "||").Replace("[", "|[").Replace("_", "|_").Replace("%", "|%").Replace("'", "|'").Replace("@", "|@");
        }

        public static bool CheckEmailValid(string emailAddress)
        {
            try
            {
                MailAddress email = new MailAddress(emailAddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool CheckURLValid(string url)
        {
            return Regex.Match(url, @"^[a-zA-Z0-9\!\""\#\$\%\&\'\(\)\=\~\|\-\^\@\[\;\:\]\,\.\/\`\{\+\*\}\>\?]*$").Success;
        }

        public static bool CheckPhoneNumberValid(string number)
        {
            return Regex.Match(number, @"^[0-9\-]+$").Success;
        }

        public static string FillBlankMethod(int stringLength = 0, string data = "")
        {
            while (stringLength > data.Length)
            {
                data += " ";
            }

            return data;
        }

        public static string ConvertToUnsign(string str)
        {
            string strFormD = str.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strFormD.Length; i++)
            {
                System.Globalization.UnicodeCategory uc =
                System.Globalization.CharUnicodeInfo.GetUnicodeCategory(strFormD[i]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(strFormD[i]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD)).Replace(" ","-");
        }

        public static IList<SelectListItem> SortTypeList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Dictionary<string, string> sortType = new Dictionary<string, string>();

            sortType.Add(SortType.ASC_NAME, SortType.ASC);
            sortType.Add(SortType.DESC_NAME, SortType.DESC);

            foreach (KeyValuePair<string, string> pair in sortType)
            {
                list.Add(new SelectListItem() { Text = pair.Key, Value = pair.Value, Selected = false });
            }

            return list;
        }

        public static IList<SelectListItem> getListStatusAll()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Dictionary<string, string> statusList = new Dictionary<string, string>();

            statusList.Add(OrderStatus.Items[OrderStatus.Create].ToString(), OrderStatus.Create.ToString());
            statusList.Add(OrderStatus.Items[OrderStatus.TakingOrder].ToString(), OrderStatus.TakingOrder.ToString());
            statusList.Add(OrderStatus.Items[OrderStatus.Shiping].ToString(), OrderStatus.Shiping.ToString());
            statusList.Add(OrderStatus.Items[OrderStatus.Delivery].ToString(), OrderStatus.Delivery.ToString());
            statusList.Add(OrderStatus.Items[OrderStatus.Finished].ToString(), OrderStatus.Finished.ToString());
            statusList.Add(OrderStatus.Items[OrderStatus.Cancel].ToString(), OrderStatus.Cancel.ToString());
            statusList.Add(OrderStatus.Items[OrderStatus.Error].ToString(), OrderStatus.Error.ToString());

            foreach (KeyValuePair<string, string> pair in statusList)
            {
                list.Add(new SelectListItem() { Text = pair.Key, Value = pair.Value, Selected = false });
            }

            return list;
        }

        //public static IList<SelectListItem> GetNoticeStatus()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    Dictionary<string, string> authorityList = new Dictionary<string, string>();

        //    authorityList.Add(NoticeMode.Items[NoticeMode.None].ToString(), NoticeMode.NoneText);
        //    authorityList.Add(NoticeMode.Items[NoticeMode.Display].ToString(), NoticeMode.DisplayText);

        //    foreach (KeyValuePair<string, string> pair in authorityList)
        //    {
        //        list.Add(new SelectListItem() { Text = pair.Key, Value = pair.Value, Selected = false });
        //    }

        //    return list;
        //}

    }
}