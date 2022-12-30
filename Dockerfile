#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CertiEx.Web/CertiEx.Web.csproj", "CertiEx.Web/"]
COPY ["CertiEx.Business/CertiEx.Business.csproj", "CertiEx.Business/"]
COPY ["CertiEx.Common/CertiEx.Common.csproj", "CertiEx.Common/"]
COPY ["CertiEx.Dal/CertiEx.Dal.csproj", "CertiEx.Dal/"]
COPY ["CertiEx.Domain/CertiEx.Domain.csproj", "CertiEx.Domain/"]
RUN dotnet restore "CertiEx.Web/CertiEx.Web.csproj"
COPY . .
WORKDIR "/src/CertiEx.Web"
RUN dotnet build "CertiEx.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CertiEx.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

EXPOSE 80

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CertiEx.Web.dll"]