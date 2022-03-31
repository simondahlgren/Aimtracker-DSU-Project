using DSUgrupp2.Models;
using DSUgrupp2.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DSUgrupp2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        #region Constructor
        /// <summary>
        /// Injects UserManager, SignInManager & RoleManager for the accout function.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="roleManager"></param>
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
             var seedRoles = CreateRoles("Trainer", "Athlete");
        }
        #endregion
        #region Login Function
        /// <summary>
        /// Basic method for returning a view when starting the application.
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Function used for the login function. Recieves a LoginViewModel, checks if modelstate is valid, if all is ok returns the user loged in.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {               
                System.Threading.Thread.Sleep(1000); // Got error if you tried to log in directly after you logged out.
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false );
                if (result.Succeeded)
                {                    
                    return RedirectToAction("index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Wrong password or accountname");
            }
            return View(model);
        }
        #endregion
        #region Registration
        /// <summary>
        /// Method for registering a user.
        /// </summary>
        /// <param name="registerViewModel"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel) // metod för att registrera användare
        {
            if (!registerViewModel.Athlete && !registerViewModel.Trainer )
            {
                ModelState.AddModelError(string.Empty, "You have to choose a role");
                
                
            }

            if (ModelState.IsValid) // Checks if modelstate is valid.
            {
                var user = new IdentityUser // Creates a user.
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    
                    
                    
                };

                System.Threading.Thread.Sleep(1000); // Got error if you tried to log in directly after you logged out.
                var result = await _userManager.CreateAsync(user, registerViewModel.Password );
                if (result.Succeeded) // Checks if login succeded.
                {
                    System.Threading.Thread.Sleep(1000); // Got error if you tried to log in directly after you logged out.
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    await Roles(user, registerViewModel); // Awaits a model 
                    return RedirectToAction("index", "home");
                }
                foreach (var error in result.Errors) // Errors = do this.
                {
                    ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError("", "Inloggningen misslyckades");
            }

            return View(registerViewModel); // Returns the view with a viewmodel.
        }
        #endregion
        #region Create Roles
        /// <summary>
        /// Method for creating roles for the application
        /// </summary>
        /// <param name="inputRoleOne"></param>
        /// <param name="inputRoleTwo"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateRoles(string inputRoleOne, string inputRoleTwo)
        {
                  
            var roleOne = new IdentityRole(inputRoleOne);
            var roleTwo = new IdentityRole(inputRoleTwo);
            var roleOneExists = await _roleManager.RoleExistsAsync(roleOne.Name);
            var roleTwoExists = await _roleManager.RoleExistsAsync(roleTwo.Name);
            if (!roleOneExists||!roleTwoExists)
            {
                var result = await _roleManager.CreateAsync(roleOne);
                var result2 = await _roleManager.CreateAsync(roleTwo);
                
            }
            return View();
        }
        #endregion
        #region Role Creator
        /// <summary>
        /// Adds roles to the application connected to accounts.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="registerViewModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> Roles(IdentityUser user, RegisterViewModel registerViewModel)
        {

            
            if (registerViewModel.Athlete == true)
            {

                
                var result = await _userManager.AddToRoleAsync(user, "Athlete");
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            if (registerViewModel.Trainer == true)
            {
                var result = await _userManager.AddToRoleAsync(user, "Trainer");
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return RedirectToAction("Privacy", "Home");
        }
        #endregion
        #region Logout
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }   
}
