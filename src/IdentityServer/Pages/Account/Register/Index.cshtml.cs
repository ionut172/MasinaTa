
using System.Security.Claims;
using IdentityModel;
using IdentityServer.Models;
using IdentityServer.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityService.Pages.Register;
    [SecurityHeaders]
    [AllowAnonymous]
    public class Index : PageModel
    {
        private UserManager<ApplicationUser> _userManager;
         [BindProperty]
        public RegisterVM Input {get;set;}
        [BindProperty]
        public bool RegisterSuccess {get;set;}
        public Index (UserManager<ApplicationUser> userManager){
            _userManager = userManager;
        }
       


        
        public IActionResult OnGet(string returnUrl)
        {
            Input = new RegisterVM {
                ReturnUrl = returnUrl,
            };
            return Page();
        }
        public async Task <IActionResult> OnPost() {
            if(Input.Button != "register") return Redirect("~/");
            if(ModelState.IsValid){
                    var user = new ApplicationUser {
                        UserName = Input.Username,
                        Email = Input.Email,
                        EmailConfirmed = true,
                    };
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    var claim = new Claim(JwtClaimTypes.Name, Input.FullName);
                    if(result.Succeeded){
                        await _userManager.AddClaimAsync(user, claim);
                        RegisterSuccess = true;
                    }
                    else {
                        RegisterSuccess = false;
                    }
            }
             return Page();
        }
   
    }

