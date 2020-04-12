FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Order_servies.csproj", "./"]
RUN dotnet restore "./Order_servies.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Order_servies.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Order_servies.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT development
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Order_servies.dll"]
