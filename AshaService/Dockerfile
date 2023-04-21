#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AshaService/AshaService.csproj", "AshaService/"]
RUN dotnet restore "AshaService/AshaService.csproj"
COPY . .
WORKDIR "/src/AshaService"
RUN dotnet build "AshaService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AshaService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AshaService.dll"]