FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Chess.API/Chess.API.csproj", "Chess.API/"]
RUN dotnet restore "src/Chess.API/Chess.API.csproj"
COPY . .
WORKDIR "/src/Chess.API"
RUN dotnet build "Chess.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chess.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chess.API.dll"]
