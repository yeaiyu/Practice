using Microsoft.AspNetCore.SignalR.Client;

internal class Program
{
    private static void Main(string[] args)
    {
        Run();
    }

    private static async void Run()
    {
        HubConnection con = new HubConnectionBuilder()
            .WithUrl("https://localhost:62814/talks")
            //.WithAutomaticReconnect()
            .Build();

        con.Closed += async error =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await con.StartAsync();
        };

        con.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Console.WriteLine($"{user}->{message}");
        });

        try
        {
            await con.StartAsync();

            await con.InvokeAsync("SendMessage", "管理员", "hello 管理员。");
        }
        catch (Exception ex)
        {
        }
        finally
        {

        }

        Console.ReadKey();
    }
}