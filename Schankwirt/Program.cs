//================================================================
//Description

//================================================================
//Own
using GuildItems.Class;
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
using Microsoft.Extensions.Logging;


//---------------------------------------------------------------------------

class Program
{
    //Global 
    //Websocket & Config
    static public DiscordSocketClient? _client;
    static public DiscordSocketConfig? _config;
    static public InteractionService _interactions;
    static public GeneralFunctions.Configreader ConfigReader = new GeneralFunctions.Configreader(TaskLogger);

    //Guild Classes
    static public GuildSetup GuildSetup = new GuildSetup();
    static public GuildEvents GuildEvents = new GuildEvents();
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
    
        //Start Migration
        using (var scope = host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DataBaseLogs>();
            db.Database.Migrate();  //Execute Migrations
        }
    
         _ = Task.Run(() => program.taskClientAsync(host.Services));
    
        await host.RunAsync();
    }


//---------------------------------------------------------------------------
//WebSocket Task
//---------------------------------------------------------------------------

    public async Task taskClientAsync(IServiceProvider services)
    {

        //Discord Config
        _config = new DiscordSocketConfig
        {
            GatewayIntents =  GatewayIntents.Guilds 
                                | GatewayIntents.GuildMembers 
                                | GatewayIntents.GuildMessages 
                                | GatewayIntents.GuildMessageReactions
                                | GatewayIntents.GuildPresences
                                | GatewayIntents.GuildIntegrations
                                | GatewayIntents.GuildVoiceStates
        };
        

        // Main start if Bot client , call of Websocket
        _client = new DiscordSocketClient(_config);
        _interactions = new InteractionService(_client);

        // Commands zur InteractionService hinzufügen
        await _interactions.AddModulesAsync(
            assembly: System.Reflection.Assembly.GetEntryAssembly(),
            services: services
        );

        _client.Guilds.FirstOrDefault();
       
        //Add Logger
        _client.Log += TaskLogger;

        //Add Interactionhandler
        _client.InteractionCreated += async (interaction) => await InteractionHandler(interaction, services);

        //Add Joined Guild Event
        _client.JoinedGuild += TaskOnJoinedGuilds;

         //When bot is ready check the guilds 
        _client.Ready += async () =>
            {
                foreach (var guild in _client.Guilds)
                {
                    guildId = guild.Id;
                    var log = new LogMessage(LogSeverity.Info, "Bot", $"Bot is now on: {guild.Name} (ID: {guild.Id})");

                    
                    await TaskLogger(log);
                    await _interactions.RegisterCommandsToGuildAsync(guild.Id);
                    await GuildSetup.InitializeGuild(guild, TaskLogger);
                    Console.WriteLine($"Setup für '{guild.Name}' abgeschlossen");
                }
            };

        //Login and start bot
        await _client.LoginAsync(TokenType.Bot, ConfigReader.__GetString);
        await _client.StartAsync();
        await Task.Delay(-1);

    }
//===========================================================================
//Task Methods
//===========================================================================

//---------------------------------------------------------------------------
//Task On Joined Guild
//---------------------------------------------------------------------------
private async Task TaskOnJoinedGuilds(SocketGuild guild)
{
    //var guildSetup = new GuildSetup();
    await GuildSetup.InitializeGuild(guild,TaskLogger);

}

//---------------------------------------------------------------------------
// Task Log Message for Client Actions
//---------------------------------------------------------------------------
    private static Task TaskLogger(LogMessage log)
    {
        Console.WriteLine(log.ToString());
        return Task.CompletedTask;
    }
//---------------------------------------------------------------------------
// Task InteractionHandler führt Commands AUS
//---------------------------------------------------------------------------
    private async Task InteractionHandler(SocketInteraction interaction, IServiceProvider services)
    {
        var context = new SocketInteractionContext(_client, interaction);
        await _interactions.ExecuteCommandAsync(context, services);

        if (interaction is SocketMessageComponent component)
        {
            await HandleButtonAsync(component);
        }

    }
//---------------------------------------------------------------------------
// Task Button Handler
//---------------------------------------------------------------------------
    public async Task HandleButtonAsync(SocketMessageComponent socketMessageComponent)
    {
        if (socketMessageComponent.Data.CustomId == "test_button")
        {
            await socketMessageComponent.RespondAsync("Button clicked!");
        }
    }
}
