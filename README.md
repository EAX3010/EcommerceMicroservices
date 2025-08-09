
Been working on this in my free time because, honestly, I was curious if I could actually pull off a full microservices setup with clean architecture without falling into the usual traps.

**Mistakes I wanted to avoid (and that I see in a lot of these projects):**

- Stuffing business logic into controllers
- "God" entities that do everything
- Services that are just glorified CRUD wrappers
- All microservices sharing a single database
- Skipping proper message-driven communication and relying on direct calls everywhere
- Treating DTOs, entities, and domain models as the same thing
- Forgetting about boundaries between microservices, making them tightly coupled

I wanted each service to be focused, maintainable, and actually follow clean architecture principles, not just look like it on a diagram.

**What's inside:**

- 4 microservices: Catalog, Basket, Discount, Orders
- PostgreSQL, SQL Server, SQLite, Redis
- REST APIs + gRPC
- Clean Architecture + CQRS + Vertical Slices
- Event-driven messaging (RabbitMQ)
- .NET 9 with the latest features
- Docker ready setup

**Current state:**

✅ Core business logic done  
✅ Full inter-service communication  
✅ CRUD + basket + discounts + orders

**Still to come:**

- Authentication
- Monitoring  
- Kubernetes deployment

