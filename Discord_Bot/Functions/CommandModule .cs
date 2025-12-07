//standard system refernces
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
//own refernces
using DiscordBot.Database;
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

    //-------------------------------------------------------------------

    //Commands & Events 
    [SlashCommand("test", "testcommand")]
    public async Task testatsk()
    {
        await DeferAsync();
        _mariadb_databaselogs.Add(new DiscordBot.Database.Logs
        {
            Id = 0,
            ChannelId = Context.Channel.Id,
            DiscordUserId = Context.User.Id,
            DiscordUserName = Context.User.Username.ToString(),
            CommandId = "tetestcommandst"
        });
       
        await _mariadb_databaselogs.SaveChangesAsync();
        await RespondAsync("Pong!");
        

    }

    //Button
    [SlashCommand("testbutton", "testcommand")]
    public async Task ButtonTest()
    {
        var button = new ButtonBuilder()
        .WithLabel("Test Button")
        .WithCustomId("test_button")
        .WithStyle(ButtonStyle.Primary);

        var component = new ComponentBuilder()
        .WithButton(button);

        
        await RespondAsync("Button works!", components: component.Build());
    }





}

