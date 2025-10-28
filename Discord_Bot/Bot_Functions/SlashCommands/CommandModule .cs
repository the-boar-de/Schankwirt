//standard system refernces
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
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
    [SlashCommand("test", "testcoammdn")]
    public async Task testatsk()
    {
        await RespondAsync("Pong!");

    }





}

