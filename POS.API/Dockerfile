#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["POS.API/POS.API.csproj", "POS.API/"]
COPY ["POS.Common/POS.Common.csproj", "POS.Common/"]
COPY ["POS.Data/POS.Data.csproj", "POS.Data/"]
COPY ["POS.Helper/POS.Helper.csproj", "POS.Helper/"]
COPY ["POS.Domain/POS.Domain.csproj", "POS.Domain/"]
COPY ["POS.MediatR/POS.MediatR.csproj", "POS.MediatR/"]
COPY ["POS.Repository/POS.Repository.csproj", "POS.Repository/"]
COPY ["SAPB1/SAPB1.csproj", "SAPB1/"]
RUN dotnet restore "POS.API/POS.API.csproj"
COPY . .
WORKDIR "/src/POS.API"
RUN dotnet build "POS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "POS.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "POS.API.dll"]
