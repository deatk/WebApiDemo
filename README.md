WebApiDemo
WebApiDemo is an educational project aimed at demonstrating the development of a RESTful API using ASP.NET Core. 
The project showcases best practices in API development, including a clean layered architecture, model mapping, and unit testing.

Project Structure
The solution is organized into the following projects:
WebApiDemo
├── WebApiDemo             # Main project containing the core API functionality
│   ├── Controllers        # API controllers
│   └── Program.cs         # Entry point of the application
├── WebApiDemoModels       # Project containing shared models and mappings
│   └── Mappings           # AutoMapper profiles
├── WebApiDemoRepositories # Data access layer
├── WebApiDemoServices     # Business logic and service layer
└── README.md

WebApiDemo: The main ASP.NET Core Web API project responsible for handling HTTP requests and responses.

WebApiDemoModels: Contains the domain models and mapping configurations. This includes Entities, Request Objects and AutoMapper profiles for model mapping.

WebApiDemoRepositories: Implements the data access layer, managing database operations and interactions.

WebApiDemoServices: Contains the business logic and service layer, processing data between the controller and repository layers.

WebApiDemoTests: (To be implemented) Will include unit and integration tests to ensure the application's reliability and correctness.

the connection.json file must be created locally using the structure provided in the connections.example.json

Getting Started
To run this project locally, follow these steps:

Clone the repository:

bash
Copy code
git clone https://github.com/deatk/WebApiDemo.git
cd WebApiDemo
Restore dependencies:

Ensure you have the .NET SDK installed. Then, run:

bash
Copy code
dotnet restore
Build the solution:

bash
Copy code
dotnet build
Run the application:

bash
Copy code
dotnet run --project WebApiDemo
The API will be accessible at https://localhost:5001 by default.

Usage
The API provides endpoints for managing resources related to the domain models. 
Detailed API documentation and usage examples will be provided as the project progresses.

Contributing
This project is intended for educational purposes, and contributions are currently not being accepted. 
For any suggestions or feedback, please open an issue on the GitHub repository.

License
This project is licensed under the Creative Commons Attribution-NoDerivatives 4.0 International License. 
This means you are free to share the material in any medium or format under the following terms:

Attribution: You must give appropriate credit, provide a link to the license, and indicate if changes were made.
You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.

NoDerivatives: If you remix, transform, or build upon the material, you may not distribute the modified material.

For more details, refer to the license deed.
