# BiblioTech

User Story:

As a user, I want to be able to manage my personal library of books. I want to add new books to my library with details such as title, author, ISBN, and genre. I want to be able to view all the books in my library, update the details of existing books, and remove books that I no longer own. Additionally, I want to create a user account, log in securely, and ensure that only I can modify my book collection.

Acceptance Criteria:

User Account Management:

- Users can create new accounts with a unique username and password.
- Users can log in with their credentials.
- User data (username, password) is securely stored.

Book Management (CRUD Operations):

- Users can add new books to their library (Create).
- Users can view a list of all books in their library (Read).
- Users can edit the details of existing books (Update).
- Users can remove books from their library (Delete).

Data Storage:

- Book data is stored persistently in a database or data store.
- Each book has a unique identifier (e.g., ID) and fields for title, author, ISBN, and genre.

Security:

- Book management operations are restricted to authenticated users.
- Users can only modify their own book data.

Interface:

- A simple web interface, Postman workspace, or Swagger UI is available for interacting with the API.

Additional Notes:

- The user story focuses on the core functionality of the application.
- The interface can be basic as the emphasis is on the backend development.
- The user story can be expanded based on the desired features and complexity of the application.

INSTRUCTIONS:

- Run `docker compose up -d` to start the application, in the root directory.
- Access the Swagger UI to test the API: [http://localhost:5657/swagger/index.html](http://localhost:5657/swagger/index.html)
