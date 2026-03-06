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
6. [Testing](#6-testing)
7. [Limitations & Future Work](#7-limitations--future-work)
8. [Documentation](#8-documentation)

## 1. Overview

**The ATU Brew** is a .NET MAUI mobile application for a small coffee shop, allowing customers to browse the menu and place orders from their mobile device.

<img src="docs/screenshots/01-splash.jpg" width="250" alt="Splash Screen" />

[The ATU Brew — App Walkthrough](docs/app.md) 

Key features:
- Browse menu by category: Hot Drinks, Cold Drinks, Food
- Add items to basket with quantity tracking
- Remove items from basket
- Checkout with customer name and phone number
- Unique order number generated per order (`ATU-XXXXXXXX`)
- Order history showing last 7 days of orders
- Local data persistence using SQLite

## 2. Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- Android emulator or physical Android device (USB debugging enabled)
- .NET MAUI Android workload

```bash
dotnet workload install maui-android
```

> **Linux users:** Android SDK must be installed at `/usr/lib/android-sdk`. See [CONTRIBUTING.md](CONTRIBUTING.md) for full setup steps.

### Build

```bash
# Clone repository
git clone https://github.com/edsonesf/ATU-CP-CA1
cd ATU-CP-CA1

# Restore packages
dotnet restore CoffeeShopApp/CoffeeShopApp.csproj

# Build (Linux)
dotnet build -p:AndroidSdkDirectory=/usr/lib/android-sdk CoffeeShopApp/CoffeeShopApp.csproj
```

### Run on Device

```bash
# Connect Android device via USB with USB Debugging enabled, then:
dotnet build -t:Run -f net9.0-android -p:AndroidSdkDirectory=/usr/lib/android-sdk CoffeeShopApp/CoffeeShopApp.csproj
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

The solution is split into three projects:

```
CoffeeShopApp          → MAUI Android app (Views, MAUI-specific ViewModels, DI wiring)
CoffeeShopApp.Core     → Shared class library (Models, base ViewModels, Services, Helpers)
CoffeeShopApp.Tests    → xUnit test project (references Core only, no MAUI dependency)
```

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
│         IDatabaseService            │
│  SaveOrderAsync / GetRecentOrders   │
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
- Shell navigation calls are isolated in MAUI ViewModels — Core ViewModels have no MAUI dependency

## 5. Menu

See [coffee-shop.md](docs/coffee-shop.md) for the full menu with prices.

## 6. Testing

The solution includes a separate test project `CoffeeShopApp.Tests` using xUnit.

```bash
cd CoffeeShopApp.Tests
dotnet test
```

**What is tested (15 tests):**
- `BasketViewModel` — add item, duplicate item quantity increment, total calculation, remove item, HasItems, ItemCount, Clear
- `OrderItem.Subtotal` — computed price × quantity
- `CheckoutViewModel` — empty name blocked, invalid phone blocked, valid order saves once and clears basket, order number has `ATU-` prefix

Tests target `CoffeeShopApp.Core`, a shared class library containing Models, ViewModels (pure logic), and Services — decoupled from MAUI so they run on any platform without an Android device.

Shell navigation and alerts are abstracted behind virtual methods (`ShowAlertAsync`, `OnOrderPlacedAsync`), allowing tests to use a `TestableCheckoutViewModel` subclass with no MAUI dependency.

## 7. Limitations & Future Work

**Current limitations:**
- Android only — iOS/macOS builds require a Mac
- Menu is hardcoded — no admin interface to add/edit items
- No user authentication — anyone can place an order
- SQLite is local to the device — no cloud sync or backend API
- No push notifications for order status

**What would be added next:**
- Backend API (ASP.NET Core) to centralise orders across devices
- Order status tracking (Pending → Ready → Collected)
- Menu loaded from database instead of hardcoded in ViewModel

## 8. Documentation

- [docs/app.md](docs/app.md) – full app walkthrough with screenshots
- [docs/coffee-shop.md](docs/coffee-shop.md) – full menu with prices
- [CONTRIBUTING.md](CONTRIBUTING.md) – git workflow, environment setup, device testing guide
