//standard system refernces
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
/*
Description to Name a command
Commands start always with the Name "Command" + "What the command does" + "Module"
Example: CommandCreateRole

The Description string is the same name as the command but with a "Description" at the end
Example: CommandCreateRoleDescription
*/

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
//===============================================================================================
/*
    Single Use commands


*/
//===============================================================================================

    const string CommandSendMessage = "test";
    const string CommandSendMessageDescription = "testslashcommand";
    [SlashCommand(CommandSendMessage, CommandSendMessageDescription)]
    [RequireRole("BotAdmin")] //Only Bot Admins can use this command
    public async Task TaskCommandSendMessage()
    {
        
        await DeferAsync();
        _mariadb_databaselogs.Add(new Logs
        {
            Id = 0,
            ChannelId = Context.Channel.Id,
            DiscordUserId = Context.User.Id,
            DiscordUserName = Context.User.Username.ToString(),
            CommandId = CommandSendMessage,
            AdditionalInfo = "Test",
            CreatedAt = DateTime.Now
        });
       
        await _mariadb_databaselogs.SaveChangesAsync();
        
        await FollowupAsync("Pong!");

    }
//===============================================================================================
/*
    Button Commands 


*/
//===============================================================================================
    const string CommandCreateButtonWithLabelAndID = "create-button-with-label-and-id";
    const string CommandCreateButtonWithLabelAndIDdescription = "Command to create a button";
    [SlashCommand(CommandCreateButtonWithLabelAndID, CommandCreateButtonWithLabelAndIDdescription)]
    [RequireRole("BotAdmin")]  //Only Bot Admins can use this command
    public async Task TaskCommandCreateButtonWithLabelAndID()
    {
        var button = new Button().ButtonWithLabelAndID("Test Button","test_button",1,ButtonStyle.Primary);

        var component = new ComponentBuilder()
        .WithButton(button);

        await RespondAsync("Button works!", components: component.Build());
    }
//===============================================================================================
/*
    Role Commands 


*/
//===============================================================================================
// Create Role

    const string CommandCreateRole = "create-role";
    const string CommandCreateRoleDescription = "Command to create a role";
    [SlashCommand(CommandCreateRole, CommandCreateRoleDescription)]
    [RequireRole("BotAdmin")]  //Only Bot Admins can use this command
    public async Task TaskCommandCreateRole(string rolename)
    {
        await DeferAsync();
        //Create a new role in the current guild
        var newRole = await guild.CreateRoleAsync(rolename);

        _mariadb_databaselogs.Add(new Logs
        {
            Id = 0,
            ChannelId = Context.Channel.Id,
            DiscordUserId = Context.User.Id,
            DiscordUserName = Context.User.Username.ToString(),
            CommandId = CommandCreateRole,
            AdditionalInfo = $"User {Context.User.Username.ToString()} created {rolename}",
            CreatedAt = DateTime.Now
        });
        await _mariadb_databaselogs.SaveChangesAsync();
        //Respond
        await FollowupAsync($"Role {newRole.Name} was created!");
    }
//===============================================================================================
/*
    Category Commands 


*/
//===============================================================================================
//Create Category

const string CommandCreateCategory = "create-category";
const string CommandCreateCategoryDescription = "Command to create a category";
    [SlashCommand(CommandCreateCategory, CommandCreateCategoryDescription)]
    [RequireRole("BotAdmin")]  //Only Bot Admins can use this command
    public async Task TaskCommandCreateCategory(string categoryname)
    {
        await DeferAsync();
        //Create a new text channel in the current guild
        var NewCategory = await guild.CreateCategoryChannelAsync(categoryname);
        //New Entry in Database -> Logs
        _mariadb_databaselogs.Add(new Logs
        {
            Id = 0,
            ChannelId = Context.Channel.Id,
            DiscordUserId = Context.User.Id,
            DiscordUserName = Context.User.Username.ToString(),
            CommandId = CommandCreateRole,
            AdditionalInfo = $"User {Context.User.Username.ToString()}created {categoryname}",
            CreatedAt = DateTime.Now
        });
        await _mariadb_databaselogs.SaveChangesAsync();
        //Respond
        await FollowupAsync($"Channel {NewCategory.Name} was created!");
    }
//-----------------------------------------------------
//Assign Role to Category
//-----------------------------------------------------

const string CommandAssignRoleToCategory = "assign-role-to-category";
const string CommandAssignRoleToCategoryDescription = "Command to assign a role to a category";
    [SlashCommand(CommandAssignRoleToCategory, CommandAssignRoleToCategoryDescription)]
    [RequireRole("BotAdmin")]  //Only Bot Admins can use this command
    public async Task TaskCommandAssignRoleTOCategory(IRole role, ICategoryChannel category)
    {
        await DeferAsync();
        //Modify the permissions of the category to assign the role
        await category.AddPermissionOverwriteAsync(role, new OverwritePermissions(viewChannel: PermValue.Allow));

        _mariadb_databaselogs.Add(new Logs
        {
            Id = 0,
            ChannelId = Context.Channel.Id,
            DiscordUserId = Context.User.Id,
            DiscordUserName = Context.User.Username.ToString(),
            CommandId = CommandAssignRoleToCategory,
            AdditionalInfo = $"User {Context.User}assigned role {role.Name} to category {category.Name}",
            CreatedAt = DateTime.Now
        });
        await _mariadb_databaselogs.SaveChangesAsync();
        //Respond
        await FollowupAsync($"User {Context.User.Username.ToString()} assigned role '{role.Name}' to category {category.Name}!");
    }
