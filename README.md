# TelemedApp  
Enterprise-Style Telemedicine Demo Application  
Built with Clean Architecture • .NET • C#  

## Overview  
TelemedApp is a **demo telemedicine platform** showcasing enterprise-grade architecture and modern .NET development practices.  
The goal of this project is to demonstrate how a real-world system can be structured using **Clean Architecture**, **modular layers**, and **scalable patterns** suitable for healthcare, fintech, or any regulated environment.

This repository is designed as a **portfolio project** to highlight senior-level engineering skills.

---

## Architecture  
The solution follows a layered Clean Architecture approach:

TelemedApp.Domain        → Entities, Value Objects, Domain Logic

TelemedApp.Application   → Use Cases, Interfaces, DTOs

TelemedApp.Infrastructure → EF Core, Repositories, Persistence

TelemedApp.Identity      → Authentication, Authorization

TelemedApp.API           → REST API Endpoints, Controllers


### Key Principles  
- Separation of concerns  
- Dependency inversion  
- Testable and maintainable structure  
- Infrastructure isolated behind interfaces  
- Clear boundaries between layers  

---

## Technologies Used  
- **.NET / C#**  
- **ASP.NET Core Web API**  
- **Entity Framework Core**  
- **Clean Architecture**  
- **Identity / Authentication**  
- **SQL (or in-memory provider for demo)**  
- **Dependency Injection**  
- **Async / Await patterns**  

---

## Features (Demo Scope)  
- Doctor & Patient domain models  
- Basic appointment logic  
- Authentication layer (Identity project)  
- Modular architecture ready for expansion  
- Example API endpoints  
- Repository & Unit of Work patterns  
- DTO-based communication between layers  

---

## Getting Started  

### 1. Clone the repository  
git clone https://github.com/PanosPanagiotou/TelemedApp.git

### 2. Open the solution
Open TelemedApp.sln in Visual Studio or Rider.

### 3. Run the API
Set TelemedApp.API as startup project and run: dotnet run

### 4. Swagger UI
Once running, navigate to: https://localhost:5001/swagger

---

## Project Structure
TelemedApp/

 ├── TelemedApp.API
 
 ├── TelemedApp.Application
 
 ├── TelemedApp.Domain
 
 ├── TelemedApp.Identity
 
 ├── TelemedApp.Infrastructure
 
 ├── TelemedApp.sln
 
 ├── .gitignore
 
 └── .gitattributes

---
## Purpose of This Project
This repository exists to demonstrate:
Senior-level .NET engineering
Clean Architecture implementation
Enterprise-style layering
Domain-driven thinking
API design and modularity
Ability to structure scalable systems

It is not a production system — it is a technical showcase.

## Contact
### Panagiotis Panagiotou  
#### Senior .NET Engineer
#### LinkedIn: https://www.linkedin.com/in/panagiotis-panagiotou-213048161/
