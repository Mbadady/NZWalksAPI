using System;
using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Models.Domains
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }

	}
}

