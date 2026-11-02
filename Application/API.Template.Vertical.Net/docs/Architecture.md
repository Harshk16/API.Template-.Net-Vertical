# Architecture

This project uses Vertical Slice Architecture for organizing ASP.NET Core API features.

## Core Guardrails

### Thin Controllers

Controllers are HTTP transport adapters. They accept input, delegate to the appropriate feature, and translate the result into an HTTP response.

Controllers must not contain business rules, database queries, messaging logic, or external API orchestration. If a controller grows beyond transport concerns, move the behavior into the relevant feature slice.

### Self-Contained Feature Slices

Each requirement should be implemented inside its own feature directory. A slice owns the request, response, validation, handler, mapping, and coordination required for one use case.

Feature slices must not depend on another slice's internal implementation. Shared code is appropriate only for stable, genuinely cross-cutting technical behavior. Explicit abstractions, domain contracts, or integration events should be used when slices need to communicate.

## Boundary Matrix

| Area | Owns | May contain | Must not contain |
|---|---|---|---|
| `Features/` | Business use cases | Requests, responses, handlers, validators, feature-specific rules | Generic infrastructure plumbing or unrelated feature internals |
| `Common/` | Technical cross-cutting concerns | Behaviors, abstractions, options, results, exceptions, middleware, extensions | Business rules, feature workflows, EF Core implementations |
| `Infrastructure/` | External technical plumbing | EF Core, migrations, brokers, API clients, security providers, storage | HTTP controller behavior or business decisions |
| `Controllers/` | HTTP transport | Routing, model binding, authorization metadata, response mapping | Business logic, persistence, messaging, external calls |
| `Program.cs` | Composition root | Startup and extension-method orchestration | Service registrations, middleware details, feature logic |

### Common

`Common` contains reusable, business-agnostic technical mechanisms such as pipeline behaviors, result types, exception handling, middleware, options, shared technical interfaces, and registration extensions.

It must not know about business concepts such as orders, customers, or payments.

### Infrastructure

`Infrastructure` contains implementations that communicate with external systems or concrete providers, including EF Core, message brokers, HTTP clients, identity integrations, and file storage.

Infrastructure may implement abstractions defined elsewhere, but business rules remain in the relevant feature or domain model.

## Dependency Direction

```text
Controllers
    ↓
Features
    ↓
Common abstractions

Infrastructure → Common abstractions
Infrastructure → external providers
```

Features should depend on abstractions when external behavior is required. Infrastructure supplies implementations during application composition.

## Documentation as Code

Update this document alongside architectural code changes. When introducing a new pattern or boundary:

1. Document the guardrail and intended dependency direction.
2. Keep examples representative and implementation-independent.
3. Remove guidance that no longer reflects the codebase.
