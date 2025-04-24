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

---

## 📋 Table of Contents
- [Overview](#-overview)
- [Features](#-features)
- [Architecture](#-architecture)
- [Tech Stack](#-tech-stack)
- [Microservices](#-microservices)
- [Getting Started](#-getting-started)
- [Docker Deployment](#-docker-deployment)

---

## 🔍 Overview

A modern e-commerce platform built using a microservices architecture with .NET 9. This project demonstrates a scalable e-commerce solution where each business capability is isolated into separate services that can be developed, deployed, and scaled independently.

---

## ✨ Features

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

## 🏗️ Architecture

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

## 🛠️ Tech Stack

- **Backend:** .NET 9, ASP.NET Core Minimal APIs
- **Database:** PostgreSQL, SQL Server, Entity Framework Core
- **Caching:** Redis StackExchange, Hybrid Caching (`Microsoft.Extensions.Caching.Hybrid`, `Microsoft.Extensions.Caching.StackExchangeRedis`)
- **Service Communication:** gRPC
- **API Composition:** Carter (9.0.0)
- **Health Checks:** AspNetCore.HealthChecks.NpgSql, AspNetCore.HealthChecks.Redis, AspNetCore.HealthChecks.UI.Client
- **DI:** Scrutor (5.0.2) + .NET native DI
- **Cloud:** Microsoft.VisualStudio.Azure.Containers.Tools.Targets (1.21.0)
- **gRPC:** Grpc.AspNetCore (2.67.0), Grpc.AspNetCore.Server.Reflection (2.67.0)
- **Tools:** Docker, Docker Compose

---

## 🚀 Microservices

### Catalog Service
- Product catalog management
- Product categories and inventory
- Product search and filtering
- PostgreSQL database
- Entity Framework Core

### Basket Service
- Shopping cart management (GetBasket, StoreBasket, DeleteBasket)
- User session handling
- Hybrid caching with custom JsonHybridCacheSerializer
- Redis StackExchange integration
- Health checks for Redis and NpgSql
- Carter for API composition

### Discount Service
- Promotional discounts and Coupon management
- gRPC implementation with service reflection
- SQL Server database with Entity Framework Core
- Database migrations and seeding
- Proto file-based service definition
- Models with Coupon entity

### Ordering Service
- Order processing with CRUD operations
- Multiple endpoint handlers (CreateOrder, DeleteOrder, GetOrders, GetOrdersByCustomer, GetOrdersByName, UpdateOrder)
- Clean architecture with separate Application, Domain, and Infrastructure layers
- Carter for minimal API routing
- Entity Framework Core with SQL Server
- Dependency injection with proper service registration

---

## 🚦 Getting Started

### Prerequisites
- .NET 9 SDK
- Docker and Docker Compose
- PostgreSQL/SQL Server (or run containerized versions)

### Setup and Installation

1. Clone the repository
```bash
git clone https://github.com/EAX3010/EcommerceMicroservices.git
cd EcommerceMicroservices
```

2. Build the solution
```bash
dotnet build
```

3. Setup databases (if running locally)
```bash
# Database migrations are applied automatically on startup
# See individual service Dockerfiles and Program.cs files for details
```

4. Run the services individually (for development)
```bash
cd Services/Catalog/Catalog.API
dotnet run

# Repeat for other services
```

---

## 🐳 Docker Deployment

To run the entire application using Docker:

```bash
docker-compose up -d
```

This will start all services and their dependencies defined in the docker-compose.yml file.

### Service Endpoints (when running with docker-compose)

- Catalog API: `http://localhost:8000`
- Basket API: `http://localhost:8001`
- Discount gRPC: `http://localhost:8002`
- Ordering API: `http://localhost:8003`

Each service includes:
- Health checks integration (AspNetCore.HealthChecks)
- Containerization with Docker
- Azure Container Tools support
- Configuration via appsettings.json

---

## 📄 License

This project is licensed under the MIT License.

---

## 📞 Contact

Project Link: [https://github.com/EAX3010/EcommerceMicroservices](https://github.com/EAX3010/EcommerceMicroservices)
