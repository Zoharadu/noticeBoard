# NoticeBoard
Backend Overview

The backend is implemented using ASP.NET Core Web API and follows Clean Architecture principles.

Architecture

Api – HTTP layer (controllers, request/response DTOs)

Application – Business logic and use cases (services, interfaces)

Domain – Core domain entities and business rules

Infrastructure – Data persistence and external concerns

Data Persistence

For the purpose of this assignment, data is persisted in a local JSON file instead of a database.
A dedicated storage model is used to represent the file structure, while domain entities remain isolated from storage concerns.

Design Decisions

DTOs are used to define a clear API contract and avoid exposing domain entities directly.

Mappers translate between domain entities and API DTOs.

Repositories abstract the data source, allowing easy replacement with a real database in the future.

This design keeps the system modular, testable, and ready for future scalabilit
