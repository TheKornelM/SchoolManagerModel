# SchoolManagerModel

Created by Kornél Makári

## Project Structure
There are 5 namespaces:
- **SchoolManagerModel.Entities**: Objects and entities stored in the database.
- **SchoolManagerModel.Exceptions**: Implemented exceptions.
- **SchoolManagerModel.Managers**: Business logic with persistence through interfaces.
- **SchoolManagerModel.Persistence**: Data storage.

## Model

### Manager Classes
There are 5 manager classes that contain the business logic:

- `ClassManager`
- `LoginManager`
- `SubjectManager`
- `TeacherManager`
- `UserManager`

## Persistence
Entity Framework is used for data storage, but you can easily use any other technology.

### Data Storage Interfaces
Manager classes are not dependent on Entity Framework. There are 4 handler interfaces:
- `IAsyncClassDataHandler`
- `IAsyncSubjectDataHandler`
- `IAsyncTeacherDataHandler`
- `IAsyncUserDataHandler`

If you want to use, for example, Dapper, you need to implement these interfaces.

### Entity Framework

Entity Framework-specific classes use these interfaces.

Entity Framework classes:
- `ClassDatabase`
- `SubjectDatabase`
- `TeacherDatabase`
- `UserDatabase`

## Database
Persistence is implemented with a local SQLite database. If you want to use another database engine, you need to install the engine-specific NuGet package and create a class based on `SchoolDbContextBase`.

`SchoolDbContextBase` is an abstract class, and you need to override two methods:
- `OnConfiguring(DbContextOptionsBuilder optionsBuilder)`: To configure the database with the specific engine.
- `OnModelCreating(ModelBuilder modelBuilder)`: To seed data (e.g., create accounts).
