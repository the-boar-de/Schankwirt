//Own 

using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Enum_Config;
using Microsoft.VisualBasic;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;



class Program
{

    //Global 
    //Websocket & Config

    DiscordSocketClient _client;
    GeneralFunctions.Configreader ConfigReader = new GeneralFunctions.Configreader("C:\\Projekte\\Discord_Bot\\test_branch\\Discord_Bot\\Discord_Bot\\Config\\",
                                                                                                    "config.json", 
                                                                                                    Logger);

    private InteractionService _interactions ;
    private ulong guildId;
    
    
    // MainAsync Methode, die den Bot startet
    static void Main(string[] args) => new Program().taskClientAsync().GetAwaiter().GetResult();
    //WebSocket Task
    public async Task taskClientAsync()
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.Guilds // Minimum für Slash Commands
        };

        var client = new DiscordSocketClient(config);

        // Main start if Bot client , call of Websocket
        if (ConfigReader.__GetConfigList != null)
        {

            _client = new DiscordSocketClient(config);
            _client.Guilds.FirstOrDefault();


            _client.InteractionCreated += InteractionHandler;
            await _client.LoginAsync(TokenType.Bot, ConfigReader.__GetConfigList[(int)E_Config.eToken]);
            await _client.StartAsync();
            _client.Log += Logger;
            _interactions= new InteractionService(_client);
            _client.MessageReceived += taskMessagerAsync;

            //When bot is ready check the guilds 
            _client.Ready += async () =>
                {
                    foreach (var guild in _client.Guilds)
                    {
                        guildId = guild.Id;
                        var log = new LogMessage(LogSeverity.Info, "Bot", $"Bot ist auf: {guild.Name} (ID: {guild.Id})");
                        await Logger(log);
                        await _interactions.RegisterCommandsToGuildAsync(guildId);
                    }
                };
            // Commands zur InteractionService hinzufügen
            await _interactions.AddModulesAsync(
                assembly: System.Reflection.Assembly.GetEntryAssembly(),
                services: null
            );
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

    // InteractionHandler führt Commands AUS
    private async Task InteractionHandler(SocketInteraction interaction)
    {
        var context = new SocketInteractionContext(_client, interaction);
        await _interactions.ExecuteCommandAsync(context, null);
    }



}
