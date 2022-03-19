using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;";

using (var connection = new SqlConnection(connectionString)){
    Console.WriteLine("Conectado");
    connection.Open();
    using(SqlCommand command = new SqlCommand()){
        command.Connection = connection;
        command.CommandType = System.Data.CommandType.Text;
        command.CommandText = "SELECT [Id], [Title] FROM [Category]";

        // var reader = command.ExecuteNonQuery(); //Para inserções
        var reader = command.ExecuteReader(); //Para leitura

        while(reader.Read()){ //cursor
            Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
        }
    
    }

}

