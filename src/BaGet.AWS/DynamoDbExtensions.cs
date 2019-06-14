namespace BaGet.AWS
{
    using System;

    using BaGet.AWS.Configuration;

    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The dynamo db extensions.
    /// </summary>
    public static class DynamoDbExtensions
    {
        /// <summary>
        /// The add dynamo db configuration.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <returns>
        /// The <see cref="IConfigurationBuilder"/>.
        /// </returns>
        public static IConfigurationBuilder AddDynamoDbConfiguration(
            this IConfigurationBuilder builder, Action<DynamoDbOptions> options)
        {
            return builder.Add(new DynamoDbConfigurationSource(options));
        }
    }
}
