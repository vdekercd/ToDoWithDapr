#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["ToDoWithDapr.WebApp/ToDoWithDapr.WebApp.csproj", "ToDoWithDapr.WebApp/"]
COPY ["TodoWithDapr.Shared/TodoWithDapr.Shared.csproj", "TodoWithDapr.Shared/"]
RUN dotnet restore "ToDoWithDapr.WebApp/ToDoWithDapr.WebApp.csproj"
COPY . .
WORKDIR "/src/ToDoWithDapr.WebApp"
RUN dotnet build "ToDoWithDapr.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDoWithDapr.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoWithDapr.WebApp.dll"]