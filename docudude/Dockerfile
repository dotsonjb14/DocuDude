# note, this doesn't work
from microsoft/dotnet:2.1-sdk as pub

COPY . /app

workdir /app

run dotnet restore "docudude.csproj"
