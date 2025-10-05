//Own 

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Enum_Config;

class Program
{

    //Global 
    //Websocket

    private DiscordSocketClient gclClient;


    //Config 
    private CL_ConfigReader.CL_ConfigReader clConfigreader;
    string gsToken = "MTM5MjkxODg3MTQ3NjAxNTMyNw.GbFEA3.4GTqX5D2QqV8yc9h-DW3LjplY6GFrUV71zIKC8"; // Niemals veröffentlichen!

    //Commands & Events
    //private CL_Commands gclCommands;


    static void Main(string[] args) => new Program().taskClientAsync().GetAwaiter().GetResult();

    // MainAsync Methode, die den Bot startet
    public async Task taskClientAsync()

    {
        clConfigreader = new CL_ConfigReader.CL_ConfigReader("C:\\Projekte\\Discord_Bot\\test_branch\\Discord_Bot\\Discord_Bot\\Config",
                                                                    "config.json");

        gsToken = clConfigreader.__Get[(int)E_Config.eToken];

        //Client call

        // Main start if Bot client , call of Websocket
        gclClient = new DiscordSocketClient();


        gclClient.Log += taskLoggerAsync;

        await gclClient.LoginAsync(TokenType.Bot, gsToken);
        await gclClient.StartAsync();

        gclClient.Log += taskLoggerAsync;
        gclClient.MessageReceived += taskMessagerAsync;

        await Task.Delay(-1);
    }
    //---------------------------------------------------------------------------
    // Log Message
    private Task taskLoggerAsync(LogMessage log)
    {
        Console.WriteLine(log.ToString());
        return Task.CompletedTask;
    }

    // Event Handler für empfangene Nachrichten
    private async Task taskMessagerAsync(SocketMessage message)
    {
        if (message.Author.IsBot) return;

        if (message.Content == "!ping")
        {
            await message.Channel.SendMessageAsync("Pong!");

        }

    }


    /*private async Task taskCommandAsny()
     {



     }*/
}
