//project refernces
using Discord;
using Discord.Interactions;

public class UpdateGreeting: BaseCommand
{

      public UpdateGreeting(IServiceProvider services) : base(services)
    {
    }

const string Command = "update-greeting";
const string CommandDescription = "Update the greeting message";

    [SlashCommand(Command, CommandDescription)]
    [RequireRole("BotAdmin")]  //Only Bot Admins can use this command
     public async Task TaskCommand(ICategoryChannel category, string channelname)
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
        await WriteToDataBase(0,Context.Channel.Id,Command, $"Created Text Channel '{newChannel.Name}' and Voice Channel '{newVoiceChannel.Name}' in Category '{category.Name}'");

        //Respond
        await FollowupAsync($"Text channel '{newChannel.Name}' and voice channel '{newVoiceChannel.Name}' were in '{category.Name}' created!");
    }




}