# This dockerfile is for a pre-built version of the application. (for faster deploys)
FROM microsoft/dotnet:2.1-aspnetcore-runtime

COPY . /app
WORKDIR /app

EXPOSE 80

ENTRYPOINT ["dotnet", "docudude.dll"]
