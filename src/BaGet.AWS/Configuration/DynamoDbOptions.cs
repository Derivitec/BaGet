namespace BaGet.AWS.Configuration
{
    /// <summary>
    /// The dynamo db options.
    /// </summary>
    public class DynamoDbOptions
    {
        /// <summary>
        /// Gets or sets the table name.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Gets or sets the aws region.
        /// </summary>
        public string AwsRegion { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }
    }
}
