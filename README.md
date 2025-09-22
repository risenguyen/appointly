# Appointly

## Showcase

Coming Soon.

## About

Appointly is a web application for managing appointments. It allows staff to manage treatments and view their schedules. This project is built with a modern tech stack, featuring a .NET backend and a React frontend.

## Tech Stack

### Frontend

- **Framework:** React 19 with Vite
- **Routing:** TanStack Router
- **UI:** shadcn/ui, Tailwind CSS
- **State Management:** TanStack Query
- **API Client:** Generated with `openapi-ts`

### Backend

- **Framework:** ASP.NET Core 8
- **Database:** Entity Framework Core 8 with SQL Server
- **Authentication:** JWT (JSON Web Tokens)
- **API:** RESTful API with OpenAPI (Scalar) documentation

## Features

- **Authentication:** Secure login for staff members.
- **Staff Management:** CRUD operations for staff members.
- **Treatment Management:** CRUD operations for treatments.
- **Appointment Scheduling:** Schedule and manage appointments.

## Project Structure

The project is organized into two main parts: `frontend` and `backend`.

### Frontend Structure

The frontend is structured as follows:

- `src/api`: Contains the generated API client.
- `src/components`: Reusable UI components.
- `src/context`: React context providers for authentication and theme.
- `src/features`: Components related to specific features (auth, staff, treatments).
- `src/hooks`: Custom hooks.
- `src/lib`: Utility functions and client configuration.
- `src/routes`: Route definitions for TanStack Router.

### Backend Structure

The backend follows a standard .NET 3-Tier Architecture approach:

- `appointly.API`: The presentation layer, containing controllers and API configuration.
- `appointly.BLL`: The business logic layer, containing services and DTOs.
- `appointly.DAL`: The data access layer, containing database context, entities, and repositories.
