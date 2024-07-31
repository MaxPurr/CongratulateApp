FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/api/CongratulateApi/CongratulateApi.csproj", "src/api/CongratulateApi/"]
COPY ["src/api/CongratulateApi.Domain/CongratulateApi.Domain.csproj", "src/api/CongratulateApi.Domain/"]
COPY ["src/api/CongratulateApi.DataAccess/CongratulateApi.DataAccess.csproj", "src/api/CongratulateApi.DataAccess/"]
COPY ["src/api/CongratulateApi.Utils/CongratulateApi.Utils.csproj", "src/api/CongratulateApi.Utils/"]
COPY ["src/api/CongratulateApi.Application/CongratulateApi.Application.csproj", "src/api/CongratulateApi.Application/"]
RUN dotnet restore "src/api/CongratulateApi/CongratulateApi.csproj"
COPY . .
WORKDIR "/src/src/api/CongratulateApi"
RUN dotnet build "CongratulateApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CongratulateApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CongratulateApi.dll"]
