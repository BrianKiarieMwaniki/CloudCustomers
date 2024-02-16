using CloudCustomer.API.Model;

namespace CloudCustomer.Test.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() =>
            new()
            {
                new User
                {
                    Name = "Test User 1",
                    Email = "test.user1@dev.com",
                    Address = new Address
                    {
                        Street = "123 Full St.",
                        City = "Somewhere",
                        ZipCode = "123456"
                    }
                },
                new User
                {
                    Name = "Test User 2",
                    Email = "test.user2@dev.com",
                    Address = new Address
                    {
                        Street = "Junction St.",
                        City = "Somewhere",
                        ZipCode = "789456"
                    }
                },
                new User
                {
                    Name = "Test User 3",
                    Email = "test.user3@dev.com",
                    Address = new Address
                    {
                        Street = "Wolf St.",
                        City = "Somewhere",
                        ZipCode = "369852"
                    }
                }
            };
    }
}
