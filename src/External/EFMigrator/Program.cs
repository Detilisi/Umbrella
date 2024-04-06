// See https://aka.ms/new-console-template for more information
/*
 * add-migration Initial -Context MauiPersistence.Common.DataContexts.ApplicationDbContext -Verbose
 */

using MauiPersistence.Common.DataContexts;;

Console.WriteLine("Migrator running..");

using (var blogContext = new ApplicationDbContext())
{
    var all = blogContext.Users.ToList();
}
