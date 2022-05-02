# wapp-workshop

Repository Pattern &amp; Cryptographic Failures

## Prerequisites

- Docker is installed and running
- .NET 6 or higher
- .NET EF cli tools (`dotnet tool install --global dotnet-ef`)
- .NET aspnet-codegenerator (`dotnet tool install -g dotnet-aspnet-codegenerator`)

## Useful links

- [.NET CLI overview](https://docs.microsoft.com/en-us/dotnet/core/tools/)
- [Entity Framework Core tools reference](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)
- [dotnet-aspnet-codegenerator](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/tools/dotnet-aspnet-codegenerator?view=aspnetcore-6.0)

## Workshop  part 1

1. Check if you have all prerequisites.
2. `dotnet restore`
3. Start the database by running `docker-compose up -d`
4. Scaffold dbcontext (& models) with `dotnet ef dbcontext scaffold "Server=localhost,1434;Database=AIRBNB2022;User Id=sa;password=DjhKBwdzVftaZufgdKkpof;Trusted_Connection=False" Microsoft.EntityFrameworkCore.SqlServer -o Models` * change password if it differs from the out-of-the-box one.
5. Uncomment the code in `IListingService.cs`, `ListingService.cs` and `Program.cs`
6. By default, `dbcontext scaffold` puts the database credentials inside `AIRBNB2022Context.cs`. For obvious reasons this is not desired. A cleaner way (using connection strings) has been provided inside `Program.cs`.
7. `dbcontext scaffold` generates `entity.HasNoKey()` by default. We cannot scaffold a controller without a key. Find the `entity.HasNoKey()` for the `Listing` entity inside `models/AIRBNB2022Context.cs` and remove it.
8. Scaffold a basic controller with `dotnet-aspnet-codegenerator controller -name ListingsController -async -api -m Listing -dc AIRBNB2022Context -outDir Controllers`
9. This controller now contains a `dbcontext<AIRBNB2022Context>`, and should function normally.
10. In order to implement a repository pattern, a relevant service (`ListingService.cs`) and interface is provided. Refactor the `ListingsController` to use this service instead of the dbcontext directly, using DI to pass in the `IListingService`. Do not forget to register the service (and interface) in `Program.cs`.
11. Instead of injecting the dbcontext, use the `IListingService`.
12. Implement the missing methods in `ListingService`. (Hint: take a look at `ListingsController.cs`)
13. (bonus) The `ListingService` now uses the `Listing` model throughout. In the last workshop we learned this isn't a great idea (remember?). Hint: `ListingsController.cs`. Create and use DTOs to mitigate this problem.

## Workshop part 2
(You can complete this part without the code from part 1.)

1. Install [wireshark](https://www.wireshark.org/#download). (On mac, you also need to install ChmodBPF, this is shipped with the installer.)
2. In `Program.cs` comment out `listenOptions.UseHttps();` and `app.UseHttpsRedirection();`.
3. Start the dotnet application and launch Wireshark.
4. In wireshark, capture the traffic of interface `Loopback: lo0`
5. Add the following filter to only capture http traffic of this application port: `tcp.port == 5001 && http`
6. Now make a post request to `http://localhost:5001/login`
7. In wireshark, check the traffic of the http request.
8. To fix this go to Program.cs and uncomment `listenOptions.UseHttps();` and `app.UseHttpsRedirection();`.
9. Now make a post request to `http://localhost:5001/login`
10. In wireshark add the following filter to only capture https traffic of this application port: `tcp.port == 5001 && ssl`
11. To prevent passwords from being saved in plain text you can use hashing.
12. In `LoginController.cs` uncomment the second login method and comment the first one out.
13. Now make a post request to `http://localhost:5001/login`