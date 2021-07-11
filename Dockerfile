FROM mcr.microsoft.com/dotnet/core/sdk as base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000


FROM mcr.microsoft.com/dotnet/core/sdk AS build
WORKDIR /app
# Copy csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build -c Release


FROM build AS publish
RUN dotnet publish -c Release -o /publish

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /publish .
# RUN ls -ls && dotnet --version
ENTRYPOINT ["dotnet", "divisionservice.dll"]