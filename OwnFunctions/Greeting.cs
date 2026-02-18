//standard system refernces
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Schankwirt.Database;
using Discord.Net;

public class Greeting
{
    //Field
    readonly Schankwirt.Database.DataBaseLogs databaselogs;

    //-------------------------------------------------------------------
    // Constructor
    //-------------------------------------------------------------------
     public Greeting(Schankwirt.Database.DataBaseLogs _database_)
    {
        databaselogs = _database_;
    }

    //-------------------------------------------------------------------
    //Methodes
    //-------------------------------------------------------------------
   public async Task SendMessage(SocketGuildUser user){
    
    var welcomemsg = databaselogs.WelcomeMessage
                        .FirstOrDefault(w => w.GuildId == user.Guild.Id );

        if (welcomemsg != null)
        {
            // Message senden
            var channel = user.Guild.GetTextChannel(welcomemsg.ChannelId);
            await channel.SendMessageAsync(welcomemsg.Message);
        }
   }
}