# 🔐 BSES Registration & Complaint Form

An Angular 19 + .NET 9 full-stack application for user registration and complaint submission with OTP verification via Twilio and secure file uploads. Built with modern UI, dynamic forms, and real-time validations.

---

## 🚀 Features

- 🔒 **OTP Verification** using Twilio
- 📄 **Form 1 (Existing Users)** – Requires CA Number verification
- 🆕 **Form 2 (New Users)** – Direct submission with generated CA Number
- 📎 **File Upload Support** for documents
- 📦 **SQL Server** database integration via EF Core
- 🎨 **Responsive Frontend** built with Angular 19
- ✅ **Popup Snackbar Alerts** for submission success
- 🔐 Secrets managed via `appsettings.Development.json` (Git-ignored)

---

## 📂 Project Structure

BsesForm/ ├── backend/ │ └── BackendAPI/ │ ├── Controllers/ │ ├── Models/ │ ├── wwwroot/uploads/ │ ├── appsettings.json │ └── appsettings.Development.json (git-ignored) ├── frontend/ │ └── (Angular 19 standalone app) └── README.md

yaml
Copy
Edit

---

## 🔧 Tech Stack

| Layer         | Tech Used                       |
|---------------|---------------------------------|
| Frontend      | Angular 19                      |
| Backend       | ASP.NET Core 9 (Web API)        |
| Database      | SQL Server (Docker or local)    |
| OTP Provider  | Twilio                          |
| ORM           | Entity Framework Core           |

---

##📸 Screenshots

