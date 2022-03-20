using Dapper;
using Microsoft.Data.SqlClient;
using UsandoDapper.Models;

const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;";

using (var connection = new SqlConnection(connectionString))
{
    
    connection.Open();

    // Insert(connection);
    // ListCategories(connection);
    // UpdateCategory(connection);
    // CreateManyCategory(connection);
    ExecuteProcedure(connection);
    ReadView(connection);

}

static void Insert(SqlConnection connection)
{

    var category = new Category();

    category.Id = Guid.NewGuid();
    category.Title = "Amazon AWS";
    category.Url = "/amazon";
    category.Description = "Categoria dos serviços da aws";
    category.Order = 8;
    category.Summary = "AWS CLOUD";
    category.Featured = false;

    var command = @"INSERT INTO 
        [Category] 
    VALUES(
        @Id,
        @Title,
        @Url,
        @Summary,
        @Order,
        @Description, 
        @Featured
    )";
    var rows = connection.Execute(command, new
    {
        Id = category.Id, //nomear os parametros caso mude no comando
        category.Title,
        category.Url,
        category.Summary,
        category.Order,
        category.Description,
        category.Featured
    });
    Console.WriteLine($"{rows} linhas inseridas");
}

static void ListCategories(SqlConnection connection)
{

    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");

    foreach (var category in categories)
        System.Console.WriteLine($"{category.Id} - {category.Title}");
}

static void CreateManyCategory(SqlConnection connection)
{

    var category = new Category();
    category.Id = Guid.NewGuid();
    category.Title = "Amazon AWS 2";
    category.Url = "/amazon";
    category.Description = "Categoria dos serviços da aws";
    category.Order = 8;
    category.Summary = "AWS CLOUD";
    category.Featured = false;

    var category2 = new Category();
    category2.Id = Guid.NewGuid();
    category2.Title = "Amazon AWS 3";
    category2.Url = "/amazon";
    category2.Description = "Categoria dos serviços da aws";
    category2.Order = 9;
    category2.Summary = "AWS CLOUD";
    category2.Featured = false;

    var command = @"INSERT INTO 
        [Category] 
    VALUES(
        @Id,
        @Title,
        @Url,
        @Summary,
        @Order,
        @Description, 
        @Featured
    )";

    var rows = connection.Execute(command, new[]{
       new {
            Id = category.Id, //nomear os parametros caso mude no comando
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        },
        new {
            Id = category2.Id, //nomear os parametros caso mude no comando
            category2.Title,
            category2.Url,
            category2.Summary,
            category2.Order,
            category2.Description,
            category2.Featured
        }
    });
    System.Console.WriteLine($"Inseriu {rows} linhas");
}

static void UpdateCategory(SqlConnection connection)
{

    var updateQuery = "UPDATE [Category] SET [Title]=@title WHERE [Id] = @Id ";
    var rows = connection.Execute(updateQuery, new
    {
        Id = new Guid("1a2607ee-2535-4bef-a87c-742e8f7575f6"),
        Title = "Novo"
    });
    System.Console.WriteLine($"Atualizou {rows} linhas");
}

static void ExecuteProcedure(SqlConnection connection){
    var procedure = "sp_DeleteStudent";
    var parameters = new {StudentId = "79b82071-80a8-4e78-a79c-92c8cd1fd052"};
    var rows = connection.Execute(procedure, parameters, commandType:  System.Data.CommandType.StoredProcedure);
    System.Console.WriteLine($"Deletou {rows} linhas");
}

static void ReadView(SqlConnection connection){
    
    var sql = "SELECT * FROM [vwCourses]";
    // var courses = connection.Query<CourseDTO>(sql);
    var courses = connection.Query(sql); //anonimo object

    foreach (var course in courses)
    {   
        System.Console.WriteLine(course.Id);
        System.Console.WriteLine(course.Tag);
        System.Console.WriteLine(course.Title);
        System.Console.WriteLine(course.Url);
        System.Console.WriteLine(course.Summary);
        System.Console.WriteLine(course.Category);
        System.Console.WriteLine(course.Author);
        System.Console.WriteLine("-----------------");
    }
}
