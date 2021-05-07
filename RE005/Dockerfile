#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["RE005/RE005.csproj", "RE005/"]
RUN dotnet restore "RE005/RE005.csproj"
COPY . .
WORKDIR "/src/RE005"
RUN dotnet build "RE005.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RE005.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RE005.dll"]