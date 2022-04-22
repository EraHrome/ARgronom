using System.ComponentModel;

namespace ARgronom.Models
{
    public enum Role
    {

        /// <summary>
        /// Пользователь
        /// </summary>
        [Description("Пользователь")]
        User,
        /// <summary>
        /// Продавец
        /// </summary>
        [Description("Продавец")]
        Seller,
        /// <summary>
        /// Модератор
        /// </summary>
        [Description("Модератор")]
        Moderator,
        /// <summary>
        /// Администратор
        /// </summary>
        [Description("Администратор")]
        Admin

    }
}
