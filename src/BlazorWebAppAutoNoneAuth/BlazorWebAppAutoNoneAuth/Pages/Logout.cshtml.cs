using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorWebAppAutoNoneAuth.Pages
{
    public class LogoutModel : PageModel
    {
        public string ReturnUrl { get; private set; }
        public async Task<IActionResult> OnGetAsync(
            string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Clear the existing external cookie
            try
            {
                await HttpContext
                    .SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return LocalRedirect("/");
        }
    }
}
