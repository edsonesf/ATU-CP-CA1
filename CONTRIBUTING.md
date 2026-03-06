# Contributing Guide

CA1 – The ATU Brew Coffee Shop App  
DEVP_IT803 - Cross Platform Development (2025/26)

---

## Table of Contents

- [Quick Start](#quick-start)
- [Git Workflow](#git-workflow)
- [Commit Message Standard](#commit-message-standard)
- [Branch Strategy](#branch-strategy)
- [Build & Run](#build--run)

---

## Quick Start

```bash
# Clone repository
git clone https://github.com/edsonesf/ATU-CP-CA1
cd ATU-CP-CA1

# Restore NuGet packages
dotnet restore

# Build
dotnet build

# Run on Android emulator
dotnet build -t:Run -f net9.0-android
```

---

## Git Workflow

### 1. Start from main (always up to date)

```bash
git checkout main
git pull origin main
```

### 2. Create a feature branch

```bash
git checkout -b feat/splash-page
```

### 3. Work, stage, and commit

```bash
git add .
git commit -m "feat: add splash page with 2s auto-navigate"
```

### 4. Push branch to GitHub

```bash
git push origin feat/splash-page
```

### 5. Merge into main when feature is complete and working

```bash
git checkout main
git merge feat/splash-page
git push origin main
```

---

## Commit Message Standard

Format: `type: short description`

| Type | When to use |
|---|---|
| `feat` | Adding a new feature |
| `fix` | Fixing a bug |
| `docs` | Documentation only changes |
| `style` | UI/styling changes (no logic) |
| `refactor` | Code restructure, no new feature |
| `test` | Adding or updating tests |
| `chore` | Build config, packages, tooling |

### Examples

```bash
git commit -m "feat: add hot drinks menu page with CollectionView"
git commit -m "feat: add basket with add/remove item commands"
git commit -m "fix: basket total not updating after item removal"
git commit -m "docs: update README with build instructions"
git commit -m "refactor: move order logic from code-behind to ViewModel"
git commit -m "chore: add sqlite-net-pcl and CommunityToolkit.Mvvm packages"
```

**Rules:**
- Use lowercase
- No full stop at the end
- Keep it under 72 characters
- Be specific — describe *what* changed, not *how*

---

## Branch Strategy

One branch per feature, merged into `main` when done and tested.

```
main
 ├── feat/project-setup
 ├── feat/splash-page
 ├── feat/menu-categories
 ├── feat/basket
 ├── feat/checkout
 └── feat/order-history
```

**Branch naming:**
- `feat/` – new feature
- `fix/` – bug fix
- `docs/` – documentation
- `refactor/` – code cleanup

**Rules:**
- Never commit directly to `main`
- Each branch = one task from the task list
- Merge only when the feature works and builds cleanly

---

## Code Quality & Best Practices

### MVVM Rules (required by marking scheme)
- No business logic in `.xaml.cs` files — only `InitializeComponent()` and DI constructor
- All commands use `[RelayCommand]` — never implement `ICommand` manually
- All bindable properties use `[ObservableProperty]` — no manual `INotifyPropertyChanged`
- ViewModels inherit from `BaseViewModel` (which extends `ObservableObject`)

### C# Clean Code
- Use `async/await` for all database and navigation calls — never block the UI thread
- Use `decimal` for all prices — never `float` or `double`
- Name methods clearly: `LoadMenuItemsAsync`, `PlaceOrderAsync`, `RemoveItemCommand`
- Keep methods short — if a method does more than one thing, split it
- Remove unused `using` statements and dead code before committing

### Naming Conventions
| Element | Convention | Example |
|---|---|---|
| Classes | PascalCase | `BasketViewModel` |
| Methods | PascalCase | `LoadOrdersAsync` |
| Properties | PascalCase | `CustomerName` |
| Private fields | camelCase with `_` | `_databaseService` |
| Constants | PascalCase | `DatabaseFilename` |

### Before Every Commit
```bash
dotnet build        # must pass with 0 errors, 0 warnings
```

- No commented-out code
- No hardcoded strings that should be constants
- No logic in XAML code-behind

---

## Build & Run

```bash
# Restore packages
dotnet restore

# Build (check for errors)
dotnet build

# Run on Android
dotnet build -t:Run -f net9.0-android
```

Always run `dotnet build` before committing — don't commit broken code.
