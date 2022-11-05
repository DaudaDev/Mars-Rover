FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MarsRover.WebApi/MarsRover.WebApi.csproj", "MarsRover.WebApi/"]
COPY ["MarsRover.Domain/MarsRover.Domain.csproj", "MarsRover.Domain/"]
RUN dotnet restore "MarsRover.WebApi/MarsRover.WebApi.csproj"
COPY . .
WORKDIR "/src/MarsRover.WebApi"
RUN dotnet build "MarsRover.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarsRover.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarsRover.WebApi.dll"]
