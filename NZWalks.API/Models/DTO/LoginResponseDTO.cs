using System;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Models.DTO
{
	public class LoginResponseDTO
	{
		public string Email { get; set; }

		public string Name { get; set; }

		public string JwtToken { get; set; }
	}
}

