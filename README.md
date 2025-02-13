**Build/Install dependencies**

- Run command dotnet build from the project root folder
- Run command dotnet ef migrations add InitialCreateNew
- Run dotnet ef database update

After the above steps, you should have an SQLite DB named AccountsManager.db at the project root

Next Steps
- Install Sqlite browser to open the database
- Run dotnet start
- This should start the API at port http://localhost:5033
- Use Postman in the backend folder to test the endPoints
- Hit CRUD endpoints to populate DB
