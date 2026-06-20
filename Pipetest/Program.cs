using System.IO.Pipes;

var pipe = new NamedPipeClientStream(".", "MyTestPipe", PipeDirection.InOut);
Console.WriteLine("Клиент: пытаюсь подключиться...");
pipe.Connect();
Console.WriteLine("Клиент: подключён.\n");

var writer = new StreamWriter(pipe) { AutoFlush = true };
var reader = new StreamReader(pipe);

try
{
    for (int i = 1; i <= 5; i++)
    {
        string message = $"Сообщение #{i}";
        writer.WriteLine(message);
        Console.WriteLine($"Клиент: отправлено -> {message}");

        string? response = reader.ReadLine();
        Console.WriteLine($"Клиент: получен ответ -> {response}");
    }
}
finally
{
    // Аккуратное закрытие даже если канал уже мёртв
    try { writer.Dispose(); } catch { }
    try { reader.Dispose(); } catch { }
    try { pipe.Dispose(); } catch { }
}

Console.WriteLine("\nКлиент: работа завершена. Нажмите любую клавишу...");
Console.ReadKey();