namespace ElGamalAlgorithm;

class Program
{
    static void Main()
    {
        // Ініціалізація параметрів Ель-Гамаля
        int p = 23; // просте число
        int g = 5;  // примітивний корінь modulo p

        // Абонент A генерує свій закритий та відкритий ключі
        int privateKeyA = 6;
        int publicKeyA = (int)Math.Pow(g, privateKeyA) % p;

        // Абонент B генерує свої ключі
        int privateKeyB = 15;
        int publicKeyB = (int)Math.Pow(g, privateKeyB) % p;

        // Повідомлення для підпису та шифрування
        string message = "Hello, ElGamal!";

        // Гешування повідомлення
        string hashedMessage = ElGamal.HashMessage(message);

        // Розбиття геш-значення на блоки
        int blockSize = 2;
        string[] blocks = ElGamal.SplitBlocks(hashedMessage, blockSize);

        // Абонент A створює цифровий підпис
        string digitalSignature = ElGamal.CreateDigitalSignature(blocks, privateKeyA, p, g);

        // Абонент B перевіряє підпис та розшифровує повідомлення
        bool signatureValid = ElGamal.VerifyDigitalSignature(blocks, digitalSignature, publicKeyA, p, g);
        string decryptedMessage = ElGamal.DecryptMessage(blocks, digitalSignature, privateKeyB, p);

        // Вивід результатів
        Console.WriteLine("Digital Signature is Valid: " + signatureValid);
        Console.WriteLine("Decrypted Message: " + decryptedMessage);
    }
}