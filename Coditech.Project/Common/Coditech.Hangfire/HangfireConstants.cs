namespace Coditech.Hangfire
{
    public struct HangfireConstants
    {
        /// <summary>
        /// Hangfire connection string key.
        /// </summary>
        public const string HangfireConnection = "HangfireConnection";

        /// <summary>
        /// Hangfire config section available in the application settings.
        /// </summary>
        public const string HangfireConfigSection = "HangfireConfigSection";

        /// <summary>
        /// Hangfire command batch max timeout.
        /// </summary>
        public const string HangfireCommandBatchMaxTimeout = "HangfireCommandBatchMaxTimeout";

        /// <summary>
        /// Hangfire sliding invisibility timeout.
        /// </summary>
        public const string HangfireSlidingInvisibilityTimeout = "HangfireSlidingInvisibilityTimeout";

        /// <summary>
        /// Hangfire queue polling interval.
        /// </summary>
        public const string HangfireQueuePollInterval = "HangfireQueuePollInterval";

        /// <summary>
        /// Hangfire prepare schema if necessary flag.
        /// </summary>
        public const string HangfirePrepareSchemaIfNecessary = "HangfirePrepareSchemaIfNecessary";
    }
}
