﻿namespace JobEez_App.Models
{
    public class PayFastSettings
    {
        public string MerchantId { get; set; }
        public string MerchantKey { get; set; }
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
        public string NotifyUrl { get; set; }
        public string SandboxUrl { get; set; }
        public string LiveUrl { get; set; }
        public string SaltPassphrase { get; set; }
    }
}
