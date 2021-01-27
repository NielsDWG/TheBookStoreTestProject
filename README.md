# TheBookStoreTestProject
Project to reproduce OData problem when expanding a list property on a list. For example: expand books for each author.

## How to run the application en reproduce the error

1. Open the project in Visual Studio
1. Change the connectionstring in appsettings.json
1. Run 'Update-Database' in the Package Manager Console
1. Start the application
1. Call the endpoint: https://{your_ip}:{your_port}/odata/authors?$expand=Books

## Software versions
- .NET 5
- Microsoft.AspNetCore.OData 8.0.0-preview3
- EF Core 5.0.2
