# Task Management Application

A full-stack task management application built with React.js frontend and ASP.NET Core backend. This application allows users to manage their tasks efficiently with features like task creation, updating, and deletion, along with user authentication.

## Project Structure

The project is divided into two main parts:

- `frontend/`: React.js application with TypeScript
- `backend/`: ASP.NET Core Web API

## Technologies Used

### Frontend
- React.js
- React Router for navigation
- JWT for authentication
- Tailwind CSS for styling

### Backend
- ASP.NET Core
- PostgreSQL database
- Entity Framework Core
- JWT Authentication
- Swagger/OpenAPI for API documentation

## Prerequisites

Before running the application, make sure you have the following installed:
- Node.js (latest LTS version)
- .NET 7.0 SDK or later
- PostgreSQL database server

## Getting Started

### Backend Setup

1. Navigate to the backend directory:
   ```bash
   cd backend
   ```

2. Restore the .NET packages:
   ```bash
   dotnet restore
   ```

3. Update the database connection string in `appsettings.json` or `appsettings.Development.json`

4. Apply database migrations:
   ```bash
   dotnet ef database update
   ```

5. Run the backend application:
   ```bash
   dotnet run
   ```

The backend API will be available at `https://localhost:7000` (or your configured port)

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm start
   ```

The frontend application will be available at `http://localhost:3000`

## Features

- User Authentication (Register/Login)
- Task Management:
  - Create new tasks
  - View task list
  - Update task status
  - Delete tasks
- Secure API endpoints with JWT authentication
- Responsive design with Tailwind CSS

## API Documentation

When running the backend in development mode, you can access the Swagger UI documentation at:
`https://localhost:7000/swagger` 