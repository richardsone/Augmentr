## How to compile and run this project

* Download [Node.js and npm](https://nodejs.org/en/download/)

* Download [dotnet core](https://www.microsoft.com/net/learn/get-started) for the runtime

* Download [Visual Studio code](https://code.visualstudio.com/) (lightweight and multiplatform) or [Visual Studio](https://www.visualstudio.com/downloads/) (heavy duty with more tooling)

* Download [MySQL](https://www.mysql.com/downloads/) and start the server. If possible, set root account with the password admin. If not you will need to modify the user and password field in the connection string in `Augmentr/Startup.cs` lines 29 and 47.

* `cd` to the Augmentr directory and `npm install`

* Hit F5 to build and launch the web app

* The Angular code has hot-loading so you can simply edit the code in ClientApp, save, and the webpage will reload with the changes

* Editing C# code requires a rebuild