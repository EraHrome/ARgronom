using System.ComponentModel;

namespace ARgronom.Models
{
    public enum Role
    {
        [Description("Неавторизованный пользователь")]
        None,
        [Description("Пользователь")]
        Customer,
        [Description("Продавец")]
        Seller,
        [Description("Модератор")]
        Moderator,
        [Description("Администратор")]
        Admin

    }
}
