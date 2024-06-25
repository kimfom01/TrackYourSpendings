FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./Core/ ./Core/
COPY ./Infrastructure/ ./Infrastructure/
COPY ./UI/ ./UI/

RUN dotnet restore ./UI/TrackYourSpendings.Web/TrackYourSpendings.Web.csproj
RUN dotnet publish ./UI/TrackYourSpendings.Web/TrackYourSpendings.Web.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TrackYourSpendings.Web.dll"]
