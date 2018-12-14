# KNEAT STAR WARS
 KNEAT Start Wars is a test project using .Net Core (Console Application) and totally able to expando to API with Swagger, 
 this project is consuming a open source api https://swapi.co/api to calculate a how many time a starship needs to stop until reach out the distance inputed for user in console app.

### Prerequisites
Necessary to install .net core sdk and .net core runtime.
After all installed, you need to restore NuGet packages. 
Connection on the web and external sites.

## Overview System

This system is using Clean Architecture proposed for Robert C. Martin. 

It is separated in some Layer and each layer has its own responsability, 

### Kneat Star Wars Overview
    1.1 - Presentation: This layer basically has an unique responsability to receive inputs and show in the console the outputs. This layer only has comunication with Application Layer.
    
    1.2 - Application: It has a responsability to keep business logic and validate any operation before to response to Presentation layer. This layer communitace with Presentation, Domain and Infrastructure.

    1.3 - Doamin: This layer has focus on map entities on Use Case, communication with all Layers (Presentation, Application and Infrastructure).

    1.4 - Infrastructure: This layer is responsible only for communicate to external API, there is no validation neither business logic.


### Getting started
    Case of everything is running correctly, it will open a command prompt asking to inform a integer number and after press enter, this value reresent a distance that each star ship needs to travel.
    
    This project will communicate to external APIs and calculate each star ship avaible on the api and return how many stop need to do.

    The output will return a `Star Ship Name`, `Star Ship Consumable`, `Star Ship MGLT` and `Stop Necessaries` to reach out the distance informed.

### Conclusion
    * This project can be improved using asynchronous methods to improve performance.
    * For to be using the .Net Core it is native to import docker container and replicate the services using Linux Alpine.

