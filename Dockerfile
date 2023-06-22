FROM mcr.microsoft.com/dotnet/aspnet:7.0.4-alpine3.17-amd64 AS base
WORKDIR /app
FROM mcr.microsoft.com/dotnet/sdk:7.0.202-alpine3.17-amd64 AS build

WORKDIR /src
COPY ["Application", "./Application"]
COPY ["Core", "./Core"]
COPY ["Infrastructure", "./Infrastructure"]
COPY ["Presentation", "./Presentation"]

RUN dotnet restore "./Application/Application.csproj"
RUN dotnet restore "./Core/Core.csproj"
RUN dotnet restore "./Infrastructure/Infrastructure.csproj"
RUN dotnet restore "./Presentation/Presentation.csproj"

COPY . .
WORKDIR "/src/."
RUN dotnet build "Presentation/Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Presentation/Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]
