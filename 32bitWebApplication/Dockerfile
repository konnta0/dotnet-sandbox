FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["32bitWebApplication/32bitWebApplication.csproj", "32bitWebApplication/"]
RUN dotnet restore "32bitWebApplication/32bitWebApplication.csproj"
COPY . .
WORKDIR "/src/32bitWebApplication"
RUN dotnet build "32bitWebApplication.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "32bitWebApplication.csproj" --runtime linux-x86 -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "32bitWebApplication.dll"]
