# DocuDude

Docudude is a document manipulation API meant to be run within the AWS ecosystem.

## Features

* Adding arbitrary text to documents

## Technologies

* .NET Core
* iText7

## Warnings

Please, for the love of all that is holy, do not run this in a way that's publicly accessable. It allows pretty much all access to do whatever you want with any document on any S3 bucket you can access with the user you run this thing as. Make a BFF and utilize it from there. 

What this means, is put it in a VPC, and do not allow external connections whatsoever. I would not even trust whitelisting.

If you ignore this warning, then you are a moron that deserves to get owned.

## Running

Please follow these steps to run locally

enter the docudude folder and run these commands
`dotnet publish -c Release -o pub`
`sudo docker build --no-cache -t docuparent pub`

enter the example-docker folder and run these commands

`sudo docker build --no-cache -t docuchild .`
`sudo docker run -p 80:80 docuchild`