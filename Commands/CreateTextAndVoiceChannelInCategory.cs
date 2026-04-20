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

public class CreateTextAndVoiceChannelInCategory: BaseCommand
{

      public CreateTextAndVoiceChannelInCategory(IServiceProvider services) : base(services)
    {
    }

const string CommandCreateCategory = "create-text-voice-in-category";
const string CommandCreateCategoryDescription = "Command to create a channel in a category";

    [SlashCommand(CommandCreateCategory, CommandCreateCategoryDescription)]
    [RequireRole("BotAdmin")]  //Only Bot Admins can use this command
     public async Task TaskCommandAssignRoleTOCategory(ICategoryChannel category, string channelname)
    {
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
        
        await DeferAsync();
        await WriteToDataBase(0,Context.Channel.Id,CommandCreateCategory, $"Created Text Channel '{newChannel.Name}' and Voice Channel '{newVoiceChannel.Name}' in Category '{category.Name}'");

        //Respond
        await FollowupAsync($"Text channel '{newChannel.Name}' and voice channel '{newVoiceChannel.Name}' were in '{category.Name}' created!");
    }




}