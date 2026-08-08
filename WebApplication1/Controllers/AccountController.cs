using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

public class AccountController : Controller
{
    private readonly IUserRepository _userRepo;

    public AccountController(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        if (_userRepo.ValidateCredentials(username, password))
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claims, "CookieAuth");
            HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(identity));
            return RedirectToAction("Index", "Visitor");
        }

        ModelState.AddModelError("", "Invalid Username or Password.");
        return View();
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public IActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            _userRepo.AddUser(user);
            return RedirectToAction("Login");
        }
        return View(user);
    }

    public IActionResult Logout()
    {
        HttpContext.SignOutAsync("CookieAuth");
        return RedirectToAction("Login");
    }
}