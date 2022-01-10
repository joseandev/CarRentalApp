using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public static class Utils
    {

        public static bool IsFormOpen(string form)
        {
            var OpenForms = Application.OpenForms.Cast<Form>();
            var isOpen = OpenForms.Any(q => q.Name == form);
            return isOpen;
        }

        public static string DefaultHashedPassword()
        {
            //Creating an cryptography object
            SHA256 sha = SHA256.Create();

            //Convert the input string to a byte array and compute the hash.
            byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes("Password123"));

            // Create a new Stringbuilder to collect the bytes
            // and create a string
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            var hashed_password = sBuilder.ToString();

            return hashed_password;
        }

        public static string HashPassword(string password)
        {
            //Creating an cryptography object
            SHA256 sha = SHA256.Create();

            //Convert the input string to a byte array and compute the hash.
            byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Create a new Stringbuilder to collect the bytes
            // and create a string
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            var hashed_password = sBuilder.ToString();

            return hashed_password;
        }
    }
}
