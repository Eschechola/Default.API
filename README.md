# Default API

Welcome to the **Default Web API**! This API provides features for managing data, including user authentication, CRUD operations, and more. It's built with **.NET 9** Web API and designed for scalability and ease of integration.

## Table of Contents

1. [Project Overview](#project-overview)
2. [Technology Stack](#technology-stack)
3. [Setup Instructions](#setup-instructions)
4. [Development Commands](#development-commands)
5. [Running the Application](#running-the-application)
6. [Contributing](#contributing)

---

## Project Overview

This project is a **Default API** developed with **.NET Core 9**. It provides endpoints for managing entities, user authentication, and other features.

Key features:
- CRUD operations for blog posts.
- User authentication with JWT (JSON Web Tokens).
- Database migrations and data management.
- Easily extendable and configurable.

---

## Technology Stack

- **Backend Framework**: .NET 9
- **Database**: SQL Server
- **Authentication**: JWT (JSON Web Tokens)
- **Password Hash**: BCrypt
- **ORM**: Entity Framework Core
- **Containerization**: Docker
- **API Documentation**: Swagger
- **Testing**: xUnit

---

## Setup Instructions

### Docker Setup

To run SQL Server in a Docker container, use the following command to start the SQL Server instance.

### Database Configuration

Follow the steps to configure the application with the correct database connection string and other settings.

---

## Development Commands


#### 1. Run Database <br>
```bash
 $ docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YOUR_PASSWORD>" -p 1433:1433 -d --name sqlserverfts sqlserver-fts
 ```

#### 2. Set Secrets <br>

```bash
$ dotnet user-secrets set "ConnectionStrings:SqlServer" "Server=127.0.0.1;User Id=sa;Password=<YOUR_PASSWORD>;Database=Default;TrustServerCertificate=True"

$ dotnet user-secrets set "Hash:Salt" "<YOUR_SALT>"

$ dotnet user-secrets set "Jwt:Secret" "<YOUR_SECRET>"

$ dotnet user-secrets set "Authorization:Basic:Password" "<BASIC_PASSWORD>"
```

#### 3. Run EF Migrations <br>
```bash
dotnet ef migrations add InitialMigration --project src/Default.Infrastructure/Default.Infrastructure.csproj

dotnet ef database update --connection "Server=127.0.0.1;User Id=sa;Password=<YOUR_PASSWORD>;Database=Default;TrustServerCertificate=True" --context DefaultContext --project src/Default.Infrastructure/Default.Infrastructure.csproj
```

---

## Running the Application

Once you've set up the environment, you can run the application locally.

1. Build and run the application.

2. Access the API documentation via Swagger at: <a>https://localhost:7117/swagger</a>

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

<br>
<p align="center">2025&copy;</p>
