FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_URLS=http://+:8080

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Services/ordering/Ordering.Api/Ordering.Api.csproj", "src/Services/ordering/Ordering.Api/"]
RUN dotnet restore "src/Services/ordering/Ordering.Api/Ordering.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/ordering/Ordering.Api"
RUN dotnet build "Ordering.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Ordering.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.Api.dll"]
