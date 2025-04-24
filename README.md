# 🛒 E-Commerce Microservices

[![.NET](https://img.shields.io/badge/.NET%209-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-2496ED?style=flat-square&logo=docker&logoColor=white)](https://www.docker.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=flat-square&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![Redis](https://img.shields.io/badge/Redis-DC382D?style=flat-square&logo=redis&logoColor=white)](https://redis.io/)
[![RabbitMQ](https://img.shields.io/badge/RabbitMQ-FF6600?style=flat-square&logo=rabbitmq&logoColor=white)](https://www.rabbitmq.com/)
[![gRPC](https://img.shields.io/badge/gRPC-6DB33F?style=flat-square&logo=grpc&logoColor=white)](https://grpc.io/)
[![Status](https://img.shields.io/badge/Status-Under%20Development-yellow?style=flat-square)](/)

> ⚠️ **Project Status: Under Active Development**

---

## ✅ Current Features

- 🔄 **CQRS** with MediatR (commands, queries, handlers)
- 🧱 **EF Core** with full migrations in Ordering & Discount
- 🔁 **Event-driven architecture** using RabbitMQ
- 🔎 **Advanced filtering & pagination**
- 🧠 **Vertical Slice Architecture**
- 🧼 **Clean Architecture** with Domain-Driven Design (DDD)
- 📜 **Custom Exception Handling** via middleware
- 📥 **Request validation** via FluentValidation
- 🚦 **Rate limiting** & circuit breaker patterns
- 🧪 **Health checks** and readiness probes
- 🌐 **API versioning** and Carter-based minimal APIs
- ⚙️ **Hybrid Caching**: Redis + In-Memory
- 🧬 **gRPC service** (Discount.gRPC)
- 🚢 Fully **Dockerized**

---

## 🧰 Tech Stack

- [.NET 9 (Preview)](https://dotnet.microsoft.com/)
- ASP.NET Core Minimal APIs + [Carter](https://github.com/CarterCommunity/Carter)
- [EF Core](https://docs.microsoft.com/en-us/ef/core/) + PostgreSQL + SQL Server
- [Redis](https://redis.io/) for distributed caching
- [RabbitMQ](https://www.rabbitmq.com/) for async messaging
- [gRPC](https://grpc.io/) for inter-service comms (Discount)
- [Mapster](https://github.com/MapsterMapper/Mapster) for object mapping
- [MediatR](https://github.com/jbogard/MediatR)
- [FluentValidation](https://docs.fluentvalidation.net/)
- [Serilog](https://serilog.net/) for logging
- Docker & Docker Compose

---

## 🧠 Architectural Highlights

- 🧼 **Clean separation** of API, Application, Domain, Infrastructure
- 🎯 **CQRS & MediatR pipeline** with validation and logging behaviors
- 🧩 **Modular microservices** for Basket, Catalog, Orders, Discounts
- 🗂 **Shared contracts layer** for interfaces, pagination, errors
- 🧱 **Ordering** and **Discount.gRPC** use EF Core migrations + seed data
- 🔐 **Centralized exception middleware** with clear response formatting

---

## 📦 Services

- **Basket.API** — Manage customer baskets (likely Redis-backed)
- **Catalog.API** — Product browsing and filtering
- **Ordering.API** — Order management with full DB support
- **Discount.gRPC** — Efficient gRPC-based discount system (EF Core backed)

---

## 📥 Getting Started

```bash
# Start up the entire system
docker-compose up -d
