# SecclApp

This repository contains the **SecclBackend**, **SecclFrontend**, and **SecclShared** projects, built with .NET 8.0 and Blazor WebAssembly. The backend provides APIs, the frontend is a Blazor WebAssembly application, and SecclShared contains shared models for both projects.

## Prerequisites

Before cloning and running the project, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- A modern web browser
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)


## Getting Started

### 1. Clone the Repository
```sh
git clone https://github.com/Rajveer221099/SecclApp.git
cd SecclApp
```

### 2. Open the Solution
- Open `SecclBackend.sln` in Visual Studio 2022 or Visual Studio Code.

### 3. Restore NuGet Packages
```sh
dotnet restore SecclBackend.sln
```

### 4. Build the Solution
```sh
dotnet build SecclBackend.sln
```

### 5. Run the Backend API
```sh
cd SecclBackend
# For Windows
set ASPNETCORE_ENVIRONMENT=Development
# For Linux/macOS use: export ASPNETCORE_ENVIRONMENT=Development
dotnet run
```
- The backend will start (by default on https://localhost:7115 or as configured in `launchSettings.json`).

### 6. Run the Frontend (Blazor WebAssembly)
```sh
cd ../SecclFrontend
dotnet run
```
- The frontend will start (by default on https://localhost:PORT as shown in the terminal).

---

## Project Structure
- **SecclBackend/**: .NET 8 Web API backend
- **SecclFrontend/**: Blazor WebAssembly frontend
- **SecclShared/**: Shared models for both backend and frontend

---

## Key Features & Enhancements
- **State Management**: Centralized AppState service in frontend
- **Component Refactoring**: Modular Blazor components (ClientList, AccountTypeTable, etc.)
- **Modern UI**: Bootstrap 5, custom CSS, responsive and professional design
- **API Aggregation**: Batch endpoint for efficient data retrieval
- **Data Caching**: In-memory caching for client data in backend
- **Token Caching**: Backend caches authentication tokens for performance
- **Error Handling**: Standardized error response model
- **Configuration Management**: API URLs and credentials in `appsettings.json` (move to secrets for production)
- **CORS Policy**: Restricted to frontend origin for security

---

## Notes for Developers
- **Do not reference SecclBackend from SecclFrontend**. Use SecclShared for shared models.
- For production, move sensitive config (like credentials) to environment variables or a secret manager.
- UI/UX is fully modular and uses Bootstrap for a modern look. You can further customize in `wwwroot/css/app.css`.
- The batch API endpoint is `/api/portfolio/batch-aggregated-data` (POST, accepts list of client IDs).

---

## Contributing
1. Fork the repo and create your branch: `git checkout -b feature/your-feature`
2. Commit your changes: `git commit -am 'Add new feature'`
3. Push to the branch: `git push origin feature/your-feature`
4. Create a pull request

---

## License
This project is licensed under the MIT License.
