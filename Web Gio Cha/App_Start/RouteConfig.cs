using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web_Gio_Cha
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
             name: "Admin",
             url: "admin/{id}",
             defaults: new { controller = "AdminSystem", action = "Index", id = UrlParameter.Optional }
         );

            routes.MapRoute(
            name: "OrderDetail",
            url: "chi-tiet-don-hang/{Code}",
            defaults: new { controller = "UserManageOrder", action = "OrderDetail", id = UrlParameter.Optional }
        );

            routes.MapRoute(
             name: "UserOrderList",
             url: "danh-sach-don/{id}",
             defaults: new { controller = "UserManageOrder", action = "ManageOrderList", id = UrlParameter.Optional }
         );

            routes.MapRoute(
          name: "AccountInfo",
          url: "tai-khoan/{id}",
          defaults: new { controller = "User", action = "ViewAccount", id = UrlParameter.Optional }
      );

            // Tao Url view liên hệ
            routes.MapRoute(
           name: "DatHang",
           url: "dat-hang/{id}",
           defaults: new { controller = "GioHang", action = "DatHang", id = UrlParameter.Optional }
       );

            // Tao Url view liên hệ
            routes.MapRoute(
           name: "XemGioHang",
           url: "xem-gio-hang/{id}",
           defaults: new { controller = "GioHang", action = "XemGioHang", id = UrlParameter.Optional }
       );

            // Tao Url view liên hệ
            routes.MapRoute(
           name: "Contact",
           url: "lien-he/{id}",
           defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
       );

            // Tao Url view trợ giúp
            routes.MapRoute(
              name: "Help",
              url: "tro-giup/{id}",
              defaults: new { controller = "Home", action = "Help", id = UrlParameter.Optional }
          );

            // Tao Url view giới thiệu
            routes.MapRoute(
                name: "Intro",
                url: "gioi-thieu/{id}",
                defaults: new { controller = "Home", action = "Intro", id = UrlParameter.Optional }
            );

            // Tao Url view product by category
            routes.MapRoute(
                name: "ViewProductDetail",
                url: "xem-chi-tiet/{metatitle}/{id}",
                defaults: new { controller = "Product", action = "ViewProductDetail", id = UrlParameter.Optional }
            );

            // Tao Url view product by category
            routes.MapRoute(
                name: "GetProductByCategory",
                url: "san-pham-{metatitle}/{categoryId}",
                defaults: new { controller = "Category", action = "GetProductByCategory", id = UrlParameter.Optional }
            );

            routes.MapRoute(
             name: "RePassword",
             url: "quen-mat-khau/{id}",
             defaults: new { controller = "Login", action = "ResetPassword", id = UrlParameter.Optional }
         );

            routes.MapRoute(
             name: "Register",
             url: "dang-ky/{id}",
             defaults: new { controller = "Login", action = "Register", id = UrlParameter.Optional }
         );
            routes.MapRoute(
             name: "Login",
             url: "dang-nhap/{id}",
             defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
         );
            routes.MapRoute(
            name: "Logout",
            url: "thoat/{id}",
            defaults: new { controller = "Login", action = "Logout", id = UrlParameter.Optional }
        );

          
            routes.MapRoute(
              name: "Products",
              url: "san-pham/{id}",
              defaults: new { controller = "Product", action = "GetProductAllCategory", id = UrlParameter.Optional }
          );

            routes.MapRoute(
            name: "NewsDetail",
            url: "tin-tuc/{metatitle}/{newsId}",
            defaults: new { controller = "ManageNews", action = "NewsDetail", id = UrlParameter.Optional }
        );

            routes.MapRoute(
             name: "NewsAll",
             url: "tin-tuc-all/{id}",
             defaults: new { controller = "ManageNews", action = "AllNews", id = UrlParameter.Optional }
         );

            routes.MapRoute(
              name: "News",
              url: "tin-tuc/{id}",
              defaults: new { controller = "ManageNews", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "Home",
              url: "trang-chu/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
