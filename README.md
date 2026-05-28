# E-Commerce Comic Bookstore (SachWuy.vn)

![.NET Framework](https://img.shields.io/badge/.NET_Framework-4.x-blue.svg)
![ASP.NET MVC](https://img.shields.io/badge/ASP.NET-MVC5-brightgreen.svg)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-6.x-orange.svg)
![SQL Server](https://img.shields.io/badge/SQL_Server-LocalDB-red.svg)

## 📖 Introduction
This is a full-stack e-commerce web application built with **ASP.NET MVC**, designed for browsing, managing, and purchasing comic books and novels. The project demonstrates a solid understanding of the MVC architectural pattern, relational database management using **Entity Framework**, and user authentication using **ASP.NET Identity**.

This project was developed as a personal project/coursework and is continually improved to practice backend web development skills.

## ✨ Key Features
* **User Authentication & Authorization**: Secure login, registration, and role-based access control (Admin/Customer) using ASP.NET Identity.
* **Product Catalog**: Browse comic books by categories/genres.
* **Shopping Cart & Checkout**: Fully functional cart system allowing users to add items, view cart summary, and process orders.
* **Admin Dashboard**: Content Management System (CMS) for administrators to manage (CRUD) books, categories, and user orders.
* **Responsive UI**: Built with HTML, CSS, Bootstrap, and Razor syntax for a seamless user experience across devices.

## 🛠️ Technology Stack
* **Backend**: C#, ASP.NET MVC 5
* **Database & ORM**: Microsoft SQL Server, Entity Framework 6 (Code-First approach)
* **Frontend**: HTML5, CSS3, JavaScript, Bootstrap, Razor Views
* **Authentication**: ASP.NET Identity

## 🗄️ Database Schema
The database (managed via EF Code-First Migrations) includes the following core entities:
* `ApplicationUser`: User identity and profile management.
* `Truyen` (Comic/Book): Stores book details (Title, Price, Author, Image, etc.).
* `TheLoai` (Category): Genres for organizing books.
* `Order` & `OrderDetail`: Records user transactions and purchased items.
* `CartItem`: Manages the current shopping session of users.

## 🚀 Getting Started

### Prerequisites
To run this project locally, you will need:
* [Visual Studio](https://visualstudio.microsoft.com/) (2019/2022) with "ASP.NET and web development" workload installed.
* [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or LocalDB.

### Installation & Setup
1. **Clone the repository:**
   ```bash
   git clone https://github.com/JaaWuy/Ecommerce-Comic-Store.git
   ```
2. **Open the solution:**
   * Open the `.sln` file (`DoAnWeb.sln`) in Visual Studio.
3. **Restore NuGet Packages:**
   * Right-click on the Solution in Solution Explorer and select **Restore NuGet Packages**.
4. **Configure Database Connection:**
   * Open `Web.config` and locate the `<connectionStrings>` section.
   * Update the connection string to match your local SQL Server instance if necessary (Default is configured for LocalDB/SQLEXPRESS).
5. **Run Migrations / Update Database:**
   * Open the **Package Manager Console** (`Tools > NuGet Package Manager > Package Manager Console`).
   * Run the following command to create the database schema:
     ```powershell
     Update-Database
     ```
   * *Alternatively*, you can run the provided SQL script directly in SQL Server Management Studio (SSMS) to attach the database.
6. **Run the Application:**
   * Press `F5` or click the **Start** button in Visual Studio to launch the app in your default browser.

## 📸 Screenshots
*(Add screenshots of your application here to make your README more visually appealing)*
- **Home Page**: `![Home](link-to-image)`
- **Shopping Cart**: `![Cart](link-to-image)`
- **Admin Dashboard**: `![Admin](link-to-image)`

## 🎯 Future Enhancements
- [ ] Implement Payment Gateway Integration (Stripe/PayPal/VNPay).
- [ ] Add product search and filtering with AJAX.
- [ ] Improve UI/UX with modern frontend frameworks (e.g., React/Vue).
- [ ] Add unit tests and integration tests.

## 🤝 Contact
- **JaaWuy** - [https://github.com/JaaWuy](https://github.com/JaaWuy)
- **LinkedIn**: [Your LinkedIn Profile](https://linkedin.com/in/yourprofile)
- **Email**: [your.email@example.com](mailto:your.email@example.com)
