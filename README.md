Room Booking Platform
------------------------
This project is an ASP.NET Core MVC application designed to manage room bookings. It includes features for viewing available rooms and booking rooms.

Features :
-----------
- Display available rooms gallery with details such as title, image, price, description, address, and capacity.
- Book a room by providing the required details like email, start date, end date, and number of guests.
- Display booking confirmation messages, either success or failure, based on the booking criteria.
  
Technologies Used:
--------------------
- ASP.NET Core MVC
- Entity Framework Core (with in-memory database instead of SQL due to resource contraints)
- Bootstrap (for styling)
- Moq and Nunit (for unit testing)
  
Prerequisites :
----------------
- .NET 6.0 SDK or later
- Visual Studio 2022 or later

Setting up the project :
--------------------------
Clone repository, open and build the solution Booking_Platform.sln in Visual Studio.

Running the application :
--------------------------
Once the solution build is successful, run the application. You can now access it in your browser at http://localhost:5189/ as mentioned in launchSettings.json

Unit Testing the application :
------------------------------
Unit tests have been added to the Nunit project called Booking Platform Tests. These include scenarios that cover successful, as well as unsuccessful booking scenarios. Use the Test Explorer in Visual Studio to run these tests.

Project Structure : This project is an ASP.NET Core MVC application that consists of the following :
-------------------

Controllers : Contains the MVC controllers handling HTTP requests and responses.
- HomeController.cs: Handles the display of the home page and room listings, as well as the booking form.
- BookingsController.cs: Handles booking requests and displays booking results - success or failure. The controller uses TempData to temporarily store the booking request's status and redirects to the home page where the status message is displayed to the user.

Models : Contains the data models representing the application's core entities - RoomDto and BookingDto
- RoomDto.cs: Represents a room with details such as title, image, price, description, address, and capacity.
- BookingDto.cs: Represents a booking record with details such as user email, start date, end date, number of people, and associated room.

View : Contains the Razor view files rendering the UI
- Home/Index.cshtml: Displays the list of available rooms and handles booking form submission.
- Shared/_Layout.cshtml: Defines the common layout for the application.
- _Gallery.cshtml: Displays the image gallery of rooms.
- _BookingForm.cshtml: Handles the booking form.

Data :
- ApplicationDbContext.cs : Defines the application's Entity Framework Core context
- PredefinedMessages.cs : Defines the predefined messages for booking requests

My Ideal Solution / things to consider in the future :
------------------------------------------------------
Solution has 3 projects :
- BookingWeb: The backend developed in C# .NET using ASP.NET Core MVC (Similar structure to the current Booking_Platform project minus Razor View files)
- BookingApp: SPA frontend developed using React or Angular
- BookingTests: The unit test project using NUnit (Similar structure to the current Booking_Platform_Tests project)
- SQL Database instead of in app memory
- Admin and Customer login. Admin has extra rights to add a new room via the UI. APIs to allow the customer to view and manage their bookings.
- Introduce Dependency injection
- Better logging and error handling
