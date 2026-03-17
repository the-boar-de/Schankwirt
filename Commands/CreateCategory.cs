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

public class CreateCategory: BaseCommand
{
const string CommandCreateCategory = "create-category";
const string CommandCreateCategoryDescription = "Command to create a category";
    public async Task Ping() => await RunAsync();

    [SlashCommand(CommandCreateCategory, CommandCreateCategoryDescription)]
    [RequireRole("BotAdmin")]  //Only Bot Admins can use this command
     public async Task TaskCommandCreateCategory(string categoryname)
    {
        //Create a new text channel in the current guild
        var NewCategory = await guild.CreateCategoryChannelAsync(categoryname);

        await WriteToDataBase(0,Context.Channel.Id,CommandCreateCategory, $"User {Context.User.Username.ToString()}created {categoryname}");

        //Respond
        await FollowupAsync($"Channel {NewCategory.Name} was created!");
    }




}