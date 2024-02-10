# Library Application

## Overview

This Library Management System is a web application designed to facilitate the management of books and library memberships. It consists of two main components: a librarian interface for managing books and members, and a client interface for library patrons to check the availability and loan status of books. The application is backed by an SQL database, with a Web API handling data transactions.

## Requirements

- **.NET 6.0** or later
- **ASP.NET Core Web API** for backend services
- **Blazor WebAssembly** for frontend applications (two separate applications for librarian and patron interfaces)
- **Entity Framework Core** (using MSSQL LocalDB) for database operations
- Coding conventions enforced by **StyleCop.Analyzers**

## Features

### Librarian Interface

- Display books and members in tabular format
- Add new members and manage their information
- Issue and receive books, with special indication for overdue returns
- Query book status (available/loaned, loaned by whom, return due date)

### Patron Interface

- View a list of available and loaned books
- Check loan status and due dates for books they've borrowed
- The interface is accessible on dedicated library terminals

## Database Schema

Includes tables for storing information about books, members, and loans. Each table includes fields relevant to the entity it represents, such as title, author, and publisher for books; name, address, and membership number for members; and loan dates and return deadlines for loans.

## Setup and Installation

1. Clone the repository to your local machine.
2. Ensure you have .NET 6.0 SDK installed.
3. Open the solution in your preferred IDE (e.g., Visual Studio) and restore NuGet packages.
4. Set up a local MSSQL LocalDB instance for the database.
5. Run the backend Web API project.
6. Launch the Blazor WebAssembly applications for both librarian and patron interfaces.

## Future Work

- Implement unit tests to cover validation logic and data access layers.
- Expand the database schema to include more detailed book and member information.
