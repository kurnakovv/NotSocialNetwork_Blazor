#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Src/Presentation/NotSocialNetwork.API/NotSocialNetwork.API.csproj", "Src/Presentation/NotSocialNetwork.API/"]
COPY ["Src/Infrastructure/NotSocialNetwork.DBContexts/NotSocialNetwork.DBContexts.csproj", "Src/Infrastructure/NotSocialNetwork.DBContexts/"]
COPY ["Src/Core/NotSocialNetwork.Application/NotSocialNetwork.Application.csproj", "Src/Core/NotSocialNetwork.Application/"]
COPY ["Src/Infrastructure/NotSocialNetwork.Mapping/NotSocialNetwork.Mapping.csproj", "Src/Infrastructure/NotSocialNetwork.Mapping/"]
COPY ["Src/Infrastructure/NotSocialNetwork.DI/NotSocialNetwork.DI.csproj", "Src/Infrastructure/NotSocialNetwork.DI/"]
COPY ["Src/Infrastructure/NotSocialNetwork.Services/NotSocialNetwork.Services.csproj", "Src/Infrastructure/NotSocialNetwork.Services/"]
COPY ["Src/Infrastructure/NotSocialNetwork.Data/NotSocialNetwork.Data.csproj", "Src/Infrastructure/NotSocialNetwork.Data/"]
RUN dotnet restore "Src/Presentation/NotSocialNetwork.API/NotSocialNetwork.API.csproj"
COPY . .
WORKDIR "/src/Src/Presentation/NotSocialNetwork.API"
RUN dotnet build "NotSocialNetwork.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotSocialNetwork.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotSocialNetwork.API.dll"]