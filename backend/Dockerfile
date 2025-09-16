# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["src/appointly.API/appointly.API.csproj", "src/appointly.API/"]
COPY ["src/appointly.BLL/appointly.BLL.csproj", "src/appointly.BLL/"]
COPY ["src/appointly.DAL/appointly.DAL.csproj", "src/appointly.DAL/"]
RUN dotnet restore "src/appointly.API/appointly.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/src/appointly.API"
RUN dotnet build "appointly.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "appointly.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "appointly.API.dll"]
