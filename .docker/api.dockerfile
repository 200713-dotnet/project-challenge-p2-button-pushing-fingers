FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build
WORKDIR /workspace
COPY Coal.Service.sln .
RUN mkdir Coal.Domain/
COPY Coal.Domain/ ./Coal.Domain/
RUN mkdir Coal.Storing/
COPY Coal.Storing/ ./Coal.Storing/
RUN dotnet publish -c Release -o out Coal.Domain/Coal.Domain.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /workspace
COPY --from=build /workspace/out .
CMD [ "dotnet", "Coal.Domain.dll" ]