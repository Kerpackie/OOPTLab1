# Use the appropriate ASP.NET image as your base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy the .csproj file to the container
COPY ["Lab1/Lab1/Lab1.csproj", "Lab1/"]

# Restore NuGet packages
RUN dotnet restore "Lab1/Lab1/Lab1.csproj"

# Copy the entire project to the container
COPY . .

# Build the project
WORKDIR "/app"
RUN dotnet build "Lab1/Lab1/Lab1.csproj" -c Release -o /app/build

# Publish the project
FROM base AS publish
RUN dotnet publish "Lab1/Lab1/Lab1.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Set up the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab1.dll"]
