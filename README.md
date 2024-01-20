<b>Gringotts Bank Online Banking API</b>

<b>Overview</b>

The Gringotts Bank Online Banking API is designed to provide a secure, efficient, and consistent platform for managing customer profiles, accounts, and transactions. The architecture is built to meet the unique requirements of Gringotts Bank, ensuring goblin-made security and reliability.

<b>System Architecture</b>

The system is architected as a high-performance, distributed design to handle the complexities of online banking operations. The key architectural components include:

API Layer:

Exposes RESTful endpoints to handle customer-related operations, account management, and transaction processing.
Implements secure authentication mechanisms, supporting bearer token authentication for enhanced security.

Service Layer:

Manages the core business logic of the system, including customer creation, account management, and transaction processing.
Implements robust validation mechanisms to ensure data integrity and error-proof system behavior.

Infrastructure  Layer:

It is a middleware layer. Handles unauthorized responses. 

Model Layer:

Stores all data transfer objects.

<b>Technologies and Techniques Used</b>

ASP.NET Core:  .net8.0

Memory Cache: Used for data storage and access. Instead of a database, it was easier to use caching for this project.

Swagger: Provides API documentation for enhanced developer experience.

NLog: Implements logging for effective monitoring and debugging when exceptions.

Authentication: JWT Bearer Token 

<b>Security Considerations</b>

Bearer Token Authentication:
Implements secure authentication mechanisms to protect customer data.
Ensures that all endpoints are protected to maintain the security of sensitive information.

<b>Conclusion</b>

The architectural design of the Gringotts Bank Online Banking API is focused on meeting the unique challenges and requirements of a secure and efficient online banking system. It prioritizes consistency, reliability, and scalability to ensure a seamless customer experience.
