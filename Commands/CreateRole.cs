//project refernces
using Discord.Interactions;

public class CreateRole : BaseCommand{  

      public CreateRole(IServiceProvider services) : base(services)
    {
    }
    
    const string Command = "create-role";
    const string CommandDescription = "Command to create a role";
    [SlashCommand(Command, CommandDescription)]
    [RequireRole("BotAdmin")] //Only Bot Admins can use this command
    public async Task TaskCommand(string rolename)
    {
        var newRole = await guild.CreateRoleAsync(rolename);

        await DeferAsync();
        await WriteToDataBase(0,Context.Channel.Id,Command, $"User {Context.User.Username.ToString()} created {rolename}");

        await FollowupAsync($"Role {newRole.Name} was created!");
    }
}