# E-Commerce Microservices

[![.NET](https://img.shields.io/badge/.NET%209-512BD4?style=flat-square&logo=.net&logoColor=white)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-2496ED?style=flat-square&logo=docker&logoColor=white)](https://www.docker.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=flat-square&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![Redis](https://img.shields.io/badge/Redis-DC382D?style=flat-square&logo=redis&logoColor=white)](https://redis.io/)
[![Status](https://img.shields.io/badge/Status-Under%20Development-yellow?style=flat-square)](/)

> ⚠️ **Project Status: Under Active Development**  
> This project is currently in active development and is not yet ready for production use. Features and documentation are being added and updated regularly. Feel free to star and watch the repository for updates!

A modern e-commerce platform built with microservices architecture using .NET 9

## Current Features
- CRUD operations
- filtering
- PostgreSQL (JSON documents) with Carter
- .Net 9 - Hybrid caching(preview) (Redis + In-Memory)
- CQRS pattern with MediatR
- Vertical Slice Architecture

## Tech Stack
- .NET Core
- Docker
- PostgreSQL
- Redis
- MediatR
- FluentValidation
- Mapster
- Carter

## Architecture Features
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