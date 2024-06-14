# Library Management System

This is a full-stack web application project for managing a library, developed using HTML5, CSS, JavaScript for the front-end, and ASP.NET for the back-end with SQL Server for database management.

## Features

- **User Management**: 
  - Authentication and authorization for librarians and members.
  - Admin panel for managing users and their roles.

- **Book Management**:
  - Add, update, delete books with details like title, author, genre, and availability.
  - Search and filter books by various criteria.

- **Borrowing System**:
  - Members can borrow books.
  - Fine calculation for late returns.

- **Dashboard**:
  - Overview of borrowed books, fines, and other relevant statistics.
  - Quick access to overdue books and pending tasks.

## Technologies Used

- **Front-end**:
  - HTML5
  - CSS (with Bootstrap for styling)
  - JavaScript (including AJAX for asynchronous operations)

- **Back-end**:
  - ASP.NET MVC framework
  - C# programming language
  - Entity Framework for database operations

- **Database**:
  - SQL Server
  - SSMS
  - Tables for users, books, transactions, fines, etc.

## Installation

1. **Clone Repository**:
    - git clone https://github.com/Zer0-Bug/Project_LibraryManagementSystem.git


2. **Database Setup**:
  - Restore the SQL Server database backup provided (`DBKUTUPHANE.bak`).
  - Update connection string in `web.config` file under `<connectionStrings>`.

3. **Run Application**:
  - Open the solution in Visual Studio.
  - Build and run the application (press F5).

## Usage

1. **Librarian**:
  - Log in using admin credentials.
  - Manage users, books, transactions, and generate reports.

2. **Member**:
  - Log in using member credentials.
  - Browse books, borrow books, and check fines.

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License - see the LICENSE.md file for details.