
#### What's included

```
Be-Template#v1.0.0
├── BeTemplate/         #Solution
│   ├── SolutionItems/  #Items like Nuget.Config, .gitignore
│   ├── API/            #API
│   ├── CustomLibrary/  #CustomLibrary
│   ├── Data/           #Data
│   ├── Repo/           #Repo
│   └── Service/        #Service
└── 
```

#### Starting

This project is created with layered architecture. The layer order is `Data` (includes `CustomLibrary`) -> `Repo` -> `Service` -> `API`

When starting a new project, the name of the solution `BeTemlate` should be changed in the all solution as wished.

#### API
 
API module includes controllers, view models, database connections (`appsettings.json`) and configurations (`Startup.cs`)

Database connections should be changed accordingly in `appsettings.json`. `Startup.cs` file can be used as it is.

#### CustomLibrary

Custom classes can be created in this module. Some extension controlling functions and similar classes added here as an example.

#### Data

Data modeling is made in this module. Simply tables that need to be created in the database are designed as classes.

#### Repo

This module contains base repository implementations and migration files. MainContext will be used for the project. When creating new tables, classes should be added to the `MainContext`.

#### Service

Interface and service classes are implemented here. All the services which are related to data models will be written in `IMainService` and `MainService`. 
