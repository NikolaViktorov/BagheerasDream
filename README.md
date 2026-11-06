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
- Domain entities and enums.
- Entity Framework Core configuration.
- SQL Server with Docker Compose.
- ASP.NET Core Identity integration.
- Initial database migration.
- Admin user seeding.
- Main frontend pages and reusable components.
- Cat and litter presentation pages.
- Contact form with client-side validation.
- Centralized frontend demo data.

### In Progress
- Connecting frontend pages to real backend data.
- Replacing temporary demo data with database records.
- Implementing the remaining application functionality.

**Note:** The current frontend uses temporary demo data. The contact form currently performs client-side validation only and does not send actual emails.

---

## 🚀 Getting Started

### Prerequisites

Make sure you have installed:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Visual Studio](https://visualstudio.microsoft.com/) or another compatible IDE.
- Git

### 1. Clone the repository

```bash
git clone <repository-url>
cd BagheerasDream
```

### 2. Start SQL Server

Start Docker Desktop and run the database container using the existing Docker Compose configuration:

```bash
docker compose up -d
```

### 3. Configure development secrets

Configure the required database connection string and other development settings using the project's configuration or ASP.NET Core User Secrets.

Do not commit passwords, connection strings containing credentials or other sensitive information to GitHub.

### 4. Apply database migrations

From the solution directory, apply the existing Entity Framework Core migrations using the appropriate startup and migrations projects:

```bash
dotnet ef database update
```

### 5. Run the application

Open the solution in Visual Studio, select `BagheerasDream.Web` as the startup project and run the application.

---

## 🎨 Design Philosophy

BagheerasDream uses a clean, elegant and minimal visual identity.

The design combines:

- Warm cream and beige backgrounds.
- Subtle gold accents.
- Elegant serif typography.
- Large photography areas.
- Minimalist layouts.
- Responsive components.

The goal is to create a welcoming digital experience that reflects the character and elegance of the British Shorthair breed.

---

## 📌 Future Improvements

- Full frontend and backend integration.
- Dynamic cat and litter management.
- Complete kitten availability management.
- Admin dashboard.
- Contact form email delivery.
- Image upload and management.
- Additional validation and application security improvements.

---

## 📄 License

This project is currently developed as a personal software project.

License information will be added in a future release.

---

**BagheerasDream** – Where elegance meets affection. 🐾
