//project refernces
using Discord.Interactions;

public class CreateCategory: BaseCommand
{
      public CreateCategory(IServiceProvider services) : base(services)
    {
    }

const string Command = "create-category";
const string CommandDescription = "Command to create a category";

    [SlashCommand(Command, CommandDescription)]
    [RequireRole("BotAdmin")]  //Only Bot Admins can use this command
     public async Task TaskCommand(string categoryname)
    {
        //Create a new text channel in the current guild
        var NewCategory = await guild.CreateCategoryChannelAsync(categoryname);
        
        await DeferAsync();
        await WriteToDataBase(0,Context.Channel.Id,Command, $"User {Context.User.Username.ToString()}created {categoryname}");

        //Respond
        await FollowupAsync($"Channel {NewCategory.Name} was created!");
    }




}