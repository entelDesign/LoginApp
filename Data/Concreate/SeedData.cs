
using LoginApp.Data;
using Microsoft.EntityFrameworkCore;


public static class SeedData
{
    public static void TestVerileriniDoldur(IApplicationBuilder app)
    {
        var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<LoginContext>();
        if (context != null)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }



            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { UserName = "mustafa", AdSoyad = "Mustafa YILDIZ", Password = "pass1234", RoleId = 100 },
                    new User { UserName = "aliveli", AdSoyad = "Ali Veli", Password = "pass1234", RoleId = 0 }
                );
                context.SaveChanges();
            }


        }
    }
}