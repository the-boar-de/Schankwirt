//standard system refernces
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
//own refernces

//project refernces



//Class
public class CommandModule : InteractionModuleBase<SocketInteractionContext>
{
    //Class Variables
    //Input

    //Output

    //Properties

    //Internal

    //-------------------------------------------------------------------

    //Commands & Events 
    [SlashCommand("test", "testcommand")]
    public async Task testatsk()
    {
        await RespondAsync("Pong!");

    }

    //Button
    [SlashCommand("testbutton", "testcommand")]
    public async Task ButtonTest()
    {
        var button = new ButtonBuilder()
        .WithLabel("Test Button")
        .WithCustomId("test_button")
        .WithStyle(ButtonStyle.Primary);

        var component = new ComponentBuilder()
        .WithButton(button);

        
        await RespondAsync("Button works!", components: component.Build());
    }





}

