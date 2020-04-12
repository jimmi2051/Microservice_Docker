FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Rating_services.csproj", "./"]
RUN dotnet restore "./Rating_services.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Rating_services.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Rating_services.csproj" -c Release -o /app

FROM base AS final
LABEL deftnt="deftnt"
ENV ASPNETCORE_ENVIRONMENT development
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Rating_services.dll"]
