FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/EmailSending.Web.Api/EmailSending.Web.Api.csproj", "src/EmailSending.Web.Api/"]
RUN dotnet restore "src/EmailSending.Web.Api/EmailSending.Web.Api.csproj"
COPY . .
WORKDIR "/src/src/EmailSending.Web.Api"
RUN dotnet build "EmailSending.Web.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EmailSending.Web.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EmailSending.Web.Api.dll"]
