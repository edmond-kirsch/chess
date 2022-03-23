FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Chess.Identity/Chess.Identity/Chess.Identity.csproj", "Chess.Identity/"]
RUN dotnet restore "Chess.Identity/Chess.Identity.csproj"
COPY  ["src/Chess.Identity/Chess.Identity", "Chess.Identity/"]
WORKDIR "/src/Chess.Identity"
RUN dotnet build "Chess.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chess.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chess.Identity.dll"]
