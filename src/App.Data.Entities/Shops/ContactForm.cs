namespace App.Data.Entities.Shops
{
    public class ContactForm: BaseDataEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public string Mobile { get; set; }
    }
}
