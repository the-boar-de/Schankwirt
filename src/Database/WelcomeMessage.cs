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
    public class WelcomeMessage
    {
        public int Id { get; set;}                          //Primaly key      
        public string Message {get; set;}                   //Welcomemessage
        public string ?DiscordUserName {get; set;}          //Name of User
        public string ?CommandId {get; set;}                //Command that was used
        public string ?AdditionalInfo {get; set;}           //Additional Info
        public DateTime CreatedAt {get; set;}               //Timestamp

    }   

}
