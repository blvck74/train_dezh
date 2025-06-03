# Инструкции по сборке и запуску TrainDezhApp

## Требования

- .NET 8.0 SDK
- PostgreSQL 15+
- Entity Framework CLI tools

## Установка зависимостей

### 1. Установка .NET 8.0 SDK
```bash
# Ubuntu/Debian
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0

# Или скачайте с официального сайта Microsoft
```

### 2. Установка PostgreSQL
```bash
# Ubuntu/Debian
sudo apt-get install postgresql postgresql-contrib

# Настройка пользователя
sudo -u postgres psql
postgres=# ALTER USER postgres PASSWORD 'postgres';
postgres=# CREATE DATABASE train_dezh;
postgres=# \q
```

### 3. Установка Entity Framework CLI
```bash
dotnet tool install --global dotnet-ef
```

## Настройка базы данных

### 1. Настройка строки подключения
Создайте файл `appsettings.json` в папке `TrainDezhApp`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=train_dezh;Username=postgres;Password=postgres"
  }
}
```

### 2. Применение миграций
```bash
cd TrainDezhApp
dotnet ef database update
```

## Сборка проекта

```bash
cd TrainDezhApp
dotnet restore
dotnet build
```

## Запуск приложения

```bash
cd TrainDezhApp
dotnet run
```

## Запуск тестов

```bash
cd TrainDezhApp.Tests
dotnet test
```

## Демонстрация функциональности

```bash
cd TrainDezhApp.Demo
dotnet run
```

## Учетные данные по умолчанию

- **Логин:** root
- **Пароль:** admin123
- **Роль:** Администратор

## Возможные проблемы

### 1. Ошибка подключения к базе данных
- Убедитесь, что PostgreSQL запущен
- Проверьте строку подключения в `appsettings.json`
- Убедитесь, что база данных `train_dezh` создана

### 2. Ошибки миграций
```bash
# Пересоздание миграций
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 3. Проблемы с GUI (в контейнере)
Приложение требует графическую среду. В контейнере без X11 GUI не будет работать.

### 4. Ошибки сборки
```bash
# Очистка и пересборка
dotnet clean
dotnet restore
dotnet build
```

## Структура проекта

```
TrainDezhApp/
├── Models/              # Модели данных
├── Services/            # Бизнес-логика
├── ViewModels/          # MVVM ViewModels
├── Views/               # UI компоненты
├── Converters/          # Конвертеры для UI
├── Styles/              # Стили Avalonia
├── Migrations/          # Миграции EF
└── appsettings.json     # Конфигурация

TrainDezhApp.Tests/      # Unit тесты
TrainDezhApp.Demo/       # Демонстрация
```