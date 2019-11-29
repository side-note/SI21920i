/*
*   ISEL-ADEETC-SI2
*   ND 2014-2019
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*/

/*
-- Se usarem o vosso servidor local, corram também este código em comentário
USE master
GO
IF NOT EXISTS (
	SELECT name
		FROM sys.databases
		WHERE name = N'AP3'
)
CREATE DATABASE AP3
GO
USE AP3;
*/

SET XACT_ABORT ON;
SET TRAN ISOLATION LEVEL serializable
BEGIN TRAN
IF OBJECT_ID('dbo.StudentCourse') IS NOT NULL
	DROP TABLE dbo.StudentCourse;
IF OBJECT_ID('dbo.Student') IS NOT NULL
	DROP TABLE dbo.Student;
IF OBJECT_ID('dbo.Course') IS NOT NULL
	DROP TABLE dbo.Course;
IF OBJECT_ID('CanEnrolStudent') IS NOT NULL
	DROP PROC CanEnrolStudent;
IF EXISTS (SELECT * FROM sys.syslogins WHERE name = N'User1') 
	DROP LOGIN User1
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'User1')
	DROP USER User1
IF EXISTS (SELECT * FROM sys.syslogins WHERE name = N'User2') 
	DROP LOGIN User2
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'User2')
	DROP USER User2
CREATE TABLE dbo.Student
(
	studentId INT IDENTITY(1,1) PRIMARY KEY,
	name NVARCHAR(256) NOT NULL UNIQUE,
	dateBirth DATE,
	sex CHAR NOT NULL
);

CREATE TABLE dbo.Course
(
	courseId INT IDENTITY(1,1) PRIMARY KEY,
	name NVARCHAR(256) NOT NULL	
);

CREATE TABLE dbo.StudentCourse
(
	studentId INT REFERENCES  dbo.Student,
	courseId INT REFERENCES dbo.Course,
	PRIMARY KEY(studentId,courseId)
);

/*
-- Se usarem o vosso servidor local, corram também este código em comentário
CREATE LOGIN User1 
    WITH PASSWORD = 'user1pwd', CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF;
CREATE USER User1 FOR LOGIN User1 
    WITH DEFAULT_SCHEMA = dbo;
GO
EXEC sp_addrolemember 'db_owner', 'User1';

CREATE LOGIN User2
    WITH PASSWORD = 'user2pwd', CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF;
CREATE USER User2 FOR LOGIN User2 
    WITH DEFAULT_SCHEMA = dbo;
GO
EXEC sp_addrolemember 'db_owner', 'User2';
*/
--populate

SET DATEFORMAT dmy;
INSERT INTO dbo.Student(name,dateBirth,sex) VALUES ('John','21-01-1970','M'),('Joe','12-07-1971','M'),('Mary','4-05-1969','F'), ('Bob','12-12-1970','M'), ('Zoe','12-12-1978','F');
INSERT INTO dbo.Course(name) VALUES ('Information systems II'), ('Internet Programming'),('Concurrent programming');
INSERT INTO dbo.StudentCourse VALUES(1,1),(1,2),(1,3),(2,2),(2,3),(3,1),(3,3)	
GO
--procs
CREATE PROCEDURE dbo.CanEnrolStudent
        @StudentId INT = NULL,@res BIT OUTPUT
    AS
    BEGIN
   	--SET @res=ROUND(RAND(),0)
    SET @res = ROUND(@StudentId % 2,0)
	END
GO
COMMIT
