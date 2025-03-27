using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation_WebApp.ViewModels;
using System.Security.Claims;

namespace Presentation_WebApp.Controllers;
public class AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, AppDbContext context) : Controller
{

    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly AppDbContext _context = context;

    [HttpGet]
    [Route("account/details")]
    public async Task<IActionResult> Details()
    {
        if (!_signInManager.IsSignedIn(User))
            return RedirectToAction("SignIn", "Auth");

        var userEntity = await _userManager.GetUserAsync(User);

        var address = await _context.Addresses.FindAsync(userEntity.AddressId);

        var viewModel = new AccountDetailsViewModel()
        {
            Basic = new AccountBasicInfo
            {
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email!,
                PhoneNumber = userEntity.PhoneNumber,
                //Bio = userEntity.Bio
            },

            Address = new AccountAddressInfo
            {
                StreetName = address?.StreetName!,
                PostalCode = address?.PostalCode!,
                City = address?.City!,

            }
        };
        return View(viewModel);
    }

    //[HttpPost]
    //public async Task<IActionResult> UpdateBasicInfo(AccountDetailsViewModel model)
    //{

    //    if (TryValidateModel(model.Basic!))
    //    {
    //        var user = await _userManager.GetUserAsync(User);
    //        if (user != null)
    //        {
    //            user.FirstName = model.Basic!.FirstName;
    //            user.LastName = model.Basic!.LastName;
    //            //user.Email = model.Basic!.Email;
    //            //user.PhoneNumber = model.Basic!.PhoneNumber;
    //            //user.UserName = model.Basic!.Email;
    //            //user.Bio = model.Basic!.Bio;

    //            var result = await _userManager.UpdateAsync(user);
    //            if (result.Succeeded)
    //            {
    //                TempData["StatusMessage"] = "Updated basic information successfully";
    //            }
    //            else
    //            {
    //                TempData["StatusMessage"] = "Unable to save basic information";
    //            }
    //        }
    //    }
    //    else
    //    {
    //        TempData["StatusMessage"] = "Unable to save basic information";
    //    }

    //    return RedirectToAction("Details", "Account");
    //}


    [HttpPost]
    public async Task<IActionResult> UpdateBasicInfo(AccountDetailsViewModel model)
    {
        if (model.Basic == null)
        {
            TempData["StatusMessage"] = "Form submission error: Missing basic info.";
            return RedirectToAction("Details", "Account");
        }

        if (!ModelState.IsValid)
        {
            TempData["StatusMessage"] = "Invalid input data.";
            return View("Details", model); // Return same view with validation errors
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            TempData["StatusMessage"] = "User not found.";
            return RedirectToAction("Details", "Account");
        }

        // Update user details
        user.FirstName = model.Basic.FirstName;
        user.LastName = model.Basic.LastName;
        user.PhoneNumber = model.Basic.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            TempData["StatusMessage"] = "Updated basic information successfully";
        }
        else
        {
            TempData["StatusMessage"] = "Unable to save basic information";
        }

        return RedirectToAction("Details", "Account");
    }



    [HttpPost]
    public async Task<IActionResult> UpdateAddressInfo(AccountDetailsViewModel model)
    {
        if (model.Address == null)
        {
            TempData["StatusMessage"] = "Form submission error: Missing address info.";
            return RedirectToAction("Details", "Account");
        }

        if (!ModelState.IsValid)
        {
            TempData["StatusMessage"] = "Invalid address input.";
            return View("Details", model); // Return view with validation errors
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            TempData["StatusMessage"] = "User not found.";
            return RedirectToAction("Details", "Account");
        }

        try
        {
            if (user.AddressId == null)
            {
                // If the address doesn't exist, create a new one
                user.Address = new AddressEntity
                {
                    StreetName = model.Address.StreetName,
                    PostalCode = model.Address.PostalCode,
                    City = model.Address.City
                };
            }
            else
            {
                var address = await _context.Addresses.FindAsync(user.AddressId);


                // If address exists, update it
                address.StreetName = model.Address.StreetName;
                address.PostalCode = model.Address.PostalCode;
                address.City = model.Address.City;
                user.Address = address;
            }

            // Save changes using UserManager
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["StatusMessage"] = "Updated address information successfully.";
            }
            else
            {
                TempData["StatusMessage"] = "Failed to update address information.";
            }
        }
        catch (Exception ex)
        {
            TempData["StatusMessage"] = "An error occurred while saving address information.";
        }

        return RedirectToAction("Details", "Account");
    }


}
