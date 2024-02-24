# CRUD_Operations

## Overview
This ASP.NET Core Razor Pages application is designed as a web-based CRUD (Create, Read, Update, Delete) system for managing user information. The application allows administrators and end-users to interact with user data, providing functionalities such as adding, editing, and deleting user records. The application ensures data integrity by preventing the creation of duplicate user records based on unique email addresses.

## Features
1. **User Information:**
 - The application manages the following user information:
        - First Name
        - Last Name
        - Date of Birth (DOB)
        - Gender
        - Country
        - Phone Number
        - Email (unique)
        - Identity Photo
2. **Database Structure:** The application is built on a well-designed database structure to efficiently store and retrieve user data.
3. **User Interface:** Users can input and submit the above-mentioned data through a user-friendly interface.
4. **CRUD Operations:** The application supports CRUD operations for managing user data.
        - **Create:** Users can add new records with unique email addresses.
        - **Read:** The interface displays all entered user data in a table, including identity photos.
        - **Update:** Users can edit existing user records.
        - **Delete:** Users can remove unnecessary records.
5. **Data Display:** The application features a functionality to display all entered user data on the same interface in a table format, including the display of identity photos.
6. **No Duplicate Records:** The application enforces uniqueness based on email addresses to prevent the creation of duplicate user records.
7. **Authentication:**
 - The application has a login page with two roles:
        - **Admins:** Have full access to CRUD operations.
        - **End Users:** Can only view user data without the ability to modify or delete records.

