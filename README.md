# Tixly
The API allows users to create, update, retrieve, and delete events and tickets, as well as generate sales reports.

Using ASP.NET Core Minimal APIs and latest features of .NET8 and C# 12.

Vertical Slice Architecture and CQRS implementation using MediatR library.

CQRS Validation Pipeline Behaviors with MediatR and FluentValidation.

## Note
This project runs an MS SQL Server database as a Docker container.

Steps to Run the Project:
1. Install Docker Desktop if you haven't already.
2. Set docker-compose as the startup project.
3. Build the project – Docker will automatically pull the required images and dependencies.
4. Once the setup is complete, you can test the APIs using Swagger or Postman.

This ensures the project is fully operational with all dependencies in place.

---

## Endpoints

### Event Management

#### 1. Get Event
**GET** `/api/Event/GetEvent`

Retrieve details of a specific event.

**Query Parameters:**
- `Id` (UUID, required): The unique identifier of the event.

**Responses:**
- `200 OK`: Returns an `EventDto` object.
- `400 Bad Request`: Invalid request parameters.
- `404 Not Found`: Event not found.

---

#### 2. Get Event List
**GET** `/api/Event/GetEventList`

Retrieve a paginated list of events.

**Query Parameters:**
- `PageIndex` (integer, optional): The page index for pagination.
- `PageSize` (integer, optional): The number of events per page.

**Responses:**
- `200 OK`: Returns `EventDtoPaginatedResult`.
- `400 Bad Request`: Invalid request parameters.
- `404 Not Found`: No events found.

---

#### 3. Create Event
**POST** `/api/Event/CreateEvent`

Create a new event.

**Request Body (JSON):**
```json
{
  "name": "string",
  "venue": "string",
  "description": "string",
  "date": "string (date-time)",
  "totalCapacity": 1000,
  "availableTickets": 500,
  "pricingTiers": ["uuid"]
}
```
**Responses:**
- `201 Created`: Returns the created event's ID.
- `400 Bad Request`: Invalid request data.

---

#### 4. Update Event
**PATCH** `/api/Event/UpdateEvent`

Update an existing event.

**Request Body (JSON):**
```json
{
  "id": "uuid",
  "name": "string",
  "venue": "string",
  "description": "string",
  "date": "string (date-time)",
  "totalCapacity": 1000,
  "availableTickets": 500
}
```

**Responses:**
- `200 OK`: Returns the updated EventDto.
- `400 Bad Request`: Invalid request data.
- `404 Not Found`: Event not found.

---

#### 5. Delete Event
**DELETE** `/api/Event/DeleteEvent`

Delete an event.

**Query Parameters:**

- `Id (UUID, required)`: The unique identifier of the event.

**Responses:**

- `200 OK`: Returns true if deletion was successful.
- `400 Bad Request`: Invalid request.
- `404 Not Found`: Event not found.

---
