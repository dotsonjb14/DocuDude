from docuparent:latest

expose 80

env AWS_PROFILE default

run bash ./transform.sh 

entrypoint ["dotnet", "docudude.dll"]
