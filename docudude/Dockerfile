# This dockerfile (will) be for the version of the app which will build itself. (IN PROGRESS)
FROM microsoft/dotnet:2.1-sdk as pub

COPY . /app

WORKDIR /app

RUN dotnet publish -c Release -o pub

FROM microsoft/dotnet:2.1-aspnetcore-runtime as built

WORKDIR /app

COPY --from=pub /app/pub/ .

EXPOSE 80

ENTRYPOINT ["dotnet", "docudude.dll"]
