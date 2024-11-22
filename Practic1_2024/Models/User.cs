using System;
using System.Collections.Generic;

namespace Practic1_2024.Models
{
    public class User
    {
        // Уникальный идентификатор пользователя
        public int id { get; set; }

        // Имя пользователя
        public string UserName { get; set; }

        // Электронная почта
        public string Email { get; set; }

        // Пароль (не рекомендуется хранить в открытом виде, это для примера)
        public string Password { get; set; }

        // Токен для сброса пароля (если нужно)
        public string? PasswordResetToken { get; set; }

        // Статус пользователя
        public short Status { get; set; } = 10;

        // Дата и время создания пользователя (например, Unix timestamp)
        public int created_at { get; set; }

        // Навигационное свойство для отзывов
        public ICollection<Review> Reviews { get; set; }

        // Навигационное свойство для заказов
        public ICollection<Order> Orders { get; set; }
    }
}
