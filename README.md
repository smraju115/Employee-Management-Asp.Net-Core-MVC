# Employee Management - ASP.NET Core MVC

A complete **ASP.NET Core MVC** application built with **.NET 8**, showcasing **Master-Details CRUD operations** in an **Employee Management System**.

This project demonstrates how to manage employee data along with their related details such as skills, dependents, or projects using ASP.NET MVC architecture (NOT Web API).

---

## 📚 Features

- ASP.NET Core 8 MVC Framework
- Entity Framework Core 8
- SQL Server / LocalDB support
- Master-Details Form Handling
- CRUD Operations with Validation
- Strongly Typed Views
- Stroe Procedure Used
- Razor Pages
- Bootstrap UI (Optional)
- Client-side + Server-side validation

---

## 📦 Technologies Used

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server / LocalDB
- Razor Views
- Bootstrap 5 (for UI)
- jQuery Validation (optional)


## 🚀 Getting Started

### ✅ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Visual Studio 2022
- SQL Server / LocalDB

---

### 🔧 Installation

1. **Clone this repository**
   ```bash
   git clone https://github.com/smraju115/Employee-Management-Asp.Net-Mvc-Core.git
   cd Employee-Management-Asp.Net-Mvc-Core
Update your DB connection

Open appsettings.json

Edit the DefaultConnection string with your database info.

Apply EF Migrations & Create DB


dotnet ef database update
Run the app

📂 Project Structure

📦Employee-Management-Asp.Net-Mvc-Core
 ┣ 📁Controllers
 ┣ 📁Models
 ┣ 📁Views
 ┃ ┣ 📁Employee
 ┃ ┗ 📁Shared
 ┣ 📁Data
 ┣ 📁Migrations
 ┣ 📁wwwroot
 ┣ 📜Program.cs
 ┗ 📜appsettings.json

Handled via Razor Views and Controller Actions:


🧪 Validation

Server-side validation via [Required] attributes

Client-side validation via jQuery + Bootstrap feedback (optional)

🤝 Contributing

Pull requests and suggestions are welcome! Please fork the repo and submit your improvements.

📄 License

This project is licensed under the MIT License.

👨‍💻 Author
Made with ❤️ by Your Name
