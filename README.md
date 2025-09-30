# MovieDB — Modular Full‑Stack (Angular 17 + ASP.NET Core 8 + EF Core + SQLite + JWT)

A modular project with Controllers, Services, Repositories, DTOs & Validation, EF Core (SQLite), JWT Auth, and a seeded catalog (1200+ movies).

## Quickstart
### Backend
```bash
cd backend/MovieDb.Api
dotnet restore
dotnet run --urls http://localhost:5000
```
Swagger: `http://localhost:5000/swagger`  
Default user: `demo` / `demo123`

### Frontend
```bash
cd frontend
npm install
npm start
# http://localhost:4200
```

## Highlights
- Clean layering: Controllers → Services → Repositories → EF Core
- DTOs + FluentValidation
- JWT authentication & BCrypt password hashing
- Sorting (title/year/rating), pagination, search
- Reviews posting protected by JWT
