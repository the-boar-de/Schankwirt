//standard system refernces
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

//own refernces
namespace DiscordBot.Database
{
    public class Logs
    {
        public int Id { get; set;}                 //Primaly key   
        public ulong ChannelID {get; set;}         //ID of Channel
        public ulong DiscordUserId {get; set;}     // ID of User
        public string ?DiscordUserName {get; set;}  //Name of User
        public string ?CommandId {get; set;}      //Command that was used

    }   

}
