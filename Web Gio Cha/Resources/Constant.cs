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
        public const int MAX_DISCOUNT = 99;

        public const string DEFAULT_VALUE = "0";
        public const string DEFAULT_PASSWORD = "pass123";


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

        public class SlideType
        {
            public const int Top = 1;

            public const int Left = 2;

            public static readonly OrderedDictionary Items = new OrderedDictionary
            {
                { Top, "Top" },
                { Left, "Left" }
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

            public const string RESET_PASSWORD = "2";

            public static readonly OrderedDictionary Items = new OrderedDictionary
            {
                { NONE, "Chưa xác nhận" },
                { CONFIRMED, "Đã xác nhận" },
                { RESET_PASSWORD, "Reset password" }
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

    public class MenuCode
    {
        public const int None = 0;
        public const int Intro = 1;

        public const int Help = 2;

        public const int Contact = 3;

        public static readonly OrderedDictionary Items = new OrderedDictionary
            {
                { None, "Chọn trang" },
                { Intro, "Trang giới thiệu" },
                { Help, "Trang trợ giúp" },
                { Contact, "Trang liên hệ" }
            }.AsReadOnly();
    }

    public class PaymentMethodType
    {
        public const int None = 0;

        public const int Tien_mat = 1;

        public const int Bank = 2;


        public static readonly OrderedDictionary Items = new OrderedDictionary
            {
                { None, "Chọn PP thanh toán" },
                { Tien_mat, "Thanh toán tại nhà" },
                { Bank, "Chuyển khoản ngân hàng" },

            }.AsReadOnly();
    }

    public class PayStatus
    {
        public const bool None = false;

        public const bool Paid = true;

        public static readonly OrderedDictionary Items = new OrderedDictionary
            {
                { None, "Chưa thanh toán" },
                { Paid, "Đã thanh toán" }
            }.AsReadOnly();
    }

    public class OrderStatus
    {
        public const int Create = 0;

        public const int TakingOrder = 1;

        public const int Shiping = 2;

        public const int Delivery = 3;

        public const int Finished = 4;

        public const int Cancel = 5;

        public const int Error = 6;

        public static readonly OrderedDictionary Items = new OrderedDictionary
            {
                { Create, "Mới khởi tạo" },
                { TakingOrder, "Đã duyệt" },
                { Shiping, "Đang giao hàng" },
                { Delivery, "Đã giao" },
                { Finished, "Hoàn tất" },
                { Cancel, "Hủy" },
                { Error, "Lỗi" }
            }.AsReadOnly();
    }

    public class SortType
    {

        public const string ASC = "ASC";

        public const string DESC = "DESC";

        public const string ASC_NAME = "Tăng dần";

        public const string DESC_NAME = "Giảm dần";
    }

    public class SortAdminOrderShipList
    {
        public const string CREATE_DATE = "CREATE_DATE";
        public const string ORDER_CODE = "ORDER_CODE";
        public const string TOTAL_MONEY = "TOTAL_MONEY";

        public static readonly OrderedDictionary Items = new OrderedDictionary
        {
            { CREATE_DATE, "Ngày khởi tạo" },
            { ORDER_CODE, "Mã đơn hàng" },
            { TOTAL_MONEY, "Tổng tiền thu" }
            
        }.AsReadOnly();
    }
}