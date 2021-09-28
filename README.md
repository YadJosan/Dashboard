# **QuickApp** - ASPNET Core 5 / Angular 11 project template
[![MIT license](https://cdn.rawgit.com/emonney/tempa/7e9d69ad/MITLicense.png)](https://github.com/emonney/QuickApp/blob/master/LICENSE)
[![VSIX Downloads](https://img.shields.io/visual-studio-marketplace/d/adentum.QuickApp-ASPNETCoreAngularXProjectTemplate)](https://marketplace.visualstudio.com/items?itemName=adentum.QuickApp-ASPNETCoreAngularXProjectTemplate)
[![Twitter Follow](https://img.shields.io/twitter/follow/kommand?style=social)](https://twitter.com/kommand)

A startup **Angular 11 / ASP.NET Core 5** (cross-platform ) **project template** with an end-to-end login, user and role management implementation.
As well as other common functionalities for **Quick Application Development**.

___
## INTRODUCING QUICKAPP PREMIUM TEMPLATES
*   All free features
*   Angular Material
*	Reactive forms
*	Public Registration, Password Recovery & Reset, Email Confirmation
*   Priority Email Support
*   Etc





## This application consists of:

*   Template pages using Angular 11 and TypeScript
*   RESTful API Backend using ASP.NET Core 5 MVC Web API
*   Database using Entity Framework Core
*   Authentication based on OpenID Connect
*   API Documentation using Swagger
*   Angular CLI for managing client-side libraries
*   Theming using Bootstrap 4

## You get the benefits of:

*   A complete backend and frontend project structure to build on, with login, user and permission-based role management already integrated
*   Data Access Layer built with the Repository and Unit of Work Pattern
*   Code First Database
*   A RESTful API Design
*   Angular Directives Quidance
*   Angular Pipes Quidance
*   Angular Animations Quidance
*   Angular Services
*   Dialog and Notification Services
*   Configuration Page and Configuration Service
*   Integrated Internationaliztion
*   Theming with SASS
*   Ready-to-use email API
*   Handling Access and Refresh Tokens with WebStorage (Bearer authentication) - No Cookies
*   Jquery Integration (Ability to use standard Jquery libraries)
*   CRUD APIs
*   Responsive Design
*   Etc.


## Installation


## Installation Notes

*   When creating a new project please wait for all dependencies to be restored; "dotnet restore" for asp.net project & "npm install" for angular project.
    When using VisualStudio this is automatic, check the output window or status bar to know that the package/dependencies restore process is complete before launching your program for the first time.
*   If you get any errors, consider running manually the steps to build the project and note where the errors occur.
    Open command prompt and do the below steps:  
    1. run 'dotnet restore' from the two project folders - Restore nuget packages
	2. run 'npm install' from the project with package.json - Restore npm packages
	3. Try running the application again - Test to make sure it all works
*	When running the client(angular) project on a different address/domain from the backend, configure the baseUrl of the client to match that of the server.
	You do this from environment.ts in the ClientApp/Angular project.
	Example: baseUrl: "http://yourbackendserver.com" OR baseUrl: "http://localhost:5050"


## Login

LOGIN WITH USERNAME OR EMAIL ADDRESS
> * **Default Administrator Account**
>   * Username: admin
>   * Email:    admin@ebenmonney.com
>   * Password: tempP@ss123
> * **Default Standard Account**
>   * Username: user
>   * Email:    user@ebenmonney.com
>   * Password: tempP@ss123


## Documentation

*   [Overview of QuickApp](https://www.ebenmonney.com/quickapp)
*   [Conceptual overview of what is ASP.NET Core](https://go.microsoft.com/fwlink/?LinkId=518008)
*   [Working with Data](https://docs.microsoft.com/en-us/ef/#pivot=efcore)
*   [Angular 11 documentation overview](https://angular.io/guide/quickstart)
*   [Getting started with Angular CLI](https://cli.angular.io)
*   [Introduction to Bootstrap 4](https://getbootstrap.com/docs/4.1/getting-started/introduction)

