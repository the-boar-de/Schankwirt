//standard system refernces
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
//own refernces
class Button
{
    //Class Variables
    //Field
    private ComponentBuilder component = new ComponentBuilder();


    public ComponentBuilder __Getcomponent{
        get {return component}
    }


    Button(string ID, string text ){
        var button = mew ButtonBuilder()
            .WithLabel(text)
            .WithCustomId(ID)
            .WithStyle(ButtonStyle.Primary);

        component.WithButton(button);

    }

}