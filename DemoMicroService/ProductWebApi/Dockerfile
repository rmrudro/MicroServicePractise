#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 81
EXPOSE 444

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProductWebAPI/ProductWebAPI.csproj", "ProductWebAPI/"]
RUN dotnet restore "ProductWebAPI/ProductWebAPI.csproj"
COPY . .
WORKDIR "/src/ProductWebAPI"
RUN dotnet build "ProductWebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductWebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductWebAPI.dll"]