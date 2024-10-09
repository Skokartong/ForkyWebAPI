# Forky Web API - README #
*Overview*

### The Forky Web API is a RESTful web service designed to support restaurant booking and management functionality. This API provides several key features such as account management (registration, login, and deletion), restaurant and menu management, table management, and booking functionalities. It is built using ASP.NET Core and employs JWT-based authentication for secure access control. ###

The system is designed around several core services, which are injected into controllers via dependency injection. It uses Entity Framework Core (EF Core) to interact with a SQL Server database. 

## Key Features: ##

    JWT Authentication: Secure API access using JSON Web Tokens (JWT).
    Account Management: Register, login, update, view, and delete user accounts.
    Restaurant Management: Create, update, delete, and view restaurants, menus, and tables.
    Booking System: Book, update, delete, and view restaurant bookings.
    CORS Support: Cross-Origin Resource Sharing policies for different client apps.
    Swagger UI: Documentation and testing tool available in development mode. 

## API Design ##
*Authentication and Authorization*

### The API uses JWT Authentication to secure endpoints. When users log in, a JWT token is generated and sent back, which must be used to authenticate subsequent requests. The token contains claims that store user information like roles and name, enabling role-based access control. ###

*Key Security Configurations:*

    Issuer: Verifies that the token is issued by a trusted source.
    Audience: Ensures the token is being used by an appropriate consumer.
    Token Signing Key: A secret key is used to generate the token signature, ensuring its integrity.
