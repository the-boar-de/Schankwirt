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

public class CreateRole : BaseCommand{  
    public async Task Ping() => await RunAsync();
    const string CommandCreateRole = "create-role";
    const string CommandCreateRoleDescription = "Command to create a role";
    [SlashCommand(CommandCreateRole, CommandCreateRoleDescription)]
    [RequireRole("BotAdmin")] //Only Bot Admins can use this command
    public async Task TaskCommandCreateRole(string rolename)
    {
        var newRole = await guild.CreateRoleAsync(rolename);

        await WriteToDataBase(0,Context.Channel.Id,CommandCreateRole, $"User {Context.User.Username.ToString()} created {rolename}");

        await FollowupAsync($"Role {newRole.Name} was created!");
    }
}