# vmw-calculator-divisionservice

# Remote development using Docker container

## Why
- I do not want to install dotnet core environment in my local machine
- I do not want to go through the path config process as this is only a VMs (workstation/virtualbox based). Which I do not know when going to die or corrupt and/or I will switch to new VM.
- I use several laguages and stack for my development purpose.
- Infact this this project is a microservice project. I have 2 services based on Maven Spring Java (embedded Tomcat), 2 service based on NodeJS, ReactJS, 1 Service based on Dotnet Core with IISExpress. I cant have it all installed on a low memory VM (or even a laptop). 

So container is my best option. I can have whatever environment I want and I do not need to store it. I can source control the Dockerfile, thus on a new machine I can simply install docker and git checkout the repo and I am good to go to 
- RUN the application
- Develop the application through remote devlopmenet using vscode.

## How

I have set this project for decker based remote development using .devcontainer/devcontainer.json

## Plugins needed are:
- **Remote Development**: identifier “ms-vscode-remote.vscode-remote-extensionpack”. This extension may show as “Preview”. This is fine. The extension is stable enough to be used on a daily basis.
- **Docker**: identifier “ms-azuretools.vscode-docker“



### The dev purpose Dockerfile.remotedev 

- create a container based on dotnet core sdk. 

- create working dir /app

- use the user in the container and run the container using the user.


### The dev purpose docker-compose 

- define service name

- define a volume attache (thus localfile to container file and vice versa)

- Give context (which is this directory itself)

- Give docker file (as this is dev purpose docker-compose hence dockerfile is Dockerfile.remotedev)


### Remote dev by configuring devcontainer.json to tie everythig together

- specify which docker-compose to use (here the it is docker-compose.yaml which is our dev environment purpose docker-compose. For comprehensive microservices run of this project see ../vmw-calculator-devops/docker-compose.yaml)

- name service (here only 1 service is in the docker-compose)

- the extensions we would like for vscode (other than the "ms-dotnettools.csharp" extension all of the other extensions are sort of 'nice to have's)


### How to run this (Remote Development)

- `docker-compose build` --> This will build the docker image
- `docker-compose up` --> This will run the docker container
- `code ./vmw-calculator-divisionservice` --> open this folder. This will auto suggest to 'open in container' click that and should be good to go. If not use the command pallete.

# That's it.


# NuGet packages used in this

- `dotnet add package Scrutor` ---> For DI (https://andrewlock.net/using-scrutor-to-automatically-register-your-services-with-the-asp-net-core-di-container)