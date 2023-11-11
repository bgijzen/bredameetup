FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GrpcTester/GrpcTester.csproj", "GrpcTester/"]
RUN dotnet restore "GrpcTester/GrpcTester.csproj"
COPY . .
WORKDIR "/src/GrpcTester"
RUN dotnet build "GrpcTester.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrpcTester.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcTester.dll"]
