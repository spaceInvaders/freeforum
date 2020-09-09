### Requirements:

MS Visual Studio 2019, MS SQL Server 2019.

### Launch settings:

1. In **Startup.cs** class (FreeForum.Web project) in **ConfigureServices** method configure this string to establish connection with your SQL Server 
   `string conStr = "Server=(localdb)\\mssqllocaldb;Database=freeforum;Trusted_Connection=True;MultipleActiveResultSets=true";`

   Actually, check this part: `Server=(localdb)\\mssqllocaldb`

2. In **FreeForum.Data** project remove **Migrations** folder

3. Build Solution (Ctrl+Shift+B)

4. Open **Package Manager Console** (Ctrl+Q > Package Manager Console)
   In it's setting **Default project** select **FreeForum.Data**

   Type in console:

      `PM> Add-Migration InitialCreate`
   `	 PM> Update-Database`

5. Finally **Run IIS Express** for FreeForum.Web project (F5)

6. After Registration you have to confirm your account via email **on the same device** you run  FreeForum.Web project

### DB diagram:

![](https://github.com/spaceInvaders/freeforum/blob/master/FreeForum.Web/wwwroot/Images/DB%20diagram.jpg)