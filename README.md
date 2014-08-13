#Enterprise Scaffolding for SPA and WebApi projects

##Getting Started (TL;DR)

It *should* just work out of the box, **if you set the correct mail server in web.config.** You also have to **'Enable Nuget Package Restore' on the *Solution* and Rebuild**. Let me know if it doesn't work for you.

Once you have the Solution running, you could just use your favourite API testing tool to hit the API (like [Postman](http://www.getpostman.com/)). There's no AngularJS frontend yet. 

Note: The entire API has been configured in Postman. Just import 'Solution Items/Postman/Frasset Api.json' in Postman and you should be good to go.

####Required:

* Visual Studio 2012 Update 4 or Visual Studio 2013 Update 2
* Nuget Package Manager (Visual Studio Extension)
* MongoDB (you could easily roll your own context for SQL / Oracle if you don't want to use Mongo)
 
####Recommended:

* Web Essentials (Visual Studio Extension)
* [Robomongo](http://robomongo.org/) - MongoDB management interface
* [Postman](http://www.getpostman.com/) - a Chrome plugin that makes your API dev testing a billion times easier


##Overview

First off, as with all open projects this is a work in progress. It is a port of actual production code that I am busy developing in my spare time (minus some obvious deployment bits and keys etc). Feel free to use as you see fit, let me know where it sucks. I accept no responsibility for anything you do with this, even if it kills your cat.

###Goals:

* Scalable, secure, testable enterprise scaffolding for new SPA and WebApi projects
* Have an open, interchangeable data context / repository system (implemented Mongo as first repo)

###TODO:

* AngularJS UI
* Tests : Unit and Integration
* API discovery

##Details

The current simplified n-tier stack is *MongoDB > Owin/Katana WebApi 2 > AngularJS*. 

###Notes

* The WebApi can be self hosted (no need for any flavour of IIS). You have to roll the wrapper console app yourself though.
* You could easily replace Mongo with any DB or store, and AngularJS with any SPA or MVC framework. 

###Architecture

The stack uses many enterprise architecure concepts, as well as following good [SOLID principals[(en.wikipedia.org/wiki/SOLID_(object-oriented_design)). Technically there is no right way, but there are many wrong ways - which is what you try to avoid at all costs.

####Objects
Lightweight POCO business objects, *with no data annotations* so they are completely reusable by any modern ORM. These objects are *not* [anemic](http://en.wikipedia.org/wiki/Anemic_domain_model), but they do not contain any complex interaction with other objects (which is the job of the Logic layer).

####Interfaces
All the contracts for the business logic, contexts and pretty much everything else. This makes concepts like dependency injection and mocking very easy.

####Contexts
The persistence layer for the Objects. Includes the generic implementations for repositories (which are essentially common persistence collections)

####Models
The modelling layer for Views, Logic and DTO . The idea is that you can still have a nice and reusable set of models that are, for instance, abstractions and/or compositions of business objects. Models can also contain view models if you want. They are basically there for passing *just exactly the right amount of information* from one layer/component to another. Alot of mapping happens between Objects and Models, as well as Models and other Models (thank you AutoMapper).

####Logic
This is the layer that all business logic is implemented in e.g. retrieval, mutation and persistence of business objects. The typically pattern for a method in here would be something like:
```
public OutModel BusinessFunction(InModel params)
{
   //retrieve business objects via context
   //mutate objects
   //persist objects
   //construct and return OutModel
}
```

####Api
The actual Api implementation, serves as a lightweight endpoint for exposing business logic.

##Basic Troubleshooting

1. Make sure you have selected 'Enable Nuget Package Restore' on the *Solution* and then do a Rebuild.
2. Make sure Api Project is set as Startup Project.
3. Make the appropriate changes to Api/web.config (mailSettings, Url.Api, connectionstring)
