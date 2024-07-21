FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
RUN mkdir -p /app/DataProtection-Keys
RUN chmod -R 777 /app/DataProtection-Keys
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_DATA_PROTECTION__APPLICATION_NAME="Shopping-Micro-Services"


USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Shopping-Micro-Services.csproj", "./"]
RUN dotnet restore "Shopping-Micro-Services.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Shopping-Micro-Services.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Shopping-Micro-Services.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopping-Micro-Services.dll"]
