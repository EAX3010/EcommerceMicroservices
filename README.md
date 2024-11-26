# E-Commerce Microservices

[![.NET](https://img.shields.io/badge/.NET%209-512BD4?style=flat-square&logo=.net&logoColor=white)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-2496ED?style=flat-square&logo=docker&logoColor=white)](https://www.docker.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=flat-square&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![Redis](https://img.shields.io/badge/Redis-DC382D?style=flat-square&logo=redis&logoColor=white)](https://redis.io/)

A modern e-commerce platform built with microservices architecture using .NET 9

## Services

### Catalog Service
- Product management (CRUD operations)
- Category-based filtering
- PostgreSQL for data persistence
- CQRS pattern with MediatR

### Basket Service
- Shopping cart management
- Hybrid caching (Redis + In-Memory)
- PostgreSQL for data persistence
- CQRS implementation

## Tech Stack
- .NET Core
- Docker
- PostgreSQL
- Redis
- MediatR
- FluentValidation
- Mapster
- Carter

## Getting Started

1. Clone the repository
2. Navigate to the solution directory
3. Run:
```bash
docker-compose up -d
```

## API Endpoints

### Catalog API (Port: 6000)
```
GET    /products              # Get all products
GET    /products/{id}         # Get product by ID
GET    /products/category/{category}
POST   /products             # Create product
PUT    /products             # Update product
DELETE /products/{id}        # Delete product
```

### Basket API (Port: 6001)
```
GET    /basket/{userName}    # Get user's basket
POST   /basket              # Create/Update basket
DELETE /basket/{userName}    # Delete basket
```

## Features

- Exception handling
- Request validation
- Health monitoring
- Logging
- Docker containerization
- CQRS pattern
- Distributed caching

