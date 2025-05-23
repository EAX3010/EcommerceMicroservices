# E-Commerce Microservices

[![.NET](https://img.shields.io/badge/.NET%209-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-2496ED?style=flat-square&logo=docker&logoColor=white)](https://www.docker.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=flat-square&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)
[![Redis](https://img.shields.io/badge/Redis-DC382D?style=flat-square&logo=redis&logoColor=white)](https://redis.io/)
[![RabbitMQ](https://img.shields.io/badge/RabbitMQ-FF6600?style=flat-square&logo=rabbitmq&logoColor=white)](https://www.rabbitmq.com/)
[![gRPC](https://img.shields.io/badge/gRPC-6DB33F?style=flat-square&logo=grpc&logoColor=white)](https://grpc.io/)
[![Azure](https://img.shields.io/badge/Azure%20Containers-0078D4?style=flat-square&logo=azure&logoColor=white)](https://azure.microsoft.com/)
[![Status](https://img.shields.io/badge/Status-Active-yellow?style=flat-square)](#)

> ⚠️ This project is under active development. Feedback and contributions are welcome!

Dapper Version [dapper migration](https://github.com/EAX3010/EcommerceMicroservices/tree/dapper-migration) – work in progress, replacing EF Core with Dapper


## 📋 Table of Contents
- [Overview](#-overview)
- [Features](#-features)
- [Architecture](#-architecture)
- [Microservices](#-microservices)
- [Docker Deployment](#-docker-deployment)

---

## 🔍 Overview

A modern e-commerce platform built using a microservices architecture with .NET 9. This project demonstrates a scalable e-commerce solution where each business capability is isolated into separate services that can be developed, deployed, and scaled independently.

---

## Features

- CQRS architecture with MediatR and pipeline behaviors
- Clean Architecture + Domain-Driven Design
- EF Core with full migration/seeding support (PostgreSQL, SQL Server, SQLite)
- Distributed messaging with RabbitMQ
- RESTful APIs via Minimal API & Carter
- Discount service via gRPC with service reflection
- Hybrid caching (Redis + in-memory)
- Marten support for document/event sourcing
- FluentValidation-based request validation
- Health checks for PostgreSQL, Redis, and service readiness
- Serilog for structured logging
- Dockerized services with support for Azure Containers
- Scrutor for automatic DI registration

---

## Architecture

The application follows domain-driven design principles and microservices architecture:

```
┌───────────────┐     ┌───────────────┐     ┌───────────────┐     ┌───────────────┐
│    Catalog    │     │    Basket     │     │   Discount    │     │   Ordering    │
│   Service     │     │   Service     │     │   Service     │     │   Service     │
└───────┬───────┘     └───────┬───────┘     └───────┬───────┘     └───────┬───────┘
        │                     │                     │                     │
        │                     │                     │                     │
        │                     │                     │                     │
┌───────┴─────────────────────┴─────────────────────┴─────────────────────┴───────┐
│                                                                                 │
│                             Service Communication                               │
│                (REST, gRPC, RabbitMQ Message Bus, Redis Cache)                  │
│                                                                                 │
└─────────────────────────────────────────────────────────────────────────────────┘
```

---

## Microservices

### Catalog Service - Vertical Slices
- Product catalog management
- Product categories and inventory
- Product search and filtering
- PostgreSQL database
- Entity Framework 

### Basket Service - Vertical Slices
- Shopping cart (GetBasket, StoreBasket, DeleteBasket)
- Hybrid caching (Preview)
- Redis  
- gRPC client for Discount service communication

### Discount Service - Vertical Slices
- Promotional discounts and Coupon management
- gRPC implementation with service reflection
- SQLite  
- Proto file-based service definition

### Ordering Service - Clean Architecture 
- Order processing with CRUD operations
- Endpoint (CreateOrder, DeleteOrder, GetOrders, GetOrdersByCustomer, GetOrdersByName, UpdateOrder)
- Clean architecture with separate Application, Domain, and Infrastructure layers
- Entity Framework with SQL Server


---

## Docker Deployment

The entire application can be deployed using Docker Compose:

```bash
docker-compose up -d
```

### Container Configuration

| Service | Container | Database | Cache | Ports (External:Internal) |
|---------|-----------|----------|-------|--------------------------|
| Catalog API | catalog.api | PostgreSQL (catalog.db) | - | 6000:8080, 6060:8081 |
| Basket API | basket.api | PostgreSQL (basket.db) | Redis (hybridcache) | 6001:8080, 6061:8081 |
| Discount gRPC | discount.grpc | SQLite (DiscountDb) | - | 6002:8080, 6062:8081 |
| Ordering API | ordering.api | SQL Server (ordering.db) | - | 6003:8080, 6063:8081 |

### Service Dependencies
- **Basket API** depends on: basket.db, hybridcache, discount.grpc
- **Catalog API** depends on: catalog.db
- **Discount gRPC** has no dependencies
- **Ordering API** depends on: ordering.db

### Volume Persistence
- catalog_volume: PostgreSQL data for Catalog service
- basket_volume: PostgreSQL data for Basket service
- ordering_volume: SQL Server data for Ordering service

### Service Endpoints (when running with docker-compose)

- Catalog API: `http://localhost:6000` (HTTP), `https://localhost:6060` (HTTPS)
- Basket API: `http://localhost:6001` (HTTP), `https://localhost:6061` (HTTPS)
- Discount gRPC: `http://localhost:6002` (HTTP), `https://localhost:6062` (HTTPS)
- Ordering API: `http://localhost:6003` (HTTP), `https://localhost:6063` (HTTPS)

Each service includes:
- Carter for minimal API routing 
- Health checks 
- Global Exception Handler
- Containerization with Docker
- Azure Container Tools support
- Configuration via appsettings.json

---

## License

This project is licensed under the MIT License.

