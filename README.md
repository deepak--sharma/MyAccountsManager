**Install dependencies**

- dotnet add package Microsoft.EntityFrameworkCore.Sqlite
- dotnet add package  Microsoft.EntityFrameworkCore.Design
- FluentValidation.AspNetCore


Steps

- dotnet ef database update  it should create a SqlLite db named AccountsManager.db at the project root
- Install Sqlite browser to open the database
- Run dotnet build
- Run dotnet start
- This should start the API at port http://localhost:5033
- Use Postman to test the endPoints
