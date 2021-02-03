#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["RDI.Challenge.Console/RDI.Challenge.Console.csproj", "RDI.Challenge.Console/"]
RUN dotnet restore "RDI.Challenge.Console/RDI.Challenge.Console.csproj"
COPY . .
WORKDIR "/src/RDI.Challenge.Console"
RUN dotnet build "RDI.Challenge.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RDI.Challenge.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RDI.Challenge.Console.dll"]