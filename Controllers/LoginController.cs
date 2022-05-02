using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;

namespace wapp_workshop.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost(Name = "Login")]
    public IActionResult Post([FromBody] Login login)
    {
        if (login.username == "username" && login.password == "password")
        {
            return Ok("login success");
        }
    
        return Ok("login failed");
    }


    // [HttpPost(Name = "Login")]
    // public IActionResult Post([FromBody] Login login)
    // {
    //     // This would usually be in your database.
    //     var correctSalt = "salt";
    //     var correctPassword = "KL5SstCEoc3YHdhTme0O9oG0c4E7sJ5m12KmlTkBbx4="; // password
    //
    //     if (login.username == "username" && correctPassword == HashPassword(login.password, correctSalt))
    //     {
    //         return Ok("login success");
    //     }
    //
    //     return Ok("login failed");
    // }

    private string HashPassword(string password, string salt)
    {
        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Convert.FromBase64String(salt),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return hashed;
    }
}