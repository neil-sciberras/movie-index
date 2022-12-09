#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["Movies.Server/Movies.Server.csproj", "Movies.Server/"]
COPY ["Movies.AppInfo/Movies.AppInfo.csproj", "Movies.AppInfo/"]
COPY ["Movies.Contracts/Movies.Contracts.csproj", "Movies.Contracts/"]
COPY ["Movies.Grains/Movies.Grains.Clients/Movies.Grains.Clients.csproj", "Movies.Grains/Movies.Grains.Clients/"]
COPY ["Movies.Grains/Movies.Grains.Clients.Interfaces/Movies.Grains.Clients.Interfaces.csproj", "Movies.Grains/Movies.Grains.Clients.Interfaces/"]
COPY ["Movies.Grains/Movies.Grains.Interfaces/Movies.Grains.Interfaces.csproj", "Movies.Grains/Movies.Grains.Interfaces/"]
COPY ["Movies.Grains/Movies.Grains/Movies.Grains.csproj", "Movies.Grains/Movies.Grains/"]
COPY ["Movies.GraphQL/Movies.GraphQL.csproj", "Movies.GraphQL/"]
COPY ["Movies.Extensions/Movies.Extensions.csproj", "Movies.Extensions/"]
COPY ["Movies.Infrastructure/Movies.Infrastructure.Authentication/Movies.Infrastructure.Authentication.csproj", "Movies.Infrastructure/Movies.Infrastructure.Authentication/"]
COPY ["Movies.Infrastructure/Movies.Infrastructure.Orleans/Movies.Infrastructure.Orleans.csproj", "Movies.Infrastructure/Movies.Infrastructure.Orleans/"]
COPY ["Movies.Utils/Movies.Utils.csproj", "Movies.Utils/"]
RUN dotnet restore "Movies.Server/Movies.Server.csproj"
COPY . .
WORKDIR "/src/Movies.Server"
RUN dotnet build "Movies.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Movies.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV ASPNETCORE_ENVIRONMENT Development
ENV DOCKER true
COPY ["movies.json", "."]
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Movies.Server.dll", "-storageDir", "."]