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
using Discord.Interactions.Builders;

class Program
{

    //Global 
    //Websocket & Config

    DiscordSocketClient _client;
    GeneralFunctions.CL_ConfigReader ConfigReader = new GeneralFunctions.CL_ConfigReader("C:\\Projekte\\Discord_Bot\\test_branch\\Discord_Bot\\Discord_Bot\\Config\\",
                                                                                                    "config.json");
    
    
    // MainAsync Methode, die den Bot startet
    static void Main(string[] args) => new Program().taskClientAsync().GetAwaiter().GetResult();
    //WebSocket Task
    public async Task taskClientAsync()
    {
 
        //Client call
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        };


        // Main start if Bot client , call of Websocket
        if (ConfigReader.__GetConfigList != null)
        {
            _client = new DiscordSocketClient(config);

            _client.Guilds.FirstOrDefault();

            _client.SlashCommandExecuted += Commandhandler;

           
            await _client.LoginAsync(TokenType.Bot, ConfigReader.__GetConfigList[(int)E_Config.eToken]);

            await _client.StartAsync();

            _client.Log += taskLoggerAsync;
            _client.MessageReceived += taskMessagerAsync;


            _client.Ready += async () =>
                {
                    foreach (var guild in  _client.Guilds)
                    {
                        Logger($"Bot ist auf: {guild.Name} (ID: {guild.Id})");
                    }

                };
            _client.lo

            await Task.Delay(-1);
        }

    }

    //---------------------------------------------------------------------------
    // Log Message
    private static Task Logger(LogMessage log)
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

    public async Task Commandhandler(SocketSlashCommand command)
    {
        //_client.BulkOverwriteGlobalApplicationCommandsAsync;
        //_client.Ready += clien_Ready;1392436922760302732
        await command.RespondAsync($"You executed {command.Data.Name}");


    }


    //Commands & Events 
    //[SlashCommand("test")]
    public async Task testatsk()
     {


     }
}
