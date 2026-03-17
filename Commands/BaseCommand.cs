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

public abstract class BaseCommand : InteractionModuleBase<SocketInteractionContext>
{//Field
    protected SocketGuild guild => Context.Guild as SocketGuild;
    //Public
    protected Schankwirt.Database.DataBaseLogs? databaselogs = null;

    protected async Task RunAsync()
    {
        await DeferAsync();
    }
    protected async Task WriteToDataBase(
        int ID,
        ulong ChannelID,
        string CommandID,
        string AdditionalInfo
    )
    {
        if( databaselogs != null)
        {
            databaselogs.Add(new Logs
            {
                Id = ID,
                ChannelId = ChannelID,
                CommandId = CommandID,
               AdditionalInfo = AdditionalInfo,
               CreatedAt = DateTime.Now
         });
        await databaselogs.SaveChangesAsync();
        }
        
    }
}
