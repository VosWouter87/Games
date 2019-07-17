FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY Yavalath/Yavalath.csproj Yavalath/
RUN dotnet restore Yavalath/Yavalath.csproj
COPY . .
WORKDIR /src/Yavalath
RUN dotnet build Yavalath.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Yavalath.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Yavalath.dll"]
