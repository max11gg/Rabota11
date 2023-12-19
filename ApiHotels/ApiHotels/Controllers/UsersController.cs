﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiHotels.Models;
using System.Security.Cryptography;
using System.Text;

namespace ApiHotels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HotelContext _context;

        public UsersController(HotelContext context)
        {
            _context = context;
        }

        public static byte[] GenerateSalt(int length)
        {
            byte[] salt = new byte[length];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            byte[] Salt = GenerateSalt(20);
            user.Salt = Convert.ToBase64String(Salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(user.PasswordUser);
            byte[] hashedBytes = new Rfc2898DeriveBytes(passwordBytes, Salt, 10000).GetBytes(32);
            user.PasswordUser = Convert.ToBase64String(hashedBytes);
            if (_context.Users == null)
          {
              return Problem("Entity set 'HotelContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // AUTH: api/Users/5
        [HttpGet("{LoginUser}/{Password}")]
        public async Task<ActionResult<string>> Authorization(string LoginUser, string Password)
        {
            var Users = await _context.Users.Where(u => u.LoginUser == LoginUser).ToListAsync();

            if (Users.Count == 0)
            {
                // пользователь не найден
                return NotFound();
            }
            else if (Users.Count > 1)
            {
                // обнаружено несколько пользователей с таким именем
                return BadRequest("Multiple usernames detected");
            }

            var user = Users[0];
            // преобразовываем строку Salt в массив байтов
            byte[] saltBytes = Convert.FromBase64String(user.Salt);

            // преобразовываем строку Password в массив байтов
            byte[] passwordBytes = Encoding.UTF8.GetBytes(Password);

            // вычисляем хеш пароля с помощью соли и 10000 итераций
            byte[] hashBytes = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 10000).GetBytes(32);
            string hashedPassword = Convert.ToBase64String(hashBytes);

            if (hashedPassword == user.PasswordUser)
            {
                // пароль совпадает, генерируем случайный токен и добавляем его в базу данных
                string token;
                Token existingToken;

                do
                {
                    token = Guid.NewGuid().ToString();
                    existingToken = await _context.Tokens.FirstOrDefaultAsync(t => t.Token1 == token);
                }
                while (existingToken != null);

                // создаем новую запись Token и сохраняем ее в базу данных
                // создаем новую запись Token и сохраняем ее в базу данных
                Token tok = new Token();
                tok.Token1 = token;
                tok.TokenDatetime = DateTime.Now;
                _context.Tokens.Add(tok);
                await _context.SaveChangesAsync();

                return token;
            }
            else
            {
                // пароль не совпадает
                return BadRequest("Неправильный пароль");
            }
        }

        [HttpPut("Password_Change")]
        public async Task<IActionResult> PutUser(int id, string New_password)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            // хешируем новый пароль
            byte[] Salt = GenerateSalt(20);
            user.Salt = Convert.ToBase64String(Salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(New_password);
            byte[] hashBytes = new Rfc2898DeriveBytes(passwordBytes, Salt, 10000).GetBytes(32);
            user.PasswordUser = Convert.ToBase64String(hashBytes);

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // GET: api/Users
        [HttpGet("auth_key")]
        public async Task<ActionResult<string>> GetAuthKey(string LoginUser)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.LoginUser == LoginUser);
            if (user == null)
            {
                return NotFound($"Admin with login '{LoginUser}' was not found.");
            }
            else if (_context.Users.Count(u => u.LoginUser == LoginUser) > 1)
            {
                return BadRequest($"Multiple admins with login '{LoginUser}' were found.");
            }

            string salt = user.Salt;
            if (string.IsNullOrEmpty(salt))
            {
                return BadRequest($"Salt for user with login '{LoginUser}' is missing or empty.");
            }

            byte[] saltBytes = Encoding.UTF8.GetBytes(salt.Substring(0, Math.Min(salt.Length, 5)));
            byte[] reverseSalt = saltBytes.Reverse().ToArray();
            string hashedReverse = Convert.ToBase64String(reverseSalt);

            return hashedReverse;
        }


        // GET: api/Users
        [HttpGet("authentication")]
        public async Task<ActionResult<string>> GetAuthentication(string LoginUser, string AuthKey)
        {
            // Retrieve the user's password salt from the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.LoginUser == LoginUser);
            if (user == null)
            {
                return BadRequest("Invalid LoginUser");
            }
            var salt = user.Salt;

            // Compute the AuthKey from the password salt
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt.Substring(0, Math.Min(salt.Length, 5)));
            byte[] reverseSalt = saltBytes.Reverse().ToArray();
            string hashedReverse = Convert.ToBase64String(reverseSalt);

            // Check if the computed AuthKey matches the provided AuthKey
            if (hashedReverse != AuthKey)
            {
                return BadRequest("Invalid AuthKey");
            }

            // Generate a random token and add it to the database
            string token;
            Token existingToken;

            do
            {
                token = Guid.NewGuid().ToString();
                existingToken = await _context.Tokens.FirstOrDefaultAsync(t => t.Token1 == token);
            }
            while (existingToken != null);

            Token tok = new Token();
            tok.Token1 = token;
            tok.TokenDatetime = DateTime.Now;
            _context.Tokens.Add(tok);
            await _context.SaveChangesAsync();

            return token;


        }


        [HttpGet("GetRoleIdByLogin")]
        public async Task<ActionResult<int>> GetRoleIdByLogin(string login)
        {
            var employee = await _context.Users
                .Where(e => e.LoginUser == login)
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound(); // Или другой статус код, который соответствует ситуации
            }

            return employee.RoleId;
        }

        [HttpGet("GetIdByLogin")]
        public async Task<ActionResult<int>> GetIdByLogin(string login)
        {
            var employee = await _context.Users
                .Where(e => e.LoginUser == login)
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound(); // Или другой статус код, который соответствует ситуации
            }

            return employee.IdUser;
        }
        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.IdUser == id)).GetValueOrDefault();
        }
    }
}
