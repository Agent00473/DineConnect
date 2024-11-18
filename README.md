# DineConnect (Under Development)
A food delivery platform that allows users to browse local restaurants, place orders, and track deliveries. The platform serves customers, restaurant owners, and delivery personnel through a web application built using Angular, with a backend powered by Web API, utilizing microservices architecture, PostgreSQL/SQL databases, and messaging through Azure Service Bus or RabbitMQ.

This project is designed to explore concepts and principles, currently focusing on implementing only the bare minimum use cases required for experimentation.

# Use Cases
  * Customer: Browses restaurants, views menus, places orders, tracks delivery.
  * Restaurant: Manages menus, receives orders, and updates order statuses.
  * Delivery Person: Picks up food orders and delivers them to customers.
  * Admin: Manages users, restaurants, orders, and payments.
# Proposed Microservices for MVP
  * Payment Service
  * Restaurant Management Service
  * Order Management Service
  * Delivery Service
  * User Management Service
# Selected Microservices for MVP
  * Payment Service.
  * Order Management Service.
  * Restaurant Management Service.
  * _User Management Service_ (**to be implemented later**).
  * _Delivery Service_ (**to be implemented later**).
# Development Flow
  * Design the Database: Create the database schema for orders, payments, and restaurants in PostgreSQL/SQL.
  * Set Up Microservices: Develop the selected microservices (Order Management, Payment, Restaurant Management) follow Clean Architecture.
  * Integrate Messaging: Implement RabbitMQ or Azure Service Bus for asynchronous communication.
  * API Testing: Use tools like Postman to test each service endpoint before moving to frontend development.
  * Frontend Development: Build the Angular frontend, integrating with the APIs developed in the backend.
  * User Testing and Feedback: Conduct user testing to gather feedback and identify areas for improvement.

## Web API Solution Name
    DineConnect
## Angular Solution Name
    DineNow
## Decisions
   * Syncronous inter-service communication with gRPC.
   * Asyncronous Communication with RabbitMQ / Azure Service bus.
   * Using RabbitMQ Publish/Subscribe Topic Exchange Model.

## System Overview
![image](https://github.com/user-attachments/assets/eb5c59f3-b73b-4348-a60e-c5156f90f927)

## Project Structure (Clean Architecture)
![image](https://github.com/user-attachments/assets/81efc361-100c-406b-9e46-7938f9559482)

## Request Response Handling
![image](https://github.com/user-attachments/assets/7e011266-51bc-4ac0-a105-2131c891c954)

