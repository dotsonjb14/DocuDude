#!/bin/bash

sed -i "s/{{AWS_PROFILE}}/$AWS_PROFILE/" appsettings.json

echo "using $AWS_PROFILE"
