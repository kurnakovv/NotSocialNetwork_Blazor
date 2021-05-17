FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

RUN dotnet tool update --global dotnet-ef --version 5.0.1
WORKDIR /src
COPY . .
RUN dotnet restore "Src/Presentation/NotSocialNetwork.API/NotSocialNetwork.API.csproj"
RUN dotnet build "Src/Presentation/NotSocialNetwork.API/NotSocialNetwork.API.csproj" --no-restore