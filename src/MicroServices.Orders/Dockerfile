#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MicroServices.Orders/MicroServices.Orders.csproj", "MicroServices.Orders/"]
COPY ["MicroServices.Models/MicroServices.Models.csproj", "MicroServices.Models/"]
RUN dotnet restore "MicroServices.Orders/MicroServices.Orders.csproj"
COPY . .
WORKDIR "/src/MicroServices.Orders"
RUN dotnet build "MicroServices.Orders.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroServices.Orders.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroServices.Orders.dll"]