using BagheerasDream.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BagheerasDream.Web.Pages.Account;

[AllowAnonymous]
public class LoginModel : PageModel
{
	private readonly SignInManager<ApplicationUser> _signInManager;

	public LoginModel(SignInManager<ApplicationUser> signInManager)
	{
		this._signInManager = signInManager;
	}

	[BindProperty]
	public InputModel Input { get; set; } = new();

	[BindProperty(SupportsGet = true)]
	public string? ReturnUrl { get; set; }

	public class InputModel
	{
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public bool RememberMe { get; set; }
	}

	public void OnGet()
	{
	}

	public async Task<IActionResult> OnPostAsync()
	{
		var result = await this._signInManager.PasswordSignInAsync(
			Input.Email,
			Input.Password,
			Input.RememberMe,
			lockoutOnFailure: true);

		if (result.Succeeded)
		{
			if (!string.IsNullOrWhiteSpace(ReturnUrl) &&
				Url.IsLocalUrl(ReturnUrl))
			{
				return this.LocalRedirect(ReturnUrl);
			}

			return this.Redirect("/admin");
		}

		this.ModelState.AddModelError(string.Empty, "Invalid email or password.");

		return Page();
	}
}