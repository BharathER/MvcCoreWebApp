using Microsoft.AspNetCore.Mvc;
using CoreWebMvc1stApp.Models;

namespace CoreWebMvc1stApp.Controllers
{
    public class ProfileController : Controller
    {
        EmplyeeDB dbobj = new EmplyeeDB();
        public IActionResult Profile_Load(int id)
        {
            Employee emp = dbobj.Profile(id);
            TempData["uid"] = id;
            return View(emp);
        }

        public IActionResult Update_click(Employee objcls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resp = dbobj.UpdateDB(Convert.ToInt32(TempData["uid"]),objcls);
                    TempData["msg"] = resp;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "SQL Error: " + ex.Message;
            }
            return View("Profile_Load");
        }
    }
}
