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
    //Constructor
      public TestCommand(IServiceProvider services) : base(services)
    {
    }

    [RequireRole("BotAdmin")]
    [SlashCommand("pingggggg", "Antwortet mit Pong")]
    protected async Task Test()
    {
        await DeferAsync();
        await WriteToDataBase(0,Context.Channel.Id,"ping","Antwortet mit Pong");
        await FollowupAsync("pong!");
    }


}