#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NotificationGateway/NotificationGateway.csproj", "NotificationGateway/"]
COPY ["NG.BLL/NG.BLL.csproj", "NG.BLL/"]
RUN dotnet restore "NotificationGateway/NotificationGateway.csproj"
COPY . .
WORKDIR "/src/NotificationGateway"
RUN dotnet build "NotificationGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationGateway.dll"]
