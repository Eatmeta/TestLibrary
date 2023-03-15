namespace Application.Common.Exceptions;

public class DublicateIsbnException : Exception
{
    public DublicateIsbnException(string isbn)
        : base($"This ISBN = {isbn} is already in the database. Please use another one.")
    {
    }
}