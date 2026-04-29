using System.Security.Cryptography;
using System.Text;

namespace ProyectoDeAprendizajeP3.Core.Application.Helpers
{
    public static class PasswordEncryptation
    {
        public static string Computesha256Hash(string password)
        {
            //Create a SHA256
            using SHA256 sha256hash = SHA256.Create();
            //ComputeHash
            byte[] bytes = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            //Convert byte array to a string
            StringBuilder sb = new();

            foreach (var item in bytes)
            {
                sb.Append(item.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
