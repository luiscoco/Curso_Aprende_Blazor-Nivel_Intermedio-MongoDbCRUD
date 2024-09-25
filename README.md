# How to create a Blazor Web App with MongoDb CRUD

## 1. Run a MongoDb Docker Container 

Install and run Docker Desktop

![image](https://github.com/user-attachments/assets/a427ea65-7fd8-4973-ba77-7a701fc87142)

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

## 8. Create two new folders Data and Services



## 9. Add the ApplicationDbContext

Create a new c# class in the Data folder


We input the source code for the ApplicationDbContext.cs



## 10. Add the Data Model 

Create the Product class



## 11. Add the Service

Add the ProductService.cs file 



Add the following source code



## 12. Add the database connection string in the appsettings.json file

Edit the appsettings.json file and add the database connection string



## 13. Update the middleware (Program.cs)

Include the services:

```csharp

```

This is the Program.cs whole code:

```csharp

```

## 14. Update the _Imports.razor 

```

```

## 15. Run the application and verify the results



