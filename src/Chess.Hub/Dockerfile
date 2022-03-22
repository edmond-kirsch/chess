FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Chess.Hub/Chess.Hub.csproj", "Chess.Hub/"]
RUN dotnet restore "src/Chess.Hub/Chess.Hub.csproj"
COPY . .
WORKDIR "/src/Chess.Hub"
RUN dotnet build "Chess.Hub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chess.Hub.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chess.Hub.dll"]
