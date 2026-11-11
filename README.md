# 🐱 Bagheera's Dream

### A modern cattery website and management platform for British Shorthair cats.

**Bagheera's Dream** is a full-stack web application developed for my mother's British Shorthair cattery. It provides an elegant online presence for the cattery, allowing visitors to discover our cats, explore litters, learn about available kittens, and get in touch.

The platform also includes a dedicated administration area for managing cats, litters, kittens, and household pets.

🌐 **Live Website:** [https://bagheerasdream.de](https://bagheerasdream.de)

This is a real-world project, designed, developed, and deployed for an actual cattery rather than a demonstration or university assignment.

---

## ✨ Features

### Public Website

- Responsive homepage introducing the cattery.
- About Us page.
- Contact page.
- Separate catalogues for male and female breeding cats.
- Individual cat profiles.
- Cat information, pedigree, and parent navigation.
- Litter catalogue and individual litter pages.
- Kitten information and availability status.
- Dedicated household pets overview.
- Reusable Razor components.
- Responsive layouts for desktop and mobile devices.
- SEO metadata and sitemap configuration.

### Administration Panel

- Secure administrator authentication using ASP.NET Core Identity.
- Protected administration routes.
- Cat management (create, edit, and delete).
- Litter management.
- Kitten management.
- Household pet management.
- Profile image uploads.
- Image management and galleries.
- Live image previews when selecting new images.
- Form validation and error handling.

### Backend & Infrastructure

- Clean Architecture-based solution structure.
- Entity Framework Core integration.
- SQL Server database.
- ASP.NET Core Identity.
- Database migrations.
- Application and infrastructure service separation.
- Docker-based deployment.
- Production hosting on a Linux VPS.
- HTTPS configuration and reverse proxy using Caddy.

---

## 🛠️ Tech Stack

| Technology | Purpose |
|---|---|
| C# | Main programming language |
| .NET 10 | Application platform |
| ASP.NET Core | Web application framework |
| Blazor Interactive Server | Frontend development |
| Razor Components | Reusable UI components |
| Entity Framework Core 10 | ORM and database access |
| SQL Server 2022 | Relational database |
| ASP.NET Core Identity | Authentication and authorization |
| Docker & Docker Compose | Containerization and deployment |
| Caddy | Reverse proxy and HTTPS |
| HTML & CSS | Frontend structure and styling |
| JavaScript | Browser-side functionality |
| Git & GitHub | Version control |

---

## 🏗️ Architecture

The application follows Clean Architecture principles, separating domain logic, application services, infrastructure, and presentation.

The solution consists of four main projects:

| Project | Responsibility |
|---|---|
| `BagheerasDream.Domain` | Domain entities, enums, and core business models |
| `BagheerasDream.Application` | Application services, interfaces, and DTOs |
| `BagheerasDream.Infrastructure` | Database access, EF Core configurations, and infrastructure services |
| `BagheerasDream.Web` | ASP.NET Core application, Blazor components, and user interface |

### Application Flow

The application uses a service-based architecture:

1. Blazor components handle user interaction and presentation.
2. Application services define the operations available to the application.
3. Infrastructure services implement database access and external operations.
4. Entity Framework Core communicates with SQL Server.

This structure keeps responsibilities separated and makes the application easier to maintain and extend.

---

## 📂 Domain Models

The application includes the following core domain entities:

- **Cat** – Represents a breeding cat, including its profile information and family relationships.
- **CatImage** – Stores images associated with breeding cats.
- **Litter** – Represents a litter, including its parents and birth information.
- **LitterImage** – Stores images associated with a litter.
- **Kitten** – Represents an individual kitten belonging to a litter.
- **Pet** – Represents a household pet that is presented separately from breeding cats.

### Kitten Availability

Kittens can have different availability statuses:

- `Available`
- `Reserved`
- `Sold`

---

## 🚀 Live Deployment

The application is deployed and publicly accessible:

**Website:** [https://bagheerasdream.de](https://bagheerasdream.de)

The production environment uses:

- Linux VPS hosting.
- Docker Compose for application deployment.
- SQL Server database.
- Caddy as a reverse proxy.
- HTTPS with automatic certificate management.

Production updates are deployed by pulling the latest changes from the GitHub repository and rebuilding the Docker containers.

---

## 💻 Getting Started

### Prerequisites

Make sure you have installed:

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Git](https://git-scm.com/)
- Visual Studio 2022 or another compatible IDE.

### 1. Clone the Repository

```bash
git clone https://github.com/NikolaViktorov/BagheerasDream.git
cd BagheerasDream
```

### 2. Configure Application Settings

Configure the required connection strings and application settings using the appropriate configuration files or ASP.NET Core User Secrets.

Never commit passwords, database credentials, or other sensitive configuration values to the repository.

### 3. Start the Database

Start Docker Desktop and run the development database configuration:

```bash
docker compose up -d
```

### 4. Apply Database Migrations

From the solution directory, apply the Entity Framework Core migrations:

```bash
dotnet ef database update --project BagheerasDream.Infrastructure --startup-project BagheerasDream.Web
```

### 5. Run the Application

Start the web project:

```bash
dotnet run --project BagheerasDream.Web
```

Alternatively, open `BagheerasDream.slnx` in Visual Studio and select `BagheerasDream.Web` as the startup project.

---

## 🎨 Design Philosophy

The visual identity of Bagheera's Dream is inspired by the elegance and personality of the British Shorthair breed.

The design combines:

- Warm cream and beige backgrounds.
- Subtle gold accents.
- Elegant serif typography.
- Large photography areas.
- Minimalist layouts.
- Consistent spacing and typography.
- Responsive components.

The goal is to create a welcoming and premium experience that reflects the character of the cattery and its cats.

---

## 🚧 Development Status

Bagheera's Dream is an actively maintained project with a live production deployment.

The application is continuously evolving as new features, improvements, and refinements are introduced.

Future development focuses on improving the administration experience, expanding content management capabilities, and refining the public website.

---

## 📄 License

This project is developed as a personal project for a family-owned cattery.

All rights reserved unless otherwise specified.

---

**Bagheera's Dream** – Where elegance meets affection. 🐾
