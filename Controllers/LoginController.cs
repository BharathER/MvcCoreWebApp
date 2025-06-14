using Microsoft.AspNetCore.Mvc;
using CoreWebMvc1stApp.Models;

namespace CoreWebMvc1stApp.Controllers
{
    public class LoginController : Controller
    {
        EmplyeeDB dbobj = new EmplyeeDB();
        public IActionResult Login_Load()
        {
            return View();
        }

        public IActionResult Insert_login(Employee clsObj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resp = dbobj.LoginDb(clsObj);
                    
                    if (resp == "Login Successful")
                    {
                        
                        return RedirectToAction("Profile_Load", "Profile", new { id = clsObj.Id });
                        //TempData["msg"] = resp;
                        //Session["id"] = clsObj.Id;

                    }
                    else
                    {
                        
                        TempData["msg"] = resp;
                    }
                  
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "SQL Error: " + ex.Message;

            }
            return View("Login_Load");
        }
    }
}
