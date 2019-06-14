namespace BaGet.AWS
{
    using System;

    using BaGet.AWS.Configuration;

    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The dynamo db configuration source.
    /// </summary>
    public class DynamoDbConfigurationSource : IConfigurationSource
    {
        /// <summary>
        /// The _options action.
        /// </summary>
        private readonly Action<DynamoDbOptions> _optionsAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamoDbConfigurationSource"/> class.
        /// </summary>
        /// <param name="optionsAction">
        /// The options action.
        /// </param>
        public DynamoDbConfigurationSource(Action<DynamoDbOptions> optionsAction)
        {
            this._optionsAction = optionsAction;
        }

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        /// <returns>
        /// The <see cref="IConfigurationProvider"/>.
        /// </returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DynamoDbConfigurationProvider(this._optionsAction);
        }
    }
}
