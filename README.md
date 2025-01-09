### WebApiDemo

WebApiDemo is an educational project aimed at demonstrating the development of a RESTful API using ASP.NET Core. 
The project showcases best practices in API development, including a clean layered architecture, model mapping, and unit testing.

---

### Key Features

- **RESTful API** built with ASP.NET Core.
- **Clean Architecture**: Logical separation of concerns into layers.
- **Clean Code**: Readable, maintainable, and testable codebase.
- **Repository Pattern**: Decoupling data access logic.
- Easy extensibility and scalability for future enhancements.

### Folder and Subproject Details

1. **WebApiDemo**  
   - Contains the entry point (`Program.cs`) and the main logic for handling HTTP requests.
   - Includes `Controllers`, which define endpoints and handle API interactions.

2. **WebApiDemoModels**  
   - Contains shared entity models and mappings.  
   - Includes AutoMapper profiles for mapping between domain models and database entities.

3. **WebApiDemoRepositories**  
   - Implements the Repository Pattern for abstracting database operations.
   - Ensures a clean separation between the business logic and data access layers.

4. **WebApiDemoServices**  
   - Houses the service layer that contains business logic.
   - Orchestrates data flow between controllers and repositories.

5. **README.md**  
   - This document, providing an overview of the project, structure, and features.

---

## Principles Followed

### Clean Code

- **Readability**: Clear naming conventions and minimal inline comments.
- **Maintainability**: Modular and self-explanatory methods and classes.
- **Testability**: Encourages the use of unit tests to validate logic (future implementation).

### Clean Architecture

- **Independent of Frameworks**: Business logic is not tightly coupled to ASP.NET Core or other libraries.
- **Separation of Concerns**: Each layer has a distinct responsibility, ensuring that changes in one layer don't propagate unnecessary changes to others.
- **Scalable and Extensible**: The architecture supports easy addition of new features or layers without disrupting existing functionality.

---

**Getting Started**
To run this project locally, follow these steps:

the *connection.json* file must be created locally using the structure provided in the *connections.example.json*

Clone the repository:

bash\
Copy code\
git clone https://github.com/deatk/WebApiDemo.git \
cd WebApiDemo

Restore dependencies:

Ensure you have the .NET SDK installed. Then, run:

bash\
Copy code\
dotnet restore

Build the solution:

bash\
Copy code\
dotnet build

Run the application:

bash\
Copy code\
dotnet run --project WebApiDemo

The API will be accessible at http://localhost:5172/swagger by default.

---

**Usage**\
The API provides endpoints for managing resources related to the domain models.\
Detailed API documentation and usage examples will be provided as the project progresses.

