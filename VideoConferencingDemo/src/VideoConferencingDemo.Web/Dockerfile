FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y nodejs
COPY ["VideoConferencingDemo.Web/*.csproj", "VideoConferencingDemo.Web/"]
COPY ["VideoConferencingDemo.Infrastructure/*.csproj", "VideoConferencingDemo.Infrastructure/"]
RUN dotnet restore "VideoConferencingDemo.Web/VideoConferencingDemo.Web.csproj"
COPY . .
WORKDIR "/src/VideoConferencingDemo.Web"
RUN dotnet build "VideoConferencingDemo.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "VideoConferencingDemo.Web.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "VideoConferencingDemo.Web.dll"]