FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Construct Planner API.csproj", "/ConstructPlannerAPI/"]
RUN dotnet restore "/ConstructPlannerAPI/Construct Planner API.csproj"
COPY . /ConstructPlannerAPI
WORKDIR /ConstructPlannerAPI
RUN mkdir -p /app/build/publish
RUN dotnet build "Construct Planner API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Construct Planner API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /ConstructPlannerAPI/Data /app/Data
RUN ls /app/Data && echo "Database file exists!" || echo "Database file not found!"
ENTRYPOINT ["dotnet", "Construct Planner API.dll"]