FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY OTP2FAAuthWebAppDemo/OTP2FAAuthWebAppDemo.csproj OTP2FAAuthWebAppDemo/
RUN dotnet restore
COPY . .
WORKDIR /src/OTP2FAAuthWebAppDemo
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV PORT 80
# ENTRYPOINT ["dotnet", "OTP2FAAuthWebAppDemo.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet OTP2FAAuthWebAppDemo.dll
