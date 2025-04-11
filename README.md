# Final project for the course APBD - Database Applications

Revenue Recognition Problem
The application will address an issue related to finance, known as the "revenue recognition problem."
Revenue recognition is a common challenge in business systems. It refers to the question of when money received can actually be recorded in the books and treated as a company’s revenue.

If you sell someone a cup of coffee, the situation is simple: the customer gets the coffee, you get the money, and you can immediately treat the money as revenue.

However, things get more complicated in other cases. For example, let’s say you pay someone in advance to perform certain services for you over the next year. Even if you pay them a large fee today, it can’t be immediately recorded as revenue, because the service is supposed to be delivered throughout the year. One approach might be to recognize one-twelfth of the payment for each month, since you could cancel the contract after a month if you realize that the person isn't capable of fulfilling the assigned tasks.

Revenue recognition rules are diverse and constantly evolving. Some are established by legal regulations, others by professional standards, and still others by company policy. Tracking revenue proves to be a fairly complex problem.

In fact, improper revenue recognition has been the cause of several major corporate scandals, such as Enron and WorldCom. These companies used various tactics to misrepresent their financial condition, leading to serious legal consequences and financial losses for investors.
That’s why accurate and regulation-compliant revenue recognition is crucial for maintaining transparency and trust in financial markets.

* Fun fact: Enron was the company that inspired Evil Corp in the TV series Mr. Robot.



# How to use it
* clone it
* restore NuGet packages (if needed)
  * JetBrains Rider should do it automatically on first run based on /EvilCorp/EvilCorp/EvilCorp.csproj file
* edit connection string at /EvilCorp/EvilCorp/appsettings.json
  * I am using mssql image running in docker
  * https://hub.docker.com/r/microsoft/mssql-server
* run migrations (id needed)
  * JetBrains Rider should do it automatically on first run
* start the server
  * select "EvilCorp: http" .NET Launch Settings Profile

# Technicals

Written in C# | based on .NET 8.0 framework | JWT authentication implemented | Swagger API Documentation Implemented

NuGet Packages needed to run
* Microsoft.AspNetCore.Authentication.JwtBearer
* Microsoft.AspNetCore.OpenApi
* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.Design
* Microsoft.EntityFrameworkCore.SqlServer
* Swashbuckle.AspNetCore
* Moq
* Newtonsoft.Json
