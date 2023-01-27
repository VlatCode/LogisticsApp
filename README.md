# Cargo4You

## Description

This repository is the backend of a cargo company website with features regarding couriers and pricing information.
It includes:
* Displaying all couriers available, showing each courier's cargo capacity, adding newly partnered couriers, removing couriers from the list
* Displaying each courier's shipping offers
* Displaying shipping offers by weight/dimensions of package
* Displaying a specific offer based on user inputs (weight, height, width and depth) of package

Front-end part of the website: https://github.com/VlatCode/LogisticsApp-FE

## Getting Started
### Installing

To run the application successfully, you need to:
* Download the entire code from this repository
* Open the solution in Visual Studio
* In the project LogisticsApp.Helpers -> DependencyInjectionHelper.cs file, edit the connection string - add your local SQL connection string
* Use EF command-line tools to apply migrations to your local database
* Run the application and test it out on Swagger!

## Authors
Vlatko Nikolovski
