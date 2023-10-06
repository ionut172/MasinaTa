FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80

# copy all .csproj files and restore as disting layers. Use of the same copy comand for every dockerfile

COPY MasinaTa.sln MasinaTa.sln
COPY src/LicitatiiService/LicitatiiService.csproj src/LicitatiiService/LicitatiiService.csproj
COPY src/SearchService/SearchService.csproj src/SearchService/SearchService.csproj
COPY src/GatewayService/GatewayService.csproj src/GatewayService/GatewayService.csproj
COPY src/BiddingService/BiddingService.csproj src/BiddingService/BiddingService.csproj
COPY src/NotificationService/NotificationService.csproj src/NotificationService/NotificationService.csproj
COPY src/IdentityServer/IdentityServer.csproj src/IdentityServer/IdentityServer.csproj
COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj

# Restore package deps

RUN dotnet restore MasinaTa.sln

# Copy the app folders over

COPY ./src/LicitatiiService ./src/LicitatiiService
COPY ./src/Contracts ./src/Contracts

WORKDIR /app/src/LicitatiiService
RUN dotnet publish -c Release -o /app/src/out

# Build runtime image

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet", "LicitatiiService.dll" ]