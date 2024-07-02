# Graduation Project: API Backend

## Description
#### "DEVELOPING AN ONLINE LEARNING PLATFORM"
This project is a website for managing courses and events and enabling students to interact with a flexible, interactive learning environment that suits their time. It enables any student to book a lecture with a specific instructor at the appropriate time.

## Table of Contents
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Privileges for Each Actor](#privileges-for-each-actor)
    - [Admin](#admin)
    - [SubAdmin](#subadmin)
    - [Main-SubAdmin](#main-subadmin)
    - [Instructor](#instructor)
    - [Student](#student)
- [Security Measures](#security-measures)
- [Email Notifications](#email-notifications)
- [Contact Information](#contact-information)

## Getting Started
To get a copy of the project up and running on your local machine for development and testing purposes, follow these steps:

1. **Clone the repository:**
   ```sh
   git clone https://github.com/your-username/your-repo.git
   # Setting Up and Running the Project
2. **Configure the email service:**

To set up the email service provider settings, update `appsettings.json` with the following configuration:

  ```json
{
  "EmailSettings": {
    "SmtpServer": "smtp.your-email-provider.com",
    "SmtpPort": 587,
    "SenderEmail": "your-email@your-domain.com",
    "SenderPassword": "your-email-password"
  }
}
```

## Usage

Instructions on how to use the project, including example requests and responses for the API endpoints.

## Privileges for Each Actor

### Admin
- Login, logout, reset password.
- Edit profile.
- View all employees, students, courses, and events.
- Create and edit employee accounts (main-subAdmin, subAdmin, and instructor).
- Accredit or reject the courses and events created by sub-admin or main sub-admin.
- Edit courses or events after accreditation.
- View all courses given by instructor, all skills for an instructor, all students enrolled in specific course, and all courses that student enrolled in.
- Change roles between main-subAdmin and subAdmin.
- View and add skills given in the system.
- View feedbacks.
- View reports and download as PDF or Excel.

### SubAdmin
- Login, logout, reset password.
- Edit profile.
- View and create courses or events.
- Edit courses or events created before admin accreditation.
- View feedbacks.

### Main-SubAdmin
- All permissions and capabilities available to the sub-admin.
- Accept or reject student requests to enroll in courses.
- Manage requests from students who want to create their own courses.
- View messages and questions sent by students through Contact Us.

### Instructor
- Login, logout, reset password.
- Edit profile.
- Set available hours.
- Add a bio and background.
- Select skills created by admin.
- View dashboard.
- Conduct scheduled lectures with students based on weekly working hours.
- CRUD operations on course and lecture content.
- View course/lecture participants.
- View feedbacks.

### Student
- Register, login, logout, reset password.
- Edit profile.
- View and enroll in all courses and lectures.
- Book lectures with chosen instructor during available weekly hours.
- View enrolled course/lecture content, interact with it, and add submissions.
- View participants registered in the course/lecture.
- Request to create custom courses.
- View and write feedbacks.

## Security Measures

The backend API is designed with several security measures to ensure the safety and integrity of the system and its data. Below are the key security practices implemented:

- **Authorization and Authentication**
  - Role-Based Access Control (RBAC): The system implements role-based access control to ensure that users have access only to the resources and actions that their roles permit.
  - JWT Tokens: JSON Web Tokens (JWT) are used for user authentication. After a user logs in, they receive a JWT which they must include in the header of subsequent requests to access protected endpoints.

- **Password Security**
  - Hashing: User passwords are securely hashed before being stored in the database.
  - Hashing Algorithm: A strong hashing algorithm such as BCrypt is used to hash passwords.
  - Salting: Unique salts are added to each password before hashing to protect against rainbow table attacks.

- **Secure Communication**
  - HTTPS: All communication between clients and the server is encrypted using HTTPS, protecting data in transit from being intercepted or tampered with.

## Email Notifications
  - Email notifications are sent for various operations such as registration confirmation, password reset, and course enrollment. Ensure the email service provider settings are correctly configured in `appsettings.json`.

## Contact Information

For questions or feedback, please contact me at [manar.a.fuqha@gmail.com](mailto:manar.a.fuqha@gmail.com).
