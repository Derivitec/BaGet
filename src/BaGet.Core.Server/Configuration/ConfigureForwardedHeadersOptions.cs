using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BaGet.Configuration
{
    using System.Net;

    using BaGet.AWS.Configuration;

    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// The configure forwarded headers options.
    /// </summary>
    public class ConfigureForwardedHeadersOptions : IConfigureOptions<ForwardedHeadersOptions>
    {
        /// <summary>
        /// The hosting environment.
        /// </summary>
        private readonly IWebHostEnvironment hostingEnvironment;

        /// <summary>
        /// The cloudfront options.
        /// </summary>
        private readonly IOptions<CloudfrontOptions> cloudfrontOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureForwardedHeadersOptions"/> class.
        /// </summary>
        /// <param name="cloudfrontOptions">
        /// The cloudfront options.
        /// </param>
        /// <param name="hostingEnvironment">
        /// The hosting env
        /// </param>
        public ConfigureForwardedHeadersOptions(IOptions<CloudfrontOptions> cloudfrontOptions, IWebHostEnvironment hostingEnvironment)
        {
            this.cloudfrontOptions = cloudfrontOptions;
            this.hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// The configure.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public void Configure(ForwardedHeadersOptions options)
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

            // Do not restrict to local network/proxy
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
            if (this.hostingEnvironment.IsDevelopment()) return;
            options.RequireHeaderSymmetry = cloudfrontOptions.Value.RequireHeaderSymmetry;
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.ForwardLimit = cloudfrontOptions.Value.ForwardLimit;
            cloudfrontOptions.Value.KnownNetworks.ForEach(e => options.KnownNetworks.Add(ConvertIp(e)));
        }

        /// <summary>
        /// The convert ip.
        /// </summary>
        /// <param name="cidrString">
        /// The cidr string.
        /// </param>
        /// <returns>
        /// The <see cref="IPNetwork"/>.
        /// </returns>
        public static IPNetwork ConvertIp(string cidrString)
        {
            var components = cidrString.Split('/');
            return new IPNetwork(IPAddress.Parse(components[0]), int.Parse(components[1]));
        }
    }
}
