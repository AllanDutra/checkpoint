## 🪪 Checkpoint

<p style="text-align: justify;">The project developed is a kind of digital point, made for company employees, in which the employee registers his point of arrival or departure and the record is available for other employees to see who is or is not in the company at a given time.</p>

<p style="text-align: justify;">The system works as follows, first it is necessary for the employee to register from the "authentication/register-employee" route, after which he is already able to obtain his access token from the "authentication/authenticate-employee" route, when authenticated, the employee will be able to access the other routes of the application, however the routes "checkpoint/clock-in", used to record the arrival or departure point, and the route "employee/get-info-from-other-employees", used to obtain information on the status of other employees, can only be accessed when the user confirms their e-mail used in the registration, to complete this act, the employee must generate a confirmation code in the route "employee/generate-email-confirmation- code", once this is done, he will receive a 6-digit confirmation code in his email box, which must be informed in the "employee/confirm-email" route, and, after the confirmation is completed, the employee will be able to access all other features of the system.</p>

<p style="text-align: justify;">
Note: New registered employees have up to 7 days to confirm their e-mails, otherwise the registration and all data linked to it will be deleted from the database.
</p>

The following technologies/patterns were used in this project:

- C#;
- .NET 7.0;
- Entity Framework Core;
- Dapper;
- SQL Server;
- JWT Authentication;
- Fluent Validation;
- CQRS;
- Repository Pattern;
- Notification Pattern;
- Domain Services;

<br>

<img src="https://ik.imagekit.io/ghmg33v8b/projects/checkpoint/checkpoint-api_elkXaDUob.png?updatedAt=1693187887828" alt="checkpoint-api" />

## 🌐 Status
<p>Finished Project ✅</p>

## 🧰 Prerequisites

- .NET 7.0 or +

- Connection String to SQL Server DB in **checkpoint/Checkpoint.API/appsettings.json** named as ConnectionStrings.CheckpointDb

- Secret key to be symmetric key of JWT encryption in **checkpoint/Checkpoint.API/appsettings.json** named as Jwt.Key

- SMTP Network Credentials in **checkpoint/Checkpoint.API/appsettings.json** named SMTPNetworkCredential.UserName and SMTPNetworkCredential.Password

## <img src="https://icon-library.com/images/database-icon-png/database-icon-png-13.jpg" width="20" /> Database

_First, Run the following command in the SQL Server query tool:_

```
CREATE DATABASE [Checkpoint];
```

_After that, run the next script still in the SQL Server query tool:_

```
USE [Checkpoint]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [EmailVerifications](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[employee_email] [nvarchar](255) NOT NULL,
	[verificationCode] [nvarchar](255) NOT NULL,
	[generationDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[employee_email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Employees](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[user] [nvarchar](255) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[verified_email] [bit] NULL,
	[registration_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PointLogs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[empolyee_id] [int] NULL,
	[date] [datetime] NOT NULL,
	[type] [char](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [EmailVerifications]  WITH CHECK ADD FOREIGN KEY([employee_email])
REFERENCES [Employees] ([email])
GO
ALTER TABLE [PointLogs]  WITH CHECK ADD FOREIGN KEY([empolyee_id])
REFERENCES [Employees] ([id])
GO
```

## 🔧 Installation
`$ git clone https://github.com/AllanDutra/checkpoint.git`

`$ cd checkpoint/Checkpoint.API`

`$ dotnet restore`

`$ dotnet run`

**Server listening at  [http://localhost:5280/](http://localhost:5280/)!**

## 🔨 Tools used

<div>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" width="80" /> 
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" width="80" />
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-plain-wordmark.svg" width="80" />
</div>
