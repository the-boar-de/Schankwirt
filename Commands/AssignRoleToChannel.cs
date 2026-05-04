//project refernces
using Discord;
using Discord.Interactions;
public class AssignRoleToChannel: BaseCommand
{

      public AssignRoleToChannel(IServiceProvider services) : base(services)
    {
    }

const string Command = "assign-role-to-channel";
const string CommandDescription = "Command to assign a role to a channel";

    [SlashCommand(Command, CommandDescription)]
    [RequireRole("BotAdmin")]  //Only Bot Admins can use this command
     public async Task TaskCommand(IRole role, IGuildChannel channel)
    {

        //Modify the permissions of a channel / assign a role to it
       await channel.AddPermissionOverwriteAsync(role,new OverwritePermissions(viewChannel: PermValue.Allow));

        await DeferAsync();
        await WriteToDataBase(0,Context.Channel.Id,Command, $"User {Context.User}assigned role {role.Name} to channel {channel.Name}");

        //Respond
        await FollowupAsync($"User {Context.User.Username.ToString()} assigned role '{role.Name}' to channel {channel.Name}!");
    }




}