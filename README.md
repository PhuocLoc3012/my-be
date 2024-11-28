# Clean Architecture in ASP.NET Core Web API

This project demonstrates how to implement **Clean Architecture** in an **ASP.NET Core Web API** application. Clean Architecture helps in structuring applications to make them maintainable, scalable, and testable by organizing the code into distinct layers.

## Key Layers in Clean Architecture

### 1. **Domain Layer**
The Domain Layer contains the **core business logic** and **domain entities**. This layer is independent of external frameworks and libraries, ensuring that the business rules remain decoupled from other concerns like data access or UI.  
It includes:
- Entities
- Value Objects
- Domain Services

### 2. **Application Layer**
The Application Layer acts as an intermediary between the Domain and Presentation layers. It contains **application services**, **data transfer objects (DTOs)**, and **business logic**. This layer:
- References the Domain layer for business entities.
- Does **not** reference the Infrastructure layer.

### 3. **Infrastructure Layer**
The Infrastructure Layer handles external concerns like data access, logging, and integration with external services. It implements interfaces defined in the Application Layer and communicates with databases and external resources.  
This layer includes:
- Data Access (e.g., using Entity Framework Core)
- Repositories
- External Service Integrations (e.g., email, logging)

### 4. **Presentation Layer**
The Presentation Layer is responsible for handling **HTTP requests** and **responses** in an ASP.NET Core Web API. It contains:
- API Controllers
- Middleware to manage user interactions
- Data delivery and response formatting

## Implementation Steps

### 1. **Create the Project**
Start by creating an ASP.NET Core Web API project in Visual Studio. You can then create separate class library projects for each layer:
- **Domain** project
- **Application** project
- **Infrastructure** project
- **Presentation** project (Web API layer)

### 2. **Define the Domain Layer**
In the **Domain** project, create entities and value objects that represent your business models. Ensure this layer is independent and has no dependencies on other layers.

### 3. **Set Up the Application Layer**
In the **Application** project, define:
- **Application Services**: Implement business logic.
- **DTOs**: Define data transfer objects used for communication between layers.
This layer should reference the **Domain** project but **not** the **Infrastructure** project.

### 4. **Configure the Infrastructure Layer**
In the **Infrastructure** project, configure data access (e.g., using **Entity Framework Core** or another ORM).  
- Create a **DbContext**.
- Implement **Repository classes** that interact with the database.
- Implement interfaces from the **Application** layer.

### 5. **Build the Presentation Layer**
In the **Presentation** (Web API) project:
- Implement **API Controllers** to handle incoming requests.
- Map requests to **application services** in the Application layer.
- Return appropriate responses (e.g., **HTTP 200 OK**, **HTTP 400 Bad Request**).

## Advantages of Clean Architecture

- **Separation of Concerns**: Each layer has a clear, distinct responsibility.
- **Testability**: Easier to test core business logic in isolation, without needing to depend on external systems like databases.
- **Flexibility**: Changes in one layer (e.g., switching from one database to another or updating the UI) do not affect the core business logic.

## Folder Structure

Here’s how the solution structure looks for a Clean Architecture implementation in ASP.NET Core Web API:
![Alt text](https://miro.medium.com/v2/resize:fit:500/1*sura91gPMoCjPNvZWsAO_g.png)

## Key Layers

### Application Layer

- **IServices Interface**: Defines the contract for application services, promoting loose coupling and enabling different implementations.
- **Services**: Concrete implementations of the `IServices` interface, which contain the business logic and orchestrate use cases.
- **DependencyInjection**: This class manages the registration and resolution of services and repositories, facilitating **Dependency Injection** (DI) throughout the application.
- **IUnitOfWork**: An interface that manages transactions across multiple repositories, ensuring that changes are committed or rolled back as a single unit, preserving data integrity.

### Domain Layer

- **Entities**: Represent the core business objects, encapsulating the business rules and logic.
- **IRepository Interface**: Defines the contract for data access, allowing the Application layer to interact with the data layer without being tightly coupled to specific implementations.
- **Repositories**: Concrete implementations of the `IRepository` interface, which handle the actual data storage and retrieval.

### Infrastructure Layer

- **Persistence**: Manages the actual data storage and retrieval (typically using a database context like **Entity Framework Core** or similar).
- **Repository**: Concrete implementations of the `IRepository` interface that perform data access operations.
- **UnitOfWork**: implement IUnitOfWork of Application layer
- **DependencyInjection**: Similar to the Application layer, this class manages the registration of infrastructure services, ensuring the Application layer can access them without direct dependencies.
### Presentation Layer
- **API**: This layer handles user interactions, typically through HTTP requests, and communicates with the Application layer to execute use cases.