/*//standard system refernces
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
//own refernces
using Schankwirt.Database;
using Microsoft.Extensions.Logging;
//project refernces
using Discord;
using Discord.Interactions;
using Discord.Rest;
using Discord.WebSocket;

Description to Name a command
Commands start always with the Name "Command" + "What the command does" + "Module"
Example: CommandCreateRole

The Description string is the same name as the command but with a "Description" at the end
Example: CommandCreateRoleDescription


//Class
public class CommandModule : InteractionModuleBase<SocketInteractionContext>
{
    //Field
    private SocketGuild guild => Context.Guild as SocketGuild;
    //Public
    private readonly Schankwirt.Database.DataBaseLogs _mariadb_databaselogs;
    //constructor
    public CommandModule(Schankwirt.Database.DataBaseLogs databaselogs)
    {
        _mariadb_databaselogs = databaselogs;
    }
    public BaseCommand Botcommand;

//===============================================================================================
/*
    Channel Commands 



//===============================================================================================


//-----------------------------------------------------
//Update greeting cMesage
//-----------------------------------------------------
const string CommandUpdateGreeting = "update-greeting";
const string CommandUpdateGreetingDescription = "Update the greeting message";

    [SlashCommand(CommandUpdateGreeting,CommandUpdateGreetingDescription)]
    [RequireRole("BotAdmin")]  //Only Bot Admins can use this command
    public async Task TaskCommandUpdateGreeting(string welcomemassge)
    {
        await DeferAsync();
        
         _mariadb_databaselogs.Add( new WelcomeMessage
         {
            Id = 0,
            ChannelId = Context.Channel.Id,
            Message = welcomemassge,
            AdditionalInfo = $"User {Context.User.Username.ToString()} created Voice Channel updated Welcome Message",
            CreatedAt = DateTime.Now


        });
        await _mariadb_databaselogs.SaveChangesAsync();
    }


//===============================================================================================
/*
    Fallback Commands 



//===============================================================================================
//Initialize Bot Again
const string CommandInitializeBot = "initiliaze-bot";
const string CommandInitializeBotDescription = "initiliaze bot again";
    [SlashCommand(CommandInitializeBot, CommandInitializeBotDescription)]
    public async Task TaskInitialieBot()
        {
            var guildsetup = new GuildItems.Class.GuildSetup();
            await guildsetup.InitializeGuild(guild);
        }

}
*/