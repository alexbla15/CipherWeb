using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace CipherData.General
{
    public class Security
    {
        private static readonly string SecretKey = string.Empty;

        // Entity (typically your application) that issued the token
        private static readonly string Issuer = "Cipher";

        // Intended recipient of the token, which could be a specific application or user base
        private static readonly string Audience = "CipherUsers";

        // A list of Claim objects that hold additional data about the user (like username or role).
        private static readonly List<Claim> Claims = new();

        // MUST BE IMPLEMENTED CORRECTLY!!!
        public async Task<bool> AuthenticateUser(string username, string password) => await Task.FromResult(true);

        // MUST BE IMPLEMENTED CORRECTLY!!!
        public async Task<bool> LoginAsync(string username, string password)
        {
            // Perform the authentication (call to authentication API, database, etc.)
            bool isAuthenticated = await AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                // Generate or retrieve the JWT token upon successful login
                string token = GenerateJwtToken(); // Assume this method gets or generates the token
                GeneralAPIRequest.SetJwtToken(token); // Set token for future requests

                return true;
            }

            return false;
        }

        private static string GenerateJwtToken()
        {
            // Defining the Secret Key
            SymmetricSecurityKey key = new (Encoding.UTF8.GetBytes(SecretKey));
            // Setting the Signing Credentials
            // initialized with the key and specifies the hashing algorithm (HmacSha256)
            // used to sign the token
            SigningCredentials creds = new (key, SecurityAlgorithms.HmacSha256);

            // Creating the JWT Token
            JwtSecurityToken token = new(
                issuer: Issuer,
                audience: Audience,
                claims: Claims,
                expires: DateTime.Now.AddMinutes(30), // expiration time for this token
                signingCredentials: creds);

            //  Serializes the JwtSecurityToken object into a compact, URL-safe string format,
            //  which is the standard JWT string.
            // The resulting token is a string that can be included in an HTTP header
            // (usually in an Authorization header with the Bearer prefix) to authenticate users
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
