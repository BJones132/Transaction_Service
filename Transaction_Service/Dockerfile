FROM dhi.io/dotnet:10-sdk-alpine AS build
WORKDIR /App

COPY . ./
RUN dotnet restore
RUN dotnet publish -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /App
COPY --from=build /App/out .

EXPOSE 5002
EXPOSE 5012
VOLUME /home/App/.aspnet/https/

ENTRYPOINT ["dotnet", "Transaction_Service.dll"]