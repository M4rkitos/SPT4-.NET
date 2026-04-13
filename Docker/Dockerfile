FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EasyAccess.Api/EasyAccess.Api.csproj", "EasyAccess.Api/"]
COPY ["EasyAccess.Domain/EasyAccess.Domain.csproj", "EasyAccess.Domain/"]
COPY ["EasyAccess.Application/EasyAccess.Application.csproj", "EasyAccess.Application/"]
COPY ["EasyAccess.Infrastructure/EasyAccess.Infrastructure.csproj", "EasyAccess.Infrastructure/"]
RUN dotnet restore "EasyAccess.Api/EasyAccess.Api.csproj"
COPY . .
WORKDIR "/src/EasyAccess.Api"
RUN dotnet build "EasyAccess.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasyAccess.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasyAccess.Api.dll"]