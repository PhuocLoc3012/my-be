# Clean Architecture in ASP.NET Core Web API

## 1. What is Clean Architecture?
Clean Architecture is a design pattern that separates an application into different layers based on their responsibility. It’s a way of organizing your code into independent, testable, and reusable components. This architecture pattern is a software design methodology that emphasizes the separation of concerns and separates the application into distinct modules.

![Alt text](https://miro.medium.com/v2/resize:fit:500/1*sura91gPMoCjPNvZWsAO_g.png)

## 2. Advantages of Clean Architecture
The primary objective of Clean Architecture is to create a structure that makes it easy to manage and maintain an application as it grows and changes over time. It also makes it easy to add new features, fix bugs, and make changes to existing functionality without affecting the rest of the application.
## 3. Implementing Clean Architecture in ASP .NET Core Web API
### **Create the Project**
Start by creating an ASP.NET Core Web API project in Visual Studio. You can then create separate class library projects for each layer:
- **Domain** project
- **Application** project
- **Infrastructure** project
- **Presentation** project (Web API layer)
### 1. **Domain Layer**
-> The project that contains the domain layer, including the entities, value objects, and domain services
### 2. **Application Layer**
→ The project that contains the application layer and implements the application services, DTOs (data transfer objects), and mappers. It should reference the **Domain** project.
### 3. **Infrastructure Layer**
 → The project contains the infrastructure layer, including the implementation of data access, logging, email, and other communication mechanisms. It should reference the **Application** project. 
This layer includes:
- Data Access (e.g., using Entity Framework Core)
- Repositories
- External Service Integrations (e.g., email, logging)
### 4. **Presentation Layer**
→ The main project contains the presentation layer and implements the ASP.NET Core web API. It should reference the **Application** and **Infrastructure** projects.
The Presentation Layer is responsible for handling **HTTP requests** and **responses** in an ASP.NET Core Web API. It contains:
- API Controllers
- Middleware to manage user interactions
- Data delivery and response formatting

Reference: [*Clean Architecture in ASP.NET Core Web API*](https://medium.com/@mohanedzekry/clean-architecture-in-asp-net-core-web-api-d44e33893e1d)






