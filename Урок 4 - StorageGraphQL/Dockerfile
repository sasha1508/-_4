﻿FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY *.csproj ./
RUN  dotnet restore 
COPY . ./
RUN dotnet publish -c Release -o out

FROM  mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 8080
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet","StorageGraphQL.dll"]