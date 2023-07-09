using System;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Repository
{
	public interface ITokenRepository
	{
		string CreateJWTToken(ApplicationUser user, List<String> roles);
	}
}

