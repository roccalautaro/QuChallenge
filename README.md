# WordFinder API

## 🖊 Overview
The **WordFinder API** allows users to search for words in a character matrix, returning the top 10 most repeated words from a given word stream found in the matrix. This project demonstrates robust API design, clean architecture principles, and LINQ-based processing for efficient word search operations.

---

## 🚀 Features
- Processes word streams to find matches in a character matrix.
- Returns the **top 10 most repeated words**, ordered by frequency.
- Ignores duplicate entries in the word stream.
- Validates matrix dimensions (up to 64x64) and ensures all rows have the same length.

---

## 🛠️ Tech Stack
- **.NET 8**
- **ASP.NET Core Web API**
- **Swagger** for API documentation
- **XUnit** for testing

---

## 📆 Project Structure
```plaintext
QuChallenge/
🗄
├── QuChallenge.Api/           # API layer
│   ├── Controllers/           # Controllers for handling HTTP requests
│   └── Program.cs             # Entry point of the application
│
├── QuChallenge.Application/   # Application layer (services and interfaces)
│
├── QuChallenge.Domain/        # Domain layer (business logic and entities)
│   ├── Entities/              # Core entities like WordFinder
│   └── Interfaces/            # Interfaces for abstraction
│
├── QuChallenge.Tests/         # Unit tests for the application
│   └── WordFinderTests.cs     # Tests for WordFinder logic
│
└── README.md                  # Project documentation
```

---

## ⚙️ Prerequisites
Ensure you have the following installed:
1. **.NET 8 SDK**: [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
2. **Visual Studio** or any IDE with .NET support.
3. **Postman** (optional): For manual API testing.

---

## 🚀 How to Run the Project

### 1. Clone the Repository
```bash
git clone https://github.com/roccalautaro/QuChallenge.git
cd QuChallenge
```

### 2. Restore Dependencies
Run the following command to restore NuGet packages:
```bash
dotnet restore
```

### 3. Build the Project
```bash
dotnet build
```

### 4. Run the API
```bash
dotnet run --project QuChallenge.Api
```

By default, the API will run on `https://localhost:7225`. You can modify the port in the `launchSettings.json` file.

### 5. Access Swagger
Once the API is running, you can access the Swagger UI at:
```
https://localhost:7225/swagger
```
This provides a comprehensive interface to test and interact with the API endpoints.

---

## 📚 Running Tests
To execute the unit tests, run the following command:
```bash
dotnet test
```
This will run all tests in the `QuChallenge.Tests` project.

---

## 🖌️ API Endpoints

### **POST /api/WordFinder/find**
Processes the word stream and matrix to return the top 10 most repeated words.

#### Request Body
```json
{
  "matrix": [
    "hellohellohello",
    "worldworldworld",
    "testtesttesttes"
  ],
  "wordStream": ["hello", "world", "test", "hello"]
}
```

#### Response
```json
[
  "hello",
  "world",
  "test"
]
```

#### Validation Rules:
- **Matrix:** Must not exceed 64x64 dimensions.
- **Rows:** All rows must have the same length.
- **WordStream:** Duplicate entries are ignored.

---

## 🛡️ Validation and Rules
- The matrix size must not exceed **64x64**.
- All rows in the matrix must have the **same length**.
- If no words from the word stream are found, the API returns an **empty set**.
- The word stream may contain duplicates, but only unique entries are processed.


## 📧 Contact
For questions or support, contact **Lautaro Rocca** at:
- **Email:** Roccalautarom99@gmail.com
- **Phone:** +54 11 5577 2076
```


