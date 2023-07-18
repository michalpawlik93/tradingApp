# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /app

EXPOSE 80
EXPOSE 443

# Copy the .NET project files and restore dependencies
COPY src/TradingApp.TradingWebApi/TradingApp.TradingWebApi.csproj ./TradingApp.TradingWebApi/
COPY src/TradingApp.Authorization/TradingApp.Authorization.csproj ./TradingApp.Authorization/
COPY src/TradingApp.Application/TradingApp.Application.csproj ./TradingApp.Application/
COPY src/TradingApp.Common/TradingApp.Common.csproj ./TradingApp.Common/
COPY src/TradingApp.StooqProvider/TradingApp.StooqProvider.csproj ./TradingApp.StooqProvider/
COPY src/TradingApp.TradingAdapter/TradingApp.TradingAdapter.csproj ./TradingApp.TradingAdapter/
COPY src/TradingApp.TradingViewProvider/TradingApp.TradingViewProvider.csproj ./TradingApp.TradingViewProvider/

# Copy the remaining source code and build the application
RUN cd ./TradingApp.TradingWebApi && dotnet restore
RUN cd ./TradingApp.Authorization && dotnet restore
RUN cd ./TradingApp.Application && dotnet restore
RUN cd ./TradingApp.Common && dotnet restore
RUN cd ./TradingApp.StooqProvider && dotnet restore
RUN cd ./TradingApp.TradingAdapter && dotnet restore
RUN cd ./TradingApp.TradingViewProvider && dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final-env
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "TradingApp.TradingWebApi.dll"]
