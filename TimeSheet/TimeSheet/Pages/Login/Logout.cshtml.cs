using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TimeSheet.Pages.Login
{
    public class LogoutModel : PageModel
    {

        public string SessionUser { get; set; }

        public IActionResult OnGet()
        {
            SessionUser = HttpContext.Session.GetString("userid");

            if (SessionUser != null)
            {
                HttpContext.Session.Clear();
            }
            return RedirectToPage("/Index");
        }
    }
}