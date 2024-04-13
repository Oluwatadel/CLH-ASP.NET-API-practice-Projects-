namespace ProfileApp.Entities
{
    public class Profile
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set;} = default!;
        public string Email { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; } = default!;

        public static List<Profile> Profiles = new()
        {
            new Profile { Id = Guid.NewGuid(), FirstName = "Bola", LastName = "Tolu", Email = "bola@example.com", DateOfBirth = new DateOnly(1990, 1, 1) },
            new Profile { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john@example.com", DateOfBirth = new DateOnly(1995, 5, 15) },
            new Profile { Id = Guid.NewGuid(), FirstName = "Alice", LastName = "Smith", Email = "alice@example.com", DateOfBirth = new DateOnly(1992, 9, 30) },
            new Profile { Id = Guid.NewGuid(), FirstName = "Bob", LastName = "Johnson", Email = "bob@example.com", DateOfBirth = new DateOnly(1988, 3, 10) },
            new Profile { Id = Guid.NewGuid(), FirstName = "Emily", LastName = "Brown", Email = "emily@example.com", DateOfBirth = new DateOnly(1998, 11, 20) },
            new Profile { Id = Guid.NewGuid(), FirstName = "Michael", LastName = "Wilson", Email = "michael@example.com", DateOfBirth = new DateOnly(1993, 7, 5) },
            new Profile { Id = Guid.NewGuid(), FirstName = "Sarah", LastName = "Jones", Email = "sarah@example.com", DateOfBirth = new DateOnly(1991, 12, 25) },
            new Profile { Id = Guid.NewGuid(), FirstName = "David", LastName = "Garcia", Email = "david@example.com", DateOfBirth = new DateOnly(1989, 8, 3) },
            new Profile { Id = Guid.NewGuid(), FirstName = "Emma", LastName = "Martinez", Email = "emma@example.com", DateOfBirth = new DateOnly(1997, 4, 17) },
            new Profile { Id = Guid.NewGuid(), FirstName = "James", LastName = "Taylor", Email = "james@example.com", DateOfBirth = new DateOnly(1994, 6, 8) }
        };
    }
}
