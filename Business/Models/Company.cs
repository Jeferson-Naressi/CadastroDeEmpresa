namespace Business.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string FantasyName { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string OpeningDate { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string LegalNature { get; set; } = string.Empty;
        public string MainActivity { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Complement { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public User? User { get; set; }

    }
}
