namespace eWallet.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<eWallet.Models.eWalletContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(eWalletContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Grants.AddOrUpdate( g => g.Title,
                new Grant
                {
                    Title = "FG Grant",
                    Description = "for setting up Farm",
                    MonetaryWorth = 1200000,
                    Products = new[]{
                        new Product { Title = "Fertilizers", Code = "Biological Product", Amount = 2400 }
                    }
                },
                new Grant{
                    Title ="FG Grant UNESCO",Description = "for setting up Farm",
                    MonetaryWorth =4800000,
                    Products = new[]{
                        new Product { Title = "Fertilizers", Code = "Biological Product", Amount = 1200 }
                    }
                },
                new Grant{Title="Delta State Grant",Description = "for setting up Farm",
                    MonetaryWorth =600000,
                    Products = new[]{
                        new Product { Title = "Fertilizers", Code = "Biological Product", Amount = 1200 }
                    }
                },
                new Grant{Title="Green River Grant",Description = "for setting up Farm",
                    MonetaryWorth =8000000,
                    Products = new[]{
                        new Product { Title = "Fertilizers", Code = "Biological Product", Amount = 2000 }
                    }
                },
                new Grant{Title="Feed the Nation Grant",Description = "for setting up Farm",
                    MonetaryWorth =5200000,
                    Products = new[]{
                        new Product { Title = "Fertilizers", Code = "Biological Product", Amount = 50000 }
                    }
                },
                new Grant{Title="Feedwell Grant",Description = "for setting up Farm",
                    MonetaryWorth =2000000,
                    Products = new[]{
                        new Product { Title = "Fertilizers", Code = "Biological Product", Amount = 7000 }
                    }
                }
            );
            //context.SaveChanges();

            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roleNames = new[]
            {
                nameof(UserType.Admin),nameof(UserType.Agent),nameof(UserType.Farmer)
            };

            foreach(var roleName in roleNames)
            {
                if (rm.RoleExists(roleName))
                {
                    continue;
                }
                var role = new IdentityRole { Name = roleName };
                rm.Create(role);
            }

            var userMan = new ApplicationUserManager(new UserStore<User>(context));
            if (!context.Farmers.Any())
            {
                var farmers = new List<Farmer>
                {
                    new Farmer{UserRole=UserType.Farmer, UserName="ojo", Email="ojoa@agroaid.com", FarmName= "Ojo and Sons", FirstName="Ojo",OtherName="Bantun Olalode", Type= FarmerType.Individual , RegistrationDate = DateTime.Parse("2017-06-23"),PhoneNumber = "08054512145", Region= Region.SouthWest, State= State.Oyo, Status=MembershipStatus.Pending},
                    new Farmer{UserRole=UserType.Farmer,UserName="Obisluck", Email="Obisluck@agroaid.com",FarmName= "Obisluck", FirstName="John ",OtherName="Butlers Bola", Type= FarmerType.Coporative , RegistrationDate = DateTime.Parse("2017-06-23"),PhoneNumber = "08054514345", Region= Region.SouthEast, State= State.Anambra, Status=MembershipStatus.Pending},
                    new Farmer{UserRole=UserType.Farmer,UserName="albarka", Email="albarka@agroaid.com",FarmName= "Albarka", FirstName="Thomas ",OtherName="Banjo Kotokoto", Type= FarmerType.Company , RegistrationDate = DateTime.Parse("2017-06-23"),PhoneNumber = "08054514345", Region= Region.SouthSouth, State= State.Delta, Status=MembershipStatus.Pending},
                    new Farmer{UserRole=UserType.Farmer,UserName="konto", Email="konto@agroaid.com",FarmName= "Konto", FirstName="Joseph",OtherName="Clarence Bokina", Type= FarmerType.Individual , RegistrationDate = DateTime.Parse("2017-06-23"),PhoneNumber = "08035467863", Region= Region.NorthCentral, State= State.Nasarawa, Status=MembershipStatus.Pending},
                    new Farmer{UserRole=UserType.Farmer,UserName="flickery", Email="flickery@agroaid.com",FarmName= "Flickery", FirstName="Bantun",OtherName="Clara Borno", Type= FarmerType.Individual , RegistrationDate = DateTime.Parse("2017-06-23"),PhoneNumber = "08035467863", Region= Region.NorthWest, State= State.Kaduna, Status=MembershipStatus.Pending}
                };

                farmers.ForEach(f => {
                    var res = userMan.CreateAsync(f, "farmer").Result;
                    if (res == IdentityResult.Success)
                    {
                        userMan.AddToRole(f.Id, nameof(UserType.Farmer));
                    }
                });
            }

            if (!context.Agents.Any())
            {
                var agents = new List<Agent>
                {
                    new Agent {UserRole=UserType.Agent,UserName="bonnola", Email="bonnola@agroaid.com",Name="Bojor Bann Ola",Description = "I supply any thing agric supplies but I give only Fertilizers here", ContactName="Bojo ", ContactPhone="08054521365",RegistrationDate=DateTime.Parse("2017-06-23"), State= State.AkwaIbom, Status=MembershipStatus.Pending ,  Region=Region.SouthSouth},
                    new Agent {UserRole=UserType.Agent,UserName="bojorola", Email="bojorola@agroaid.com",Name="Bojor Bann Ola",Description = "I supply any thing agric supplies but I give only Fertilizers here", ContactName="Bojo ", ContactPhone="08054521365",RegistrationDate=DateTime.Parse("2017-06-23"), State= State.AkwaIbom, Status=MembershipStatus.Pending ,  Region=Region.SouthSouth},
                    new Agent {UserRole=UserType.Agent,UserName="bojor", Email="bojor@agroaid.com",Name="Bojor Bann Ola",Description = "I supply any thing agric supplies but I give only Fertilizers here", ContactName="Bojo ", ContactPhone="08054521365",RegistrationDate=DateTime.Parse("2017-06-23"), State= State.AkwaIbom, Status=MembershipStatus.Pending ,  Region=Region.SouthSouth},
                    new Agent {UserRole=UserType.Agent,UserName="bann", Email="bann@agroaid.com",Name="Bojor Bann Ola",Description = "I supply any thing agric supplies but I give only Fertilizers here", ContactName="Bojo ", ContactPhone="08054521365",RegistrationDate=DateTime.Parse("2017-06-23"), State= State.AkwaIbom, Status=MembershipStatus.Pending ,  Region=Region.SouthSouth}
                };

                agents.ForEach(a => {
                    var res = userMan.CreateAsync(a, "agents").Result;
                    if (res == IdentityResult.Success)
                    {
                        userMan.AddToRole(a.Id, nameof(UserType.Agent));
                    }
                });
            }

            if (!context.Administrators.Any())
            {
                var admin = new List<Administrator>
                {
                    new Administrator {UserRole=UserType.Admin,UserName="james", Email="james@agroaid.com",Status=MembershipStatus.Approved },
                    new Administrator {UserRole=UserType.Admin,UserName="alexius", Email="alexius@agroaid.com",Status=MembershipStatus.Approved}
                };

                admin.ForEach(a => {
                    var res = userMan.CreateAsync(a, "ewallet").Result;
                    if (res == IdentityResult.Success)
                    {
                        userMan.AddToRole(a.Id, nameof(UserType.Admin));
                    }
                });
            }
        }
    }
}
