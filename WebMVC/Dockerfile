FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY WebMVC/WebMVC.csproj WebMVC/
RUN dotnet restore WebMVC/WebMVC.csproj
COPY . .
WORKDIR /src/WebMVC
RUN dotnet build WebMVC.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebMVC.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebMVC.dll"]
