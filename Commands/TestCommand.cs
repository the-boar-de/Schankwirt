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

public class TestCommand : BaseCommand
{  

    [RequireRole("BotAdmin")]
    [SlashCommand("ping", "Antwortet mit Pong")]
    public async Task Ping() => await RunAsync();

    protected override async Task ExecuteAsync()
    {
        _mariadb_databaselogs.Add(new Logs
        {
            Id = 0,
            ChannelId = Context.Channel.Id,
            CommandId = CommandSendMessage,
            AdditionalInfo = "Test",
            CreatedAt = DateTime.Now
        });

        var embed = BuildSuccessEmbed("Pong!", $"Latenz: {Context.Client.Latency}ms");
        await FollowupAsync(embed: embed);
    }


}