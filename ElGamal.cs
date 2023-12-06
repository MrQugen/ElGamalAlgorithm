using System.Security.Cryptography;
using System.Text;

namespace ElGamalAlgorithm;

public class ElGamal
{

    // Гешування повідомлення за допомогою SHA256
    public static string HashMessage(string message)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(message));
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }
    }

    // Розбиття геш-значення на блоки
    public static string[] SplitBlocks(string hashedMessage, int blockSize)
    {
        return Enumerable.Range(0, hashedMessage.Length / blockSize)
            .Select(i => hashedMessage.Substring(i * blockSize, blockSize))
            .ToArray();
    }

    // Створення цифрового підпису
    public static string CreateDigitalSignature(string[] blocks, int privateKey, int p, int g)
    {
        // Обчислення r та s для кожного блоку
        StringBuilder digitalSignature = new StringBuilder();
        foreach (var block in blocks)
        {
            int k = 3; // довільне число, може бути генероване випадково
            int r = (int)Math.Pow(g, k) % p;
            int hashedBlock = int.Parse(block, System.Globalization.NumberStyles.HexNumber);
            int s = (hashedBlock - privateKey * r) * ModInverse(k, p - 1) % (p - 1);

            // Додавання r та s до цифрового підпису
            digitalSignature.Append($"{r:X2}{s:X2}");
        }

        return digitalSignature.ToString();
    }

    // Перевірка цифрового підпису
    public static bool VerifyDigitalSignature(string[] blocks, string digitalSignature, int publicKey, int p, int g)
    {
        int blockSize = digitalSignature.Length / (2 * blocks.Length);

        // Перевірка для кожного блоку
        for (int i = 0; i < blocks.Length; i++)
        {
            string block = blocks[i];
            string rs = digitalSignature.Substring(i * blockSize, blockSize * 2);
            int r = int.Parse(rs.Substring(0, blockSize), System.Globalization.NumberStyles.HexNumber);
            int s = int.Parse(rs.Substring(blockSize), System.Globalization.NumberStyles.HexNumber);

            int hashedBlock = int.Parse(block, System.Globalization.NumberStyles.HexNumber);
            int left = (int)(Math.Pow(g, hashedBlock) % p * Math.Pow(publicKey, r) % p) % p;
            int right = (int)(Math.Pow(r, s) % p * Math.Pow(r, hashedBlock) % p) % p;

            if (left != right)
            {
                return false; // Підпис не валідний
            }
        }

        return true;
    }

    // Розшифрування повідомлення
    public static string DecryptMessage(string[] blocks, string digitalSignature, int privateKey, int p)
    {
        int blockSize = digitalSignature.Length / (2 * blocks.Length);

        StringBuilder decryptedMessage = new StringBuilder();
        for (int i = 0; i < blocks.Length; i++)
        {
            string rs = digitalSignature.Substring(i * blockSize, blockSize * 2);
            int r = int.Parse(rs.Substring(0, blockSize), System.Globalization.NumberStyles.HexNumber);
            int s = int.Parse(rs.Substring(blockSize), System.Globalization.NumberStyles.HexNumber);

            int inverseS = ModInverse(s, p - 1);
            int decryptedBlock = (r * inverseS) % (p - 1);

            decryptedMessage.Append(blocks[i]);
        }

        return decryptedMessage.ToString();
    }

    // Обчислення модульного оберненого числа (оберненого до a за модулем m)
    private static int ModInverse(int a, int m)
    {
        for (int x = 1; x < m; x++)
        {
            if ((a * x) % m == 1)
            {
                return x;
            }
        }
        return -1; // Не існує оберненого числа
    }
}