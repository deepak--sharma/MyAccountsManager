namespace AccountService.Models
{
    public class Account
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

    }
}