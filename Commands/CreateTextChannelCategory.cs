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

public class CreateTextChannelCategory : BaseCommand{  

      public CreateTextChannelCategory(IServiceProvider services) : base(services)
    {
    }
    
    const string Command = "create-text-in-category";
    const string CommandDescription = "Command to create a channel in a category";
    [SlashCommand(Command, CommandDescription)]
    [RequireRole("BotAdmin")] //Only Bot Admins can use this command
    public async Task TaskCommandCreateRole(ICategoryChannel category, string channelname)
    {
        // Create text Channel in Category
        var newChannel = await guild.CreateTextChannelAsync(channelname, properties => 
        {
            properties.CategoryId = category.Id; //Add Text Channel to specific Category
         });

        await DeferAsync();
        await WriteToDataBase(0,Context.Channel.Id,Command,  $"User {Context.User.Username.ToString()} created Text Channel '{newChannel.Name}' in Category '{category.Name}'");

        await FollowupAsync($"Text channel '{newChannel.Name}' was in '{category.Name}' created!");
    }
}