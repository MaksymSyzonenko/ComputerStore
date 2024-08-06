using ComputerStore_MSSQL.Data.Entities;

namespace ComputerStore.DefaultData
{
    public static class DefaultUsers
    {
        public static List<Guid> Ids = new()
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

        public static List<UserEntity> Users = new()
        {
            new UserEntity
            {
                Id = Ids[0].ToString(),
                FirstName = "Олег",
                LastName = "Шевчук",
                UserName = "OlegS",
                Email = "olegshevchuk@gmail.com"
            },
            new UserEntity
            {
                Id = Ids[1].ToString(),
                FirstName = "Іван",
                LastName = "Петренко",
                UserName = "IvanP",
                Email = "ivanpetrenko@example.com"
            },
            new UserEntity
            {
                Id = Ids[2].ToString(),
                FirstName = "Марія",
                LastName = "Ковальчук",
                UserName = "MariaK",
                Email = "mariakov@gmail.com"
            },
            new UserEntity
            {
                Id = Ids[3].ToString(),
                FirstName = "Олександр",
                LastName = "Сидоренко",
                UserName = "OleksandrS",
                Email = "oleksandrsidor@example.com"
            },
            new UserEntity
            {
                Id = Ids[4].ToString(),
                FirstName = "Анна",
                LastName = "Василенко",
                UserName = "AnnaV",
                Email = "annavasil@example.com"
            },
            new UserEntity
            {
                Id = Ids[5].ToString(),
                FirstName = "Юрій",
                LastName = "Коваленко",
                UserName = "YuriyK",
                Email = "yuriykovalenko@example.com"
            },
            new UserEntity
            {
                Id = Ids[6].ToString(),
                FirstName = "Софія",
                LastName = "Мельник",
                UserName = "SofiaM",
                Email = "sofiamelnik@example.com"
            },
            new UserEntity
            {
                Id = Ids[7].ToString(),
                FirstName = "Максим",
                LastName = "Іванов",
                UserName = "MaximI",
                Email = "maximivanov@example.com"
            },
            new UserEntity
            {
                Id = Ids[8].ToString(),
                FirstName = "Аліна",
                LastName = "Коваленко",
                UserName = "AlinaK",
                Email = "alinakovalenko@example.com"
            },
            new UserEntity
            {
                Id = Ids[9].ToString(),
                FirstName = "Павло",
                LastName = "Сидоров",
                UserName = "PavloS",
                Email = "pavlosidorov@example.com"
            },
            new UserEntity
            {
                Id = Ids[10].ToString(),
                FirstName = "Наталія",
                LastName = "Петрова",
                UserName = "NataliaP",
                Email = "nataliapetrova@example.com"
            }
        };
    }
}
