# How to create a Blazor Web App for SQL Server CRUD

## 1. Run a Docker Container with SQL Server

Install and run Docker Desktop

Open a command prompt window and run this command:


![image](https://github.com/user-attachments/assets/8fdf1a18-5dd9-4ef7-b127-0f8b198c866a)

## 2. Connect to SQL Server Management Studio (SSMS)



## 3. Create the database 



## 4. Create the database table



## 5. Seed the database with data



## 6. Create a Blazor Web Application in Visual Studio 2022 Community Edition

Run Visual Studio and create a new project

![image](https://github.com/user-attachments/assets/000ec328-1122-444b-b5cb-c0316d84b5a8)

Search for Blazor project templates and select Blazor Web App and press the next button

![image](https://github.com/user-attachments/assets/f7411d00-90a4-4e57-b3a0-0add4144d5fb)

Input the project name and chose the project location in the hard disk and press the next button 

![image](https://github.com/user-attachments/assets/97fe1efd-c3c0-45b8-87e2-b51605d00912)

Leave all the default values and press the Create button

![image](https://github.com/user-attachments/assets/d580a161-0bef-4154-a4fe-3b724ed19f3f)

## 7. Add the Nuget packages

Install the required NuGet packages:



## 8. Create two new folders Data and Services



## 9. Add the ApplicationDbContext

Create a new c# class in the Data folder

![image](https://github.com/user-attachments/assets/6ece1384-4837-4ff3-a666-9acdd3458d29)

We input the source code for the ApplicationDbContext.cs



## 10. Add the Data Model 

Create the Product class



## 11. Add the Service

Add the ProductService.cs file 

![image](https://github.com/user-attachments/assets/a34c83b0-ef05-4a72-9338-bee03bcdc2b9)

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



