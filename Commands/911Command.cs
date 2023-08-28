using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System;
using System.Net.Http.Headers;
using UnityEngine;

namespace AshersRoleplayCommands
{
    public class _911Command : IRocketCommand
    {
        private readonly ARPConfiguration _config;

        public _911Command(ARPConfiguration config)
        {
            _config = config;
        }

        public string Name => "911";
        public string Help => "Call 911 for an emergency!";
        public string Syntax => "<message>";
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string> { "arp.911.call" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (caller is UnturnedPlayer player)
            {
                string message = string.Join(" ", command);

                UnturnedPlayer uCaller = (UnturnedPlayer)caller;

                if (command.Length < 1)
                {
                    UnturnedChat.Say(caller, "Syntax: /911 <reason>", Color.red);
                    return;
                }
                if (command.Length > 64)
                {
                    UnturnedChat.Say(caller, "Your call reason is too long!", Color.red);
                }

                AshersRoleplayCommands.Instance._911Call(caller: uCaller, Message: message);

                SendToDiscord(player, message, _config._911WebhookUrl);
            }
        }

        private void SendToDiscord(UnturnedPlayer player, string message, string webhookUrl)
        {
            try
            {
                var embed = new
                {
                    title = $"'{player.CharacterName}' Called 911:",
                    description = message,
                    color = 0x642424,
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
