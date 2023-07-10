# New Zealand Walks API

Welcome to the New Zealand Walks API! This API provides information about various walks in New Zealand, including details about the walks, the regions they belong to, and their difficulty levels. The API is built using ASP.NET Core, utilizing Dotnet Identity for authentication and JWT for authorization.

## Table of Contents

- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [API Documentation](#api-documentation)
  - [Authentication](#authentication)
  - [Endpoints](#endpoints)
- [Database Structure](#database-structure)
- [Contributing](#contributing)
- [License](#license)

## Getting Started

### Prerequisites

To run the New Zealand Walks API, make sure you have the following installed on your machine:

- [.NET Core SDK](https://dotnet.microsoft.com/download) (version 3.1 or later)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or a compatible database engine

### Installation

1. Clone the repository to your local machine:

   ```bash
   git clone https://github.com/Mbadady/NZWalksAPI.git
2. Navigate to the project directory:
   ```bash
   cd NZWalksAPI
3. Restore the project dependencies:
   ```bash
   dotnet restore
4. Create a new SQL Server database for the project.
5. Update the database connection string in the `appsettings.json` file:
   ```bash
   {"ConnectionStrings": {
    "DefaultConnection": "Server=<server-name>;Database=<database-name>;User=<username>;Password=<password>;"},...}
6. Apply the database migrations to create the necessary tables:
    ```bash
        dotnet ef database update
7. Start the API:
   ```bash
   dotnet run --urls=http://localhost:<port-number>

The API should now be up and running on `http://localhost:<port-number>`.

## API Documentation
### Authentication

The API uses JWT (JSON Web Tokens) for authentication. To access protected endpoints, you need to include the JWT token in the `Authorization` header of your HTTP requests using the `Bearer` scheme.

To obtain a JWT token, make a `POST` request to the `/api/auth/login` endpoint with valid credentials (username and password) as JSON in the request body. The response will include the JWT token, which you can use for subsequent requests.

### Endpoints
The New Zealand Walks API provides the following endpoints:

- `GET /api/walks` - Get a list of all walks in New Zealand.

- `GET /api/walks/{id}` - Get detailed information about a specific walk.

- `POST /api/walks` - Create a new walk (requires authentication).

- `PUT /api/walks/{id}` - Update information about a specific walk (requires authentication).

- `DELETE /api/walks/{id}` - Delete a specific walk (requires authentication).

- `GET /api/regions` - Get a list of all regions in New Zealand.

- `GET /api/regions/{id}` - Get detailed information about a specific region.

- `POST /api/regions` - Create a new region (requires authentication).

- `PUT /api/regions/{id}` - Update information about a specific region (requires authentication).

- `DELETE /api/regions/{id}` - Delete a specific region (requires authentication).

For detailed information on the request and response formats of each endpoint, please refer to the API documentation or explore the API using tools like Swagger or Postman.

## Database Structure

The New Zealand Walks API uses three main tables to store information:

**Walks**: Contains information about the walks in New Zealand, including walk name, walk image Url, description, length in KM, difficulty level, and the region it belongs to.

**Regions**: Stores details about the regions in New Zealand, such as region name, code, and region image Url.

**Difficulties**: Maintains the various difficulty levels for the walks, including a name and description for each difficulty level.

These tables are related through foreign key constraints to establish the relationships between walks, regions, and difficulties.

## Contributing
Contributions to the New Zealand Walks API are welcome! If you find any issues or want to suggest improvements, please feel free to submit a pull request or open an issue in the GitHub repository.

When contributing, please follow the existing coding style and guidelines. Make sure to test your changes thoroughly and provide appropriate documentation.

## License
This project is licensed under the MIT License. Feel free to use, modify, and distribute the code for both commercial and non-commercial purposes.
