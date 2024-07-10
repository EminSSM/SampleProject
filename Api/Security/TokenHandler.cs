using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Api.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public static class TokenHandler
{
	public static Token CreateToken(IConfiguration configuration, string email)
	{
		Token token = new Token();

		string secretKey = configuration["Jwt:SecretKey"];
		if (string.IsNullOrEmpty(secretKey))
		{
			throw new InvalidOperationException("JWT SecretKey is missing or empty.");
		}

		SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
		SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		token.Expiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["Jwt:ExpirationInMinutes"]));

		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, email),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

		JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
			issuer: configuration["Jwt:Issuer"],
			audience: configuration["Jwt:Audience"],
			claims: claims,
			expires: token.Expiration,
			signingCredentials: credentials);

		JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
		token.AccessToken = handler.WriteToken(jwtSecurityToken);

		byte[] refreshTokenBytes = new byte[32];
		using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
		{
			rng.GetBytes(refreshTokenBytes);
		}
		token.RefreshToken = Convert.ToBase64String(refreshTokenBytes);

		return token;
	}
}