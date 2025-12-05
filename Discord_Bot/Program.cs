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
using Microsoft.Extensions.Hosting;


//---------------------------------------------------------------------------

class Program
{
    //Global 
    //Websocket & Config
    static public DiscordSocketClient? _client;
    static public DiscordSocketConfig? _config;
    static public InteractionService? _interactions = new InteractionService(_client);
    static public GeneralFunctions.Configreader ConfigReader = new GeneralFunctions.Configreader(Logger);

    //Database

    //Docker Input - Bot
    //Discord Project - Variables
    private ulong guildId;
    

//---------------------------------------------------------------------------    
// MAIN
//---------------------------------------------------------------------------

    // Main Entry Point
    static async Task Main(string[] args)
    {
        var program = new Program();
        // Create Host
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                //regstir Services
                 var startup = new Startup();
                startup.ConfigureServices(services);
            })
            .Build();
        
            _ = Task.Run( () => program.taskClientAsync(host.Services));
    
    await host.RunAsync();
    }


//---------------------------------------------------------------------------
//WebSocket Task
//---------------------------------------------------------------------------

    public async Task taskClientAsync(IServiceProvider services)
    {

        _config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.Guilds // Minimum für Slash Commands
        };

        // Main start if Bot client , call of Websocket
        _client = new DiscordSocketClient(_config);
        _client.Guilds.FirstOrDefault();
       
        await _client.LoginAsync(TokenType.Bot, ConfigReader.__GetString);
        await _client.StartAsync();

        //Add Logger
        _client.Log += Logger;

        //Add Interactionhandler
        _client.InteractionCreated += InteractionHandler;

        //Add Messager
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

//---------------------------------------------------------------------------
// Log Message
//---------------------------------------------------------------------------
    private static Task Logger(LogMessage log)
    {
        Console.WriteLine(log.ToString());
        return Task.CompletedTask;
    }

//---------------------------------------------------------------------------
// Event Handler für empfangene Nachrichten
//---------------------------------------------------------------------------
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
