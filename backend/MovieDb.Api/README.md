# MovieDB Backend (ASP.NET Core 8) — Modular

- Controllers: `Auth`, `Movies`, `Reviews`
- Layers: Controllers → Services → Repositories → EF Core (SQLite)
- DTOs + FluentValidation (Register, CreateReview)
- JWT auth, BCrypt password hashing
- Seed: 1,200+ movies + demo user (`demo`/`demo123`)
- Sorting, pagination, search; reviews require auth

## Run
```bash
dotnet restore
dotnet run --urls http://localhost:5000
# Swagger: http://localhost:5000/swagger
```
