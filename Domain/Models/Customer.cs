﻿namespace Psp.Pos.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int LoyaltyPoints { get; set; }
        public String LoyaltyLevel { get; set; }
        public String[] FeedBack { get; set; }
    }
}
