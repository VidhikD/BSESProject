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

<img width="1500" alt="Screenshot 2025-04-09 at 1 42 28 PM" src="https://github.com/user-attachments/assets/730c9006-65f2-4c07-8041-77b97af6dacb" />
<img width="1512" alt="Screenshot 2025-04-09 at 1 43 05 PM" src="https://github.com/user-attachments/assets/616e8bb3-ed4c-4f2e-b1dc-3a745faa0e38" />
<img width="1512" alt="Screenshot 2025-04-09 at 1 43 16 PM" src="https://github.com/user-attachments/assets/591fab86-1af0-4fb8-952b-4cd430ade843" />
<img width="1512" alt="Screenshot 2025-04-28 at 2 48 11 AM" src="https://github.com/user-attachments/assets/85df132c-3ae4-4238-9cc3-3d3edc025967" />
<img width="1512" alt="Screenshot 2025-04-28 at 2 48 55 AM" src="https://github.com/user-attachments/assets/94ecc281-dacc-47e3-9a5e-cb1cb774eaae" />
<img width="1512" alt="Screenshot 2025-04-28 at 2 52 19 AM" src="https://github.com/user-attachments/assets/a48ebe4b-2485-4f69-8add-34ff27ad2c16" />




