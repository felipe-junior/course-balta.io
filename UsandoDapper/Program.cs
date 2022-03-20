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
    // ExecuteProcedure(connection);
    // ReadView(connection);
    // OneToOne(connection);
    // OneToMany(connection);
    // QueryMultiple(connection);
    // SelectIn(connection);
    queryWithTransaction(connection);
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

static void OneToOne (SqlConnection connection){
    var sql = @"
    SELECT *
    FROM
        [CareerItem]
    INNER JOIN
        [Course] ON [CareerItem].[CourseId] = [Course].[Id]
    ";
    var careerItems = connection.Query<CareerItem, Course, CareerItem>(
        sql, 
        (careerItem, course)=>{
                careerItem.course = course;
                return careerItem;
        }
    ,   splitOn:"Id");

    foreach (var careerItem in careerItems)
    {
        System.Console.WriteLine($"CareerItemId: {careerItem.CareerId}");
        System.Console.WriteLine("CareerItemTitle: " + careerItem.Title);
        System.Console.WriteLine(careerItem.course.Id);
        System.Console.WriteLine(careerItem.course.Title);
        System.Console.WriteLine("----------");
    }
}

static void OneToMany (SqlConnection connection){
    var sql = @"
    SELECT
        [Career].[Id],
        [Career].[Title],
        [CareerItem].[CareerId],
        [CareerItem].[Title]
    FROM
        [Career]
    INNER JOIN
        [CareerItem] ON [CareerItem].[CareerId] = [Career].[Id]
    ORDER BY
        [Career].[Title]
    ";

    var careers = new List<Career>();
    var careersListFromDb = connection.Query<Career, CareerItem, Career>(sql, (career, careerItem)=>{
        
        var careerTemp =  careers.Where(x => x.Id == career.Id).FirstOrDefault();
        
        if (careerTemp is null){
            career.CareerItems.Add(careerItem);
            careers.Add(career);
        } else {
            careerTemp.CareerItems.Add(careerItem);
        }
        return career;
    }, splitOn:"CareerId");

    foreach (var career in careers)
    {
        System.Console.WriteLine(career.Title);
        foreach (var careerItems in career.CareerItems)
        {
            System.Console.WriteLine(careerItems.Title);
        }
        System.Console.WriteLine("------------");
    }
}
static void QueryMultiple (SqlConnection connection){
    var sql= "SELECT * FROM [Category]; SELECT * FROM [Course]";
    using(var multiple = connection.QueryMultiple(sql)){
        var categories = multiple.Read<Category>();
        var courses = multiple.Read<Course>();

        foreach (var item in categories)
        {
            System.Console.WriteLine(item.Title);
        }

        foreach (var item in courses)
        {
            System.Console.WriteLine(item.Title);
        }
    }
}

static void SelectIn (SqlConnection connection){

    var query = @"SELECT * FROM [Career] where [Id] IN @Id";
    var careers = connection.Query<Career>(query, new {
        Id= new []{
            "01ae8a85-b4e8-4194-a0f1-1c6190af54cb",
            "e6730d1c-6870-4df3-ae68-438624e04c72"
        }
    });
    foreach (var item in careers)
    {
        System.Console.WriteLine(item.Title);
    }
}

static void queryWithTransaction (SqlConnection connection){
    
    var category = new Category();

    category.Id = Guid.NewGuid();
    category.Title = "NOVA CATEGORIA";
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
    
    using (var transaction = connection.BeginTransaction()){
        var rows = connection.Execute(command, new
        {
            Id = category.Id, //nomear os parametros caso mude no comando
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        },transaction);
        transaction.Commit();
        Console.WriteLine($"{rows} linhas inseridas");

    }
}