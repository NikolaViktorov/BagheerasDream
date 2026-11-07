# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files
COPY BagheerasDream.slnx ./
COPY BagheerasDream.Domain/BagheerasDream.Domain.csproj BagheerasDream.Domain/
COPY BagheerasDream.Application/BagheerasDream.Application.csproj BagheerasDream.Application/
COPY BagheerasDream.Infrastructure/BagheerasDream.Infrastructure.csproj BagheerasDream.Infrastructure/
COPY BagheerasDream.Web/BagheerasDream.Web.csproj BagheerasDream.Web/

# Restore dependencies
RUN dotnet restore BagheerasDream.slnx

# Copy source code
COPY . .

# Publish application
RUN dotnet publish BagheerasDream.Web/BagheerasDream.Web.csproj 
    -c Release 
    -o /app/publish 
    --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "BagheerasDream.Web.dll"]