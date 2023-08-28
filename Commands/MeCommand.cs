using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System;

namespace AshersRoleplayCommands
{
    public class MeCommand : IRocketCommand
    {
        private readonly ARPConfiguration _config;

        public MeCommand(ARPConfiguration config)
        {
            _config = config;
        }

        public string Name => "me";
        public string Help => "Send an in-game action.";
        public string Syntax => "<message>";
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string> { "arp.me" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (caller is UnturnedPlayer player)
            {
                string message = string.Join(" ", command);
                string formattedMessage = $"<color=red>{player.CharacterName} | ME:</color> <color=white>{message}"; ;

                UnturnedChat.Say(formattedMessage, UnityEngine.Color.white, rich: true);
                SendToDiscord(player, message, _config.MeWebhookUrl);
            }
        }

        private void SendToDiscord(UnturnedPlayer player, string message, string webhookUrl)
        {
            try
            {
                var embed = new
                {
                    title = $"[ME] '{player.CharacterName}'",
                    description = message,
                    color = 0xFF0000,
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
