> [!WARNING]  
> ПІД ЧАС НАПИСАННЯ КОДУ ВИКОРИСТОВУВАВСЯ CHATGPT

# ElGamal Algorithm Implementation (Реалізація алгоритму Ель-Гамаля)

Цей код реалізує алгоритм Ель-Гамаля для створення цифрового підпису, перевірки підпису, шифрування та розшифрування повідомлення.

## Використання

1. Запустіть програму, щоб перевірити цифровий підпис та розшифрувати повідомлення.
2. Вивід у консолі покаже, чи є цифровий підпис валідним та розшифроване повідомлення.

## Структура проекту

- **ElGamal.cs:** Основний клас, що містить весь код алгоритму Ель-Гамаля.
- **Program.cs:** Цей файл, який містить основний метод для запуску програми.

## Методи

### HashMessage

```csharp
string HashMessage(string message)
```

Функція для гешування повідомлення за допомогою SHA256.

### SplitBlocks

```csharp
string[] SplitBlocks(string hashedMessage, int blockSize)
```

Функція для розбиття геш-значення на блоки заданого розміру.

### CreateDigitalSignature

```csharp
string CreateDigitalSignature(string[] blocks, int privateKey, int p, int g)
```

Створення цифрового підпису для заданих блоків повідомлення та ключа.

### VerifyDigitalSignature

```csharp
bool VerifyDigitalSignature(string[] blocks, string digitalSignature, int publicKey, int p, int g)
```

Перевірка валідності цифрового підпису для заданих блоків повідомлення та публічного ключа.

### DecryptMessage

```csharp
string DecryptMessage(string[] blocks, string digitalSignature, int privateKey, int p)
```

Розшифрування повідомлення за допомогою цифрового підпису та приватного ключа.

### ModInverse

```csharp
int ModInverse(int a, int m)
```

Обчислення модульного оберненого числа для заданих `a` та `m`.
