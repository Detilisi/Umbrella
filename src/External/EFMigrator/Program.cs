// See https://aka.ms/new-console-template for more information

using MauiPersistence.Common.DataContexts;;

Console.WriteLine("Migrator running..");

using (var blogContext = new ApplicationDbContext())
{
    var all = blogContext.Users.ToList();
}
