﻿using System.Collections.Generic;

namespace System.Linq.Dynamic.Core.Tests.Helpers.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public int? NullableInt { get; set; }

        public int Income { get; set; }

        public UserProfile Profile { get; set; }

        public List<Role> Roles { get; set; }

        public bool TestMethod(User other)
        {
            return true;
        }

        public static IList<User> GenerateSampleModels(int total, bool allowNullableProfiles = false)
        {
            var list = new List<User>();

            for (int i = 0; i < total; i++)
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "User" + i,
                    Income = 1 + (i % 15) * 100
                };

                if (!allowNullableProfiles || (i % 8) != 5)
                {
                    user.Profile = new UserProfile
                    {
                        FirstName = "FirstName" + i,
                        LastName = "LastName" + i,
                        Age = (i % 50) + 18
                    };
                }

                user.Roles = new List<Role>(Role.StandardRoles);

                list.Add(user);
            }

            return list.ToArray();
        }
    }
}