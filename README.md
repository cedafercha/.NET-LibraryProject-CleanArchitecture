# README :)

Practical exercise covering various topics, including:

- **Clean Architecture:** Designing layers with unique responsibilities.
- **CQRS pattern:** Handling read and write operations in separate processes.
- **AAA pattern in Tests:** Enhancing order and readability during development.
- **Exception middleware:** Improving control over response messages and HTTP codes.
- **Asynchronous Processes:** Managing tasks effectively.
- **LINQ:** Performing database queries and writes.
- **SOLID Principles:** Ensuring maintainable and scalable code.
- **Mediator Pattern:** Facilitating communication between objects.
- **Clean Code:** Writing readable and well-structured code.

## Project Structure - Layers
### Api
- This layer contains the Controller and the Exception Middleware.
 
### Application
- This layer includes the ViewModels, Profiles for handling AutoMapper, and Query-Commands implemented using the Mediator pattern.

### Domain
- This layer contains the business logic, primarily the **LoanService**, which is responsible for managing the core processes of the system.

### Infrastructure
- his layer manages database access using Finder and Repository classes, separating reading and writing responsibilities.

### Tests
- This layer includes both unit and integration tests.

## Running the App
```sh
dotnet build
dotnet run --project ./LibraryProject/LibraryProject.Api/LibraryProject.Api.csproj

// The application is now running on port 5002.
// If you want to use Swagger, navigate to the /swagger route on port 5002.
```

## Endpoints
###### This exercise uses an in-memory database.

### POST - LOAN
This endpoint allows us to create a new loan using the following payload:

```javascript
{
    "isbn": "guid"
    "userType": "number" // should be an integer value between 1 and 3
    "userId": "string"
}
```

The system simulates a new loan with the following rules:

1. Loan Restrictions:

    - A user can have only one loan at a time.
    - If the user already has an active loan or the book is already on loan, the system returns an error.

2. Return Date Calculation:

    - The system determines a return date based on the user type, excluding Saturdays and Sundays.
        - UserType 1 (Affiliated): 10 days.
        - UserType 2 (Employee): 8 days.
        - UserType 3 (Guest): 7 days.

3. Data Validation:

    - The endpoint validates the data type of all properties to ensure proper functionality.

### GET - LOAN BY ID
This endpoint retrieves the loan record with the following structure:

```javascript
{
    "id": "guid"
    "isbn": "guid"
    "userId": "string"
    "UserType": "number"
    "ReturnDate": "string" // Date with format dd/MM/yyyy
}
```

If a loan with the provided id doesn't exist, the system returns an error with a 404 NotFound code.

### GET - ALL LOANS
The endpoint returns an array containing all loans in the system.

