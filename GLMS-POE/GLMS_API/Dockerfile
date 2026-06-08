FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["GLMS_API/GLMS_API.csproj", "GLMS_API/"]
RUN dotnet restore "GLMS_API/GLMS_API.csproj"

COPY . .

WORKDIR "/src/GLMS_API"

RUN dotnet publish "GLMS_API.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "GLMS_API.dll"]