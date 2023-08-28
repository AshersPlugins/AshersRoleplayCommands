using Rocket.API;

namespace AshersRoleplayCommands
{
    public class ARPConfiguration : IRocketPluginConfiguration
    {
        public string TwitterWebhookUrl { get; set; }
        public string BlackMarketWebhookUrl { get; set; }
        public string MeWebhookUrl { get; set; }
        public string _911WebhookUrl { get; set; }
        public string _111WebhookUrl { get; set; }
        public string _122WebhookUrl { get; set; }
        public string _222WebhookUrl { get; set; }

        public void LoadDefaults()
        {
            TwitterWebhookUrl = "TWITTER_WEBHOOK_URL";
            BlackMarketWebhookUrl = "BLACKMARKET_WEBHOOK_URL";
            MeWebhookUrl = "ME_ACTION_WEBHOOK_URL";
            _911WebhookUrl = "911_POLICE_WEBHOOK_URL";
            _111WebhookUrl = "111_TAXI_WEBHOOK_URL";
            _122WebhookUrl = "122_TOW_SERVICE_WEBHOOK_URL";
            _222WebhookUrl = "222_EMS_WEBHOOK_URL";
        }
    }
}
