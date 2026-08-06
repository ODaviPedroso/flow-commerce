# Overview

FlowCommerce is an open source .NET commerce application organized around a responsibility-driven architecture inspired by Clean Architecture. Contributions should preserve clear dependency boundaries and remain focused on the scope agreed in an issue.

# Development Workflow

All contributions follow this workflow:

```text
Issue
↓
Feature Branch
↓
Pull Request
↓
Review
↓
Merge
```

Create or select an issue before starting work. Keep each branch and pull request focused on that issue, address review feedback, and merge only after approval and successful validation.

# Branch Naming

Use a short, descriptive branch name with one of these prefixes:

- `feature/*`
- `fix/*`
- `hotfix/*`
- `refactor/*`
- `docs/*`
- `test/*`
- `chore/*`

# Commit Convention

Use [Conventional Commits](https://www.conventionalcommits.org/) with a concise, imperative description.

Examples:

```text
feat(product): create aggregate
fix(order): validate quantity
docs(readme): update documentation
chore(ci): update workflow
```

# Pull Requests

Every pull request must include:

- Context
- Changes
- Out of scope
- Validation
- Checklist

Keep the description focused, link the related issue, and clearly identify breaking changes when applicable.

# Validation

Run the following commands before opening a pull request:

```text
dotnet restore
dotnet build
dotnet test
```
