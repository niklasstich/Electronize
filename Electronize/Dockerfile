FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Electronize/Electronize.csproj", "Electronize/"]
RUN dotnet restore "Electronize/Electronize.csproj"
COPY . .
WORKDIR "/src/Electronize"
RUN dotnet build "Electronize.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Electronize.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Electronize.dll"]
