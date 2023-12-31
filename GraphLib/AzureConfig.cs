﻿
namespace GraphLib
{
    public class AzureConfig
    {
        public const string ConfigName = "AzureAd";
		public string AzureCloudInstance { get; set; } = string.Empty;
		public string Domain { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
		public string ClientSecret { get; set; } = string.Empty;
	}
}
