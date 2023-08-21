using ComputerStore.Models;

namespace ComputerStore.DefaultData
{
    public static class DefaultReviews
    {
        public static List<Review> Reviews = new List<Review>
        {
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[0].ToString(),
                ProductID = DefaultProducts.IDs[0],
                Rating = 3.5,
                Comment = "Непогана модель",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[0].ToString(),
                ProductID = DefaultProducts.IDs[3],
                Rating = 4,
                Comment = "Дуже задоволений покупкою",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[5].ToString(),
                ProductID = DefaultProducts.IDs[7],
                Rating = 5,
                Comment = "Відмінна якість",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[6].ToString(),
                ProductID = DefaultProducts.IDs[1],
                Rating = 2.5,
                Comment = "Незадовільно",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[2].ToString(),
                ProductID = DefaultProducts.IDs[1],
                Rating = 4.5,
                Comment = "Чудовий вибір",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[3].ToString(),
                ProductID = DefaultProducts.IDs[3],
                Rating = 3,
                Comment = "Зручний та функціональний",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[4].ToString(),
                ProductID = DefaultProducts.IDs[2],
                Rating = 4.5,
                Comment = "Все працює як очікувалося",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[4].ToString(),
                ProductID = DefaultProducts.IDs[6],
                Rating = 3.5,
                Comment = "Непогана ціна за якість",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[4].ToString(),
                ProductID = DefaultProducts.IDs[4],
                Rating = 2,
                Comment = "Не задоволений обслуговуванням",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[5].ToString(),
                ProductID = DefaultProducts.IDs[11],
                Rating = 5,
                Comment = "Рекомендую всім!",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[3].ToString(),
                ProductID = DefaultProducts.IDs[11],
                Rating = 3,
                Comment = "Зручний та функціональний",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[4].ToString(),
                ProductID = DefaultProducts.IDs[11],
                Rating = 4.5,
                Comment = "Все працює як очікувалося",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[7].ToString(),
                ProductID = DefaultProducts.IDs[6],
                Rating = 3.5,
                Comment = "Непогана ціна за якість",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[7].ToString(),
                ProductID = DefaultProducts.IDs[19],
                Rating = 2,
                Comment = "Не задоволений обслуговуванням",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[7].ToString(),
                ProductID = DefaultProducts.IDs[19],
                Rating = 5,
                Comment = "Рекомендую всім!",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[6].ToString(),
                ProductID = DefaultProducts.IDs[18],
                Rating = 3,
                Comment = "Зручний та функціональний",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[6].ToString(),
                ProductID = DefaultProducts.IDs[32],
                Rating = 4.5,
                Comment = "Все працює як очікувалося",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[9].ToString(),
                ProductID = DefaultProducts.IDs[36],
                Rating = 3.5,
                Comment = "Непогана ціна за якість",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[9].ToString(),
                ProductID = DefaultProducts.IDs[14],
                Rating = 2,
                Comment = "Не задоволений обслуговуванням",
                ReviewDate = DateTime.Now
            },
            new Review
            {
                ReviewID = Guid.NewGuid(),
                UserID = DefaultUsers.IDs[8].ToString(),
                ProductID = DefaultProducts.IDs[20],
                Rating = 5,
                Comment = "Рекомендую всім!",
                ReviewDate = DateTime.Now
            }
        };
    }
}
