﻿namespace K9.WebApplication.Config
{
    public class DefaultValuesConfiguration
    {
        public static DefaultValuesConfiguration Instance { get; set; }
        
        public string DefaultUserId { get; set; }
        public string WhatsAppSupportNumber { get; set; }
    }
}