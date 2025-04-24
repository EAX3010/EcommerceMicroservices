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

## 🛠️ Tech Stack

- **Backend:** .NET 9 , ASP.NET Core Minimal APIs
- **Database:** PostgreSQL, SQL Server, SQLite, Marten (Event Store / Document DB)
- **Caching:** Redis, In-Memory, Hybrid Caching (`Microsoft.Extensions.Caching.Hybrid`)
- **Message Bus:** RabbitMQ
- **gRPC:** `Grpc.AspNetCore`, Reflection-enabled
- **Validation:** FluentValidation
- **API Composition:** Carter
- **Object Mapping:** Mapster
- **Logging:** Serilog
- **Health Checks:** `AspNetCore.HealthChecks.*`
- **DI:** Scrutor + .NET native DI
- **Tools:** Docker, Docker Compose, Azure Container Targets


---

## 🚀 Getting Started

```bash
# Clone the repo
git clone https://github.com/EAX3010/EcommerceMicroservices.git
cd EcommerceMicroservices

# Start up the services
docker-compose up -d
