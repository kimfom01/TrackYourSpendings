FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./TrackYourSpendings.Domain/ ./TrackYourSpendings.Domain/
RUN dotnet build ./TrackYourSpendings.Domain/TrackYourSpendings.Domain.csproj

COPY ./TrackYourSpendings.Application/ ./TrackYourSpendings.Application/
RUN dotnet build ./TrackYourSpendings.Application/TrackYourSpendings.Application.csproj

COPY ./TrackYourSpendings.Infrastructure/ ./TrackYourSpendings.Infrastructure/
RUN dotnet build ./TrackYourSpendings.Infrastructure/TrackYourSpendings.Infrastructure.csproj

COPY ./TrackYourSpendings.Web/ ./TrackYourSpendings.Web/
RUN dotnet build ./TrackYourSpendings.Web/TrackYourSpendings.Web.csproj

RUN dotnet publish ./TrackYourSpendings.Web/TrackYourSpendings.Web.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TrackYourSpendings.Web.dll"]
