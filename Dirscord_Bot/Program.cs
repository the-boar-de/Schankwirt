using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

//Own 
using Dirscord_Bot.Class;

class Program
{

    //Global 
    //Websocket

    private DiscordSocketClient gMainClient;
    private CL_Websocket gclWebsocket;

    //Config 
    private CL_ConfigReader gclConfigReader;
    string gsToken = "MTM5MjkxODg3MTQ3NjAxNTMyNw.GbFEA3.4GTqX5D2QqV8yc9h-DW3LjplY6GFrUV71zIKC8"; // Niemals veröffentlichen!

    //Commands & Events
    private CL_Commands gclCommands;


    static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

    // MainAsync Methode, die den Bot startet
    public async Task MainAsync()

    {

        // Main start if Bot client , call of Websocket
        gMainClient = new DiscordSocketClient();

        gclWebsocket = new CL_Websocket(gMainClient);

        gMainClient.Log += Log;
        gMainClient.MessageReceived += MessageReceivedAsync;

        gclConfigReader = new CL_ConfigReader("config.json",
                                                "/appdata/");

        await gclWebsocket.M_WebsocketAsync(gsToken);

    }

    // Log Message
    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
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
