using System;

public class DBConnection
{
    private static string connectionString = "your-database-connection-string";

    public static SqlConnection GetConnection()
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}
