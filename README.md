# BloggerMVC

**BloggerMVC** is a simple side‑project built to learn and demonstrate ASP.NET MVC, Entity Framework Core, and SQL Server. It implements a basic blog platform where authorized users can create and manage posts, and any visitor can browse and read blogs.

---

## 📦 Features

- **User Authentication & Authorization**  
  - Only registered (and authorized) users can create, edit, or delete blog posts.  
  - Public visitors can view posts without an account.

- **Blog Posts**  
  - Create, read, update, and delete (CRUD) posts.  
  - Assign posts to one or more categories.  
  - View posts by category.

- **Engagement**  
  - “Like” posts.  
  - Comment on posts.  
  - React to comments (e.g., like/dislike).  
  - Reply to existing comments (nested threads).

- **Categories**  
  - Create and manage blog categories.  
  - Filter posts by category.

- **Responsive UI**  
  - Basic Bootstrap‑based layout for mobile and desktop.

---

## 🛠️ Tech Stack

- **Backend**:  
  - ASP.NET Core MVC  
  - Entity Framework Core  
  - SQL Server (LocalDB / Express)

- **Frontend**:  
  - Razor Views  
  - [Quill Rich Text Editor](https://quilljs.com/)

- **Other**:  
  - [AutoMapper](https://automapper.org/) for DTO mapping (Will implement later)
  - [FluentValidation](https://fluentvalidation.net/) for input validation  (Will implement later)
  - ASP.NET Identity for user management  (Will implement later)

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK 7.0+](https://dotnet.microsoft.com/download)  
- SQL Server (LocalDB, Express, or full)  
- (Optional) Visual Studio 2022 / VS Code

### Installation

1. **Clone the repo**  
   ```bash
   git clone https://github.com/ThanhTNV/BloggerMVC.git
   cd BloggerMVC
   ```

2. **Configure the database**  
   - In `appsettings.json`, update your connection string under `ConnectionStrings:DefaultConnection` to point to your SQL Server instance.

3. **Apply migrations**  
   ```bash
   dotnet ef database update
   ```

4. **Run the app**  
   ```bash
   dotnet run
   ```
   Then browse to `https://localhost:5001` (or the URL shown in your console).

---

## 📝 Project Structure

```
/Blogger.Application
├── Controllers/        # MVC controllers
├── Data/               # EF Core DbContext & Migrations
├── Models/             # Domain & ViewModel classes
├── Views/              # Razor views
├── wwwroot/            # Static files (CSS, JS, images)
├── Services/           # Business‑logic services
└── Program.cs          # App configuration & DI
```

---

## 🔄 Usage

1. **Register** a new user (or seed an admin account).  
2. **Log in** to access the “Create Post” page.  
3. **Create** categories under **Categories → Create**.  
4. **Write** and **publish** blog posts, assigning categories.  
5. Visitors can **browse** posts, **like**, **comment**, and **reply**.

---

## 🤝 Contributing

This project is a personal learning sandbox—feel free to fork it, experiment, and send PRs if you’d like!  

1. Fork the repository  
2. Create a feature branch (`git checkout -b feature/YourFeature`)  
3. Commit your changes (`git commit -m 'Add YourFeature'`)  
4. Push to the branch (`git push origin feature/YourFeature`)  
5. Open a Pull Request

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

> Built with ❤️ by ThanhTNV as a learning project for ASP.NET MVC, EF Core & SQL Server.

