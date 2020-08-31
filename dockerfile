FROM mcr.microsoft.com/dotnet/core/sdk as publish
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o out Coal.Client/*.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet
WORKDIR /dist
COPY --from=publish /app/out /dist
CMD [ "dotnet", "Coal.Client.dll" ]