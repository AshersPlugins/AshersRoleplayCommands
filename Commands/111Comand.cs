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
    public class _111Command : IRocketCommand
    {
        private readonly ARPConfiguration _config;

        public _111Command(ARPConfiguration config)
        {
            _config = config;
        }

        public string Name => "111";
        public string Help => "Call for a local taxi!";
        public string Syntax => "";
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string> { "arp.111.call" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (caller is UnturnedPlayer player)
            {
                UnturnedPlayer uCaller = (UnturnedPlayer)caller;

                AshersRoleplayCommands.Instance._111Call(caller: uCaller);
                SendToDiscord(player, _config._111WebhookUrl);
            }
        }

        private void SendToDiscord(UnturnedPlayer player, string webhookUrl)
        {
            try
            {
                var embed = new
                {
                    title = $"'{player.CharacterName}' Called a Taxi",
                    color = 0xFFBF00,
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
