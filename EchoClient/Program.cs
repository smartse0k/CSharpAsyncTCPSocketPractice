using EchoClient;

Client client = new Client("127.0.0.1", 10000);

while (true)
{
    string? input = Console.ReadLine();

    if (input == null)
    {
        break;
    }

    byte[] data = System.Text.Encoding.Default.GetBytes(input);
    client.Send(data);
}