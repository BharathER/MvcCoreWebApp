using Microsoft.AspNetCore.Mvc;
using CoreWebMvc1stApp.Models;

namespace CoreWebMvc1stApp.Controllers
{
    public class DBInsertController : Controller
    {
        EmplyeeDB dbobj = new EmplyeeDB();
        public IActionResult Insert_Load()
        {
            return View();
        }

        public IActionResult Insert_click(Employee objcls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resp = dbobj.InsertDB(objcls);
                    TempData["msg"] = resp;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "SQL Error: " + ex.Message;
               
            }
            return View("Insert_Load", objcls);
        }
    }
}
