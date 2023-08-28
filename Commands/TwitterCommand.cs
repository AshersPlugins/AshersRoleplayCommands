using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System;

namespace AshersRoleplayCommands
{
    public class TwitterCommand : IRocketCommand
    {
        private readonly ARPConfiguration _config;

        public TwitterCommand(ARPConfiguration config)
        {
            _config = config;
        }

        public string Name => "twitter";
        public string Help => "Post a message on Twitter.";
        public string Syntax => "<message>";
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public List<string> Aliases => new List<string>();
        public List<string> Permissions =>  new List<string> { "arp.twitter" } ;

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (caller is UnturnedPlayer player)
            {
                string message = string.Join(" ", command);
                string formattedMessage = $"<color=#00acee>[Twitter] {player.CharacterName}:</color> <color=white>{message}";

                UnturnedChat.Say(formattedMessage, UnityEngine.Color.white, rich: true);
                SendToDiscord(player, message, _config.TwitterWebhookUrl);
            }
        }

        private void SendToDiscord(UnturnedPlayer player, string message, string webhookUrl)
        {
            try
            {
                var embed = new
                {
                    title = $"'{player.CharacterName}' Tweeted:",
                    description = message,
                    color = 0x00acee,
                };

                var payload = new
                {
                    embeds = new List<object> { embed }
                };

                string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.UploadString(webhookUrl, "POST", jsonPayload);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }
    }
}
