#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Demo.Repository.Pattern/Demo.Repository.Pattern.csproj", "Demo.Repository.Pattern/"]
RUN dotnet restore "Demo.Repository.Pattern/Demo.Repository.Pattern.csproj"
COPY . .
WORKDIR "/src/Demo.Repository.Pattern"
RUN dotnet build "Demo.Repository.Pattern.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.Repository.Pattern.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.Repository.Pattern.dll"]