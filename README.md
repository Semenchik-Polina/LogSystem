# Log system

Log system is a prototype of log system for a website and includes:
  - LogIn, SignUp forms
  - Form for editing user's profile
  - Representation of all user's actions with filtering and sorting.
  - Daily reports about user's actions sent to admins by email 

### Tech
* Three layer architecture
* ASP.Net MVC - WebApi
* Angular
* Bootstrap
* SQLite
* ORM Dapper
* Automapper
* Ninject

### Installation

Open terminal at the project folder and use the following commands: 

```sh
$ cd .\LogSystem.PL\Angular\src\app
$ npm install
$ ng build --extractCss --watch
```

Then in Nuget Package Console:

```sh
PM> Uninstall-package Microsoft.CodeDom.Providers.DotNetCompilerPlatform
PM> Uninstall-package Microsoft.Net.Compilers
```

Eventually press start!
