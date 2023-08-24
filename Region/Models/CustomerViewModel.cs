namespace Region.Models
{
    public class CustomerViewModel
    {
        public List<Country> Countries { get; set; }
        public int CountryId { get; set; }

        public string CountryName { get; set; } = null!;

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreateOn { get; set; }

        public DateTime? UpdateOn { get; set; }
    }
}
