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

## Environment Setup (Linux/Debian)

Only needed once on a new machine.

### 1. Install .NET 9 SDK

```bash
# Add Microsoft package repository
wget https://packages.microsoft.com/config/debian/13/packages-microsoft-prod.deb -O /tmp/packages-microsoft-prod.deb
sudo dpkg -i /tmp/packages-microsoft-prod.deb

# Install .NET 9 SDK
sudo apt update
sudo apt install dotnet-sdk-9.0

# Verify
dotnet --version  # should print 9.0.x
```

### 2. Install MAUI Android workload

```bash
sudo dotnet workload install maui-android

# Verify
dotnet workload list  # should show maui-android
```

### 3. Install Android SDK

```bash
sudo apt install android-sdk
```

### 4. Install Android platform dependencies (API level 35)

```bash
sudo dotnet build -t:InstallAndroidDependencies -f net9.0-android \
  "-p:AndroidSdkDirectory=/usr/lib/android-sdk" \
  "-p:AcceptAndroidSDKLicenses=True" \
  CoffeeShopApp/CoffeeShopApp.csproj
```

---

## Quick Start

```bash
# Clone repository
git clone https://github.com/edsonesf/ATU-CP-CA1
cd ATU-CP-CA1

# Restore NuGet packages
dotnet restore CoffeeShopApp/CoffeeShopApp.csproj

# Build
dotnet build -p:AndroidSdkDirectory=/usr/lib/android-sdk CoffeeShopApp/CoffeeShopApp.csproj

# Run on Android emulator
dotnet build -t:Run -f net9.0-android -p:AndroidSdkDirectory=/usr/lib/android-sdk CoffeeShopApp/CoffeeShopApp.csproj
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
| `improve` | Enhancement to an existing feature |
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
- `improve/` – enhancement to existing feature

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
dotnet build        # must pass with 0 errors
dotnet test --project CoffeeShopApp.Tests  # must pass with 0 failures
```

- No commented-out code
- No hardcoded strings that should be constants
- No logic in XAML code-behind

---

## Build & Run

> **Important (Linux):** `/home/edson/ATU` is a symlink. Always use the real path `/home/edson/Documents/ATU/...` in build commands, otherwise the Android resource packager (aapt2) will fail to find generated assets.

```bash
# Restore packages
dotnet restore CoffeeShopApp/CoffeeShopApp.csproj

# Build (check for errors) — use real path, not symlink
dotnet build -p:AndroidSdkDirectory=/usr/lib/android-sdk \
  /home/edson/Documents/ATU/CrossPlatform/CA1/ATU-CP-CA1/CoffeeShopApp/CoffeeShopApp.csproj

# Run on connected device or emulator
dotnet build -t:Run -f net9.0-android -p:AndroidSdkDirectory=/usr/lib/android-sdk \
  /home/edson/Documents/ATU/CrossPlatform/CA1/ATU-CP-CA1/CoffeeShopApp/CoffeeShopApp.csproj
```

Always run `dotnet build` before committing — don't commit broken code.

---

## Building an APK

### Step 1 — Run tests first

```bash
dotnet test CoffeeShopApp.Tests/CoffeeShopApp.Tests.csproj
```

### Step 2 — Build the APK

```bash
dotnet publish -f net9.0-android -c Release \
  -p:AndroidSdkDirectory=/usr/lib/android-sdk \
  -p:AndroidPackageFormat=apk \
  /home/edson/Documents/ATU/CrossPlatform/CA1/ATU-CP-CA1/CoffeeShopApp/CoffeeShopApp.csproj
```

The signed APK will be at:
```
CoffeeShopApp/bin/Release/net9.0-android/publish/com.companyname.coffeeshopapp-Signed.apk
```

### Step 3 — Install on device

**Option A — ADB (recommended):**
```bash
/usr/lib/android-sdk/platform-tools/adb install \
  CoffeeShopApp/bin/Release/net9.0-android/publish/com.companyname.coffeeshopapp-Signed.apk
```

**Option B — Manual:**
1. Copy the `.apk` file to your phone (USB, email, or cloud)
2. Open it on the phone
3. Allow "Install from unknown sources" if prompted (Settings → Security)

---

## Testing on a Device

### Option 1 — Physical Android Phone (recommended)

1. On your phone: **Settings → About Phone → tap "Build Number" 7 times** to unlock Developer Options
2. Go to **Settings → Developer Options → enable USB Debugging**
3. Connect phone to your computer via USB and accept the prompt on the phone
4. Verify the device is detected:
   ```bash
   /usr/lib/android-sdk/platform-tools/adb devices
   # Should show your device serial number
   ```
5. Deploy and run:
   ```bash
   dotnet build -t:Run -f net9.0-android -p:AndroidSdkDirectory=/usr/lib/android-sdk CoffeeShopApp/CoffeeShopApp.csproj
   ```

### Option 2 — Android Emulator

1. Install the emulator tools:
   ```bash
   sudo /usr/lib/android-sdk/tools/bin/sdkmanager "emulator" "system-images;android-35;google_apis;x86_64"
   ```
2. Create a virtual device:
   ```bash
   /usr/lib/android-sdk/tools/bin/avdmanager create avd -n Pixel6 -k "system-images;android-35;google_apis;x86_64"
   ```
3. Start the emulator:
   ```bash
   /usr/lib/android-sdk/emulator/emulator -avd Pixel6 &
   ```
4. Wait for it to boot, then deploy:
   ```bash
   dotnet build -t:Run -f net9.0-android -p:AndroidSdkDirectory=/usr/lib/android-sdk CoffeeShopApp/CoffeeShopApp.csproj
   ```

> **Note:** The emulator requires hardware virtualisation (KVM on Linux). Check with `kvm-ok`. It also uses significant RAM — a physical device is simpler.
