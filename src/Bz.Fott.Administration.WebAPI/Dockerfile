#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Bz.Fott.Administration.WebAPI/Bz.Fott.Administration.WebAPI.csproj", "src/Bz.Fott.Administration.WebAPI/"]
COPY ["src/Bz.Fott.Administration.Application/Bz.Fott.Administration.Application.csproj", "src/Bz.Fott.Administration.Application/"]
COPY ["src/Bz.Fott.Administration.Domain/Bz.Fott.Administration.Domain.csproj", "src/Bz.Fott.Administration.Domain/"]
COPY ["src/Bz.Fott.Administration.Infrastructure/Bz.Fott.Administration.Infrastructure.csproj", "src/Bz.Fott.Administration.Infrastructure/"]
RUN dotnet restore "src/Bz.Fott.Administration.WebAPI/Bz.Fott.Administration.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Bz.Fott.Administration.WebAPI"
RUN dotnet build "Bz.Fott.Administration.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bz.Fott.Administration.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bz.Fott.Administration.WebAPI.dll"]