FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 433

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TrackYourSpendings.Web/TrackYourSpendings.Web.csproj", "TrackYourSpendings.Web/"]
RUN dotnet restore "TrackYourSpendings.Web/TrackYourSpendings.Web.csproj"
COPY . .
WORKDIR "/src/TrackYourSpendings.Web"
RUN dotnet build "TrackYourSpendings.Web.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "TrackYourSpendings.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "TrackYourSpendings.Web.dll"]
