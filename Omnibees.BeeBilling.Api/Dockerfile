FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["./Omnibees.BeeBilling.Api/Omnibees.BeeBilling.Api.csproj", "."]
COPY ["../Omnibees.BeeBilling.Application/Omnibees.BeeBilling.Application.csproj", "../Omnibees.BeeBilling.Application/"]
#COPY ["../Omnibees.BeeBilling.Core/Omnibees.BeeBilling.Core.csproj", "../Omnibees.BeeBilling.Core/"]
COPY ["../Omnibees.BeeBilling.Domain/Omnibees.BeeBilling.Domain.csproj", "../Omnibees.BeeBilling.Domain/"]
COPY ["../Omnibees.BeeBilling.Infrastructure/Omnibees.BeeBilling.Infrastructure.csproj", "../Omnibees.BeeBilling.Infrastructure/"]

RUN dotnet restore "Omnibees.BeeBilling.Api.csproj"

COPY ../ .

WORKDIR /src/Omnibees.BeeBilling.Api
RUN dotnet publish "Omnibees.BeeBilling.Api.csproj" -c Debug -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Omnibees.BeeBilling.Api.dll"]
