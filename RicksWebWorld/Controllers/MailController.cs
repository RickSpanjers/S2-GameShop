using System;
using System.Collections.Generic;
using System.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RicksWebWorld.ViewModels;
using RicksWebWorld.Repositories;
using RicksWebWorld.Context.Dapper;
using RicksWebWorld.Models;
using System.Text;

namespace RicksWebWorld.Controllers
{
	public class MailController : Controller
	{
		private readonly UserRepository userRep = new UserRepository(new UserDapperContext());
		private readonly UserController uc = new UserController();
		private readonly OrderController oc = new OrderController();
		private readonly AutoMapperExtension mapextension = new AutoMapperExtension();

		/// <summary>
		/// Sends a mail to the admin's emailaddress
		/// </summary>
		/// <param name="model">ContactMailViewModel</param>
		/// <returns>Returns actionresult Index</returns>
		public async Task<IActionResult> SendContactMailAsync(ContactMailViewModel model)
		{
			//Set up sendgrid API
			string apiKey = "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";
			SendGridClient client = new SendGridClient(apiKey);
			EmailAddress from = new EmailAddress(model.Email, model.FullName);
			EmailAddress to = new EmailAddress("Rick.spanjers@outlook.com", "RicksWebWorld");
			string subject = "New submission contactform WebWorld";

			//Set the content
			string plainTextContent = model.Details;
			string htmlContent = model.Details;
			SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

			//Send the mail
			Response response = await client.SendEmailAsync(msg);
			return RedirectToAction("Index", "Home");
		}


		public async Task<IActionResult> ApplyNewsLetterAsync(string Email)
		{
			//Set up sendgrid API
			string apiKey = "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";
			SendGridClient client = new SendGridClient(apiKey);
			EmailAddress from = new EmailAddress(Email, "New Submission");
			EmailAddress to = new EmailAddress("Rick.spanjers@outlook.com", "RicksWebWorld");
			string subject = "New application for newsletter WebWorld";	

			//Set the content
			string plainTextContent = Email + " has subscribed to the WebWorld newsletter";
			string htmlContent = Email + " has subscribed to the WebWorld newsletter";
			SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

			//Send the mail
			Response response = await client.SendEmailAsync(msg);
			return RedirectToAction("Index", "Home");
		}

		/// <summary>
		/// Sends a mail to the admin's emailaddress
		/// </summary>
		/// <param name="model">ContactMailViewModel</param>
		/// <returns>Returns actionresult Index</returns>
		public async Task<IActionResult> SendEmailConfirmation(UserRoleViewModel model)
		{	
			//Set up sendgrid API
			string apiKey = "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";
			SendGridClient client = new SendGridClient(apiKey);
			EmailAddress from = new EmailAddress("info@RicksWebWorld.nl", "RicksWebWorld");
			EmailAddress to = new EmailAddress(model.Email);
			string subject = "Confirm your e-mailaddress RicksWebWorld";

			//Set the content
			uc.Add(model, new List<int>());
			User InsertedUser = userRep.RetrieveUserByUsername(model.Username);
			var callbackUrl = Url.Action("ConfirmEmail", "User", new { userId = InsertedUser.ReturnUserId() });
			Uri baseUri = new Uri("http://rickswebworld.azurewebsites.net");
			Uri myUri = new Uri(baseUri, callbackUrl);

			StringBuilder sb = new StringBuilder();
			sb.Append("<a href ='");
			sb.Append(myUri);
			sb.Append("'>Click here to confirm</a>");
			string fullUrl = sb.ToString();

			string plainTextContent = "Click on the URL to confirm your e-mailaddress " + fullUrl;
			string htmlContent = "Click on the URL to confirm your e-mailaddress " + fullUrl;
			SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

			//Send the mail
			Response response = await client.SendEmailAsync(msg);
			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> SendResetPasswordMail(PasswordResetViewModel model)
		{	
			//Set up sendgrid API
			string apiKey = "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";
			SendGridClient client = new SendGridClient(apiKey);
			EmailAddress from = new EmailAddress("info@RicksWebWorld.nl", "RicksWebWorld");
			EmailAddress to = new EmailAddress(model.Email);
			string subject = "Reset your password RicksWebWorld";

			//Set the content
			User selectedUser = userRep.RetrieveUserByEmail(model.Email);
			var callbackUrl = Url.Action("PasswordReset", "User", new { userId = selectedUser.ReturnUserId() });
			Uri baseUri = new Uri("http://rickswebworld.azurewebsites.net");
			Uri myUri = new Uri(baseUri, callbackUrl);

			StringBuilder sb = new StringBuilder();
			sb.Append("<a href ='");
			sb.Append(myUri);
			sb.Append("'>Click here to reset</a>");
			string fullUrl = sb.ToString();

			string plainTextContent = "Click on the URL to reset your password";
			string htmlContent = "<p>Click on the URL to reset your password</p> " + fullUrl;
			SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
			Response response = await client.SendEmailAsync(msg);
			return RedirectToAction("Login", "User");	
		}

		public async Task<IActionResult> OrderConfirmationMail(UserOrderViewModel model)
		{
			//Set up sendgrid API
			string apiKey = "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";
			SendGridClient client = new SendGridClient(apiKey);
			EmailAddress from = new EmailAddress("info@RicksWebWorld.nl", "RicksWebWorld");
			EmailAddress to = new EmailAddress(model.Email);
			string subject = "Order Confirmation RicksWebWorld";
			
			string content01 = "<h1>Thank you for your purchase!</h1>";
			string content02 = "<p>Hi, we're getting your order ready to be shipped. We will notify you when it has been sent</p>";
			string content03 = "<hr/>";
			string content04 = "<a style='display:inherit' href='http://rickswebworld.azurewebsites.net'>Visit our store</a>";
			string content05 = "<img style='width:150px; padding-top:20px;' src='http://rickswebworld.azurewebsites.net/images/logo-webworld.png'>";
			string htmlContent = content01 + content02 + content03 + content04 + content05;

			SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
			Response response = await client.SendEmailAsync(msg);
			return RedirectToAction("Login", "User");
			
		}
	}
}