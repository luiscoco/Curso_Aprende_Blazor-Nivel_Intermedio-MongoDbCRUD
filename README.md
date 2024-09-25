# How to create a Blazor Web App with MongoDb CRUD

## 1. Run a MongoDb Docker Container 

Install and run Docker Desktop

![image](https://github.com/user-attachments/assets/a427ea65-7fd8-4973-ba77-7a701fc87142)

Verify first the Mongo image

![image](https://github.com/user-attachments/assets/62496ca6-c86d-4e23-a825-44fa4fe9c0b7)

Also verify the Mongo container is running properly

![image](https://github.com/user-attachments/assets/6e386354-e175-42d0-9007-876a8f44688a)

Open a command prompt window and run this command:

```
docker run -d -p 27017:27017 --name mongodb mongo
```

## 2. Connect to MongoDb running container

For accessing the MongoDb running docker container we run this command:

```
docker exec -it mongodb mongosh
```

## 3. Create the database 

Use the **use** command to switch to (or create) a database

MongoDB creates the database automatically when you first insert data into a collection

```
use BlazorAppDB
```

## 4. Create the database table and Seed the database with data

The collection is created when you first insert data

You can insert data into the Products collection like this:

```
db.Product.insertMany([
  {
    "ProductName": "Laptop",
    "Price": 1000.00,
    "Quantity": 10
  },
  {
    "ProductName": "Smartphone",
    "Price": 500.00,
    "Quantity": 50
  },
  {
    "ProductName": "Tablet",
    "Price": 300.00,
    "Quantity": 30
  },
  {
    "ProductName": "Headphones",
    "Price": 50.00,
    "Quantity": 100
  },
  {
    "ProductName": "Monitor",
    "Price": 200.00,
    "Quantity": 20
  }
])
```

## 5. Create a Blazor Web Application in Visual Studio 2022 Community Edition

Run Visual Studio and create a new project

![image](https://github.com/user-attachments/assets/000ec328-1122-444b-b5cb-c0316d84b5a8)

Search for Blazor project templates and select Blazor Web App and press the next button

![image](https://github.com/user-attachments/assets/f7411d00-90a4-4e57-b3a0-0add4144d5fb)

Input the project name and chose the project location in the hard disk and press the next button 

![image](https://github.com/user-attachments/assets/97fe1efd-c3c0-45b8-87e2-b51605d00912)

Leave all the default values and press the Create button

![image](https://github.com/user-attachments/assets/d580a161-0bef-4154-a4fe-3b724ed19f3f)

## 6. Add the Nuget packages

Install the required NuGet packages:

```
dotnet add package MongoDB.Driver
```

## 7. Create two new folders Data and Services

![image](https://github.com/user-attachments/assets/691a9841-16e7-4f51-a2b1-72908b11a4f5)

## 8. Add the MongoDbContext

Create a new c# class in the Data folder

![image](https://github.com/user-attachments/assets/ed019bbc-1b7e-43d0-8e75-1f3c76d254f5)

We input the source code for the MongoDbContext.cs

```csharp
using MongoDB.Driver;

namespace BlazorApp2.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            _database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
        }

        public IMongoCollection<Product> Products
        {
            get { return _database.GetCollection<Product>(nameof(Product)); }
        }
    }
}
```

## 9. Add the Data Model 

Create the Product class

![image](https://github.com/user-attachments/assets/8f264457-71e7-49e4-aa6c-0a419df15686)

Input the source code

```csharp
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BlazorApp2.Data
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }  // MongoDB uses a string for its IDs
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
```

## 10. Add the Service

Add the ProductService.cs file 

![image](https://github.com/user-attachments/assets/ac322193-6694-4ac8-af7a-cffb883cf0ac)

Add the following source code

```csharp
using BlazorApp2.Data;
using MongoDB.Driver;

namespace BlazorApp2.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(MongoDbContext context)
        {
            _products = context.Products;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _products.Find(product => true).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _products.Find<Product>(product => product.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _products.DeleteOneAsync(product => product.Id == id);
        }
    }
}
```

## 11. Add the database connection string in the appsettings.json file

Edit the appsettings.json file and add the database connection string

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "MongoDB": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "BlazorAppDB",
    "CollectionName": "Products"
  },
  "AllowedHosts": "*"
}
```

## 12. Update the middleware (Program.cs)

Include the services:

```csharp
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<ProductService>();
```

This is the Program.cs whole code:

```csharp
using BlazorApp2.Components;
using BlazorApp2.Data;
using BlazorApp2.Services;

var builder = WebApplication.CreateBuilder(args);

// Register MongoDB context and services
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<ProductService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
```

## 13. Update the _Imports.razor 

This is the file to be uptdated

![image](https://github.com/user-attachments/assets/8fd63dce-f39a-4915-8007-04a0cc5b623e)

These are the refernces to include

```razor
@using System.Net.Http
@using System.Net.Http.Json
@using Microsoft.AspNetCore.Components.Forms
@using Microsoft.AspNetCore.Components.Routing
@using Microsoft.AspNetCore.Components.Web
@using static Microsoft.AspNetCore.Components.Web.RenderMode
@using Microsoft.AspNetCore.Components.Web.Virtualization
@using Microsoft.JSInterop
@using BlazorApp2
@using BlazorApp2.Components
@using BlazorApp2.Data
@using BlazorApp2.Services
```

## 14. Run the application and verify the results

![image](https://github.com/user-attachments/assets/20543437-555c-4bb1-950c-dd44b6c95a4a)


