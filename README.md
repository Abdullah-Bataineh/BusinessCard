<div align="center">
    <img src="logo.png" alt="Logo" width="500"/>
</div>

# Business Card
## 💡Project Description
This project is an innovative website that aims to facilitate the creation of business cards, allowing users to register their personal information easily and smoothly. The site provides an attractive and simple user interface that enables the entry of details such as name, gender, date of birth, email, address and an optional personal photo in an efficient manner. In addition, you can enter user information by uploading a csv, xml or QrCode file, or you can also scan a QR code. You can also view all the cards you have created, delete, edit or export them.
## 🗒️Key Features
- **Information Registration**: Allows users to enter their personal details through a customized and user-friendly form.
- **Information Importing**: The application provides the capability to import information from **CSV** or **XML** files and **QrCode image**, or by scanning QR codes directly using the camera, significantly simplifying the registration process.
- **Record Inquiry and Filtering**: Enables users to view all created business cards, with options to filter records by name, date of birth, gender, email, phone number, or address.
- **Record Management**: The application offers flexible options for deleting, editing, or exporting records to **CSV**, **XML**, or **QR Code** files.

## 📑 Mockup
To visualize the user interface and workflow, refer to the [Mockup PDF](https://drive.google.com/file/d/1lKhFCrDnEjpMGq_9E_sdPR1itFl_DD0a/view?usp=drive_link) file for a detailed view of the application screens and features.

## 📹 How to Use
For a complete tutorial on using the application, check out the [Usage Video](https://drive.google.com/file/d/1FvDuZz7Q2610QR6lAFbRzuwoamv6H636/view?usp=drive_link) which guides you through each feature step-by-step.
## 💻 System Requirements(For Development)

| Icon                                                                                             | Software                          | Version              | Description                                                                                                                    |
|--------------------------------------------------------------------------------------------------|-----------------------------------|----------------------|-------------------------------------------------------------------------------------------------------------------------------|
| ![SQL Server](https://img.icons8.com/?size=50&id=laYYF3dV0Iew&format=png&color=000000)                                             | **SQL Server**                    | 2022                 | Required to create and manage the server database.                                                                           |
| ![SQL Server](https://img.utdstc.com/icon/981/2d8/9812d89705787310adf08f0edf758921b8d551e8329c8d8c5eeabf4d06b08378:40)                                            | **SQL Server Management Studio**  | `v20.2`       | To access and manage tables.                                                                |
| ![Visual Studio](https://skillicons.dev/icons?i=visualstudio)                                   | **Visual Studio**                 | 2022                 | Required for creating API services with .NET tools and extensions.                                                           |
| ![VS Code](https://skillicons.dev/icons?i=vscode)                                             | **VS Code**                       | Latest               | Used to create and manage the Angular project, with extensions like **Angular Language Service** for better support.         |

## 🌐 FrameWorks And Dependencies

| Icon                                                                                     | Language/Technology               | Version              | Description                                                    |
|------------------------------------------------------------------------------------------|-----------------------------------|----------------------|----------------------------------------------------------------|
|  ![dotnet](https://skillicons.dev/icons?i=dotnet)   | **.NET**                          | `7.0`                    | Used for backend API development.                              |
| ![angular](https://skillicons.dev/icons?i=angular) | **Angular**                       | `v16.2.16`                   | Frontend framework for building user interfaces.              |
| ![nodejs](https://skillicons.dev/icons?i=nodejs) | **Node.js**                      | `v18.10.0`               | Used for server-side development and managing backend services. |
| ![SQL Server](https://img.icons8.com/?size=50&id=laYYF3dV0Iew&format=png&color=000000)   | **SQL Server**                   | 2022                 | Database management system for storing application data.       |

## 📥 Installation Instructions

### 1. First Time Installation

If this is the first time installing the required software, follow these steps:

1. **Download SQL Server 2022**:
   - Visit the official Microsoft website: [Download SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
   - Scroll Down The Page and Click (Download now) with Developer.
   - Follow the installation prompts to install SQL Server.

2. **Download SQL Server Management Studio (SSMS)**:
   - Go to the [SSMS Download Page](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
   -Scroll Down and Click on the download link (Download SQL Server Management Studio (SSMS) 20.2) and follow the installation instructions.

3. **Download Visual Studio 2022**:
   - Visit the [Visual Studio Download Page](https://visualstudio.microsoft.com/vs/)
   - Choose the Community version (free for individual developers) and install it.
   - when install Chooce in **Workloads** `ASB.NET and web development` and click with **Individual components** chooce `.NET 7.0 RunTime` and `.NET 7.0 WebAssembly` and click install.

4. **Download VS Code**:
   - Visit the [VS Code Download Page](https://code.visualstudio.com/Download)
   - Download and install the latest version for your operating system.

5. **Install Node.js**:
   - Visit the [Node.js Download Direct v18.10.0](https://nodejs.org/dist/v18.10.0/node-v18.10.0-x64.msi)
   - Download the 18.10.0 version and follow the installation instructions.

6. **Install Angular**:
   - After Setup Node.js `v18.10.0` 
   - Open Command Prompt `cmd` with Right click Run as Administrator
   -  Enter command `npm install -g @angular/cli@16.2.16` and enter
   - complete download steps with chooce routing and completing

### 2. If Software is Already Installed

If you already have the required software installed, ensure they are updated to the latest versions:

- **SSMS**: Create Database and extract the connection string and set the link in the API project.
1. Use the query to get the connection string, taking into account the input so that it includes userid, password if you need them to log in to the server.
2. The script checks if a `User ID` and `Password` are provided:
- If **both** are provided, it uses **SQL Server Authentication**.
- If **neither** are provided, it defaults to **Windows Authentication** with `Trusted_Connection=True`.

```sql
DECLARE @ServerName NVARCHAR(128) = @@SERVERNAME;
DECLARE @DatabaseName NVARCHAR(128) = DB_NAME();
DECLARE @UserID NVARCHAR(128) = NULL;  -- Set to your SQL Server User ID if needed, otherwise leave NULL
DECLARE @Password NVARCHAR(128) = NULL;  -- Set to your SQL Server Password if needed, otherwise leave NULL
DECLARE @ConnectionString NVARCHAR(4000);

IF @UserID IS NOT NULL AND @Password IS NOT NULL
BEGIN
    -- Use SQL Server Authentication with User ID and Password
    SET @ConnectionString = 'Server=' + @ServerName + ';Database=' + @DatabaseName + 
                            ';User ID=' + @UserID + ';Password=' + @Password + ';';
END
ELSE
BEGIN
    -- Use Windows Authentication (Trusted Connection)
    SET @ConnectionString = 'Server=' + @ServerName + ';Database=' + @DatabaseName + ';Trusted_Connection=True;';
END

SELECT @ConnectionString AS ConnectionString;
```
- **Visual Studio**: 
1. Open Visual Studio.
2. Go to **File** > **Open** > **Folder...** and select the project folder, or right-click the folder and choose **Open in Visual Studio**.

   ##### Setting the Connection String

3. Once the project is open, locate the `appsettings.json` file in the project root.
4. Open `appsettings.json` and set the `ConnectionStrings` property by adding your database connection string. For example:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Your_Connection_String_Here"
       //delete this and set the connection string with extract mssql
     }
   }
   ```
   ##### Add Migrations Using Entity Framework
5. Click with Tools in tool bar in visual studio and click with  **Nuget Package Manager** and click **Package Manager Console** with open command 
write the command this 
   - ``dotnet ef migrations add InitialCreat --project "BusinessCardAPI\BusinessCardApi.csproj"``.
   - with finish Migration write this command
   ``dotnet ef Migrations add IntialCreate --project "BusinessCardApi\BusinessCardApi.csproj"``.
   - and ``dotnet ef database update --project "BusinessCardApi\BusinessCardApi.csproj"`` this create table in database and cretate Migrations File in project.

6. in the end Run This Projects click **ctrl+F5** or **Run Icon**

- **VS Code**: 
1. Open VS Code and go to File Project BusinessCardWeb.
2. open terminal and check inside project
3. add command ``npm i`` with install all packages
4. add command ``ng s -o`` with run project. 



### 🚀 Enjoy Using the Business Card Application!

---

## 📞 Contact

For any questions or support, please feel free to reach out:

- **Email**: [abdtawil25@gmail.com)](mailto:abdtawil25@gmail.com)
- **Phone**: +962795072791
- **LinkedIn**: [linkedin](https://linkedin.com/in/abdullabataineh/)
- **GitHub**: [github](https://github.com/Abdullah-Bataineh)

We look forward to assisting you!
