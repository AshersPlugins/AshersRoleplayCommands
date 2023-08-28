using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AshersRoleplayCommands
{
    public class BlackMarketCommand : IRocketCommand
    {
        private readonly ARPConfiguration _config;

        public BlackMarketCommand(ARPConfiguration config)
        {
            _config = config;
        }

        public string Name => "blackmarket";
        public string Help => "Access the black market anonymously.";
        public string Syntax => "<message>";
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public List<string> Aliases => new List<string> { "bm" } ;
        public List<string> Permissions => new List<string> { "arp.blackmarket" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (caller is UnturnedPlayer player)
            {
                string message = string.Join(" ", command);
                string formattedMessage = $"<color=#808080>[Black Market]</color> <color=white>{message}";

                UnturnedChat.Say(formattedMessage, UnityEngine.Color.white, rich: true);
                SendToDiscord(player, message, _config.BlackMarketWebhookUrl);
            }
        }

        private void SendToDiscord(UnturnedPlayer player, string message, string webhookUrl)
        {
            try
            {
                var embed = new
                {
                    title = $"Black Martket: '{player.CharacterName}'",
                    description = message,
                    color = 0x808080,
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
