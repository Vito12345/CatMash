namespace CatMash.Domain
{
    /// <summary>
    /// Object cat with votes
    /// </summary>
    public class Cat
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Url to get image of cat
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Vote counter for this cat
        /// </summary>
        public int Votes { get; set; }
    }
}
