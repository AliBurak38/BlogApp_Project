using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using NewBlogApp.Data.Abstractt;
using NewBlogApp.Data.Concrete.EFCore;
using System;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BlogContext>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("sql_connection");
    options.UseSqlite(connectionString);

});




builder.Services.AddScoped<IPostRepository, EFPostsRepository>();
builder.Services.AddScoped<ITagRepository, EFTagRepository>();
builder.Services.AddScoped<ICommentRepository, EFCommentRepository>();
builder.Services.AddScoped<IUserRepository, EFUserRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


SeedData.TestVerileriniDoldur(app);

app.MapControllerRoute(
    name: "Posts_Details",
    pattern: "Posts/Details/{Url}",
    defaults: new { controller = "Posts", action="Details" }

  );
app.MapControllerRoute(
    name: "Posts_by_Tags",
    pattern: "Posts/Tag/{tag}",
    defaults: new { controller = "Posts", action = "Index" }

  );

app.MapControllerRoute(
    name: "user_profile",
    pattern: "profile/{username}",
    defaults: new { controller = "Users", action = "Profile" }

  );


app.MapControllerRoute(
    name: "default",
    pattern:"{controller=Home}/{action=Index}/{id?}"
    

    );




app.Run();

