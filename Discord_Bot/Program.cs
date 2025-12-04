//================================================================
//Description

//================================================================
//Own
using Enum_Config;
//System
using System;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Threading.Tasks;

//Projekt
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using DiscordBot.Database;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;


//---------------------------------------------------------------------------

class Program
{
    //Global 
    //Websocket & Config
    public DiscordSocketClient? _client;
    public  InteractionService? _interactions ;
    GeneralFunctions.Configreader ConfigReader = new GeneralFunctions.Configreader("config.json",Logger);

    //Database


    //Docker Input - Bot
    string token = Environment.GetEnvironmentVariable("TOKEN") ?? "empty";
    //Discord Project - Variables
    private ulong guildId;
    

//---------------------------------------------------------------------------    
// MAIN
//---------------------------------------------------------------------------

    // Main Entry Point
       static async Task Main(string[] args)
    {
        // Create Host
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                //regstriere Services
                 var startup = new Startup();
                startup.ConfigureServices(services);
            })
            .Build();
        
            _ = Task.Run(() => program.taskClientAsync(host.Services));
    
    await host.RunAsync();
    }


//---------------------------------------------------------------------------
//WebSocket Task
//---------------------------------------------------------------------------

    public async Task taskClientAsync(IServiceProvider services)
    {

        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.Guilds // Minimum für Slash Commands
        };


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
//---------------------------------------------------------------------------

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
//---------------------------------------------------------------------------
// InteractionHandler führt Commands AUS
//---------------------------------------------------------------------------

    private async Task InteractionHandler(SocketInteraction interaction)
    {
        var context = new SocketInteractionContext(_client, interaction);
        await _interactions.ExecuteCommandAsync(context, null);

        if (interaction is SocketMessageComponent component)
        {
            await HandleButtonAsync(component);
        }

    }
//---------------------------------------------------------------------------
//Button Handler
//---------------------------------------------------------------------------
    public async Task HandleButtonAsync(SocketMessageComponent socketMessageComponent)
    {
        if (socketMessageComponent.Data.CustomId == "test_button")
        {
            await socketMessageComponent.RespondAsync("Button clicked!");
        }
    }
}
