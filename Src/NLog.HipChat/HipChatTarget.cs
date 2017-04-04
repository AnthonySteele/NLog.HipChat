using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HipChat;
using NLog;
using NLog.Targets;

namespace JustEat.NLog.HipChat
{
    [Target("HipChat")]
    public sealed class HipChatTarget : TargetWithLayout
    {
        [Required]
        public string AuthToken { get; set; }

        [Required]
        public string RoomId { get; set; }

        [DefaultValue("Nlog.HipChatTarget")]
        public string SenderName { get; set; }

        [DefaultValue(HipChatClient.BackgroundColor.yellow)]
        public HipChatClient.BackgroundColor BackgroundColor { get; set; }

        [DefaultValue("html")]
        public string MessageFormat { get; set; }


        protected override void Write(LogEventInfo logEvent)
        {
            string log = Layout.Render(logEvent);

            SendMessageToHipchat(log);
        }

        private HipChatClient.MessageFormat ParseFormat()
        {
            var isText = string.Equals(MessageFormat, "text", StringComparison.OrdinalIgnoreCase);
            return isText
                ? HipChatClient.MessageFormat.text
                : HipChatClient.MessageFormat.html;
        }

        private void SendMessageToHipchat(string message)
        {
            var client = new HipChatClient(AuthToken, RoomId, ParseFormat());
            client.From = SenderName;

            client.SendMessage(message, BackgroundColor);
        }
    }
}
