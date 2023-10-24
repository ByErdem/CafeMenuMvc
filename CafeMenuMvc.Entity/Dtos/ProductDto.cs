using CafeMenuMvc.Shared.Abstract;

namespace CafeMenuMvc.Entity.Dtos
{
    public class ProductDto : EntityBase
    {
        public int PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public int CATEGORYID { get; set; }
        public string CATEGORYNAME { get; set; }
        public decimal PRICE { get; set; }
        public string IMAGEPATH { get; set; }
        public string IMAGEBASE64 { get; set; }
    }
}
