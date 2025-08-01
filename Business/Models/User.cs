﻿namespace Business.Models
{
    public class User 
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public String Email { get; set; } = string.Empty;
        public string PasswordHash  { get; set; } = string.Empty;
        public List<Company> Companies { get; set; } = new ();
    }
}
