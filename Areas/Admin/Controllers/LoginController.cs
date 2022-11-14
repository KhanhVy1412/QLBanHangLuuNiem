using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanHangLuuNiem.Models;
using QLBanHangLuuNiem.Models.ViewModels;

namespace QLBanHangLuuNiem.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        QLShopLuuNiemEntities7 db = new QLShopLuuNiemEntities7();

        //localhos:xxxx/Login/Index
        public ActionResult Index()
        {
            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                ViewBag.username = Request.Cookies["username"].Value;
                ViewBag.password = Request.Cookies["password"].Value;
            }
            return View();
        }

        public void ghinhotaikhoan(string username, string password)
        {
            HttpCookie us = new HttpCookie("username");
            HttpCookie pas = new HttpCookie("password");

            us.Value = username;
            pas.Value = password;

            us.Expires = DateTime.Now.AddDays(1);
            pas.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(us);
            Response.Cookies.Add(pas);

        }

        [HttpPost]
        public ActionResult kiemtradangnhap(string username, string password, string ghinho)
        {
            if (Request.Cookies["username"] != null && Request.Cookies["username"] != null)
            {
                username = Request.Cookies["username"].Value;
                password = Request.Cookies["password"].Value;
            }

            if (checkpassword(username, password))
            {
                var userSession = new UserLogin();
                userSession.TenDangNhap = username;

                var listGroups = GetListGroupID(username);//Có thể viết dòng lệnh lấy các GroupID từ CSDL, ví dụ gán ="ADMIN", dùng List<string>

                Session.Add("SESSION_GROUP", listGroups);
                Session.Add("USER_SESSION", userSession);

                if (ghinho == "on")//Ghi nhớ
                    ghinhotaikhoan(username, password);
                return Redirect("~/Admin/SanPham");

            }
            return Redirect("~/Admin/Login");
        }
        public List<string> GetListGroupID(string userName)
        {
            // var user = db.User.Single(x => x.UserName == userName);

            var data = (from a in db.Quyens
                        join b in db.TaiKhoans on a.MaQuyen equals b.MaQuyen
                        where b.TenDangNhap == userName

                        select new
                        {
                            UserGroupID = b.MaQuyen,
                            UserGroupName = a.TenQuyen
                        });

            return data.Select(x => x.UserGroupName).ToList();

        }
        public bool checkpassword(string username, string password)
        {
            if (db.TaiKhoans.Where(x => x.TenDangNhap == username && x.MatKhau == password).Count() > 0)

                return true;
            else
                return false;


        }

        public ActionResult SignOut()
        {

            Session["USER_SESSION"] = null;
            Session["SESSION_GROUP"] = null;


            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                HttpCookie us = Request.Cookies["username"];
                HttpCookie ps = Request.Cookies["password"];

                ps.Expires = DateTime.Now.AddDays(-1);
                us.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(us);
                Response.Cookies.Add(ps);
            }

            //return Redirect("/Admin/Login");
            return Redirect("~/SPClient/Index");
        }
    }
}