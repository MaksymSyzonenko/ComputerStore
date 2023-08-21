using ComputerStore.Models;
using ComputerStore.Services;
using System.Security.Permissions;

namespace ComputerStore.DefaultData
{
    public static class DefaultUsers
    {
        public static List<Guid> IDs = new()
        {
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
        };

        public static List<User> Users = new()
        {
            new User
            {
                Id = IDs[0].ToString(),
                FirstName = "Олег",
                LastName = "Шевчук",
                UserName = "OlegS",
                Email = "olegshevchuk@gmail.com"
            },
            new User
            {
                Id = IDs[1].ToString(),
                FirstName = "Іван",
                LastName = "Петренко",
                UserName = "IvanP",
                Email = "ivanpetrenko@example.com"
            },
            new User
            {
                Id = IDs[2].ToString(),
                FirstName = "Марія",
                LastName = "Ковальчук",
                UserName = "MariaK",
                Email = "mariakov@gmail.com"
            },
            new User
            {
                Id = IDs[3].ToString(),
                FirstName = "Олександр",
                LastName = "Сидоренко",
                UserName = "OleksandrS",
                Email = "oleksandrsidor@example.com"
            },
            new User
            {
                Id = IDs[4].ToString(),
                FirstName = "Анна",
                LastName = "Василенко",
                UserName = "AnnaV",
                Email = "annavasil@example.com"
            },
            new User
            {
                Id = IDs[5].ToString(),
                FirstName = "Юрій",
                LastName = "Коваленко",
                UserName = "YuriyK",
                Email = "yuriykovalenko@example.com"
            },
            new User
            {
                Id = IDs[6].ToString(),
                FirstName = "Софія",
                LastName = "Мельник",
                UserName = "SofiaM",
                Email = "sofiamelnik@example.com"
            },
            new User
            {
                Id = IDs[7].ToString(),
                FirstName = "Максим",
                LastName = "Іванов",
                UserName = "MaximI",
                Email = "maximivanov@example.com"
            },
            new User
            {
                Id = IDs[8].ToString(),
                FirstName = "Аліна",
                LastName = "Коваленко",
                UserName = "AlinaK",
                Email = "alinakovalenko@example.com"
            },
            new User
            {
                Id = IDs[9].ToString(),
                FirstName = "Павло",
                LastName = "Сидоров",
                UserName = "PavloS",
                Email = "pavlosidorov@example.com"
            },
            new User
            {
                Id = IDs[10].ToString(),
                FirstName = "Наталія",
                LastName = "Петрова",
                UserName = "NataliaP",
                Email = "nataliapetrova@example.com"
            }
        };
    }
}
