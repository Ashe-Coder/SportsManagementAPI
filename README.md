# Sports Management API

## Overview
The **Sports Management API** is a simple RESTful API built with **ASP.NET Core**. It allows you to manage sports-related data such as players, teams, leagues, and matches. It also includes a background service that periodically updates the total number of passes for matches.

## Features
- Manage players, teams, leagues, and matches.
- Background service to calculate and update total passes for matches.

## Technologies Used
- **ASP.NET Core**: Web framework.
- **Entity Framework Core**: ORM for database access.
- **SQL Server**: Database.
- **AutoMapper**: For mapping objects.
- **xUnit**: For testing.

## API Endpoints

### Players
- **GET** `/api/players/{id}`: Get a player by ID.
- **POST** `/api/players`: Add a new player.
- **PUT** `/api/players`: Update a player.
- **DELETE** `/api/players/{id}`: Delete a player.

### Teams
- **GET** `/api/teams/{id}`: Get a team by ID.
- **POST** `/api/teams`: Add a new team.
- **PUT** `/api/teams`: Update a team.
- **DELETE** `/api/teams/{id}`: Delete a team.

### Leagues
- **GET** `/api/leagues/{id}`: Get a league by ID.
- **POST** `/api/leagues`: Add a new league.
- **PUT** `/api/leagues`: Update a league.
- **DELETE** `/api/leagues/{id}`: Delete a league.

### Matches
- **GET** `/api/matches/{id}`: Get a match by ID.
- **POST** `/api/matches`: Add a new match.
- **PUT** `/api/matches/{id}`: Update a match.
- **DELETE** `/api/matches/{id}`: Delete a match.

## Background Service
A background service runs every minute to calculate and update the **TotalPasses** for each match.
