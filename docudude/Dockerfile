FROM microsoft/dotnet:2.1-sdk as pub

COPY . /app

WORKDIR /app

RUN dotnet restore "docudude.csproj"
