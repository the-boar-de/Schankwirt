//Own 

using Discord;
using Discord.WebSocket;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

class Program
{

    //Global 
    //Websocket

    private DiscordSocketClient gclClient;


    //Config 

    string gsToken = "MTM5MjkxODg3MTQ3NjAxNTMyNw.GbFEA3.4GTqX5D2QqV8yc9h-DW3LjplY6GFrUV71zIKC8"; // Niemals veröffentlichen!

    //Commands & Events
    //private CL_Commands gclCommands;


    static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

    // MainAsync Methode, die den Bot startet
    public async Task MainAsync()

    {
        //Client call

        // Main start if Bot client , call of Websocket
        gclClient = new DiscordSocketClient();


        gclClient.Log += t_LogAsync;


       
        await gclClient.LoginAsync(TokenType.Bot, gsToken);
        await gclClient.StartAsync();

      


        gclClient.Log += t_LogAsync;
        gclClient.MessageReceived += MessageReceivedAsync;

        await Task.Delay(-1);
    }
    //---------------------------------------------------------------------------
    // Log Message
    private Task t_LogAsync(LogMessage log)
    {
        Console.WriteLine(log.ToString());
        return Task.CompletedTask;
    }

    // Event Handler für empfangene Nachrichten
    private async Task MessageReceivedAsync(SocketMessage message)
    {
        if (message.Author.IsBot) return;

        if (message.Content == "!ping")
        {
            await message.Channel.SendMessageAsync("Pong!");

        }

    }



}
