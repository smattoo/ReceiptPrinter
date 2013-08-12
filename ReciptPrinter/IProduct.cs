namespace ReciptPrinter
{
    public interface IProduct
    {
        int Price { get; set; }
        bool IsImported { get; set; }
        ProductType Type { get; set; }
    }
}