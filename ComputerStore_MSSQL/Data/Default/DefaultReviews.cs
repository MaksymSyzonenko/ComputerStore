using ComputerStore_MSSQL.Data.Entities;

namespace ComputerStore.DefaultData
{
    public static class DefaultReviews
    {
        public static List<ReviewEntity> Reviews = new()
        {
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[0].ToString(),
                ProductId = DefaultProducts.Ids[0],
                Rating = 3.5,
                Comment = "Непогана модель",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[0].ToString(),
                ProductId = DefaultProducts.Ids[3],
                Rating = 4,
                Comment = "Дуже задоволений покупкою",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[5].ToString(),
                ProductId = DefaultProducts.Ids[7],
                Rating = 5,
                Comment = "Відмінна якість",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[6].ToString(),
                ProductId = DefaultProducts.Ids[1],
                Rating = 2.5,
                Comment = "Незадовільно",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[2].ToString(),
                ProductId = DefaultProducts.Ids[1],
                Rating = 4.5,
                Comment = "Чудовий вибір",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[3].ToString(),
                ProductId = DefaultProducts.Ids[3],
                Rating = 3,
                Comment = "Зручний та функціональний",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[4].ToString(),
                ProductId = DefaultProducts.Ids[2],
                Rating = 4.5,
                Comment = "Все працює як очікувалося",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[4].ToString(),
                ProductId = DefaultProducts.Ids[6],
                Rating = 3.5,
                Comment = "Непогана ціна за якість",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[4].ToString(),
                ProductId = DefaultProducts.Ids[4],
                Rating = 2,
                Comment = "Не задоволений обслуговуванням",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[5].ToString(),
                ProductId = DefaultProducts.Ids[11],
                Rating = 5,
                Comment = "Рекомендую всім!",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[3].ToString(),
                ProductId = DefaultProducts.Ids[11],
                Rating = 3,
                Comment = "Зручний та функціональний",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[4].ToString(),
                ProductId = DefaultProducts.Ids[11],
                Rating = 4.5,
                Comment = "Все працює як очікувалося",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[7].ToString(),
                ProductId = DefaultProducts.Ids[6],
                Rating = 3.5,
                Comment = "Непогана ціна за якість",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[7].ToString(),
                ProductId = DefaultProducts.Ids[19],
                Rating = 2,
                Comment = "Не задоволений обслуговуванням",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[7].ToString(),
                ProductId = DefaultProducts.Ids[19],
                Rating = 5,
                Comment = "Рекомендую всім!",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[6].ToString(),
                ProductId = DefaultProducts.Ids[18],
                Rating = 3,
                Comment = "Зручний та функціональний",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[6].ToString(),
                ProductId = DefaultProducts.Ids[32],
                Rating = 4.5,
                Comment = "Все працює як очікувалося",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[9].ToString(),
                ProductId = DefaultProducts.Ids[36],
                Rating = 3.5,
                Comment = "Непогана ціна за якість",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[9].ToString(),
                ProductId = DefaultProducts.Ids[14],
                Rating = 2,
                Comment = "Не задоволений обслуговуванням",
                ReviewDate = DateTime.Now
            },
            new ReviewEntity
            {
                ReviewId = Guid.NewGuid(),
                UserId = DefaultUsers.Ids[8].ToString(),
                ProductId = DefaultProducts.Ids[20],
                Rating = 5,
                Comment = "Рекомендую всім!",
                ReviewDate = DateTime.Now
            }
        };
    }
}
