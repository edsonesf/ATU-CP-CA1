# Atlantic Technological University (ATU)

DEVP_IT803 - LY_ICSWD_B: Cross Platform Development (2025/26)

Professor Gerard McCloskey

Continuous Assessment 1 (CA1) – The ATU Brew Coffee Shop App

## CA1 – Individual Project

| Student ID | Name | GitHub |
|---|---|---|
| L00196839 | Edson Ferreira | edsonesf |

---

# The ATU Brew – Coffee Shop Mobile App

## Table of Contents

1. [Overview](#1-overview)
2. [Getting Started](#2-getting-started)
3. [Technology Stack](#3-technology-stack)
4. [Architecture](#4-architecture)
5. [Menu](#5-menu)
6. [Documentation](#6-documentation)

## 1. Overview

**The ATU Brew** is a .NET MAUI mobile application for a small coffee shop, allowing customers to browse the menu and place orders from their mobile device.

Key features:
- Browse menu by category: Hot Drinks, Cold Drinks, Food
- Add items to basket with quantity controls
- Checkout with customer name and phone number
- Unique order number generated per order
- Order history showing all orders placed today
- Local data persistence using SQLite

## 2. Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- Android emulator or physical device
- .NET MAUI workload installed

```bash
dotnet workload install maui
```

### Build & Run

```bash
# Clone repository
git clone https://github.com/edsonesf/ATU-CP-CA1
cd ATU-CP-CA1

# Restore packages
dotnet restore

# Build
dotnet build

# Run on Android emulator
dotnet build -t:Run -f net9.0-android
```

## 3. Technology Stack

- **C# / .NET 9**
- **.NET MAUI** – cross-platform mobile UI framework
- **SQLite** – local data persistence via `sqlite-net-pcl`
- **CommunityToolkit.Mvvm** – MVVM helpers (ObservableObject, RelayCommand)

### NuGet Packages

| Package | Purpose |
|---|---|
| `sqlite-net-pcl` | SQLite ORM |
| `SQLitePCLRaw.bundle_green` | SQLite native bindings |
| `CommunityToolkit.Mvvm` | MVVM base classes and source generators |

## 4. Architecture

MVVM (Model-View-ViewModel) pattern — required by the assessment marking scheme.

```
┌─────────────────────────────────────┐
│           Views (XAML)              │
│  Pages — no business logic here     │
└────────────────┬────────────────────┘
                 │ Data Binding
┌────────────────▼────────────────────┐
│          ViewModels                 │
│  Commands, properties, logic        │
└────────────────┬────────────────────┘
                 │ Uses
┌────────────────▼────────────────────┐
│      Services / DatabaseService     │
│  SQLite async CRUD operations       │
└────────────────┬────────────────────┘
                 │ Maps to
┌────────────────▼────────────────────┐
│            Models                   │
│  MenuItem, OrderItem, Order         │
└─────────────────────────────────────┘
```

**Key rules:**
- No business logic in `.xaml.cs` files
- All commands use `[RelayCommand]`
- All bindable properties use `[ObservableProperty]`
- `BasketViewModel` registered as Singleton (shared state across pages)
- `DatabaseService` registered as Singleton (single DB connection)

## 5. Menu

See [coffee-shop.md](coffee-shop.md) for the full menu with prices.

## 6. Documentation

- [coffee-shop.md](coffee-shop.md) – menu data
