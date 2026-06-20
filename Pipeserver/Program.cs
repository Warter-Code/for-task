using System.IO.Pipes;

var server = new NamedPipeServerStream("MyTestPipe", PipeDirection.InOut);
Console.WriteLine("Сервер: запущен, жду клиента...");
server.WaitForConnection();
Console.WriteLine("Сервер: клиент подключился.\n");

using var reader = new StreamReader(server);
using var writer = new StreamWriter(server) { AutoFlush = true };

for (int i = 0; i < 5; i++)
{
    string? msg = reader.ReadLine();
    Console.WriteLine($"Сервер: получил -> {msg}");
    writer.WriteLine($"Сервер получил: {msg}");
}

Console.WriteLine("\nСервер: все сообщения обработаны. Нажмите любую клавишу для выхода...");
Console.ReadKey();   // <-- вот это важно! Держит канал открытым, пока клиент не закончит