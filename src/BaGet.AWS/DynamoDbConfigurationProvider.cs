using System.Collections.Generic;

namespace BaGet.AWS
{
    using System;
    using System.Linq;

    using Amazon;
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DocumentModel;

    using BaGet.AWS.Configuration;

    using Microsoft.Extensions.Configuration;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The dynamo db configuration provider.
    /// </summary>
    public class DynamoDbConfigurationProvider : ConfigurationProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamoDbConfigurationProvider"/> class.
        /// </summary>
        /// <param name="optionsAction">
        /// The options action.
        /// </param>
        public DynamoDbConfigurationProvider(Action<DynamoDbOptions> optionsAction)
        {
            this.OptionsAction = optionsAction;
        }

        /// <summary>
        /// Gets the options action.
        /// </summary>
        Action<DynamoDbOptions> OptionsAction { get; }

        /// <summary>
        /// The load.
        /// </summary>
        public override void Load()
        {
            var options = new DynamoDbOptions();

            this.OptionsAction(options);

            var awsRegion = RegionEndpoint.GetBySystemName(options.AwsRegion);
            var client = new AmazonDynamoDBClient(awsRegion);
            var table = Table.LoadTable(client, options.TableName);

            var item = table.GetItemAsync(options.Id).GetAwaiter().GetResult();

            var config = item.ToJson();
            var jsonObject = JObject.Parse(config);
            var jTokens = jsonObject.Descendants().Where(p => p.Count() == 0);
            var results = jTokens.Aggregate(new Dictionary<string, string>(), (properties, jToken) =>
            {
                properties.Add(jToken.Path.Replace(".",":").Replace("[", ":").Replace("]", string.Empty), jToken.ToString());
                return properties;
            });
            this.Data = results;
        }
    }
}
