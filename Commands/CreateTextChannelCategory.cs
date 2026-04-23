//project refernces
using Discord;
using Discord.Interactions;

public class CreateVoiceChannelInCategory : BaseCommand{  

      public CreateVoiceChannelInCategory(IServiceProvider services) : base(services)
    {
    }
    const string Command = "create-voice-in-category";
    const string CommandDescription = "Command to create a channel in a category";
    [SlashCommand(Command, CommandDescription)]
    [RequireRole("BotAdmin")] //Only Bot Admins can use this command
    public async Task TaskCommand(ICategoryChannel category, string channelname)
    {
        // Create text Channel in Category
        var newChannel = await guild.CreateVoiceChannelAsync(channelname, properties => 
        {
            properties.CategoryId = category.Id; //Add Voice Channel to specific Category
         });

        await DeferAsync();
        await WriteToDataBase(0,Context.Channel.Id,Command, $"User {Context.User.Username.ToString()} created Voice Channel '{newChannel.Name}' in Category '{category.Name}'");

        await FollowupAsync($"Text channel '{newChannel.Name}' was in '{category.Name}' created!");
    }
}