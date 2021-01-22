#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Selma.IdentityProvider.Interfaces.REST/Selma.IdentityProvider.Interfaces.REST.csproj", "Selma.IdentityProvider.Interfaces.REST/"]
RUN dotnet restore "Selma.IdentityProvider.Interfaces.REST/Selma.IdentityProvider.Interfaces.REST.csproj"
COPY . .
WORKDIR "/src/Selma.IdentityProvider.Interfaces.REST"
RUN dotnet build "Selma.IdentityProvider.Interfaces.REST.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Selma.IdentityProvider.Interfaces.REST.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Selma.IdentityProvider.Interfaces.REST.dll"]