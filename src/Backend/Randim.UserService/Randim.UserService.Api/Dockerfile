FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Randim.UserService/Randim.UserService.Api/Randim.UserService.Api.csproj", "Randim.UserService/Randim.UserService.Api/"]
RUN dotnet restore "Randim.UserService/Randim.UserService.Api/Randim.UserService.Api.csproj"
COPY . .
WORKDIR "/src/Randim.UserService/Randim.UserService.Api"
RUN dotnet build "Randim.UserService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Randim.UserService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Randim.UserService.Api.dll"]
