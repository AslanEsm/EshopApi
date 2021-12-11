namespace AngularEshop.Services.DTOs
{
    public class OrderBasketDetail
    {
        public int OrderDetailId { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string ImageName { get; set; }
        public int Count { get; set; }
    }
}