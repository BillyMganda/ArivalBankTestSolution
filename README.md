# Arival Bank Test Solution

## Getting Started

### Prerequisites
1. .NET SDK v6(.NET6)
2. Visual Studio or Visual Studio Code (for development and testing)
3. Redis Server (for caching)
4. MS-SQL Server

### Installing
Clone the repository to your local machine:
`git clone https://github.com/BillyMganda/ArivalBankTestSolution.git` <br />
Open the project in your preferred IDE (Visual Studio or Visual Studio Code). <br />
Create an appsettings.json file in the project root directory with the following contents:
````json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "SQL_DBConnection": "YOUR-SQL-CONNECTION-STRING"
  },
  "ServiceConfigs": {
    "CodeExpirationMinutes": 30,
    "ConcurrentCodesPerPhone": 5
  },
  "RedisConfig": {
    "ConnectionString": "localhost:6379",
    "InstanceName": "ArivalBankTest"
  }
}
````
Replace "YOUR-SQL-CONNECTION-STRING" with your MS-SQL Server connection string.

### Endpoints
#### Send a Confirmation Code
URL: https://localhost:7154/api/TwoFactorAuthentication/send-code <br />
Method: POST <br />
Request Body:
````json
{
  "PhoneNumber": "+1234567890"
}
````
Response:
````json
{
  "CodeSent": true
}
````

#### Check a Confirmation Code
URL: https://localhost:7154/api/TwoFactorAuthentication/check-code <br />
Method: POST <br />
Request Body:
````json
{
  "PhoneNumber": "+1234567890",
  "Code": "123456"
}
````
Response:
````json
{
  "CodeValid": true
}
````
