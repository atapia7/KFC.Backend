using System.Security.Cryptography;
using System.Text;

namespace KFC.UseCases.Utility;

public static class PasswordHasher
{
    // Método para generar un hash de contraseña utilizando SHA256
    public static string HashPassword(string password)
    {
        // Convertir la contraseña en bytes
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        // Crear un objeto de tipo SHA256 para generar el hash
        using (SHA256 sha256 = SHA256.Create())
        {
            // Calcular el hash de la contraseña
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);

            // Convertir el hash en una cadena hexadecimal
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                stringBuilder.Append(hashBytes[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }

    // Método para verificar si una contraseña coincide con un hash dado
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        // Generar un hash utilizando la contraseña proporcionada
        string hashedInput = HashPassword(password);

        // Comparar el hash generado con el hash almacenado
        return string.Equals(hashedInput, hashedPassword, StringComparison.OrdinalIgnoreCase);
    }
}