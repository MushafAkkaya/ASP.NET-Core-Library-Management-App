# Library Management App

This is a simple **Library Management Application** built with **ASP.NET Core MVC**. It allows users to manage books and authors in a library system. The application supports basic CRUD (Create, Read, Update, Delete) operations for both books and authors.

---

## Table of Contents
1. [Features](#features)
2. [Technologies Used](#technologies-used)
3. [Project Structure](#project-structure)
4. [How to Run the Project](#how-to-run-the-project)
5. [Testing the Application](#testing-the-application)
6. [Contributing](#contributing)
7. [License](#license)

---

## Features

### Book Management
- **List Books**: View a list of all books in the library.
- **Book Details**: View detailed information about a specific book.
- **Add Book**: Add a new book to the library.
- **Edit Book**: Update the details of an existing book.
- **Delete Book**: Remove a book from the library.

### Author Management
- **List Authors**: View a list of all authors.
- **Author Details**: View detailed information about a specific author.
- **Add Author**: Add a new author to the library.
- **Edit Author**: Update the details of an existing author.
- **Delete Author**: Remove an author from the library.

---

## Technologies Used

- **ASP.NET Core MVC**: For building the web application.
- **C#**: Primary programming language.
- **Razor Views**: For rendering HTML with dynamic data.
- **Static Data**: Books and authors are stored in static lists (in-memory) for simplicity.
- **Model-View-ViewModel (MVVM) Pattern**: Used to separate concerns and improve maintainability.

---

## Project Structure

The project is organized as follows:

```
LibraryManagementApp/
├── Controllers/
│   ├── HomeController.cs       # Handles home and about pages
│   ├── BookController.cs       # Handles book-related operations
│   └── AuthorController.cs     # Handles author-related operations
├── Models/
│   ├── Book.cs                 # Represents a book entity
│   ├── Author.cs               # Represents an author entity
│   ├── BookListViewModel.cs    # ViewModel for listing books
│   ├── BookAddViewModel.cs     # ViewModel for adding a book
│   ├── BookEditViewModel.cs    # ViewModel for editing a book
│   ├── AuthorListViewModel.cs  # ViewModel for listing authors
│   ├── AuthorAddViewModel.cs   # ViewModel for adding an author
│   └── AuthorEditViewModel.cs  # ViewModel for editing an author
├── Views/                      # Contains Razor views for each action
├── Program.cs                  # Entry point of the application
└── README.md                   # Project documentation
```

---

## How to Run the Project

### Prerequisites
- **.NET 6 SDK** or later installed on your machine.

### Steps to Run
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/LibraryManagementApp.git
   ```
2. Navigate to the project directory:
   ```bash
   cd LibraryManagementApp
   ```
3. Run the application:
   ```bash
   dotnet run
   ```
4. Open your browser and go to:
   ```
   https://localhost:5001
   ```

---

## Testing the Application

### Manual Testing
- Navigate through the application using the provided links and buttons.
- Perform CRUD operations on books and authors to ensure functionality.

### Automated Testing (Optional)
- Add unit tests for controllers and models using **xUnit** or **NUnit**.
- Test validation logic and edge cases.

---

## Contributing

Contributions are welcome! If you'd like to contribute, please follow these steps:
1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Commit your changes.
4. Submit a pull request.

---

## License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.
```

---
