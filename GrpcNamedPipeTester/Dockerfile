FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GrpcNamedPipeTester/GrpcNamedPipeTester.csproj", "GrpcNamedPipeTester/"]
RUN dotnet restore "GrpcNamedPipeTester/GrpcNamedPipeTester.csproj"
COPY . .
WORKDIR "/src/GrpcNamedPipeTester"
RUN dotnet build "GrpcNamedPipeTester.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrpcNamedPipeTester.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcNamedPipeTester.dll"]
