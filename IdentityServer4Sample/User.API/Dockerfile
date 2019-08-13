FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /code
COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /code/out ./

EXPOSE 80

ENTRYPOINT ["dotnet", "User.API.dll"]