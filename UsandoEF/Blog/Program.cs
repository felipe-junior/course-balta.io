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
        selectTagById(context, 3);
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