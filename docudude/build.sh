#!/bin/bash

rm -rf pub && dotnet publish -c Release -o pub
sudo docker build --no-cache -t dotsonjb14/docudude -f pub/builtDockerfile pub
