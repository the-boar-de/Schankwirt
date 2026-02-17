//standard system refernces
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
//own refernces
public class Button
{
    //Field

    //-------------------------------------------------------------------
    //Methodes
    //-------------------------------------------------------------------

    //Button With Label and ID
    public ButtonBuilder ButtonWithLabelAndID(
        string label,
        string stringID,
        int ID,
        ButtonStyle buttonStyle
    )
    {
        var button = new ButtonBuilder()
                            .WithLabel(label)
                            .WithCustomId(stringID)
                            .WithId(ID)
                            .WithStyle(buttonStyle);
        return button;
    }
    //-------------------------------------------------------------------

    //Button with Label and URL
    public ButtonBuilder ButtonWithLabelAndUrl(
        string label,
        string Url,
        ButtonStyle buttonStyle
    )
    {
        var button = new ButtonBuilder()
                            .WithLabel(label)
                            .WithUrl(Url)
                            .WithStyle(buttonStyle);
        return button;
    }
    //-------------------------------------------------------------------

    //Button With Label and Emote
    public ButtonBuilder ButtonWithLabelAndEmote(
        string label,
        string stringID,
        IEmote emote,
        ButtonStyle buttonStyle
    )
    {
        var button = new ButtonBuilder()
                            .WithLabel(label)
                            .WithCustomId(stringID)
                            .WithEmote(emote)
                            .WithStyle(buttonStyle);
        return button;
    }
    //-------------------------------------------------------------------

    //Button with Label and URL


}
