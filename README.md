# Policy Management System

A comprehensive ASP.NET Core Web API application for managing insurance policies and user enrollments with role-based access control.

## 📋 Table of Contents

- [Features](#features)
- [Technology Stack](#technology-stack)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Database Setup](#database-setup)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [API Documentation](#api-documentation)
- [Project Structure](#project-structure)
- [Authentication](#authentication)
- [Testing](#testing)

## ✨ Features

### User Features
- User registration and authentication
- View available active policies
- Request policy enrollment
- View personal enrollment history

### Admin Features
- Create and manage policies
- Activate/deactivate policies
- View and manage enrollment requests
- Approve or reject enrollment requests

### Security & Validation
- JWT-based authentication
- Role-based authorization (User/Admin)
- Global exception handling with custom error responses
- Input validation on all endpoints
- Password hashing using BCrypt
- Email uniqueness validation
- Duplicate enrollment prevention

## 🛠️ Technology Stack

- **Framework**: .NET 10.0
- **Database**: PostgreSQL 
- **ORM**: Entity Framework Core 10.0.2
- **Authentication**: JWT (JSON Web Tokens)
- **Password Hashing**: BCrypt.Net
- **API Documentation**: Swagger/OpenAPI
- **Architecture**: Clean Architecture with Repository Pattern

## 📦 Prerequisites

Before you begin, ensure you have the following installed:

1. **.NET SDK 10.0 or later**
   - Download from: https://dotnet.microsoft.com/download
   - Verify installation: `dotnet --version`

2. **PostgreSQL 12 or later**
   - Download from: https://www.postgresql.org/download/
   - Ensure PostgreSQL service is running
   - Default port: 5432

3. **IDE (Choose one)**
   - Visual Studio 2022 (recommended)
   - Visual Studio Code with C# extension
   - JetBrains Rider

4. **Git**
   - Download from: https://git-scm.com/downloads

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/sravani-divami/c-.net-training.git
cd c-.net-training
git checkout capstone-project
cd policy/PolicyManagementSystem
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Database Setup

#### Create PostgreSQL Database

```sql
-- Open PostgreSQL command line or pgAdmin and run:
CREATE DATABASE "plicy-management-db";
```

#### Update Connection String

Edit `appsettings.json` and update the connection string with your PostgreSQL credentials:

```json
{
  "ConnectionStrings": {
    "policyManagementDbConnectionString": "Host=localhost;Port=5432;Database=plicy-management-db;Username=YOUR_USERNAME;Password=YOUR_PASSWORD"
  }
}
```

#### Run Database Migrations

```bash
# Create initial migration (if not exists)
dotnet ef migrations add InitialCreate

# Apply migrations to database
dotnet ef database update
```

### 4. JWT Configuration

The JWT settings are pre-configured in `appsettings.json`. For production, update the secret key:

```json
{
  "Jwt": {
    "Key": "YOUR_SECRET_KEY_AT_LEAST_32_CHARACTERS_LONG",
    "Issuer": "PolicyManagementSystem",
    "Audience": "PolicyManagementSystemUsers",
    "ExpiresInMinutes": 60
  }
}
```

## 🏃 Running the Application

### Using .NET CLI

```bash
# Development mode with hot reload
dotnet watch run

# Production mode
dotnet run
```

### Using Visual Studio

1. Open `PolicyManagementSystem.sln`
2. Press `F5` or click the "Run" button
3. Select `https` profile

### Access Points

- **API Base URL**: `https://localhost:5026` (or `http://localhost:5000`)
- **Swagger UI**: `https://localhost:5026/swagger`

## 📚 API Documentation

### Authentication APIs

#### Register User
```http
POST /api/auth/register
Content-Type: application/json

{
  "username": "johndoe",
  "email": "john@example.com",
  "password": "SecurePass123"
}
```

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "john@example.com",
  "password": "SecurePass123"
}

Response: { "token": "eyJhbGciOiJ..." }
```

### User Policy APIs (Requires Authentication)

#### Get Available Policies
```http
GET /api/policies
Authorization: Bearer {token}
```

#### Request Policy Enrollment
```http
POST /api/policies/{policyId}/enroll
Authorization: Bearer {token}
```

#### View My Enrollments
```http
GET /api/my/enrollments
Authorization: Bearer {token}
```

### Admin Policy APIs (Requires Admin Role)

#### Add Policy
```http
POST /api/admin/policies
Authorization: Bearer {admin_token}
Content-Type: application/json

{
  "policyNumber": "POL-001",
  "policyName": "Health Insurance",
  "description": "Comprehensive health coverage",
  "premiumAmount": 5000,
  "isActive": true
}
```

#### Update Policy
```http
PUT /api/admin/policies/{id}
Authorization: Bearer {admin_token}
Content-Type: application/json
```

#### Activate/Deactivate Policy
```http
PATCH /api/admin/policies/{id}/status
Authorization: Bearer {admin_token}
Content-Type: application/json

{
  "isActive": true
}
```

### Admin Enrollment APIs

#### View Pending Enrollments
```http
GET /api/admin/enrollments?status=Pending
Authorization: Bearer {admin_token}
```

#### Approve Enrollment
```http
POST /api/admin/enrollments/{id}/approve
Authorization: Bearer {admin_token}
```

#### Reject Enrollment
```http
POST /api/admin/enrollments/{id}/reject
Authorization: Bearer {admin_token}
```

## 📁 Project Structure

```
PolicyManagementSystem/
│
├── Controllers/              # API Controllers
│   ├── AuthController.cs
│   ├── PolicyController.cs
│   └── PolicyEnrollmentController.cs
│
├── Data/                     # Database Context
│   └── ApplicationDbContext.cs
│
├── DTOs/                     # Data Transfer Objects
│   ├── Auth/
│   ├── Policy/
│   └── PolicyEnrollment/
│
├── Entities/                 # Domain Models
│   ├── User.cs
│   ├── Policy.cs
│   └── PolicyEnrollment.cs
│
├── Exceptions/               # Custom Exceptions
│   ├── BaseException.cs
│   ├── NotFoundException.cs
│   ├── BadRequestException.cs
│   └── UnauthorizedException.cs
│
├── Filters/                  # Action Filters
│   ├── GlobalExceptionFilter.cs
│   └── ValidationFilter.cs
│
├── Repositories/             # Data Access Layer
│   ├── Interfaces/
│   └── Implementations/
│
├── Services/                 # Business Logic Layer
│   ├── Interfaces/
│   └── Implementations/
│
├── Utils/                    # Utility Classes
│   ├── JwtTokenGenerator.cs
│   └── IJwtTokenGenerator.cs
│
├── appsettings.json         # Application Configuration
└── Program.cs               # Application Entry Point
```

## 🔐 Authentication

### Using Swagger UI

1. Navigate to Swagger UI: `https://localhost:5026/swagger`
2. Register a new user via `/api/auth/register`
3. Login via `/api/auth/login` and copy the token
4. Click the **"Authorize"** button at the top
5. Enter: `Bearer {your-token}`
6. Click **"Authorize"** and close the dialog
7. All authenticated endpoints will now work

### Using Postman/Insomnia

1. Add header to your requests:
   ```
   Authorization: Bearer {your-token}
   ```

## 🧪 Testing

### Create Admin User

Since the registration creates users with "User" role by default, you need to manually update a user to Admin in the database:

```sql
UPDATE "Users" 
SET "Role" = 'Admin' 
WHERE "Email" = 'admin@example.com';
```

### Test Scenarios

1. **User Workflow**:
   - Register → Login → Get Policies → Enroll → View Enrollments

2. **Admin Workflow**:
   - Login as Admin → Create Policy → View Enrollments → Approve/Reject

## 🔧 Troubleshooting

### Database Connection Issues

**Error**: "Connection refused" or "Could not connect to server"
- Verify PostgreSQL is running: `sudo service postgresql status` (Linux) or check Services (Windows)
- Check connection string in `appsettings.json`
- Verify PostgreSQL is listening on port 5432

### Migration Issues

**Error**: "A connection was successfully established..."
```bash
# Reset migrations
dotnet ef database drop
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### JWT Authentication Issues

**Error**: "No authenticationScheme was specified"
- Ensure JWT configuration is in `appsettings.json`
- Verify `app.UseAuthentication()` is before `app.UseAuthorization()` in Program.cs

### Port Already in Use

**Error**: "Address already in use"
```bash
# Change port in launchSettings.json or kill the process
# Windows:
netstat -ano | findstr :5026
taskkill /PID {process_id} /F

# Linux/Mac:
lsof -i :5026
kill -9 {process_id}
```

## 📝 Environment Variables

For production, use environment variables instead of hardcoding secrets:

```bash
# Windows (PowerShell)
$env:ConnectionStrings__DefaultConnection="Host=..."
$env:Jwt__Key="YourSecretKey"

# Linux/Mac
export ConnectionStrings__DefaultConnection="Host=..."
export Jwt__Key="YourSecretKey"
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/your-feature`
3. Commit your changes: `git commit -m 'Add some feature'`
4. Push to the branch: `git push origin feature/your-feature`
5. Open a Pull Request

## 📄 License

This project is for educational purposes as part of C# .NET training.

## 👨‍💻 Author

**Sravani**
- GitHub: [@sravani-divami](https://github.com/sravani-divami)

## 📞 Support

For issues and questions:
- Create an issue in the repository
- Contact: [Your contact information]

---

**Note**: This is a capstone project for C# .NET training. The application demonstrates best practices in building secure, scalable Web APIs with ASP.NET Core.
