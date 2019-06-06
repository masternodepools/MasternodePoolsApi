namespace MasternodePools.Data.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Locale { get; set; }
        public string Discriminator { get; set; }
    }
}
