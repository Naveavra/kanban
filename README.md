# Kanban Board with Three-Tier Architecture

This project is a Kanban board implementation written in C# that showcases the use of a three-tier architecture. The three-tier architecture separates the application into three distinct layers: presentation layer, business logic layer, and data access layer. The project also includes a small frontend implementation to provide a user interface for interacting with the Kanban board.

# Features
* Kanban board: The application allows users to create and manage tasks on a Kanban board, which consists of three columns: "To Do," "In Progress," and "Done." Users can move tasks between these columns by dragging and dropping them.
* Three-tier architecture: The project demonstrates the proper separation of concerns by implementing a three-tier architecture. Each layer has its responsibilities and interacts with the other layers using well-defined interfaces.
* Frontend implementation: The project includes a simple frontend implementation to provide a user-friendly interface for interacting with the Kanban board. The frontend is designed using HTML, CSS, and JavaScript.

# Project Structure
The project is organized into the following three layers:

-Presentation Layer: This layer handles the user interface and user interactions. It includes the frontend implementation responsible for rendering the Kanban board and allowing users to interact with it. The presentation layer communicates with the business logic layer to perform operations on the Kanban board.

-Business Logic Layer: This layer contains the core logic of the Kanban board application. It handles tasks such as task creation, moving tasks between columns, and updating task status. The business logic layer interacts with the data access layer to persist data and retrieve it as needed.

Data Access Layer: This layer is responsible for managing the persistence and retrieval of data. It interacts with the underlying data storage (e.g., a database) to save and retrieve task information.

# Conclusion
This Kanban board project demonstrates the implementation of a three-tier architecture using C#. By separating the presentation layer, business logic layer, and data access layer, the project achieves a modular and maintainable design. The frontend implementation provides an intuitive interface for managing tasks on the Kanban board. Feel free to explore and enhance the project to meet your specific requirements.
