namespace App.Service.Contract.Entities.Shop
{
    public class WsMenuItemCategory : WsEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool? Isactive { get; set; }
        public bool? IsInternal { get; set; }
        public int? CategoryGroupId { get; set; }
        public string TranslateName { get; set; }
        public int? SortOrder { get; set; }
    }
}