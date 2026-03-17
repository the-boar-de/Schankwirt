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
    protected readonly Schankwirt.Database.DataBaseLogs _mariadb_databaselogs;
    protected BaseCommand(Schankwirt.Database.DataBaseLogs? databaselogs = null)
    {
        _mariadb_databaselogs = databaselogs;
    }
   protected abstract Task ExecuteAsync();

        // Drumherum wird einmal hier definiert
    protected async Task RunAsync()
    {
        await DeferAsync();

        try
        {
            await ExecuteAsync();
        }
        catch (Exception ex)
        {
            await FollowupAsync(embed: BuildErrorEmbed(ex.Message));
        }
    }
        // Wiederverwendbare Embed-Builder
    protected Embed BuildSuccessEmbed(string title, string description)
        => new EmbedBuilder()
            .WithTitle(title)
            .WithDescription(description)
            .WithColor(Color.Green)
            .Build();

    protected Embed BuildErrorEmbed(string message)
        => new EmbedBuilder()
            .WithTitle("Fehler")
            .WithDescription(message)
            .WithColor(Color.Red)
            .Build();


}