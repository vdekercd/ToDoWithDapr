#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["ToDoWithDapr.API/ToDoWithDapr.API.csproj", "ToDoWithDapr.API/"]
COPY ["TodoWithDapr.Shared/TodoWithDapr.Shared.csproj", "TodoWithDapr.Shared/"]
RUN dotnet restore "ToDoWithDapr.API/ToDoWithDapr.API.csproj"
COPY . .
WORKDIR "/src/ToDoWithDapr.API"
RUN dotnet build "ToDoWithDapr.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDoWithDapr.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoWithDapr.API.dll", "--urls", "http://*:80" ]