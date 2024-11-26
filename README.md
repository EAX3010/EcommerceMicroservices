# E-Commerce Microservices

[![.NET](https://img.shields.io/badge/.NET%209-512BD4?style=flat-square&logo=.net&logoColor=white)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-2496ED?style=flat-square&logo=docker&logoColor=white)](https://www.docker.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=flat-square&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![Redis](https://img.shields.io/badge/Redis-DC382D?style=flat-square&logo=redis&logoColor=white)](https://redis.io/)

A modern e-commerce platform built with microservices architecture using .NET 9

## Services

- CRUD operations
- filtering
- PostgreSQL (JSON documents) with Carter
- .Net 9 - Hybrid caching(preview) (Redis + In-Memory)
- CQRS pattern with MediatR

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

### Catalog API
```
GET    /products              # Get all products
GET    /products/{id}         # Get product by ID
GET    /products/category/{category}
POST   /products             # Create product
PUT    /products             # Update product
DELETE /products/{id}        # Delete product
```

### Basket API
```
GET    /basket/{userName}    # Get user's basket
POST   /basket              # Create/Update basket
DELETE /basket/{userName}    # Delete basket
```

```

### Basket API
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

## Getting Started
1. Run:
```bash
docker-compose up -d
```


