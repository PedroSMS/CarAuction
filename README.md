# CarAuction

## Design decisions

- **Clean Architecture Folder Structure:** Adopts a modular structure that clearly separates concerns across different layers and makes it easier to navigate the codebase, especially as the project grows
- **Object Inheritance:** Promotes code reusability by defining common attributes and provides a logical hierarchy that mirrors real-world relationships, making the codebase more intuitive and easier to extend
- **SQL Database and Class Table Inheritance:** Implements non-null constraints on mandatory attributes to enforce data integrity and consistency
- **CQRS:** Enables independent optimization of read and write operations, potentially improving performance and scalability.
