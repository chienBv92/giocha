using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Web_Gio_Cha.Resources
{
    public class Constant
    {
        public const string SESSION_SCROLL_TOP = "SESSION_SCROLL_TOP";

        public const int TIME_OUT = 419;
        public const int NOT_FOUND = 404;
        public const int CREATED = 201;

        public const int SUCCESSFUL = 200;
        public const int INTERNAL_SERVER_ERROR = 500;
        public const int EXPECTATION_FAILED = 417;

        public const long MAX_50 = 50;
        public const long MAX_100 = 100;
        public const long MAX_250 = 250;
        public const int MAX_PHONE_LENGTH = 13;
        public const int MAX_EMAIL_LENGTH = 100;
        public const int MAX_USER_NAME_LENGTH = 50;
        public const int MIN_INPUT_PASS = 6;
        public const int MAX_INPUT_PASS = 50;
        public const string DEFAULT_VALUE = "0";


        public class DeleteFlag
        {
            public const string NON_DELETE = "0";

            public const string DELETE = "1";

            public static readonly OrderedDictionary Items = new OrderedDictionary
            {
                { NON_DELETE, "Chưa xóa" },
                { DELETE, "Đã xóa" }
            }.AsReadOnly();
        }

        public class CityLevel
        {
            public const int City = 0;

            public const int District = 1;

            public const int Town = 2;

            public static readonly OrderedDictionary Items = new OrderedDictionary
            {
                { City, "tỉnh" },
                { District, "quận, huyện" },
                { Town, "xã, phường" },
            }.AsReadOnly();
        }
        public class ConfirmEmail
        {
            public const string NONE = "0";

            public const string CONFIRMED = "1";

            public static readonly OrderedDictionary Items = new OrderedDictionary
            {
                { NONE, "Chưa xác nhận" },
                { CONFIRMED, "Đã xác nhận" }
            }.AsReadOnly();
        }

        public class Status
        {
            public const bool NONE = false;

            public const bool ACTIVE = true;

        }

    }

    public class WindowName
    {
        public const string COOKIE_NAME = "WindowName";
        public const string MAIN = "Main";

        public static readonly OrderedDictionary Items = new OrderedDictionary
        {
            { "Login", MAIN }
        }.AsReadOnly();
    }

    public class Level_City
    {
        public const string Level_0 = "";

        public const string Level_1 = "1";

        public const string Level_2 = "2";

        public static readonly OrderedDictionary Items = new OrderedDictionary
            {
                { Level_0, "Cấp Tỉnh" },
                { Level_1, "Cấp quận, huyện" },
                { Level_2, "Cấp xã, phường" }
            }.AsReadOnly();
    }
}