// Step 1: Set start up project to EFMigrator project
// Step 2: Open Package Manager Console and set Persistance as defualt project
// Step 3: Run this -> add-migration Initial -Context Persistence.Common.DataContexts.ApplicationDbContext -Verbose

using Persistence.Common.DataContexts;

Console.WriteLine("Migrator running..");

using (var blogContext = new ApplicationDbContext())
{
    var all = blogContext.Users.ToList();
}
