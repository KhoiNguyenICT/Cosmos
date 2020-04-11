using System;

namespace Cell.Cosmos.Exceptions
{
    public class InvalidSqlQueryException : Exception
    {
        public InvalidSqlQueryException(string sql) : base($"The query {sql} is not valid.")
        {
        }
    }
}