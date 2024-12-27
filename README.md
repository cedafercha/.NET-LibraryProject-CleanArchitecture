# README :)

Practical exercise working with different topics such as:
- Clean Architecture having different layers with unique responsibilities.
- CQRS pattern for handling read and write in separate processes.
- AAA pattern in Tests improving order and readability at the time of development.
- Exception middleware for better control of response messages and http codes.
- Handling asynchronous processes using Tasks.
- Linq for DB queries/writes.
- SOLID principles.
- Mediator pattern.
- Clean code.

## Project Structure - Layers
### Api
- In this layer the Controller is declared along with the Exception Middleware.
 
### Application
- Contains the ViewModels, the Profiles for handling AutoMapper and the Query-Commands implemented with Mediator.

### Domain
- It has the business logic, mainly the LoanService, which is responsible for the entire main process of the system.

### Infrastructure
- It is responsible for having access to the DB with Finder and Repository classes, separating reading and writing responsibilities.

### Tests
- Unit and integration tests.

## Running the App
```sh
dotnet build
dotnet run --project ./LibraryProject/LibraryProject.Api/LibraryProject.Api.csproj

// The application is now running on port 5002.
// If you want to use it with Swagger, go to the /swagger route on port 5002.
```

## Endpoints
###### This exercise use in-memory DB.

### POST - LOAN
In this endpoint we can create a new loan using the following payload:
```json
{
    "isbn": guid;
    "userType": should be an integer value between 1 and 3;
    "userId": Alfanumeric;
}
```

The system simulates a new loan with the following rules:
- A user can have only 1 loan at a time. If the user already has an active loan, the system returns an error.
- The process provides a return date based on the user type ignoring Saturdays and Sundays.
    - UserType 1 (Affiliated): 10 days.
    - UserType 2 (Employee): 8 days.
    - UserType 3 (Guest): 7 days.
- The endpoint also provides a data type validation for all properties, ensuring a correct functionality.

### GET - LOAN BY ID
This endpoint provides the loan record with the following structure:

```json
{
    "id": guid;
    "isbn": guid;
    "userId": string;
    "UserType": UserType;
    "ReturnDate": DateTime;
}
```

If a loan with the provided id doesn't exist, the system returns an error with a 404 NotFound code.

### GET - ALL LOANS
The endpoint returns an array with all the loans in the system.