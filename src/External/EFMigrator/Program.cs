// See https://aka.ms/new-console-template for more information

using MauiPersistance.Services;

Console.WriteLine("Migrator running..");

using (var blogContext = new LocalDatabase())
{
    var all = blogContext.Authors.ToList();
}
