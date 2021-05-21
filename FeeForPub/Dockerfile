FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["FeeForPub/FeeForPub.csproj", "FeeForPub/"]
RUN dotnet restore "FeeForPub/FeeForPub.csproj"
COPY . .
WORKDIR "/src/FeeForPub"
RUN dotnet build "FeeForPub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FeeForPub.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FeeForPub.dll"]