//===============================================================================================
/*
    Channel Commands 


*/
//===============================================================================================
//Create Text And Voice Channel With Category

const string CommandCreateTextAndVoiceChannelInCategory = "create-text-voice-in-category";
const string CommandCreateTextAndVoiceChannelInCategoryDescription = "Command to create a channel in a category";
    [SlashCommand(CommandCreateTextAndVoiceChannelInCategory, CommandCreateTextAndVoiceChannelInCategoryDescription)]
    [RequireRole("BotAdmin")]  //Only Bot Admins can use this command
    public async Task TaskCommandCreateTextAndVoiceChannelInCategory(ICategoryChannel category, string channelname)
    {
        await DeferAsync();

        // Create text Channel in Category
        var newChannel = await guild.CreateTextChannelAsync(channelname, properties => 
        {
            properties.CategoryId = category.Id; //Add Text Channel to specific Category
         });
        //Create voice Channel in Category
         var newVoiceChannel = await guild.CreateVoiceChannelAsync(channelname, properties => 
        {
            properties.CategoryId = category.Id; //Add Voice Channel to specific Category
         });
         //New Entry in Database -> Logs
         _mariadb_databaselogs.Add(new Logs
        {
            Id = 0,
            ChannelId = Context.Channel.Id,
            DiscordUserId = Context.User.Id,
            DiscordUserName = Context.User.Username.ToString(),
            CommandId = CommandCreateTextAndVoiceChannelInCategory,
            AdditionalInfo = $"Created Text Channel '{newChannel.Name}' and Voice Channel '{newVoiceChannel.Name}' in Category '{category.Name}'",
            CreatedAt = DateTime.Now
        });
        await _mariadb_databaselogs.SaveChangesAsync();
        //Respond
        await FollowupAsync($"Text channel '{newChannel.Name}' and voice channel '{newVoiceChannel.Name}' were in '{category.Name}' created!");

    }
//-----------------------------------------------------
//Create Text Channel With Category
//-----------------------------------------------------

const string CommandCreateTextChannelInCategory = "create-text-in-category";
const string CommandCreateTextChannelInCategoryDescription = "Command to create a channel in a category";
    [SlashCommand(CommandCreateTextChannelInCategory, CommandCreateTextChannelInCategoryDescription)]
    [RequireRole("BotAdmin")] //Only Bot Admins can use this command
    public async Task TaskCommandCreateTextChannelInCategory(ICategoryChannel category, string channelname)
    {
        await DeferAsync();

        // Create text Channel in Category
        var newChannel = await guild.CreateTextChannelAsync(channelname, properties => 
        {
            properties.CategoryId = category.Id; //Add Text Channel to specific Category
         });
         //New Entry in Database -> Logs
         _mariadb_databaselogs.Add(new Logs
        {
            Id = 0,
            ChannelId = Context.Channel.Id,
            DiscordUserId = Context.User.Id,
            DiscordUserName = Context.User.Username.ToString(),
            CommandId = CommandCreateTextChannelInCategory,
            AdditionalInfo = $"User {Context.User.Username.ToString()} created Text Channel '{newChannel.Name}' in Category '{category.Name}'",
            CreatedAt = DateTime.Now
        });

        await _mariadb_databaselogs.SaveChangesAsync();
        //Respond
        await FollowupAsync($"Text channel '{newChannel.Name}' was in '{category.Name}' created!");

    }
//-----------------------------------------------------
//Create Voice Channel With Category
//-----------------------------------------------------

const string CommandCreateVoiceChannelInCategory = "create-voice-in-category";
const string CommandCreateVoiceChannelInCategoryDescription = "Command to create a channel in a category";
    [SlashCommand(CommandCreateVoiceChannelInCategory, CommandCreateVoiceChannelInCategoryDescription)]
    [RequireRole("BotAdmin")]  //Only Bot Admins can use this command
    public async Task TaskCommandCreateVoiceChannelInCategory(ICategoryChannel category, string channelname)
    {
        await DeferAsync();

        // Create voice Channel in Category
        var newChannel = await guild.CreateVoiceChannelAsync(channelname, properties => 
        {
            properties.CategoryId = category.Id; //Add Voice Channel to specific Category
         });
         //New Entry in Database -> Logs
         _mariadb_databaselogs.Add(new Logs
        {
            Id = 0,
            ChannelId = Context.Channel.Id,
            DiscordUserId = Context.User.Id,
            DiscordUserName = Context.User.Username.ToString(),
            CommandId = CommandCreateVoiceChannelInCategory,
            AdditionalInfo = $"User {Context.User.Username.ToString()} created Voice Channel '{newChannel.Name}' in Category '{category.Name}'",
            CreatedAt = DateTime.Now
        });
        
        await _mariadb_databaselogs.SaveChangesAsync();
        //Respond
        await FollowupAsync($"Voice channel '{newChannel.Name}' was in '{category.Name}' created!");

    }
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
            GuildId = Context.User.Id,
            DiscordUserName = Context.User.Username.ToString(),
            Message = welcomemassge,
            AdditionalInfo = $"User {Context.User.Username.ToString()} created Voice Channel updated Welcome Message",
            CreatedAt = DateTime.Now


     });
        await _mariadb_databaselogs.SaveChangesAsync();
    }


//===============================================================================================
/*
    Fallback Commands 


*/
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