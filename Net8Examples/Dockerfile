FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Net8Examples/Net8Examples.csproj", "Net8Examples/"]
RUN dotnet restore "Net8Examples/Net8Examples.csproj"
COPY . .
WORKDIR "/src/Net8Examples"
RUN dotnet build "Net8Examples.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Net8Examples.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Net8Examples.dll"]
