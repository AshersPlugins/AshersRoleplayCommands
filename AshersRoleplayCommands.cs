using Rocket.API;
using Rocket.Core;
using Logger = Rocket.Core.Logging.Logger;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AshersRoleplayCommands
{
    public class AshersRoleplayCommands : RocketPlugin<ARPConfiguration>
    {
        private VersionChecker _versionChecker;
        private const string VersionCheckUrl = "http://raw.githubusercontent.com/AshersPlugins/AshersRoleplayCommands/main/Version/CheckVersion.txt";

        public static AshersRoleplayCommands Instance { get; private set; }

        protected override void Load()
        {
            _versionChecker = new VersionChecker(VersionCheckUrl);
            string remoteVersion = _versionChecker.GetRemoteVersion();

            Logger.Log("######################################################");
            Logger.Log("##              AshersRoleplayCommands              ##");
            Logger.Log("##                    By Im.Asher                   ##");
            Logger.Log("##               Successfully Loaded!               ##");
            Logger.Log("##                                                  ##");
            Logger.Log("##           Support >> Im.Asher On Discord         ##");
            Logger.Log("######################################################");

            if (string.IsNullOrEmpty(remoteVersion))
            {
                Logger.LogError("Failed to check for updates.");
                return;
            }

            if (remoteVersion == GetVersion())
            {
                Logger.Log("The plugin is up to date.");
                Logger.Log($"Version: {GetVersion()}");
            }
            else
            {
                Logger.Log("Plugin Out of Date! Update Required!");
                Logger.Log($"Current version: {GetVersion()}");
                Logger.Log($"Latest version: {remoteVersion}");
            }

            AshersRoleplayCommands plugin = GetComponent<AshersRoleplayCommands>();
            RegisterCommands(plugin.Configuration.Instance);
        }

        protected override void Unload()
        {
            _versionChecker = new VersionChecker(VersionCheckUrl);
            string remoteVersion = _versionChecker.GetRemoteVersion();

            Logger.Log("######################################################");
            Logger.Log("##              AshersRoleplayCommands              ##");
            Logger.Log("##                    By Im.Asher                   ##");
            Logger.Log("##              Successfully Unloaded!              ##");
            Logger.Log("##                                                  ##");
            Logger.Log("##           Support >> Im.Asher On Discord         ##");
            Logger.Log("######################################################");

            if (string.IsNullOrEmpty(remoteVersion))
            {
                Logger.LogError("Failed to check for updates.");
                return;
            }

            if (remoteVersion == GetVersion())
            {
                Logger.Log("The plugin is up to date.");
            }
            else
            {
                Logger.Log("Plugin Out of Date! Update Required!");
                Logger.Log($"Current version: {GetVersion()}");
                Logger.Log($"Latest version: {remoteVersion}");
            }
        }

        private string GetVersion()
        {
            return Assembly.GetName().Version.ToString();
        }

        private void RegisterCommands(ARPConfiguration config)
        {
            // Find Commands
            var _911Command = new _911Command(config);
            var _111Command = new _111Command(config);
            var _122Command = new _122Command(config);
            var _222Command = new _222Command(config);
            var bmCommand = new BlackMarketCommand(config);
            var twitterCommand = new TwitterCommand(config);
            var meCommand = new MeCommand(config);

            // Register Them
            R.Commands.Register(twitterCommand);
            R.Commands.Register(bmCommand);
            R.Commands.Register(meCommand);
            R.Commands.Register(_911Command);
            R.Commands.Register(_111Command);
            R.Commands.Register(_122Command);
            R.Commands.Register(_222Command);

            Logger.Log("# Registered all ARP commands. #");
        }

        // 911, 222, 111 & 122 Call Functions
        // 911 Calls
        public void _911Call(UnturnedPlayer caller, string Message)
        {
            LocationNode Node = (from x in LevelNodes.nodes
                                 where x.type == ENodeType.LOCATION
                                 orderby Vector3.Distance(x.point, caller.Position)
                                 select x).FirstOrDefault() as LocationNode;
            string Location = Node.name;
            string name = caller.CharacterName;
            var players = Get911Players();

            foreach (var player in players)
            {
                player.Player.quests.replicateSetMarker(true, caller.Position, "911 Call");

                string Line0 = $"<color=#642424>####           NEW 911 CALL           ####</color>";
                string Line1 = $"<color=#642424>[911] Caller:</color> <color=white>{player.CharacterName}";
                string Line2 = $"<color=#642424>[911] Reason:</color> <color=white>{Message}";
                string Line3 = $"<color=#642424>[911] Location:</color> <color=white>{Location}";
                string Line4 = $"<color=#642424>###################################</color>";

                UnturnedChat.Say(player, Line0, UnityEngine.Color.white, rich: true);
                UnturnedChat.Say(player, Line1, UnityEngine.Color.white, rich: true);
                UnturnedChat.Say(player, Line2, UnityEngine.Color.white, rich: true);
                UnturnedChat.Say(player, Line3, UnityEngine.Color.white, rich: true);
                UnturnedChat.Say(player, Line4, UnityEngine.Color.white, rich: true);

                UnturnedChat.Say(caller, $"Your 911 call has been sent.", UnityEngine.Color.green);
            }
        }
        public List<UnturnedPlayer> Get911Players()
        {
            return Provider.clients.Select(UnturnedPlayer.FromSteamPlayer).Where(player => player.HasPermission("arp.911.view")).ToList();
        }

        // 222 Calls
        public void _222Call(UnturnedPlayer caller, string Message)
        {
            LocationNode Node = (from x in LevelNodes.nodes
                                 where x.type == ENodeType.LOCATION
                                 orderby Vector3.Distance(x.point, caller.Position)
                                 select x).FirstOrDefault() as LocationNode;
            string Location = Node.name;
            string name = caller.CharacterName;
            var players = Get222Players();

            foreach (var player in players)
            {
                player.Player.quests.replicateSetMarker(true, caller.Position, "911 Call");

                string Line0 = $"<color=#FF7276>####           NEW EMS CALL           ####</color>";
                string Line1 = $"<color=#FF7276>[222] Caller:</color> <color=white>{player.CharacterName}";
                string Line2 = $"<color=#FF7276>[222] Reason:</color> <color=white>{Message}";
                string Line3 = $"<color=#FF7276>[222] Location:</color> <color=white>{Location}";
                string Line4 = $"<color=#FF7276>###################################</color>";

                UnturnedChat.Say(player, Line0, UnityEngine.Color.white, rich: true);
                UnturnedChat.Say(player, Line1, UnityEngine.Color.white, rich: true);
                UnturnedChat.Say(player, Line2, UnityEngine.Color.white, rich: true);
                UnturnedChat.Say(player, Line3, UnityEngine.Color.white, rich: true);
                UnturnedChat.Say(player, Line4, UnityEngine.Color.white, rich: true);

                UnturnedChat.Say(caller, $"Your 222 call has been sent.", UnityEngine.Color.green);
            }
        }
        public List<UnturnedPlayer> Get222Players()
        {
            return Provider.clients.Select(UnturnedPlayer.FromSteamPlayer).Where(player => player.HasPermission("arp.222.view")).ToList();
        }

        // 111 Calls
        public void _111Call(UnturnedPlayer caller)
        {
            LocationNode Node = (from x in LevelNodes.nodes
                                 where x.type == ENodeType.LOCATION
                                 orderby Vector3.Distance(x.point, caller.Position)
                                 select x).FirstOrDefault() as LocationNode;
            string Location = Node.name;
            string name = caller.CharacterName;
            var players = Get111Players();

            foreach (var player in players)
            {
                player.Player.quests.replicateSetMarker(true, caller.Position, "911 Call");

                string Line0 = $"<color=#FFBF00>####           NEW TAXI CALL           ####</color>";
                string Line1 = $"<color=#FFBF00>[111] Caller:</color> <color=white>{player.CharacterName}";
                string Line2 = $"<color=#FFBF00>[111] Location:</color> <color=white>{Location}";
                string Line3 = $"<color=#FFBF00>###################################</color>";

                UnturnedChat.Say(player, Line0, UnityEngine.Color.white, rich: true);
                UnturnedChat.Say(player, Line1, UnityEngine.Color.white, rich: true);
                UnturnedChat.Say(player, Line2, UnityEngine.Color.white, rich: true);
                UnturnedChat.Say(player, Line3, UnityEngine.Color.white, rich: true);

                UnturnedChat.Say(caller, $"You called a taxi!", UnityEngine.Color.green);
            }
        }
        public List<UnturnedPlayer> Get111Players()
        {
            return Provider.clients.Select(UnturnedPlayer.FromSteamPlayer).Where(player => player.HasPermission("arp.111.view")).ToList();
        }

        // 111 Calls
        public void _122Call(UnturnedPlayer caller)
        {
            LocationNode Node = (from x in LevelNodes.nodes
                                 where x.type == ENodeType.LOCATION
                                 orderby Vector3.Distance(x.point, caller.Position)
                                 select x).FirstOrDefault() as LocationNode;
            string Location = Node.name;
            string name = caller.CharacterName;
            var players = Get122Players();

            foreach (var player in players)
            {
                player.Player.quests.replicateSetMarker(true, caller.Position, "911 Call");

                string Line0 = $"<color=#7E481C>####            TOW REQUEST            ####</color>";
                string Line1 = $"<color=#7E481C>[122] Caller:</color> <color=white>{player.CharacterName}";
                string Line2 = $"<color=#7E481C>[122] Location:</color> <color=white>{Location}";
                string Line3 = $"<color=#7E481C>###################################</color>";

                UnturnedChat.Say(player, Line0, UnityEngine.Color.white, rich: true);
                UnturnedChat.Say(player, Line1, UnityEngine.Color.white, rich: true);
                UnturnedChat.Say(player, Line2, UnityEngine.Color.white, rich: true);
                UnturnedChat.Say(player, Line3, UnityEngine.Color.white, rich: true);

                UnturnedChat.Say(caller, $"You called a taxi!", UnityEngine.Color.green);
            }
        }
        public List<UnturnedPlayer> Get122Players()
        {
            return Provider.clients.Select(UnturnedPlayer.FromSteamPlayer).Where(player => player.HasPermission("arp.122.view")).ToList();
        }
    }
}
