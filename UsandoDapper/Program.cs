using Dapper;
using Microsoft.Data.SqlClient;
using UsandoDapper.Models;

const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;";

using (var connection = new SqlConnection(connectionString)){
    
    connection.Open();
    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");

    foreach (var category in categories)
    {
        System.Console.WriteLine($"{category.Id} - {category.Title}");
    }

}

