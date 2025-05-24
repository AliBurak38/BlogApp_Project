using Microsoft.EntityFrameworkCore;
using NewBlogApp.Entitiy;

namespace NewBlogApp.Data.Concrete.EFCore
{
    public static class SeedData
    {

        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
          var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
            if ((context!=null))
            {
                if(context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
           if (!context.Tags.Any())
            {

                context.Tags.AddRange(

                    new Tag { Text = "Web programlama",Url= "Web programlama",Color=TagColors.primary},
                    new Tag { Text = "Backend",Url="Backend", Color = TagColors.secondary },
                    new Tag { Text = "Frontend" ,Url="Frontend",Color=TagColors.info},
                    new Tag { Text = "Fullstack",Url="Fullstack",Color=TagColors.success }
                    
                    );
                context.SaveChanges();
           

                if (!context.Users.Any())
                {
                    context.Users.AddRange(

                     new User { UserName = "Ali Burak Öztürk",Name="Ali Burak",Email="ali@mail.com",Password="123466",Image="suat.png"},
                     new User { UserName = "İbrahim Öztürk", Name = "İbrahim", Email = "ibrahim@mail.com", Password = "123456",Image ="cicikus.jpg" }


                    );
                    context.SaveChanges();

                }
                if (!context.Posts.Any())
                {
                    context.Posts.AddRange(
                     new Post
                     {
                         Title = "Asp.Net Core ",
                         Content = "Asp.Net Core Dersleri",
                         Description = "Asp.Net Core ile ilgili tüm bilgileri öğretir.",
                         Url = "Asp.Net Core",
                         IsActive = true,
                         Image = "aspnet.jpg",
                         PublishedOn = DateTime.Now.AddDays(-10),
                         Tags = context.Tags.Take(3).ToList(),
                         UserId = 1,
                         Comments= new List<Comment> {
                             new Comment { Text="Çok faydalı bir kurs kendimi geliştirdim",PublishedOn= DateTime.Now.AddDays(-10),UserId=1},
                             new Comment { Text="Gayet iyi bir kurs ",PublishedOn= DateTime.Now.AddDays(-5),UserId=2},
                         }
                     },
                     new Post
                     {
                         Title = "Php Dersleri",
                         Content = "Php hakkında tüm bilgileri öğretir.",
                         Description = "Php ile ilgili tüm bilgileri öğretir.",
                         Url = "Php Ders",
                         IsActive = true,
                         Image = "php.jpg",
                         PublishedOn = DateTime.Now.AddDays(-15),
                         Tags = context.Tags.Take(2).ToList(),
                         UserId = 2


                     },
                     new Post
                     {
                         Title = "Django Dersleri",
                         Content = "Django  Dersleri hakkında bilgiler verir.",
                         Description = "Django ile ilgili tüm bilgileri öğretir.",
                         Url = "Django Ders",
                         IsActive = true,
                         Image = "backend.jpg",
                         PublishedOn = DateTime.Now.AddDays(-20),
                         Tags = context.Tags.Take(4).ToList(),
                         UserId = 1

                     },


                      new Post
                      {
                          Title = "Web Tasarım Dersleri",
                          Content = "Web Tasarımı ile ilgili temel düzeyde bilgilere hakim olursunuz",
                          Description = "Web Tasarım ile ilgili temel düzeyde bilgilere hakim olursunuz",
                          Url = "Web Tasarımı",
                          IsActive = true,
                          Image = "webtasarım.jpg",
                          PublishedOn = DateTime.Now.AddDays(-4),
                          Tags = context.Tags.Take(4).ToList(),
                          UserId = 1

                      }
                       
                     );
                    context.SaveChanges();
                }
            }
        }
    }
}
