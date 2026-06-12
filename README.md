# ✈️ TravelBuddy

> A full-stack ASP.NET Core MVC web application for managing and booking tourist excursions, featuring role-based access control, a cancellation request workflow, and an in-app notification system.

![.NET Version](https://img.shields.io/badge/.NET-10.0-purple)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-MVC-blue)
![License](https://img.shields.io/badge/license-Educational-green)

---

## 📋 Table of Contents

- [About the Project](#about-the-project)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Features](#features)
- [Usage](#usage)
- [Database Setup](#database-setup)
- [Configuration](#configuration)
- [External Components](#external-components)
- [License](#license)
- [Contact](#contact)

---

## 📖 About the Project

**TravelBuddy** is a server-side rendered web application that allows registered users to browse tourist excursions, create and manage bookings, maintain a favourites list, and submit cancellation requests. Administrators manage the full excursion catalogue and process cancellation requests with approve/decline decisions. The system automatically notifies users of outcomes via an in-app notification inbox.

Built as part of the **ASP.NET Fundamentals** course to demonstrate MVC architecture, Entity Framework Core Code-First, ASP.NET Core Identity, service layer separation, custom validation attributes, and area-based project organisation.

---

## 🛠️ Technologies Used

| Technology | Version | Purpose |
|---|---|---|
| ASP.NET Core MVC | .NET 10 | Web framework |
| Entity Framework Core | 10 | ORM / Database access |
| Microsoft SQL Server | — | Relational database |
| ASP.NET Core Identity | — | Authentication and role management |
| Bootstrap | 5 | Responsive frontend styling |
| Razor Views (.cshtml) | — | Server-side HTML rendering |
| toastr.js | — | Toast UI notifications |
| Font Awesome | 6 | Icon library |
| jQuery | — | Required by Bootstrap and toastr |
| Google Fonts | — | Montserrat & Open Sans typography |

---

## ✅ Prerequisites

Make sure you have the following installed before running the project:

- [.NET SDK 10.0+](https://dotnet.microsoft.com/download)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) (LocalDB or full instance)
- [Git](https://git-scm.com/)

---

## 🚀 Getting Started

Follow these steps to get the project running locally.

### 1. Clone the repository

```bash
git clone https://github.com/blueshero92/TravelBuddy.git
cd TravelBuddy
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Configure the connection string

In `TravelBuddy/appsettings.json` set your SQL Server connection:

```json
"ConnectionStrings": {
  "TravelBuddyDbConnection": "Server=(localdb)\\mssqllocaldb;Database=TravelBuddyDb;Trusted_Connection=True;"
}
```

### 4. Configure the admin seed account

```json
"AdminSettings": {
  "Username": "admin",
  "Email": "admin@travelbuddy.com",
  "Password": "Admin123!",
  "FullName": "Site Administrator"
}
```

### 5. Apply database migrations

```bash
dotnet ef database update --project TravelBuddy.Data --startup-project TravelBuddy
```

### 6. Run the application

```bash
dotnet run --project TravelBuddy
```

The app will be available at `https://localhost:5001` or `http://localhost:5000`.

---

## 📁 Project Structure

```
TravelBuddy/
├── TravelBuddy/                        # Main web project
│   ├── Areas/
│   │   ├── Admin/                      # Admin controllers and views
│   │   └── Identity/                   # ASP.NET Core Identity Razor Pages
│   ├── Controllers/                    # User-facing MVC controllers
│   ├── ViewComponents/                 # UnreadNotificationCount ViewComponent
│   ├── Views/                          # Razor views per controller
│   └── wwwroot/                        # Static files (CSS, JS)
├── TravelBuddy.Data/                   # DbContext, EF migrations, entity configurations, seeding
├── TravelBuddy.Data.Models/            # Entity classes and enums
├── TravelBuddy.Services.Core/          # Service interfaces and implementations
├── TravelBuddy.ViewModels/             # Input models and view models
├── TravelBuddy.GCommon/                # Shared constants, output messages, custom validation attributes
└── TravelBuddy.Infrastructure/         # Infrastructure helpers
```

---

## ✨ Features

### Registered User
- [x] Browse excursions with details (title, destination, dates, price, capacity, image)
- [x] Book an excursion — capacity is decremented and enforced (blocked at 0)
- [x] View personal bookings with live status (Confirmed / Pending / Cancelled)
- [x] Add and remove excursions from a personal favourites list
- [x] Submit cancellation requests for active bookings
- [x] Blocked from re-submitting if a request is already Pending, Approved, or Declined
- [x] In-app notification inbox with unread count badge
- [x] Notifications auto-marked as read on inbox open

### Administrator
- [x] Full CRUD for excursions
- [x] Server-side custom validation attributes for date rules
- [x] Client-side date picker min constraints matching server rules
- [x] Review all pending cancellation requests
- [x] Approve → booking marked Cancelled + user notified
- [x] Decline → booking restored to Confirmed + user notified
- [x] Full request history preserved in the database

---

## 💻 Usage

### Booking an Excursion
1. Browse excursions from the home page or the **Excursions** section
2. Click **View Details** on any excursion
3. Click **Book Now** and confirm on the booking confirmation page
4. View your booking under **My Bookings** in the navigation menu

### Cancelling a Booking
1. Navigate to **My Bookings**
2. Click **Cancel** on a confirmed booking and confirm
3. The request is sent to an administrator — status shows as **Pending**
4. Track the outcome under **My Cancellation Requests**
5. An in-app notification is sent once the admin decides

### Notifications
- The 🔔 bell icon in the navbar shows the unread count
- Click it to open the notification inbox — all notifications are marked as read automatically

### Admin — Excursion Management
1. Log in as an administrator and navigate to **Control Panel → Excursion Management**
2. Add, edit, or delete excursions
3. Date pickers enforce minimum date rules directly in the browser

### Admin — Cancellation Requests
1. Navigate to **Control Panel → Cancellation Requests**
2. Click **Approve** (green) or **Reject** (red) on any pending request
3. The booking status updates and the user receives a notification

---

## 🗄️ Database Setup

The project uses **Entity Framework Core** with a Code-First approach. All entity configurations live in `TravelBuddy.Data/Configuration/`.

### Key Entities

| Entity | Description |
|---|---|
| `ApplicationUser` | Extends ASP.NET Core Identity user with `FullName` |
| `Excursion` | Excursion with title, destination, dates, price, capacity, image |
| `Booking` | Links user to excursion; statuses: Pending / Confirmed / Cancelled |
| `BookingCancellationRequest` | Composite PK (UserId + BookingId); statuses: Pending / Approved / Declined |
| `Notification` | User inbox messages with `IsRead` flag |
| `Favorite` | Composite PK (UserId + ExcursionId) |

Connection string is configured in `appsettings.json`:

```json
"ConnectionStrings": {
  "TravelBuddyDbConnection": "Server=(localdb)\\mssqllocaldb;Database=TravelBuddyDb;Trusted_Connection=True;"
}
```

To apply all migrations:

```bash
dotnet ef database update --project TravelBuddy.Data --startup-project TravelBuddy
```

---

## ⚙️ Configuration

Key settings in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "TravelBuddyDbConnection": "your-connection-string-here"
  },
  "AdminSettings": {
    "Username": "admin",
    "Email": "admin@travelbuddy.com",
    "Password": "Admin123!",
    "FullName": "Site Administrator"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

> ⚠️ **Never commit sensitive data** (passwords, connection strings) to source control. Use `appsettings.Development.json` or environment variables for local secrets.

---

## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork the repository
2. Create a new branch: `git checkout -b feature/your-feature-name`
3. Commit your changes: `git commit -m "Add some feature"`
4. Push to the branch: `git push origin feature/your-feature-name`
5. Open a Pull Request

---

## 📄 License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.

---

## 📬 Contact

**Deyan Dimitrov** – https://github.com/blueshero92

Project Link: [https://github.com/blueshero92/TravelBuddy]


