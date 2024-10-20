using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
public class DBConnection
{
    private static string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=VirtualArtGalleryDB;Integrated Security=True;Trust Server Certificate=True";

    public static SqlConnection GetConnection()
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}
