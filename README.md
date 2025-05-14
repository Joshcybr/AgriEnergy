# AgriEnergy  
https://github.com/Joshcybr/AgriEnergy

AgriApp is a role-based ASP.NET Core MVC web application designed to streamline farm product management for both Farmers and Employees. It supports user registration and login, product listings, and personalized dashboards — all with a modern dark themed UI.


### User Roles
- **Farmer**
  - Register with full name, date of birth, and cell phone and region.
  - View personalized dashboard with a welcome message.
  - Add, edit, and delete farm products.
- **Employee**
  - View Employee Dashboard with different access rights.

###  Farmer Dashboard
- Personalized greeting using the registered name.
- Product list view with:
  - Modern dark UI
  - Neon green highlights
  - Custom styled Edit/Delete buttons
  - Responsive layout

###  Product Management
- Add product (name, description, quantity, price, Catergory).
- Edit or remove products via action buttons.
- All product data linked to the logged in Farmer.

###  UI & Styling
- Dark-themed layout for consistency.
- Neon green highlights for branding and visibility.

### Database
- Switched from SQL Server to **SQLite** for simpler setup and deployment.


## Tech Stack

- **Backend:** ASP.NET Core MVC (.NET 6+) ;C# AND Java Script
- **Frontend:** Razor Views, HTML, CSS 
- **Authentication:** ASP.NET Identity (Role-based)
- **IDE:** Visual Studio 2022


## How to Run The app

. Clone the Repository
https://github.com/Joshcybr/AgriEnergy
 git clone 
 
2. Open the Solution
 - Launch AgriEnergy.sln in Visual Studio 2022.

3. Configure the Database

 - Open appsettings.json and ensure the SQLite connection string is set:
 "ConnectionStrings": {
 "DefaultConnection": "Data Source=agriapp.db"
 }

4. Apply Migrations
 - Open the Package Manager Console:
 Tools > NuGet Package Manager > Package Manager Console
 - Run:
 Update-Database
5. Run the App

6. Register Users
 - Choose role (Farmer or Employee)
 - fill in the form
7. Log In and Test
 


## Refferances


https://www.w3schools.com/

https://www.youtube.com/watch?v=F4ePoeSien8&t=15090s

https://www.youtube.com/watch?v=8_eMgS6UszY&list=PLIY8eNdw5tW_ZQawyxK0Dd1cZXwcNFWn8

https://uiverse.io/AqFox/young-dragon-29
