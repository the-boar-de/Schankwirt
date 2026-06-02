//standard system refernces
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

//own refernces

//project refernces
using Discord;
using Discord.Interactions;
using Discord.Rest;
using Discord.WebSocket;

//Class namespase (refernce)
namespace GuildItems.Class
{
    //Class
    public class GuildSetup
    {
        //Class Variables
        //Input

        //Output

        //Properties


        //Internal
        private string ClassName = "GuildSetup";
         //Messages
        private const string InfoMessageCategory = "Category was created";
        private const string InfoMessageRole = "Role was created";
        private const string InfoMessageAssignment = "Role was assigned to category";
        private const string InfoMessageChannel = "Channel for Bot command was created";
        //-------------------------------------------------------------------
        //Methodes
        public async Task InitializeGuild(SocketGuild guild, Func<LogMessage, Task>? logger = null)
        {
            Console.WriteLine($"[Setup] Starte Setup für '{guild.Name}'");
            //-------------------------------------------------------------------------------
            //Get all categories
            //-------------------------------------------------------------------------------
            var categories = guild.CategoryChannels;
            //Check if category "BotUsage" exists
            var botUsageCategory = categories.FirstOrDefault(x => x.Name == "BotUsage");

            if (botUsageCategory== null)
            {
                Console.WriteLine("[Setup] Erstelle Category 'BotUsage'");
                //category does not exist - create it
                await guild.CreateCategoryChannelAsync("BotUsage");
                 
                 botUsageCategory = guild.CategoryChannels.FirstOrDefault(x => x.Name == "BotUsage");

                //Log Message 
                if (logger != null)
                {
                    var InfoLog = new LogMessage(LogSeverity.Info, $"{ClassName}", InfoMessageCategory);
                    await logger(InfoLog);
                }


            }
            //-------------------------------------------------------------------------------
            //Get all Roles
            //-------------------------------------------------------------------------------
            var roles = guild.Roles;
            var botAdminRole = roles.FirstOrDefault(x => x.Name == "BotAdmin");
            //Check if Role "BotAdmin" exists
            if (botAdminRole == null)
            {
                 Console.WriteLine("[Setup] Erstelle Role 'BotAdmin'");
                //Role does not exist - create it
                await guild.CreateRoleAsync("BotAdmin", GuildPermissions.All, Color.DarkRed, false, false);

                botAdminRole = guild.Roles.FirstOrDefault(x => x.Name == "BotAdmin");

                //Log Message 
                if (logger != null)
                {
                    var InfoLog = new LogMessage(LogSeverity.Info, $"{ClassName}", InfoMessageAssignment);
                    await logger(InfoLog);
                }
            }
            //-------------------------------------------------------------------------------
            //Set Permissions
            //-------------------------------------------------------------------------------
            if (botUsageCategory != null 
                    && botAdminRole != null 
                )
            {
                //Check if permission already exists
                var hasPermission = botUsageCategory.PermissionOverwrites
                     .Any(x => x.TargetId == botAdminRole.Id);

                if (!hasPermission)
                {   
                    Console.WriteLine("[Setup] Setze Permissions");
                    //Set permissions for BotAdmin in BotUsage category
                    await botUsageCategory.AddPermissionOverwriteAsync(botAdminRole, new OverwritePermissions(viewChannel: PermValue.Allow));

                     //Log Message 
                     if (logger != null)
                     {
                        var InfoLog = new LogMessage(LogSeverity.Info, $"{ClassName}", InfoMessageRole);
                        await logger(InfoLog);
                     }
                }
            }
            //-------------------------------------------------------------------------------
            //Create Channel
            //-------------------------------------------------------------------------------
            var channel = guild.TextChannels.FirstOrDefault(x => x.Name == "bot-commands");
            if (channel == null 
                && botUsageCategory != null)
            {
                Console.WriteLine("[Setup] Erstelle TextChannel 'bot-commands' in Category 'BotUsage'");
                //Channel does not exist - create it
                await guild.CreateTextChannelAsync("bot-commands", x =>
                {
                    x.CategoryId = botUsageCategory.Id;
                });

                //LogMessage
                if(logger != null)
                {
                    var InfoLog = new LogMessage(LogSeverity.Info, $"{ClassName}", InfoMessageChannel);
                    await logger(InfoLog);
                }
            }
        }
    }
}
