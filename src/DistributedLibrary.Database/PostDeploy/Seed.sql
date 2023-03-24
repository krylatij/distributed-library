/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--USER
MERGE dbo.[AspNetUsers] AS Target USING (VALUES
--
('00000000-0000-0000-0000-000000000000', 'System', 'dev@null.com')
) AS Source (Id, UserName, Email)
ON (Target.Id = Source.Id)
WHEN MATCHED
  AND (Target.UserName <> Source.UserName)
  THEN UPDATE
    SET
      UserName = Source.UserName 
WHEN NOT MATCHED BY Target
  THEN INSERT
    (
      Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount
    )
    VALUES
    (Source.Id, Source.UserName, Source.UserName, Source.Email, Source.Email, 1, 0, 0, 0, 0);


--BOOKS
MERGE dbo.[Book] AS Target USING (VALUES
--
('Designing Data-Intensive Applications: The Big Ideas Behind Reliable, Scalable, and Maintainable Systems', 'Martin Kleppmann', '978-1449373320', 'O''Reilly Media', '2017-05-02', 611),
('Expert Data Modeling with Power BI: Get the best out of Power BI by building optimized data models for reporting and business needs', 'Soheil Bakhshi', '978-1800205697', 'Packt Publishing', '2021-06-11', 612),
('Engineers Survival Guide: Advice, tactics, and tricks After a decade of working at Facebook, Snapchat, and Microsoft', 'Merih Taze', '979-8985349900', 'Packt Publishing', '2021-11-28', 245),
('System Design Interview – An insider''s guide', 'Alex Xu', '979-8664653403', 'Alex Xu', '2020-06-12', 320),
('Software Architecture: The Hard Parts: Modern Trade-Off Analyses for Distributed Architectures', 'Neal Ford', '978-1492086895', 'O''Reilly Media', '2021-11-30', 459),
('Software Architect''s Handbook: Become a successful software architect by implementing effective architecture concepts', 'Joseph Ingeno', '978-1788624060', 'Packt Publishing', '2018-08-30', 594),
('Microservices Design Patterns in .NET: Making sense of microservices design and architecture using .NET Core', 'Trevoir Williams', '978-1804610305', 'Packt Publishing', '2023-01-13', 300),
('.NET MAUI Cross-Platform Application Development: Leverage a first-class cross-platform UI framework to build native apps on multiple platforms', 'Roger Ye', '978-1800569225', 'Packt Publishing', '2023-01-27', 400),
('Practical Microservices with Dapr and .NET: A developer''s guide to building cloud-native applications using the event-driven runtime, 2nd Edition', 'Davide Bedin', '978-1803248127', 'Packt Publishing', '2022-11-11', 312),
('Implementing Event-Driven Microservices Architecture in .NET 7: Develop event-based distributed apps that can scale with ever-changing business demands using C# 11 and .NET 7', 'Joshua Garverick', '978-1803232782', 'Packt Publishing', '2023-03-17', 326),
('Microservices Communication in .NET Using gRPC: A practical guide for .NET developers to build efficient communication mechanism for distributed apps', 'Fiodar Sazanavets', '978-1803236438', 'Packt Publishing', '2022-02-11', 486),
('DELETE ME #1', 'John Doe', '000-0000000001', 'John Doe', '2020-06-12', 100),
('DELETE ME #2', 'John Doe', '000-0000000002', 'John Doe', '2020-06-12', 200),
('DELETE ME #3', 'John Doe', '000-0000000003', 'John Doe', '2020-06-12', 300),
('DELETE ME #4', 'John Doe', '000-0000000004', 'John Doe', '2020-06-12', 400),
('DELETE ME #5', 'John Doe', '000-0000000005', 'John Doe', '2020-06-12', 500),
('DELETE ME #6', 'John Doe', '000-0000000006', 'John Doe', '2020-06-12', 600)
) AS Source (Title, Author, ISBN, Publisher, PublicationDate, [PageCount])
ON (Target.ISBN = Source.ISBN)
WHEN MATCHED  
  THEN UPDATE
    SET
      Title = Source.Title,
      Author = Source.Author,
      ISBN = Source.ISBN,
      Publisher = Source.PublicationDate,
      [PageCount] = Source.[PageCount],
      ContributorId = '00000000-0000-0000-0000-000000000000'
WHEN NOT MATCHED BY Target
  THEN INSERT
    (
      Title, Author, ISBN, Publisher, PublicationDate, [PageCount], ContributorId
    )
    VALUES
    (Source.Title, Source.Author, Source.ISBN, Source.Publisher, Source.PublicationDate, Source.[PageCount], '00000000-0000-0000-0000-000000000000');

