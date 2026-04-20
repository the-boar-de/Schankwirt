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

public class AssignRole: BaseCommand
{

      public AssignRole(IServiceProvider services) : base(services)
    {
    }

const string CommandCreateCategory = "assign-role-to-category";
const string CommandCreateCategoryDescription = "Command to assign a role to a category";

    [SlashCommand(CommandCreateCategory, CommandCreateCategoryDescription)]
    [RequireRole("BotAdmin")]  //Only Bot Admins can use this command
     public async Task TaskCommandAssignRoleTOCategory(IRole role, ICategoryChannel category)
    {
        //Modify the permissions of the category to assign the role
        await category.AddPermissionOverwriteAsync(role, new OverwritePermissions(viewChannel: PermValue.Allow));
        
        await DeferAsync();
        await WriteToDataBase(0,Context.Channel.Id,CommandCreateCategory, $"User {Context.User}assigned role {role.Name} to category {category.Name}");

        //Respond
        await FollowupAsync($"User {Context.User.Username.ToString()} assigned role '{role.Name}' to category {category.Name}!");
    }




}