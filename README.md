
Readme · MD
# Clothing Store — Multi-Brand E-Commerce Backend
 
A layered ASP.NET Core Web API backend for a multi-brand clothing store —
catalog browsing, product variants (size/color), cart, checkout, order
history, and reviews, with role-based access for admins.
 
## Tech Stack
 
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger (API docs)
## Architecture
 
```
Model → Repository → Service → Controller
                             → DTOs (Response / Create / Update)
```
 
- **Model** — EF Core entity
- **Repository** — database access and queries
- **Service** — business rules, validation, DTO mapping
- **Controller** — handles HTTP requests, calls the service
Junction tables (`CartItem`, `OrderItem`) have no standalone service or
controller — they're only created/edited through their parent (`Cart`,
`Order`).
 
## Data Model
 
- **User** — has a `role`: `Admin` or `Customer`
- **Brand**
- **Category** — supports subcategories
- **Product** — belongs to a Brand and a Category
- **ProductVariant** — size, color, price, stock for a Product
- **Cart** / **CartItem** — one cart per user
- **Order** / **OrderItem** — created at checkout
- **Review** — rating (1–5) + comment on a Product
## Roles
 
- New accounts register as `Customer`. Admin accounts are set up separately.
- Admin-only: managing Brand/Category/Product/ProductVariant, updating order
  status, and admin-level listing endpoints.
- Everyone else only sees/edits their own data (cart, orders, profile).
