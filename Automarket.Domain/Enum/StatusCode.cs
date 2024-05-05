namespace Automarket.Domain.Enum
{
    public enum StatusCode
    {
        OK = 200,
        InternalServerError = 500,
        //Car
        CarNotFound = 900,
        WrongPageNumber = 901,
        //User
        UsernameIsNotUnique = 1000,
        InvalidPassword = 1001,
        InvalidUsername = 1002,

    }
}
