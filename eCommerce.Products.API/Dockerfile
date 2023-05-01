#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0.5-alpine3.17-amd64 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0.203-alpine3.17-amd64 AS build
WORKDIR /src
COPY ["eCommerce.Products.API/eCommerce.Products.API.csproj", "eCommerce.Products.API/"]
COPY ["eCommerce.Products.Infrastructure/eCommerce.Products.Infrastructure.csproj", "eCommerce.Products.Infrastructure/"]
COPY ["eCommerce.Products.Persistence/eCommerce.Products.Persistence.csproj", "eCommerce.Products.Persistence/"]
COPY ["eCommerce.Products.Application/eCommerce.Products.Application.csproj", "eCommerce.Products.Application/"]
COPY ["eCommerce.Products.Domain/eCommerce.Products.Domain.csproj", "eCommerce.Products.Domain/"]
COPY ["eCommerce.Products.Presentation/eCommerce.Products.Presentation.csproj", "eCommerce.Products.Presentation/"]
RUN dotnet restore "eCommerce.Products.API/eCommerce.Products.API.csproj"
COPY . .
WORKDIR "/src/eCommerce.Products.API"
RUN dotnet build "eCommerce.Products.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eCommerce.Products.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.Products.API.dll"]