# This dockerfile (will) be for the version of the app which will build itself. (IN PROGRESS)
FROM microsoft/dotnet:2.1-sdk as pub

COPY . /app

WORKDIR /app

RUN dotnet restore "docudude.csproj"
