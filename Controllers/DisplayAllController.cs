using Microsoft.AspNetCore.Mvc;
using CoreWebMvc1stApp.Models;

namespace CoreWebMvc1stApp.Controllers
{
    public class DisplayAllController : Controller
    {
        EmplyeeDB dbobj = new EmplyeeDB();
        public IActionResult DisplayAll_Load()
        {
            List<Employee> empList = dbobj.ProfileList();
            return View(empList);
        }
    }
}