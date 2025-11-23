
# Store App Example

This is a simple store web app example that showcases clean architecture and use of EFCore to read/write into an sqlite database.



## Prerequisites

- .Net 9 SDK
- Node.js 24.10.0 and Angular CLI 21.0.0
- SQLite
- EF Core tools

``` 
dotnet tool install --global dotnet-ef 
```


## Backend - Setup & Run

Make sure you run the following from the root of the StoreApi project in the StoreSolution.

- Restore Nuget Packages
```
dotnet restore
```
This step may not be needed. You can try running the API Solution now in vs studio
- Apply migrations
```
dotnet ef database Update
```

- The API should be running at https://localhost:7097
- Access UI at https://localhost:7097/scalar/v1 
- APIs at https://localhost:7097/api/store
## Frontend - Setup & Run

Make sure you run the following from the root of the StoreWeb project in the StoreSolution. 
(Backend must be running to see products.)

- Install dependencies:
```
npm Install
```
- Run Angular dev server:
```
ng serve
```
- Navigate to 
```
http://localhost:4200

```
## Architecture

### Overall Architecture Approach. 
The solution follows a simplified Clean Architecture structure to help keep project maintainable and testable.  
I kept the backend project seperated into:
- Domain : business models and domain rules
- Application : logic and orchestration 
- Infrastructure : handles EFCore and database.
- API : has http endpoints and middleware for global exceptions handling.


## Database Schema

Product  
- Id
- Name
- Description
- Price
- CategoryId
- StockQuantity
- CreatedDate
- IsActive

Category
- Id
- Name
- Description
- IsActive 
- Products 

Indexes:
- IsActive on both tables as it's often used
- Name and Price for sorting and filtering
- CategoryId for filters 

## Note:
 The search implemented is ok for this example as it's small. The use of wild cards like '%%' will remove the indexing advantages as well as the ToLower() function. If you need to perform a search on large data sets you should consider database specific methods, case insensitive collations, Full-Text Search (FTS5 for sqlite), to help reduce full table scans.


## Tech Choices

- .Net 9 because it's modern with great ways to manage resources with transient, scoped, and singleton dependency injections.
- EF Core to simplify database interactions and maintenance with code first approach.
- Angular 21 as it's the latest and has DI & Http capabilities
- SQLite because it's free and adequate for the example. Would suggest other databases like Postgres for more serious application development. 


## Design

Most of the solution follows DI and Single Responsibility rules with the exception of the search method. I kept everything in this method for simplicity and time. You could break this up so into different methods so that each filter has a clear and single responsibility. 
- The services perform single units of responsibility (ProductService handles products)
- Use interfaces for DI and unit testing later
- Followed Repository design with services and repos to separate logic and database interactions

### EF core
- AsNoTracking() is used on read operations to improve performance as it tells EFCore to not worry about watching for changes in the background. 
- Using DTO's to protect internal models used with the database. This makes it easier to change domain models with less risk to breaking API's.
- Used pagination with Skip/Take to reduce the amount of data returned as it could be large. (You may consider changing this if your data set get's really large. Instead track the last id and take x amount after.)
- Used EFCore for sorting to reduce having all records in memory on the service and performing a sort. We can let the database handle this.

### Product search
I chose to implement this product search as it's a common functionality used in applications all over. It demonstrates real querying and filtering at some level with server logic.  
Searching allows:
- Searching by term
- Categories 
- Min/Max pricing
- Items in stock, out of stock
- Sorting 
- Paging

### Repository Design

I chose to use the Repository design as it helps keep business logic and Database interactions seperate. This makes it easier to mock for unit tests if needed later and it's an easy to follow design for other team members. Maintainability and simple code is a benefit for quick moving development and reduction to code smell. 

### Indexing 

- IsActive on both tables as it's often used
- Name and Price for sorting and filtering
- CategoryId for filters 

These indexes should provide sufficient search and filtering indexing used by our methods. You could enhance this with composite indexes as well. 

### Note:
 The search implemented is ok for this example as it's small. The use of wild cards like '%%' will remove the indexing advantages as well as the ToLower() function. If you need to perform a search on large data sets you should consider database specific methods, case insensitive collations, Full-Text Search (FTS5 for sqlite), to help reduce full table scans.

 
## More Time and Larger Implementation

Given this is a lite example there is a bit of work that should be done if you wanted to use this for a more solid solution.  
Some things I would include: 
- Authentication (JWT or Active Directory)
  - This would add some security and restrict access to apps communicating with each other using RBAC. 
- Add a caching layer (Redis) for hot data. This keeps reads fast and less pings on the database during high traffic times. 
- Enhance the search with better indexing.
- Implement an APIM to set rate limit policies keeping it seperate from code. You can also use this to allow external access to APIs by controlling subscription keys in Azure.
- Better UI for frontend with filtering and searching as it's a better experience for users. 
- Admin UI for managing data like products and categories. 
- Add testing like XUnit.
- Containerizing services for cloud based deployments.
- Service bus for message queueing that better distributes data across services and free's resources. 
- Fluent Validation


## Refactoring Priorities
- Better validation extracted to a middleware or use FluentValidation
- Enhance search operation 
- Expand on repo interfaces into query/command patterns so that complex queries dont crowd basic CRUD operations.


## Production: 
If this were to be deployed into a production setting I would take the time to set up a few other things as well. Keep in mind, a lot of things are considerations dependent on the size of the application and projected traffic to use the services.

- Integrate Application Insights for more logging and visibility into failures or latencies. 
- CI/CD Pipelines to automate building and deploying docker images and database changes.
- Deploy services to contained images in azure for automated scalability (Vertical and Horizontal)
- API Versioning

## Assumptions & Trade offs
Because the example is small, I'm assuming no authentication is required as well as no caching or message queueing is needed. This makes the solutions less robust and definitely not secure as there is no rate limiting and API's could be abused. The search query is simple but works at the cost of poor query optimization for the argument of completeness and simplicity. Most decisions here are made in favor of being simple and readable as it's easier for someone to see how everything is working. The validation of requests is simple and could definitely be done better with something like FluentValidation so you can validate requests before reaching the controller.
