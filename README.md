# Track Your Spendings
 
<div align="center">
    <img src="image.png" alt="icon">
</div>

## Introduction

- This is an application where users can record and manage their financial budgets.
- The application is tailored to simplify the often complex process of categorizing transactions and analyzing expenses, making personal finance management accessible to all users.

---

## Features

- Ability to manage multiple wallets
- Search transactions record history by name, category and date
- Group and filter transactions by categories and date
- Record transactions
- Secured and protected account
- Sign in with google

---

## Roadmap

- Reports page with bar chart to show total spendings across months within a year
- Pie chart on report page to show percentage of spendings per category
- Read and parse bank statements (tinkoff, sberbank, vtb, etc.)

---

## Hosting

Self hosted on a VPS using docker

---

## Database Provider

- Postgres through [neon.tech](https://neon.tech/)

---

## Authors

Contributors names and contact info

- Kim Fom - [kimfom01@gmail.com](mailto:kimfom01@gmail.com)

---

## Installation

### Prerequisites

- Ensure [.NET SDK/Runtime](https://dotnet.microsoft.com/download) (version 8.0 is installed on your machine).
- Ensure you have `postgres` installed on your machine or you can connect to remote db.
- Install [Visual Studio](https://visualstudio.microsoft.com/) or your favorite editor/IDE.

### Getting the Project

- Clone the repository: `git clone https://github.com/kimfom01/TrackYourSpendings.git`
- Alternatively, download and extract the project ZIP file.

### Configuration

[//]: # "- Set necessary environment variables in `.env` file or system environment."

- Update `appsettings.json` with your typical [postgresql connection string](https://www.connectionstrings.com/postgresql/).

### Building the Project

- Navigate to the project's root directory in the terminal.
- Run `dotnet build` to compile the project.

### To generate new database migrations

- Navigate to the `Infrastructure` directory

```sh
cd src/Infrastructure
```

- Run the following to create migration for the app schema

```sh
dotnet ef migrations add InitialCreate --project TrackYourSpendings.Infrastructure/TrackYourSpendings.Infrastructure.csproj --context AppDataContext --output-dir ./Migrations/App/
```

- Run the following to create migration for the identity schema

```sh
 dotnet ef migrations add InitialCreate --project TrackYourSpendings.Infrastructure/TrackYourSpendings.Infrastructure.csproj --context AppIdentityDbContext --output-dir ./Migrations/Identity/
```

### Running the Application

- Navigate to the project root directory
- Restore dependencies `dotnet restore`
- Execute `dotnet run` (or `dotnet run -lp Kestrel` for https) within the project directory.
- Open `http://localhost:5162` or (`https://localhost:7001` for https) on the web browser to access the application.

### Publishing (For Deployment)

- Run `dotnet publish -c Release -o ./publish` to package the application for deployment.
- Deploy the contents of the `./publish` directory to your hosting environment.
