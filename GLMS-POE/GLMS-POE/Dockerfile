FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["GLMS-POE/GLMS-POE.csproj", "GLMS-POE/"]
RUN dotnet restore "GLMS-POE/GLMS-POE.csproj"

COPY . .

WORKDIR "/src/GLMS-POE"

RUN dotnet publish "GLMS-POE.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "GLMS-POE.dll"]