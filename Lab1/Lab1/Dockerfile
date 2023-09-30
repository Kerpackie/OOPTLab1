FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Lab1/Lab1/Lab1.csproj", "Lab1/Lab1/"]
RUN dotnet restore "Lab1/Lab1/Lab1.csproj"
COPY . .
WORKDIR "/src/Lab1/Lab1"
RUN dotnet build "Lab1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lab1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab1.dll"]
