// See https://aka.ms/new-console-template for more information
using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
var ctx = new BlogDataContext();

using (var context = new BlogDataContext()){

    try{
        // createTag(context);
        // updateTag(context, 3);
        // deleteTag(context, 1);
        // selectAllTags(context);
        // selectTagById(context, 3);
        // subInsert(context);
        selectAllPostsOrderBy(context);
        // SubsetUpdate(context);

    }
    catch (Exception e){

    }

}

static void createTag(BlogDataContext context){
    var tag = new Tag {Name= "Asp.Net", Slug="aspnet-novo"};
    context.Tags.Add(tag);
    context.SaveChanges();
}

static void updateTag(BlogDataContext context, int id){
    var tag = context.Tags.FirstOrDefault(tag => tag.Id == id);
    if(tag is not null){
        tag.Name = ".NET";
        tag.Slug  = "dotnet";
        context.Tags.Update(tag);
        context.SaveChanges();
    } else{
        System.Console.WriteLine("Não encontrou");
    }
}

static void deleteTag(BlogDataContext context, int id){
    var tag = context.Tags.FirstOrDefault(tag => tag.Id == id);
    if(tag is not null){
        context.Tags.Remove(tag);
        context.SaveChanges();
    }
    else
        System.Console.WriteLine("Nao encontrou");
}
static void selectAllTags(BlogDataContext context){
    var tags = context
        .Tags
        .AsNoTracking() // Ganho de perfomance se não atualizar ou inserir um item no banco, em read only nao precisa
        .Where(tag => tag.Name.Contains(".NET"))
        .ToList();
    
    foreach (var tag in tags)
    {
        System.Console.WriteLine(tag.Name);
    }
}

static void selectTagById(BlogDataContext context, int id){
    var tag = context
    .Tags
    .AsNoTracking()
    .FirstOrDefault(tag => tag.Id == id); // SingleOrDefault(tag => tag.Id == id);

    System.Console.WriteLine(tag?.Name);
}

static void subInsert(BlogDataContext context){
    var user = new User{
            Name= "Felipe",
            Bio= "Mero estudante do cefet",
            Email= "felipe@gmail.com",
            Image="https://teste.io",
            PasswordHash="123456789",
            Slug= "felipesantos"
        }; 

        var category = new Category{
            Name = "Backend",
            Slug = "Backend"
        };
        var post = new Post{
            Author = user,
            Category = category,
            Body = "<p> Hello world </p>",
            Slug = "comecando-com-ef-core",
            Summary = "Neste artigo vamos aprender EF Core",
            Title = "Começando com EF Core",
            CreateDate = DateTime.Now,
            LastUpdateDate = DateTime.Now
        };

        context.Posts.Add(post);
        context.SaveChanges();
}

static void selectAllPostsOrderBy(BlogDataContext context){
    int id= 1;
    var posts = context.Posts
    .AsNoTracking()
    .Include(post=>post.Author) // JOIN, evitar usar um thenInclude após o include por ele fazer subselect
    .Include(post => post.Author)
    .Where(post=> post.AuthorId == id) //Nao vai fazer o join na tabela User porque o authorId está na tabela post
    // .Where(post => post.Author.Slug =="") // Faz o join POST X USER pra pegar o slug
    .OrderByDescending(post => post.LastUpdateDate)
    .ToList();

    foreach (var post in posts)
    {
        System.Console.WriteLine(post.Author?.Name);
    }
}

static void SubsetUpdate(BlogDataContext context){
    var post = context.Posts
    .Include(post => post.Author)
    .Include(post=> post.Category)
    .FirstOrDefault();

    post.Category.Name = "TESTE";

    context.Posts.Update(post);
    context.SaveChanges();
}