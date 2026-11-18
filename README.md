# Beauty Online — Salon Booking Platform

A multi-tenant web platform for hair salons and beauty shops to take online bookings, manage staff schedules, sell vouchers, and accept card payments.

This is a 2016–2019 codebase (latest work from August 2019), published as a portfolio snapshot. Secrets and credentials have been removed and replaced with `REPLACE_ME` placeholders. The project is not production-ready as-is.

## Licence

All rights reserved © John Zhao. The source is shown for portfolio purposes only; no permission is granted to use it commercially. See [LICENSE](LICENSE).


## What problem it solves

Independent salons often still take appointments by phone or walk-in. This application gives a salon:

- A public website where customers can book a service with a chosen stylist and time slot
- An admin area to manage staff, services, customers, bookings and vouchers
- Online card payment (Stripe) and email notifications
- Per-shop branding, so more than one salon can run on the same codebase

## Features

- Customer accounts, login, and booking history
- Service catalogue (name, duration, price range, category, stylist assignment)
- Staff (shop user) management and per-stylist appointment calendar (jqWidgets scheduler)
- Booking statuses (request, confirm, cancel) and store credit
- Voucher types and email delivery of vouchers
- Stripe card payments
- Contact / feedback forms and shop news
- Reporting API
- Email via SMTP (originally Gmail / SendGrid)
- Optional MongoDB data path alongside SQL Server (experimental / later work)

## Architecture

Classic n-tier .NET Framework solution:

```
App.Web                ASP.NET MVC 5 site + Web API (per-shop Areas: Demo, JoinUs)
App.Service            Application service handlers
App.Service.Contract   Service contracts / DTOs
App.BusinessEngine     Domain logic (bookings, customers, scheduler, payments, email)
App.Data               Entity Framework 6 DbContext and mappings
App.Data.Entities      Domain entities
App.Data.Repositories  Repositories (including stored-procedure helpers)
Core.Common            Shared utilities (encryption, configuration, auditing)
App.Test.*             Unit and integration tests
Develop.Web.Static     Static / marketing web project
App.WindowsService     Stub Windows service (not implemented)
```

The web front end is AngularJS 1.4 with Bootstrap 3 and a jqWidgets calendar. Dependency injection uses Autofac; mapping uses AutoMapper; logging uses log4net.

Multi-tenancy is implemented as MVC Areas: each salon gets its own area with branded views and content, sharing the same controllers, services and database (shop-scoped data).

## Tech stack

| Layer | Technology |
| --- | --- |
| Runtime | .NET Framework 4.5.2 (some libraries target 4.5) |
| Web | ASP.NET MVC 5.1, ASP.NET Web API 2, Razor |
| Front end | AngularJS 1.4, Bootstrap 3, jQuery, jqWidgets scheduler |
| Data | Entity Framework 6.1, SQL Server; optional MongoDB driver 2.2 |
| Payments | Stripe.net 2.4 |
| Email | SMTP / SendGrid |
| DI / mapping | Autofac 3.3, AutoMapper 3.2 |
| Tests | MSTest, Moq, AutoFixture |
| CI | Azure Pipelines (`azure-pipelines.yml`) — Windows, NuGet restore, MSBuild, VSTest |
| IDE | Visual Studio 2015 or 2017 (or later with the .NET Framework targeting pack) |

## Project structure

```
├── azure-pipelines.yml
├── src/
│   ├── App.sln
│   ├── App.Web/                 # MVC + Web API host
│   ├── App.Service/             # Service layer
│   ├── App.BusinessEngine/      # Business rules + Stripe/email/Mongo helpers
│   ├── App.Data*/               # EF + entities + repositories
│   ├── Core.Common/             # Shared kernel
│   ├── App.Test.UnitTests/
│   ├── App.Test.IntegrationTest/
│   └── Exlib/                   # Third-party / helper binaries
```

NuGet packages are not committed. They restore to `src/packages/` as a packages.config-style solution.

## How to build and run

1. Install Visual Studio 2017 or later with the **ASP.NET and web development** workload and the **.NET Framework 4.5.2 targeting pack**. You also need a local SQL Server (Express is fine) and, optionally, MongoDB if you exercise that path.
2. Clone the repo and open `src/App.sln`.
3. Restore NuGet packages (right-click the solution → Restore NuGet Packages, or `nuget restore src/App.sln`).
4. In `src/App.Web/Web.config` (and any other `App.config` you will run), replace every `REPLACE_ME` with local values:
   - SQL Server connection string password
   - Stripe secret key (use a test key)
   - SMTP username and password
   - Encryption passphrases / RSA key XML in `Core.Common` if you need login token encrypt/decrypt to work
   - The MongoDB URL in `App.BusinessEngine/MangoDb/MangoDbContext.cs` if you use that store
5. Create an empty database (default name `beauty_online`) and update the connection string to match. The project uses EF Code First in places; you may need to apply or recreate the schema from the existing mappings — this snapshot does not include a full migration history.
6. Set `App.Web` as the startup project and run. The demo shop area lives under the `Demo` MVC Area.

This repo will not run against the original production data or keys. Treat it as source to read, build, and modernise.


Third-party binaries with unclear licences were removed from `src/Exlib/` (ConfuserEx, QNV/caller-ID DLLs, DesktopAlert, SqlConverter zips, and vendored Xamarin.Android.Support assemblies). They are not required to read or compile the web application. If you later build a related mobile/desktop helper that referenced them, obtain those libraries separately (Xamarin Android Support packages can be restored via NuGet / the Xamarin workload).

## Status / roadmap

This is an archived snapshot of a live Irish salon product from the late 2010s. It is useful as a portfolio piece and as the starting point for a rewrite.

Known limitations of the snapshot:

- Framework and front-end stack are dated (MVC 5, AngularJS 1, .NET Framework).
- Encryption keys, API keys and connection strings must be supplied locally.
- The Windows service project is an empty stub.
- Some debug leftovers remain (for example a test token string in `TokenEncryptor`).

Planned direction if this product is revived: move to a modern ASP.NET Core API + a current SPA or server-rendered UI, use managed identity / secret stores instead of config-file secrets, tighten multi-tenant isolation, and productise onboarding so a salon can go live without a custom MVC Area.
