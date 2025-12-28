//standard system refernces
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.Rest;
using Discord.WebSocket;
//own refernces
using DiscordBot.Database;
using Microsoft.Extensions.Logging;
//project refernces

//Class
public class CommandModule : InteractionModuleBase<SocketInteractionContext>
{
    //Field
    //Public
    public readonly DiscordBot.Database.DataBaseLogs _mariadb_databaselogs;
    //constructor
    public CommandModule(DiscordBot.Database.DataBaseLogs databaselogs)
    {
        _mariadb_databaselogs = databaselogs;
    }
//-----------------------------------------------------------------------------------------------
/*
    Single Use commands


*/
//-----------------------------------------------------------------------------------------------

    const string description = "testslashcommand";
    const string commandname = "test";
    [SlashCommand(commandname, description)]
    public async Task testatsk()
    {
        await RespondAsync("Pong!");
        await DeferAsync();
        _mariadb_databaselogs.Add(new Logs
        {
            Id = 0,
            ChannelId = Context.Channel.Id,
            DiscordUserId = Context.User.Id,
            DiscordUserName = Context.User.Username.ToString(),
            CommandId = commandname
        });
       
        await _mariadb_databaselogs.SaveChangesAsync();
        
        

    }
//-----------------------------------------------------------------------------------------------
/*
    Button Commands 


*/
//-----------------------------------------------------------------------------------------------
    const string ButtonWithLabelAndID = "ButtonWithLabelAndID";
    const string ButtonWithLabelAndIDdescription = "Command to create a button with and ID";
    [SlashCommand(ButtonWithLabelAndID, ButtonWithLabelAndIDdescription)]
    public async Task ButtonTest()
    {
        var button = new Button().ButtonWithLabelAndID("Test Button","test_button",1,ButtonStyle.Primary);

        var component = new ComponentBuilder()
        .WithButton(button);

        await RespondAsync("Button works!", components: component.Build());
    }





}

