# Cagravol
Large volume files manager

## 1.- Brief of the Project**

The project is basically a manager for large volume files (from 2Gb up to 50gb).

The main need that resolve this project is that an "Event Organizer" (fairs, conferences, trade shows, congress, expositions, etc) is able to manage the graphical art/design between the customer (expositors, exhibitors, sponsors, advertisement, marketing, etc) and the provider for all this graphical/art stuff, in an effective and efficient way.

The Event Organizer create a project (the event), set how many lots/sets/spots/places/spaces want, and then can assign each space to a customer by specifing an email address. From this moment, customer can send all the graphic stuff using this platform (no matter how big files are), so his/her material will be provided successfully, and every part (Event Organizer, Customer and Grapical Arts Provider) have the same information about the work done.

## 2.- Architecture.

The project was build using AngularJS for the frontend part, ASP.NET WebAPI for the backend, MSSQL Server for the database, and is used IIS to serve the site.

These are some features you will find in the project:
   - Domain Data Driven design.
   - Dependency Injection
   - Entity Framework Code-First
   - API REST
   - I18n

Solution file is in the root: __SGE.Cagravol.sln__.
Main project is __SGE.Cagravol.WebAPI__, under _1.5-WebAPI_ folder
Under the folder __/app__ is the AngularJS part for the frontend. There is no need to build.
The backend part is basically shared between controllers, services and repositories.

## 3.- Some other notes on the project.

   - This project was done entirely, except unit testing, by myself in aprox 45 days of work.
   - The work was planned for 35 days of work, and was increased 2 times due to change request and issue clarifications, so justified.
   - Unit testing was done, but not by me. Due to that is missing in here.
   - I have the permission from SGE Company (the company I made the project for) to use it in my personal portfolio for possible employers verification, as in this case.
   - I don't have any other permission or any other property over this code.
   - Currently, I don't have contact with SGE Company, so don't know if their customer is running the project, but can assure that was running for 2 years at least.
   - Is code first, so if you change the database connection string in the project __SGE.Cagravol.WebAPI__ -> __Web.config__ between lines 12 and 13:
     - It creates a database called _CAGRAVOL-DEV_ in the local database aliased to (.) 
     - It is using Windows Authentication
   - For launch the migration, in the Package Manager Console:
     - Select default Project: __1.5-WebAPI\SGE.Cagravol.WebAPI__
     - Type _"Update-database -verbose -ProjectName SGE.Cagravol.Infrastructure.Data"_
   - "CAGRAVOL" is a spanish pun/abbreviation of _"CArga de (archivos de) GRAndes VOLúmenes"_, that means something like _"large volume files manager"_.



