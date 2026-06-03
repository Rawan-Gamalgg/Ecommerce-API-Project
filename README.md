# E-Commerce API

A comprehensive ASP.NET Core REST API for building full-featured e-commerce applications. This project implements a three-tier architecture with role-based access control, JWT authentication, and complete product management capabilities.

## Overview

This E-Commerce API provides a robust backend solution for online retail platforms with features including product catalog management, user authentication, shopping cart functionality, order processing, and role-based authorization.

## Features

### Authentication & Authorization
- User registration and login with JWT token-based authentication
- Role-based access control (RBAC) with role management
- Secure password handling
- Token-based session management

### Product Management
- Complete CRUD operations for products
- Category-based product organization
- Advanced product filtering and search
- Pagination support for large datasets
- Product image management and uploads

### Shopping Cart & Orders
- Add/remove items from shopping cart
- Real-time cart management
- Order creation and tracking
- Order status management
- Order item details and history

### Image Management
- Product image upload and storage
- Image validation
- Efficient image retrieval

## Technology Stack

- **Framework:** ASP.NET Core (.NET 10)
- **Database:** SQL Server with Entity Framework Core
- **Authentication:** JWT (JSON Web Tokens)
- **ORM:** Entity Framework Core with Code-First migrations
- **Validation:** FluentValidation
- **Architecture:** Three-Tier Layered Architecture

## Project Architecture

```
├── API Layer (E-commerce API Project)
│   ├── Controllers/          # HTTP endpoints
│   │   ├── ProductsController
│   │   ├── CategoriesController
│   │   ├── AuthController
│   │   ├── CartController
│   │   ├── OrdersController
│   │   ├── RolesController
│   │   └── ImagesController
│   └── Program.cs            # Configuration and DI setup
│
├── BLL Layer (Business Logic Layer)
│   ├── Managers/             # Business logic services
│   │   ├── ProductManager
│   │   ├── CategoryManager
│   │   ├── CartManager
│   │   ├── OrderManager
│   │   ├── AuthManager
│   │   └── ImageManager
│   ├── DTOs/                 # Data Transfer Objects
│   ├── Validators/           # FluentValidation validators
│   ├── Settings/             # Configuration classes
│   └── Common/               # Shared utilities
│
└── DAL Layer (Data Access Layer)
    ├── Data/
    │   ├── Models/           # Entity models
    │   ├── Context/          # DbContext
    │   └── Configuration/    # Entity configurations
    ├── Repositories/         # Data access patterns
    ├── UnitOfWork/          # Unit of Work pattern
    └── Migrations/          # Database migrations
```

## API Endpoints

### Products
- `GET /api/products` - Get all products with filtering and pagination
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create new product
- `PUT /api/products/{id}` - Update product
- `DELETE /api/products/{id}` - Delete product

### Categories
- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get category by ID
- `POST /api/categories` - Create new category
- `PUT /api/categories/{id}` - Update category
- `DELETE /api/categories/{id}` - Delete category

### Authentication
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login and token generation

### Cart
- `GET /api/cart` - Get current cart
- `POST /api/cart/items` - Add item to cart
- `PUT /api/cart/items/{id}` - Update cart item
- `DELETE /api/cart/items/{id}` - Remove item from cart

### Orders
- `GET /api/orders` - Get user orders
- `POST /api/orders` - Create new order
- `GET /api/orders/{id}` - Get order details

### Images
- `POST /api/images/upload` - Upload product image

### Roles
- `GET /api/roles` - Get all roles
- `POST /api/roles` - Create new role

## Getting Started

### Prerequisites
- .NET 10 SDK
- SQL Server (2019 or later)
- Visual Studio 2022 or VS Code with C# extension

### Installation

1. Clone the repository
```
git clone <repository-url>
cd ecommerce-api
```

2. Configure the database connection
   - Update `appsettings.json` with your SQL Server connection string

3. Apply database migrations
```
dotnet ef database update
```

4. Run the application
```
dotnet run
```

The API will be available at `https://localhost:5001` or `http://localhost:5000`

## Project Structure Details

### API Layer
Handles HTTP requests and responses. Controllers validate incoming requests and call business logic through manager interfaces.

### Business Logic Layer
Contains application logic, business rules, data validation, and DTOs. Uses FluentValidation for input validation and mapper utilities for DTO conversions.

### Data Access Layer
Implements the Repository pattern with Unit of Work for data access. Handles all database operations through Entity Framework Core with SQL Server.

## Key Design Patterns

- **Repository Pattern** - Abstraction for data access
- **Unit of Work Pattern** - Transaction management
- **Dependency Injection** - Loose coupling between layers
- **Data Transfer Objects (DTOs)** - API contract separation
- **Fluent Validation** - Declarative input validation
- **Role-Based Access Control** - Authorization mechanism

## Database Schema

The database includes the following main entities:
- **Users** - Application users with authentication
- **Roles** - User roles for authorization
- **Products** - Product catalog
- **Categories** - Product categories
- **CartItems** - Shopping cart items
- **Orders** - Customer orders
- **OrderItems** - Order line items

## Configuration

Key settings in `appsettings.json`:
- Database connection string
- JWT settings (secret key, expiration)
- Image upload settings
- Role definitions

## Security

- JWT token-based authentication
- Role-based authorization
- Input validation on all endpoints
- SQL injection prevention through EF Core parameterization

## Contributing

1. Create a feature branch
2. Commit your changes
3. Push to the branch
4. Create a Pull Request

## License

[Add your license information here]

## Support

For issues or questions, please create an issue in the repository.
