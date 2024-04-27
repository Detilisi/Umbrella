// See https://aka.ms/new-console-template for more information
/*
 * add-migration Initial -Context Persistence.Common.DataContexts.ApplicationDbContext -Verbose
 */

using Persistence.Common.DataContexts;

Console.WriteLine("Migrator running..");

using (var blogContext = new ApplicationDbContext())
{
    var all = blogContext.Users.ToList();
}
