# Meeting rooms reservation system Demo

A demo for a system to schedule meeting accross several rooms and offices.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

For ease of running, I planned to use NoSql database but the attempt to configure MongoDB doesn't go well. 

So I built a simple layer for writing and reading to JSON files and used it as a storage system, In case of upgrading only this layer will be changed and the remaining of the code will be as it is.

### Prerequisites

To run this project make sure you have installed .Net Core 3.1 or above.

* [Click here](https://dotnet.microsoft.com/download/dotnet-core/3.1) for more information on downloading and installing .Net core 3.1. 

* [Newtonsoft.Json 12.0.3](https://www.nuget.org/packages/Newtonsoft.Json/)
* [Moq 4.14.5](https://www.nuget.org/packages/moq/#:~:text=Moq%204.14.-,5,NET.)
* [MSTest.TestFramework 2.1.2](https://www.nuget.org/packages/MSTest.TestFramework/)

### Installing

To install Newtonsoft.Json package, type in Package Manager at visual studio

```
Install-Package Newtonsoft.Json -Version 12.0.3
```

And for Moq package, type in Package Manager at visual studio

```
Install-Package Moq -Version 4.14.5
```

### Built with
* .Net core 3.1
* AspNetCore.MVC
* MSTest 2.1.2

## Running the tests

To run unit test make sure you have installed MSTest.TestFramework 2.1.2 or above
```
Install-Package MSTest.TestFramework -Version 2.1.2
```
And run project <ins>ReservationSystem.Test</ins>.

## Deployment

Before running the project, make sure you have assigned the property <ins>RootDir</ins> in <ins>appsettings.json</ins> with valid path and this path is saftly accessible with windows account running the project:
```json
"FileSettings": {
    "RootDir": "C:\\temp",
    "OfficesFileName": "Offices.json",
    "RoomsFileName": "Rooms.json",
    "ReservationsFileName": "Reservation.json"
  },
```
* preferred not to change the other properties *

To use a pre-initialized data, copy the files inside <ins>data</ins> folder into the path associated with <ins>RootDir</ins>.

## User stories
* Configure [Add / Edit / Delete] offices.
* Configure [Add / Edit / Delete] meeting rooms.
* Schedule meeting rooms at one of the configured rooms.
* List / Search for a scheduled meeting.

## Usage
In the home page of the project <ins>Index.cshtml</ins> you can find under configuration section two links to start configuring how many offices do you have and then how many rooms are associated with these offices.

This step should be configured before start scheduling any meeting room.

After succesfully schedule a meeting, you should be able to see it's details when clicking on <ins>See all reservations</ins>. 

And also you shouldn't be able to schedule another meeting at the same time and the same room, unless you cancelled this previous meeting.

## System Architecture
Below is a technical details about system architecture and design patterns used in implementation.

### Design patterns used
* MVC
* Dependency Injection
* Factory pattern
* Singleton pattern

### System layers
* View layer uses the model to view/modeling the data and receive input from the user.
* View sends inserted data from the user to controller actions.
* Controller actions formatting the data in order to pass it to the repository layer.
* Controller may also pass the data to middle layer which will pass it repository, e.g. case of <ins>ReservationsController</ins> which passing information to <ins>MeetingsScheduler</ins>
* Repository layer responsible for validating the data and if correct, it will pass it to the service layer.
* Service layer responsible for formating data and write/read it into JSON files. (Data access layer)

## Feature work
* Apply logger configuration instead of writing in console or throw exceptions.
* Apply frontend layer to give a magic appearance and also a client validation layer.
* Apply user authentication system. 
* Use MongoDB instead of local JSON files.

## Conclusion
This project is built for demo/ testing purposes and it still under enhancements. 

The main part was to built infrastructure to make it easy for extending and applying new features or upgrade the use of JSON files as storage to use any other database.

## Authors

* **Moustafa Attia** - *Software Engineer .Net/C#* - [LinkedIn](https://www.linkedin.com/in/moustafa91/)
