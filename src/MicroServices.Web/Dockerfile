#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MicroServices.Web/MicroServices.Web.csproj", "MicroServices.Web/"]
COPY ["MicroServices.Models/MicroServices.Models.csproj", "MicroServices.Models/"]
RUN dotnet restore "MicroServices.Web/MicroServices.Web.csproj"
COPY . .
WORKDIR "/src/MicroServices.Web"
RUN dotnet build "MicroServices.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroServices.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroServices.Web.dll"]
