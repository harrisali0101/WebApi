#Intro
This is a .NET Core Web Api for a Applicant who can have the following properties:
1.Name.
2.Family Name.
3.Address.
4.Country.
5.Email.
6.Age.
7.Hired or not.

It performs simple CRUD Operation and is connected to inmemory database.
Cross origin resource sharing is enabled in this api which allows us to send any type of request to this api from
localhost://4200 angular frontend.

Validations have also been applied on the given properties of Applicant using fluent validations such as: 
Name – at least 5 Characters.
FamilyName – at least 5 Characters.
Address – at least 10 Characters.
EmailAddress – must be an valid email.
Age – must be between 20 and 60.
Hired – If provided should not be null.
These validations are in ApplicantDetailsValidator file in DAL.

# WebApi
The backend is divided into two tiers.
1.Data Access Layer.
2.Business Logic Layer.

#Data Access Layer
1.Applicant model is created in the DAL in Models folder.
2.Validations are applied to the model.
3.DataContext Class is also created in the DAL. 
4.In the Repository folder all the functions related to the CRUD operation are made which communicate with the database.
5.All required Packages are installed.

#Business Logic Layer
1.A empty api controller is added.
2.Reference to the DAL is added to the BLL project.
3.All the HTTP Get, Post, Put, Delete methods are made in controller class which will receive the request from the frontend
and then give a call to the DAL repository functions which will in turn give a request to the database accordingly.
4.In startup.cs we add the required services such as AddCors, AddDbContext, AddFluentValidations.

#Testing
This Api has been developed in Microsoft Visual Studio 2019 Community Version.
It is a ASP.NET Core Web Api on .NET Core version ASP.NET Core 5.0
This Web Api runs on Kestrel Host on localhost://5000 and was tested using swagger.

