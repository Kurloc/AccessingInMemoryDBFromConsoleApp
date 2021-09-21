## Steps to run

1. Make sure you have sqlite installed
   https://www.sqlite.org/download.html
2. restore the nuget packages for this project
3. Migrate the DB - Must run from a cmd in the project.
    1. example cmd path: C:\Users\Doomphx\RiderProjects\AccessingInMemoryDBFromConsoleApp
    1. dotnet ef migrations add InitialCreate --project AccessingInMemoryDBFromConsoleApp
    2. dotnet ef database update InitialCreate --project AccessingInMemoryDBFromConsoleApp
4. Your sqlite db should be created now, run the program and it will save 1 entry to your db. 
5. Connect to the sqlite file with some database reader.
   This works if you don't have one already. https://sqlitebrowser.org/
6. Now you have a working db connection.

## Next Steps
1. Change the ExampleDbModel to look like the data you want to save.
2. Once that's done rebuild the database using migrations.
   1. Delete the old migrations in AccessingInMemoryDBFromConsoleApp\Migrations
   2. Run the migration commands from above and it will recreate your database.