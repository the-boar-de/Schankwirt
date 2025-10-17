//Own 

using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Enum_Config;
using Discord.Commands.Builders;

class Program
{

    //Global 
    //Websocket & Config

    private DiscordSocketClient gclClient = new DiscordSocketClient();
    private CL_ConfigReader.CL_ConfigReader gclConfigreader = new CL_ConfigReader.CL_ConfigReader("C:\\Projekte\\Discord_Bot\\test_branch\\Discord_Bot\\Discord_Bot\\Config\\",
                                                                                                    "config.json");
    // MainAsync Methode, die den Bot startet
    static void Main(string[] args) => new Program().taskClientAsync().GetAwaiter().GetResult();
    //WebSocket Task
    public async Task taskClientAsync()
    {
 
        //Client call

        // Main start if Bot client , call of Websocket
        if (gclConfigreader.__GetConfigList != null)
        {  
            this.gclClient.Log += taskLoggerAsync;

            await this.gclClient.LoginAsync(TokenType.Bot, gclConfigreader.__GetConfigList[(int)E_Config.eToken]);

            await this.gclClient.StartAsync();

            this.gclClient.Log += taskLoggerAsync;
            this.gclClient.MessageReceived += taskMessagerAsync;

            await Task.Delay(-1);

        }

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

    
    //Commands & Events 
    private async Task taskCommandAsny()
     {
         var guild = client.GetGuild(guildId);
    //

        

     }
}
