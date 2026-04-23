//project refernces
using Discord;
using Discord.Interactions;
public class AssignRole: BaseCommand
{

      public AssignRole(IServiceProvider services) : base(services)
    {
    }

const string Command = "assign-role-to-category";
const string CommandDescription = "Command to assign a role to a category";

    [SlashCommand(Command, CommandDescription)]
    [RequireRole("BotAdmin")]  //Only Bot Admins can use this command
     public async Task TaskCommand(IRole role, ICategoryChannel category)
    {
        //Modify the permissions of the category to assign the role
        await category.AddPermissionOverwriteAsync(role, new OverwritePermissions(viewChannel: PermValue.Allow));
        
        await DeferAsync();
        await WriteToDataBase(0,Context.Channel.Id,Command, $"User {Context.User}assigned role {role.Name} to category {category.Name}");

        //Respond
        await FollowupAsync($"User {Context.User.Username.ToString()} assigned role '{role.Name}' to category {category.Name}!");
    }




}