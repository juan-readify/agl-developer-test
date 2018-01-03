# agl-developer-test
AGL Code Submission

## Description
This is my code submission for the AGL programming challenge described [here](http://agl-developer-test.azurewebsites.net/)

The developed solution implements a Web API using ASP.Net Core

## What's in the Solution folder

1. PersonsApi  
   This project implements and exposes the client used to retreieve the data from the supplied endpoint.
   It defines the types and deals with the data serialization.

   Dependencies: HttpClient, Json.Net

2. PersonsWebApi  
   This project implements the Web API, exposing two endpoints:  
   * api/persons  
   Returns the data as retrieved from PersonsApi
   
   * api/person/catsbyownergender  
      Returns a list of cat names, grouped by the gender of their owner.  
      Each sublist is orderded in alphabetic order.

3. PersonsWebApiTests  
   This is a unit test project with the tests for the PersonsWebApi

## How to use
1. Download or clone this repository
2. Open the Solution.sln under the Solution folder with Visual Studio 2017
3. Make sure that PersonsWebApi is set as Startup project
4. Run the project

By default the browser will make a request to the api/person endpoint.  
Edit the URL in the browser address bar to api/person/catsbyownergender to get the list of cat names grouped by their owner's gender.

