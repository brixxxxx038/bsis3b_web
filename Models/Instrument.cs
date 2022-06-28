namespace bsis3b_web.Models
{
    public class Instrument
    {
        public int Id { get; set; }

        public Item Item { get; set; }

        public int ItemId { get; set; }

        public Type Type { get; set; }

        public int TypeId { get; set; }

        public int Price { get; set; }

        public string ImagePath { get; set; }
    }
}