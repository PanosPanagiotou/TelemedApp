namespace TelemedApp.Domain.Entities
{
    public class PharmacyItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string DrugName { get; set; } = string.Empty;
        public string Strength { get; set; } = string.Empty;
        public string Form { get; set; } = string.Empty; // tablet, syrup
        public int QuantityOnHand { get; set; }
        public string Supplier { get; set; } = string.Empty;
    }
}