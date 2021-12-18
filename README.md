# Table Purger Worker
This repository contains a simple .NET Core worker which its main goal is to purge records from a table depending on how long those records are in the table.

## Install & Run

In case you want to run this application, you can clone this repository by running the bellow command.

``` git clone  https://github.com/Igor03/table-purger-worker.git ```

Although this app was built based on a SQLServer Database, you can easily adapt it to others databases due to its integration with Entity Framework Core. The DDL code for the table used is shown bellow.

```sql
CREATE TABLE PURGEABLE_TABLE(
	ID INT IDENTITY(1, 1),
	NAME VARCHAR(255) NOT NULL,
	CREATION_DATE DATETIME NOT NULL DEFAULT GETDATE(),
	PRIMARY KEY (ID)
);
```

## Important
* This application uses [Visual Studio Secrets Manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows) to store secrets locally. For instance, we are not showing out database connection string. Hence, you'll have to configure it in your own environment.

* Another awesome resource, that I used to build this app, is this [youtube video](https://www.youtube.com/watch?v=PzrTiz_NRKA&ab_channel=IAmTimCorey), produced by Tim Corey.

## Contact

In case you need any help running/understanding this, feel free to contact me and I'll try to help you the best way I can.
