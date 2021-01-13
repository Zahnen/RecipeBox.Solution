* figure out a string method for breaking up ingredient area. Foreach loop? Split-by-character?
* Stretch goal search bar (search by ingredient, search by tag) ref(https://docs.microsoft.com/en-us/dotnet/csharp/how-to/search-strings)


# --RecipeBox--
### An MVC application written in C#, January 12, 2021

#### _Contributors: [Max Brockbank](https://www.github.com/zahnen) & [Taylor Delph](https://www.github.com/maxbrockbank) & [Zahnen Garner](https://www.github.com/taylulz)_

---  

_RecipeBox is an app that allows users to keep track of recipes & was developed as team project by students studying at [Epicodus](https://www.epicodus.com). The application allows users to add recipes into a database & search for recipes that they've added. The application was created to apply concepts we learned this week which include implementing user authentication with Identity, the basics of code-first migration with EF Core & MySQL, establishing relationships within databases using join tables, using ASP.NET Core, and implementing CRUD functionality within an MVC application._  

---  

## 📘 User Stories

<details>
  <summary>Expand</summary>
  This project was created to respond to the following prompt:
  
  _Build an app that allows users to keep track of recipes._
  <table>
  <tr>
    <th>Scenario 1</th>
    <th></th>
  </tr>
  <tr>
    <td>Behavior</td>
    <td>As a user, I want to add a recipe with ingredients and instructions, so I remember how to prepare my favorite dishes.</td>
  </tr>
  <tr>
    <td>Completion</td>
    <td>✅</td>
  </tr>
  </table>
  <table>
    <tr>
      <th>Scenario 2</th>
      <th></th>
    </tr>
    <tr>
      <td>Behavior</td>
      <td>As a user, I want to tag my recipes with different categories, so recipes are easier to find. A recipe can have many tags and a tag can have many recipe</td>
    </tr>
    <tr>
      <td>Completion</td>
      <td>✅</td>
    </tr>
  </table>
  <table>
    <tr>
      <th>Scenario 3</th>
      <th></th>
    </tr>
    <tr>
      <td>Behavior</td>
      <td>As a user, I want to be able to update and delete tags, so I can have flexibility with how I categorize recipes.</td>
    </tr>
    <tr>
      <td>Completion</td>
      <td>✅</td>
    </tr>
  </table>
  <table>
    <tr>
      <th>Scenario 4</th>
      <th></th>
    </tr>
    <tr>
      <td>Behavior</td>
      <td>As a user, I want to edit my recipes, so I can make improvements or corrections to my recipes.</td>
    </tr>
    <tr>
      <td>Completion</td>
      <td>✅</td>
    </tr>
  </table>
  <table>
    <tr>
      <th>Scenario 5</th>
      <th></th>
    </tr>
    <tr>
      <td>Behavior</td>
      <td>As a user, I want to be able to delete recipes I don't like or use, so I don't have to see them as choices.</td>
    </tr>
    <tr>
      <td>Completion</td>
      <td>✅</td>
    </tr>
  </table>
  <table>
    <tr>
      <th>Scenario 6</th>
      <th></th>
    </tr>
    <tr>
      <td>Behavior</td>
      <td>As a user, I want to rate my recipes, so I know which ones are the best.</td>
    </tr>
    <tr>
      <td>Completion</td>
      <td>✅</td>
    </tr>
  </table>
    <table>
    <tr>
      <th>Scenario 7</th>
      <th></th>
    </tr>
    <tr>
      <td>Behavior</td>
      <td>As a user, I want to list my recipes by highest rated so I can see which ones I like the best.</td>
    </tr>
    <tr>
      <td>Completion</td>
      <td>✅</td>
    </tr>
  </table>
    <table>
    <tr>
      <th>Scenario 8</th>
      <th></th>
    </tr>
    <tr>
      <td>Behavior</td>
      <td>As a user, I want to see all recipes that use a certain ingredient, so I can more easily find recipes for the ingredients I have.
</td>
    </tr>
    <tr>
      <td>Completion</td>
      <td>✅</td>
    </tr>
  </table>
</details>

---  

## 🔧 Setup/Installation Instructions

Running this project locally requires:
- A code editor such as [VisualStudio Code](https://code.visualstudio.com/) 
- [ASP.NET Core](https://dotnet.microsoft.com/download/dotnet-core/2.2)
- [MySQL/MySQL Workbench](https://www.mysql.com/).
- A loose familiarity with MySQL databases & navigating through local files using your command line program such as Terminal or Gitbash (e.g., "cd Desktop").

Please ensure all of the aforementioned softwares are installed on your device or refer to the previous links to begin installation. If you have questions on the installation process, please don't hesitate to reach out using the contact information below.

### 1. Clone or Download the project:

#### To Clone:
- Open your preferred command line program.
- Navigate to the location or directory you'd like the project directory to be created in. (e.g., "cd Desktop" if you'd like to clone the project to your desktop)
- Enter the command "$ git clone https://github.com/zahnen/RecipeBox.Solution" in your command line.

#### To Download:
- Navigate to the [project repository](https://github.com/zahnen/RecipeBox.Solution) in your browser.
- Click the green "Code" button toward the top right of the page.
- Click "Download ZIP" and extract the files.
- Open the newly-downloaded project in your preferred code editor.


### 2. Establish a MySQL database for using the project:

#### Code-First migration with EF Core (Preferred):

- Create a file at the root level of the directory named "appsettings.json" _Note: This file is included in this repository for the purposes of submitting the project as a graded assignment. If you elect to move forward with code-first migration with EF Core, you may simply replace the contents of this file rather than create a duplicate "appsettings.json" file._
- Add the following code into the appsettings.json file

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=[ENTERDATABASENAMEHERE];uid=[ENTERUSERIDHERE];pwd=[ENTERPASSWORDHERE]];"
    }
}

```
- Replace the [ENTERDATABASENAMEHERE], [ENTERUSERIDHERE], and [ENTERPASSWORDHERE] placeholders with your desired database name + your user information associated with your local MySQL server.
- Navigate to the directory titled "RecipeBox" within the root directory created when cloning the project.
- Run the command "dotnet ef database update" to establish the database in MySQL using the migrations included with this repository (suggested).
- If you choose to modify the database by altering the model logic of this project, save all changes and then run the command "dotnet ef migrations add [ENTERMIGRATIONNAMEHERE]" within the "RecipeBox" directory to create a new migation using EF Core. Replace [ENTERMIGRATIONNAMEHERE] with whatever you'd like to name this migration. Follow this by running the command "dotnet ef database update" to update the MySQL database with your changes. You will need to create a new migration _and_ update the MySQL database each time you modify elements of the existing database within the code.
- Verify that your newly establish database exists within your MySQL Workbench before proceeding (you may need to right click the explorer and click "Refresh All" to see the new database).

#### Importing the database using the included .sql file:

- Open MySQL Workbench and navigate to the server on which you'd like to host the database.
- In the workbench's navigation panel, click "Server" and then click "Data Import".
- Select the option to "Import from Self-Contained File" and select the file titled "recipebox.sql" from within the project directory using the file navigator.
- To run the project successfully, I recommend that all schema objects be imported when prompted and that you create a new schema when importing (click "New" next to the "Default Target Schema" input and name the schema how you see fit).
- Ensure that you're proceeding with the "Dumpstructure and Data" option towards the bottom of the import screen.
- Click "Start Import"
- Verify that your newly establish database exists within your MySQL Workbench before proceeding (you may need to right click the explorer and click "Refresh All" to see the new database).


### 3. Run the project:

- Once the project is cloned and the MySQL database is established, use your preferred command line program to navigate to the directory titled "RecipeBox" within the root directory created when cloning the project.
- To run the console application, enter "dotnet build" in your command line while still within the RecipeBox directory. Follow this command with "dotnet run"
- Your command line will open a server (likely "http://localhost:5000/"). Navigate to this URL in your browser to view the project.

---  

## 📊 SQL Schema

# ADD DIAGRAM

---  

## ❗ Known Bugs/Issues

There are no known bugs or issues at this time. If you come across any, please let me know by contacting us at any of the below addresses.

---  

## ❓ Support and Contact Details

- Zahnen Garner // zahnen@gmail.com
- Taylor Delph // taylulzcode@gmail.com
- Max Brockbank // maxbrockbank1999@gmail.com

---  

## 💻 Technologies Used

_This application required use of the following programs/languages/libraries to create:_
- _[Microsoft Visual Studio Code](https://code.visualstudio.com/)_
- _[Git/GitHub](https://github.com/)_
- _[C#](https://docs.microsoft.com/en-us/dotnet/csharp/)_
- _[.NET Core v 2.2](https://dotnet.microsoft.com/download)_
- _[ASP.NET MVC](https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/getting-started)_
- _[ASP.NET Razor](https://docs.microsoft.com/en-us/aspnet/web-pages/overview/getting-started/introducing-razor-syntax-c)_
- _[EF Core](https://docs.microsoft.com/en-us/ef/core/)_
- _[MySQL/My SQL Workbench](https://www.mysql.com/)_
- _[MySQL Designer](https://ondras.zarovi.cz/sql/demo/)_
- _[Bootstrap](https://getbootstrap.com/)_
- _[CSS](https://developer.mozilla.org/en-US/docs/Learn/CSS)_
- _[Identity](https://docs.microsoft.com/en-us/aspnet/identity/overview/getting-started/introduction-to-aspnet-identity)_

---  

## 📃  License

*Licensed under MIT*

Copyright (c) 2020 Zahnen Garner, Taylor Delph, Max Brockbank