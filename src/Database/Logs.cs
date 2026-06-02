//standard system refernces
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

//own refernces
namespace Schankwirt.Database
{
    public class Logs
    {
        public int Id { get; set;}                 //Primaly key   
        public ulong ChannelId {get; set;}         //ID of Channel
        public ulong DiscordUserId {get; set;}     // ID of User
        public string ?DiscordUserName {get; set;}  //Name of User
        public string ?CommandId {get; set;}      //Command that was used
        public string ?AdditionalInfo {get; set;} //Additional Info
        public DateTime CreatedAt {get; set;} //Timestamp

    }   

}
