GrandeTravel

Sydney Australia TAFE Diploma in Software Development Project 2016

This project was developed during the TAFE Diploma in Software Development program.

GrandeTravel is a Travel Packages management system built using MVC architecture, Dependency Injection, and Entity Framework Core (Code First) with C#.
The front-end was developed using HTML, CSS, JavaScript, Bootstrap, and jQuery.

User Roles

The system includes three roles: Admin, Travel Agent, and Customer.

Admin

Responsible for managing most system operations in the background.
Can add or deactivate Travel Agents and Customers.

Travel Agent

Has full CRUD (Create, Read, Update, Delete) functionality to manage travel packages.
For example, agents can create packages, define the title, description, price, location, and other details.
Agents can also discontinue a package. In this case, the package is not deleted from the database; it is simply hidden from customers.

Customer

Can filter and search for travel packages.
Customers can create a booking, which generates a confirmation including the selected date and the total price based on the number of people included.
