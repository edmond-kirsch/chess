FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 6001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Chess.Statistics/Chess.Statistics.Api/Chess.Statistics.Api.csproj", "Chess.Statistics/Chess.Statistics.Api/"]
RUN dotnet restore "Chess.Statistics/Chess.Statistics.Api/Chess.Statistics.Api.csproj"
COPY  ["src/Chess.Statistics/Chess.Statistics.Api", "Chess.Statistics/Chess.Statistics.Api/"]
WORKDIR "/src/Chess.Statistics/Chess.Statistics.Api"
RUN dotnet build "Chess.Statistics.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chess.Statistics.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chess.Statistics.Api.dll"]
