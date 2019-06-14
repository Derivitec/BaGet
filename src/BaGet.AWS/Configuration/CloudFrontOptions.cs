namespace BaGet.AWS.Configuration
{
    using System.Collections.Generic;

    /// <summary>
    /// The cloud front options.
    /// </summary>
    public class CloudfrontOptions
    {
        /// <summary>
        /// The forward limit.
        /// </summary>
        private int? forwardLimit;

        /// <summary>
        /// Gets or sets the forward limit.
        /// </summary>
        public int? ForwardLimit
        {
            get => this.forwardLimit;
            set
            {
                if (value == 0)
                {
                    this.forwardLimit = null;
                }

                this.forwardLimit = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether require header symmetry.
        /// </summary>
        public bool RequireHeaderSymmetry { get; set; }

        /// <summary>
        /// Gets or sets the known networks.
        /// </summary>
        public List<string> KnownNetworks { get; set; }
    }
}
