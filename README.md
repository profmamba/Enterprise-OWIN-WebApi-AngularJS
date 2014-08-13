#Enterprise Scaffolding for SPA and WebApi projects

##Overview

First off, as with all open projects this is a work in progress. It is a port of actual production code that I am busy developing in my spare time (minus some obvious deployment bits and keys etc). Feel free to use as you see fit, let me know where it sucks. I accept no responsibility for anything you do with this, even if it kills your cat.

###Goals:

* Scalable, secure, testable enterprise scaffolding for new SPA and WebApi projects
* Have an open, interchangeable data context / repository system (implemented Mongo as first repo)
* Get familiar with new OWIN / Katana concepts

##Getting Started

It *should* just work out of the box, **if you set the correct mail server in web.config.** Let me know if it doesn't work for you.

####Required:

* Visual Studio 2012 Update 4 or Visual Studio 2013 Update 2
* Nuget Package Manager (Visual Studio Extension)
* MongoDB (you could easily roll your own context for SQL / Oracle if you don't want to use Mongo)
 
####Recommended:

* Web Essentials (Visual Studio Extension)
* [Robomongo](http://robomongo.org/) - MongoDB management interface
* [Postman](http://www.getpostman.com/) - a Chrome plugin that makes your API dev testing a billion times easier

####Basic Troubleshooting

1. Make sure Api Project is set as Startup Project.
2. Make the appropriate changes to Api/web.config (mailSettings, Url.Api, connectionstring)
