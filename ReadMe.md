#NSBReturnFailing

This is a small example that reproduces a problem I have described on [Google Groups](https://groups.google.com/forum/#!msg/particularsoftware/uXe0QicJO_w/jDCkgoj-JysJ)

##Summary
My "client" is an Asp.Net MVC 4 Site that sends commands (with Bus.Send(...)) to an NServiceBus.Host application on the same machine. The host processes the command, publishes one or more events and returns an error code (with Bus.Return(...)) to the website. The website receives the error code and displays the appropriate information to the user. I'm using an async Action in my MVC controller and await the result of Bus.Send(...).Register<ErrorCodeEnum>();
This works perfectly on my local developer machine. But when I deploy everything to our DEV server it works for 4-5 commands after AppPool Recycle, but then the ControlMessages containing the error code information queue up at the Websites endpoint. They just sit there and never get picked up by the website.
Have you ever heard of a problem like that? I know that I am trying to hide the fact that there is an asynchronous communication going on from the user and that this probably is not best practice. But it should be possible to use NServiceBus in this way, shouldn't it?

##How to reproduce
My configuration is:
* NServiceBus 4.1.1 with SQL Transport 1.0.5
* SQL Serve 2008 R2
* Windows Server 2008 R2 with IIS7

I reproduced the problem on two different machines with this configuration.

I could _not_ reproduce the problem on my local machine in IIS Express or IIS 7.5!
