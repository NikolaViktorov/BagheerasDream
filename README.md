# 🐱 BagheerasDream

**A modern cattery management and presentation platform for British Shorthair cats.**

BagheerasDream is a web application designed to present a British Shorthair cattery, showcase its cats and litters, and provide visitors with an elegant way to explore available kittens and get in touch.

The project is built with ASP.NET Core and follows Clean Architecture principles, with a focus on maintainability, scalability and a clear separation of concerns.

---

## ✨ Features

### Frontend
- Elegant and responsive homepage.
- About Us page presenting the cattery.
- Contact page with client-side form validation.
- Separate catalogues for female and male cats.
- Individual cat profile pages.
- Parent information and pedigree navigation.
- Litter catalogue with individual litter pages.
- Kitten cards displaying their availability status.
- Reusable Razor components.
- Consistent design with responsive layouts.

### Backend
- Clean Architecture solution structure.
- Domain entities and enums.
- Entity Framework Core integration.
- SQL Server database.
- Docker Compose configuration.
- ASP.NET Core Identity authentication.
- Admin user seeding.
- Database migrations.
- Development secrets configuration.

---

## 🛠️ Tech Stack

| Technology | Purpose |
|---|---|
| C# | Main programming language |
| ASP.NET Core | Web application framework |
| Blazor | Frontend development |
| Entity Framework Core | ORM and database access |
| SQL Server 2022 | Relational database |
| ASP.NET Core Identity | Authentication and user management |
| Docker Compose | Database containerization |
| HTML & CSS | Frontend structure and styling |
| Git & GitHub | Version control |

---

## 🏗️ Architecture

The application follows Clean Architecture principles to separate business logic, infrastructure and presentation.

The solution is organized into the following main projects:

- **BagheerasDream.Domain** – Domain entities, enums and core business models.
- **BagheerasDream.Infrastructure** – Database access, Entity Framework Core and infrastructure services.
- **BagheerasDream.Web** – ASP.NET Core application, Blazor components and user interface.

The architecture is designed to keep the domain independent from external infrastructure and make future development easier.

---

## 📂 Domain Models

The core domain currently includes:

- **Cat** – Represents an individual breeding cat, including its basic information and parents.
- **Litter** – Represents a litter, including its parents, birthday and kittens.
- **Kitten** – Represents an individual kitten belonging to a litter.

### Kitten Availability

Each kitten has one of the following statuses:

- `Available`
- `Reserved`
- `Sold`

---

## 🚧 Development Status

BagheerasDream is currently under active development.

### Completed
- Initial solution structure and Clean Architecture setup.
- Domain entities and
