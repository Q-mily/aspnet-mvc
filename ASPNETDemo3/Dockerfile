#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ASPNETDemo3.csproj", "ASPNETDemo3/"]
RUN dotnet restore "ASPNETDemo3/ASPNETDemo3.csproj"

WORKDIR "/src/ASPNETDemo3"
COPY . .

RUN dotnet build "ASPNETDemo3.csproj" -c Release -o /app/build
COPY ./wwwroot /app/build

FROM build AS publish
RUN dotnet publish "ASPNETDemo3.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet", "ASPNETDemo3.dll"]