namespace chronos.DAL.Enums;

public enum DbError
{
    // ПОДКЛЮЧЕНИЕ
    ConnectionTimeout,
    Authentication, // логин и/или пароль не подходят к БД
    Authorization, // у пользователя нет прав для работы с определенной таблицей
    
    // БАЗА ДАННЫХ
    SqlNotCorrect,
    Constraint, // нарушается констреинт
    NoAvailableResources, // у базы нет ресурсов для выполнения скрипта
}