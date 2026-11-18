namespace App.Service.Contract.Entities.Shop
{
    public class WsMenuItem : WsEntity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Code { get; set; }
        public string TranslatedName { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }
        public string VideoLink { get; set; }
        public bool? Isactive { get; set; }
        public int? HotGroupId { get; set; }
        public int? SortOrder { get; set; }
        public int? MenuItemCategoryId { get; set; }
    }
}