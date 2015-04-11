namespace Poc.Identity.WebHost.Configuration.Users
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Thinktecture.IdentityServer.Core;
    using Thinktecture.IdentityServer.Core.Services.InMemory;

    public class UsersRepository
    {
        public static List<InMemoryUser> GetAll()
        {
            return new List<InMemoryUser>
                       {
                           new InMemoryUser
                               {
                                   Subject = "User_id_1",
                                   Username = "admin",
                                   Password = "test",
                                   Claims =
                                       new Claim[]
                                           {
                                               new Claim(
                                                   Constants.ClaimTypes.Email,
                                                   "marek@gmail.com"),
                                               new Claim(
                                                   Constants.ClaimTypes.Role,
                                                   "Admin"),
                                               new Claim(
                                                   Constants.ClaimTypes.Role,
                                                   "User")
                                           }
                               },

                            new InMemoryUser
                               {
                                   Subject = "User_id_2",
                                   Username = "user",
                                   Password = "test",
                                   Claims =
                                       new Claim[]
                                           {
                                               new Claim(
                                                   Constants.ClaimTypes.Email,
                                                   "user@gmail.com"),
                                               new Claim(
                                                   Constants.ClaimTypes.Role,
                                                   "User")
                                           }
                               }
                       };
        }
    }
}