# Blazor.ChatIA

This project is an ASP.NET Blazor application integrated with the OpenAI Platform to provide advanced chat functionalities powered by the **Retrieval-Augmented Generation (RAG)** method. The project follows a structure inspired by **Clean Architecture** and SOLID principles, ensuring strong separation of concerns to enhance scalability and maintainability. It includes encryption of sensitive data using AES (Advanced Encryption Standard) with a symmetric key.

---

## Key Features

- **AI Agent**: The OpenAI integration enables context-aware and highly accurate responses through the RAG method.
- **File Handling**: Users can upload files for dynamic query contexts.
- **Clean Architecture Design**: A modular and well-structured design using **Domain**, **Application**, **Data**, **Infrastructure**, and **App** layers.
- **Symmetric Data Encryption**: All sensitive data is encrypted using AES (Advanced Encryption Standard) with a symmetric key to ensure data confidentiality and security.
- **Execution Options**:
  1. **Local Build**: Requires dependencies to be installed manually.
  2. **Docker Compose**: Simplifies configuration and execution through containerization.

---

## Technologies Used

- **ASP.NET Core** 9.0
- **Blazor** (for UI)
- **Entity Framework Core** (for ORM)
- **PostgreSQL** (as the database)
- **OpenAI Platform** (for AI functionality)
- **Docker Compose** (for building and running containerized services)
- **C# 13** (primary programming language)

---

## File Structure

The project is structured to follow a modular, **Clean Architecture-inspired design**, separating key responsibilities into distinct layers:

### Layers and Responsibilities

- **`App/`**  
  Contains the entry point of the application along with presentation-specific logic, including:
  - **Blazor Components** (UI elements)
  - Pages and Razor views for rendering the front-end.
  
- **`Application/`**  
  Handles core application logic, interfaces, DTOs (Data Transfer Objects), and models. This layer defines contracts and rules for the application logic but provides no direct implementations. Key components:
  - **Interfaces**: Abstractions for repositories, services, and gateways.
  - **Use Cases**: Core use case implementations for managing application behavior.
  - **Models and DTOs**: Application-specific representations of data for validation and communication.

- **`Domain/`**  
  The heart of the application, containing entities and business rules:
  - **Entities**: Represent the core business logic and use-case-agnostic rules.
  - Include shared value objects and domain-specific behaviors.

- **`Data/`**  
  Responsible for persistence and interaction with the database. It uses **Entity Framework Core** for managing the database context and repositories:
  - **DbContext**: Configures and interacts with the PostgreSQL database.
  - **Repositories**: Implementations for managing database entities using the repository pattern.

- **`Infra/` (Infrastructure)**  
  Provides external system implementations and integrates tools like AI presenters and services. Key components:
  - **Presenters**: Intermediaries for formatting or adapting data across layers.
  - **IA Tools**: Services for communicating with third-party systems like OpenAI.
  - **Gateways**: Manages integrations with external APIs.
  - **Security Service**: Implements encryption and decryption logic using AES (Advanced Encryption Standard) with a symmetric key.

---

### Encryption of Sensitive Data

Sensitive data is encrypted using **AES (Advanced Encryption Standard)** with a **symmetric key**. This ensures secure handling of data in transit and at rest by applying encryption and decryption methods through the `SecurityService` class.

#### Overview of `SecurityService`

The `SecurityService` provides the following functionalities:
- **Generate Key and IV (Initialization Vector):** Dynamically generates a symmetric key and IV for encryption operations.
- **Encrypt and Decrypt Methods:** Supports encryption and decryption of plain text, byte arrays, and streams.
- **Encryption Algorithm:** Uses AES (Advanced Encryption Standard) with a 256-bit key for strong security.

---

## Getting Started

The project supports two modes of execution: **Local Build** and **Docker Compose**.

---

### Option 1: Running Locally (Build)

Requires manual installation of dependencies and configuration of your environment.

#### Prerequisites

Install the following tools:
- [.NET 9.0 SDK](https://dotnet.microsoft.com/)
- [Node.js and npm](https://nodejs.org/) (for managing Tailwind CSS)
- [PostgreSQL](https://www.postgresql.org/) (database)

#### Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/ViniciusOrtiz/Blazor.ChatAi.git
   ```

2. Install Tailwind CSS dependencies:

   ```bash
   cd Blazor.ChatAi
   npm install
   ```

3. Configure `appsettings.json`:
   Update the `appsettings.json` file with the following values:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Username=myuser;Password=mypassword;Database=mydatabase"
     },
     "OpenAISettings": {
       "ApiKey": "your-openai-api-key"
     }
   }
   ```
   - `SecuritySettings` section is mocked with random value. Change it if you want using values generated by the `SecurityService.GenerateKeyAndIV()` method
   - Replace `your-openai-api-key` with the API key obtained from [OpenAI](https://platform.openai.com/).
   - Replace `your-encryption-key` and `your-initialization-vector` with values generated by the `SecurityService.GenerateKeyAndIV()` method.

4. Apply migrations:

   ```bash
   dotnet ef migrations add InitialMigration
   dotnet ef database update
   ```

5. Start the application:

   ```bash
   dotnet run
   ```

6. Open your browser and go to **http://localhost:7248**.

---

### Option 2: Running with Docker Compose

Run the application and its infrastructure components (e.g., PostgreSQL) in a containerized environment.

#### Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/ViniciusOrtiz/Blazor.ChatAi.git
   ```

2. Copy `.env.example` to `.env`:

   ```bash
   cp .env.example .env
   ```

3. Set the necessary values in the `.env` file:
   ```plaintext
    PGSQL_HOST=psql
    PGSQL_PORT=5432
    PGSQL_DATABASE=postgres
    PGSQL_USER=myuser
    PGSQL_PASSWORD=mypassword
    PGSQL_CONNECTION_STRING=Host=${PGSQL_HOST};Port=${PGSQL_PORT};Username=${PGSQL_USER};Password=${PGSQL_PASSWORD};Database=${PGSQL_DATABASE}
    OPENAI_API_KEY=
   ```

   Replace the placeholders with valid values.

4. Run Docker Compose:

   ```bash
   docker-compose --env-file .env up -d
   ```

5. Access the application in your browser at **http://localhost:8000**.

---

### Comparison of Execution Methods

| Feature                    | Local Build                                      | Docker Compose                                   |
|----------------------------|-------------------------------------------------|------------------------------------------------|
| **Dependency Installation**| Required on host machine                        | Not required (containerized dependencies)       |
| **Configuration**          | Manual setup in `appsettings.json`              | Use of `.env` for environment variables         |
| **Setup Time**             | Longer (manual effort)                          | Faster (automated with Docker Compose)          |

---

🎥 **Demo:** 

![Demo](Assets/Demo/Demo.gif)


---

## Contributing

Contributions are welcome! Feel free to open issues or submit pull requests.

---

## License

This project is licensed under the MIT License. See the `LICENSE` file for more details.

---

## Contact

If you have questions or suggestions, feel free to reach out:

- **Name**: [Vinicius Ortiz]  
- **Email**: vinicius-ortiz@outlook.com