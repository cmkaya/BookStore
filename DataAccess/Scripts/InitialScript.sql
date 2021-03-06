DELETE FROM Authors
--DELETE FROM BookGenres
--DELETE FROM Genres
DELETE FROM Books
DELETE FROM Users
DELETE FROM UserDetails
DELETE FROM Cities
DELETE FROM Countries
DELETE FROM Roles

dbcc CHECKIDENT ('Authors', RESEED, 0)
--dbcc CHECKIDENT ('BookGenres', RESEED, 0)
--dbcc CHECKIDENT ('Genres', RESEED, 0)
dbcc CHECKIDENT ('Books', RESEED, 0)
dbcc CHECKIDENT ('Users', RESEED, 0)
dbcc CHECKIDENT ('Cities', RESEED, 0)
dbcc CHECKIDENT ('Countries', RESEED, 0)
dbcc CHECKIDENT ('Roles', RESEED, 0)
dbcc CHECKIDENT ('UserDetails', RESEED, 0)


SET IDENTITY_INSERT [dbo].[Authors] ON

INSERT [dbo].[Authors] ([Id], [FirstName], [LastName], [Birthdate], [Bio], [Guid]) VALUES (1, N'James', N'Clear', NULL, N'James Clear is an American author, entrepreneur, and photographer whose work on habits and human potential focuses on how we can live better. He writes about habits and human potential and the art and science of how to live better. James believes the best way to change the world is in concentric circles: starting with yourself and working your wayout from there. His newest book, Atomic Habits, was released in October 2018.', NULL)
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName], [Birthdate], [Bio], [Guid]) VALUES (2, N'Stephen', N'Covey', N'1932-10-24', N'Stephen Richards Covey was the author of the best-selling book, "The Seven Habits of Highly Effective People". Other books he wrote include "First Things First", "Principle-Centered Leadership", and "The Seven Habits of Highly Effective Families". In 2004, Covey released "The 8th Habit". In 2008, Covey released "The Leader In Me—How Schools and Parents Around the World Are Inspiring Greatness, One Child at a Time". He was also a professor at the Jon M. Huntsman School of Business at Utah State University. Covey died at the Eastern Idaho Regional Medical Center in Idaho Falls, Idaho, on July 16, 2012, due to complications from a bicycle accident he suffered the previous April.', NULL)
INSERT [dbo].[Authors] ([Id], [FirstName], [LastName], [Birthdate], [Bio], [Guid]) VALUES (3, N'Barack', N'Obama', N'1961-08-04', N'Barack Obama was the 44th president of the United States, elected in November 2008 and holding office for two terms. He is the author of two previous New York Times bestselling books, Dreams from My Father and The Audacity of Hope, and the recipient of the 2009 Nobel Peace Prize. He lives in Washington, D.C., with his wife, Michelle. They have two daughters, Malia and Sasha.', NULL)
SET IDENTITY_INSERT [dbo].[Authors] OFF
--GO
--SET IDENTITY_INSERT [dbo].[BookGenres] ON 

--INSERT [dbo].[BookGenres] ([Id], [BookId], [GenreId], [Guid]) VALUES (1, 2, 2, NULL)
--INSERT [dbo].[BookGenres] ([Id], [BookId], [GenreId], [Guid]) VALUES (2, 1, 1, NULL)
--INSERT [dbo].[BookGenres] ([Id], [BookId], [GenreId], [Guid]) VALUES (3, 1, 6, NULL)
--SET IDENTITY_INSERT [dbo].[BookGenres] OFF
    GO
SET IDENTITY_INSERT [dbo].[Books] ON
INSERT [dbo].[Books] ([Id], [Name], [Description], [Publisher], [Page], [PublicationDate], [UnitPrice], [StockAmount], [AuthorId], [Guid]) VALUES (1, N'A Promised Land', N'The memoir, remaining focused on Obama''s political life, begins with his early life, details his first campaigns, and stretches through most of his first term as president.[8] The book concludes with the events surrounding the killing of Osama bin Laden in May 2011,[12][13] ending with a meeting between Obama and the Navy SEALs who conducted the raid.[8] While the book remains focused on politics, the first 200 pages of the book, approximately, are devoted to Obama''s life and career up through his time in Chicago.', N'Crown', 768, N'2020-11-17', CAST(17.13 AS Decimal(18, 2)), 100, 3, NULL)
INSERT [dbo].[Books] ([Id], [Name], [Description], [Publisher], [Page], [PublicationDate], [UnitPrice], [StockAmount], [AuthorId], [Guid]) VALUES (2, N'Atomic Habits', N'tomic Habits will reshape the way you think about progress and success, and give you the tools and strategies you need to transform your habits--whether you are a team looking to win a championship, an organization hoping to redefine an industry, or simply an individual who wishes to quit smoking, lose weight, reduce stress, or achieve any other goal.', N'Avery', 319, CAST(N'2018-10-16T00:00:00.0000000' AS DateTime2), CAST(18.39 AS Decimal(18, 2)), 100, 1, NULL)
SET IDENTITY_INSERT [dbo].[Books] OFF
--GO
--SET IDENTITY_INSERT [dbo].[Genres] ON 

--INSERT [dbo].[Genres] ([Id], [Name], [Guid]) VALUES (1, N'Bios & Memoirs', NULL)
--INSERT [dbo].[Genres] ([Id], [Name], [Guid]) VALUES (2, N'Self Development', NULL)
--INSERT [dbo].[Genres] ([Id], [Name], [Guid]) VALUES (3, N'Computers & Technology', NULL)
--INSERT [dbo].[Genres] ([Id], [Name], [Guid]) VALUES (4, N'Business & Careers', NULL)
--INSERT [dbo].[Genres] ([Id], [Name], [Guid]) VALUES (5, N'Science Fiction & Fantasy', NULL)
--INSERT [dbo].[Genres] ([Id], [Name], [Guid]) VALUES (6, N'Politics & Government', NULL)
--SET IDENTITY_INSERT [dbo].[Genres] OFF

    GO
SET IDENTITY_INSERT [dbo].[Countries] ON

INSERT [dbo].[Countries] ([Id], [Name], [Guid]) VALUES (1, N'Turkey', NULL)
INSERT [dbo].[Countries] ([Id], [Name], [Guid]) VALUES (2, N'Germany', NULL)
INSERT [dbo].[Countries] ([Id], [Name], [Guid]) VALUES (3, N'Spain', NULL)
SET IDENTITY_INSERT [dbo].[Countries] OFF

    GO
SET IDENTITY_INSERT [dbo].[Cities] ON

INSERT [dbo].[Cities] ([Id], [CountryId], [Name], [Guid]) VALUES (1, 1, N'Istanbul', NULL)
INSERT [dbo].[Cities] ([Id], [CountryId], [Name], [Guid]) VALUES (2, 1, N'Ankara', NULL)
INSERT [dbo].[Cities] ([Id], [CountryId], [Name], [Guid]) VALUES (3, 1, N'Izmir', NULL)
INSERT [dbo].[Cities] ([Id], [CountryId], [Name], [Guid]) VALUES (4, 2, N'Hannover', NULL)
INSERT [dbo].[Cities] ([Id], [CountryId], [Name], [Guid]) VALUES (5, 2, N'Berlin', NULL)
INSERT [dbo].[Cities] ([Id], [CountryId], [Name], [Guid]) VALUES (6, 3, N'Madrid', NULL)
SET IDENTITY_INSERT [dbo].[Cities] OFF

    GO
SET IDENTITY_INSERT [dbo].[Roles] ON
INSERT [dbo].[Roles] ([Id], [Name], [Guid]) VALUES (1, N'User', NULL)
INSERT [dbo].[Roles] ([Id], [Name], [Guid]) VALUES (2, N'Admin', NULL)
SET IDENTITY_INSERT [dbo].[Roles] OFF

    GO
SET IDENTITY_INSERT [dbo].[UserDetails] ON
INSERT [dbo].[UserDetails] ([Id], [CountryId], [CityId], [EMail], [Address], [Guid]) VALUES (1, 1, 2, N'cem@hotmail.com', N'Yenimahalle', NULL)
INSERT [dbo].[UserDetails] ([Id], [CountryId], [CityId], [EMail], [Address], [Guid]) VALUES (2, 2, 4, N'alex@hotmail.com', N'Linden', NULL)
INSERT [dbo].[UserDetails] ([Id], [CountryId], [CityId], [EMail], [Address], [Guid]) VALUES (3, 3, 6, N'kaya@hotmail.com', N'Tiegarten', NULL)
INSERT [dbo].[UserDetails] ([Id], [CountryId], [CityId], [EMail], [Address], [Guid]) VALUES (4, 1, 1, N'ugur@hotmail.com', N'Salamanca', NULL)
SET IDENTITY_INSERT [dbo].[UserDetails] OFF

    GO
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([Id], [RoleId], [UserDetailId], [UserName], [Password], [IsActive], [Guid]) VALUES (1, 1, 1, N'cemcem', N'1234', 1, NULL)
INSERT [dbo].[Users] ([Id], [RoleId], [UserDetailId], [UserName], [Password], [IsActive], [Guid]) VALUES (2, 2, 2, N'alex12', N'1234', 1, NULL)
INSERT [dbo].[Users] ([Id], [RoleId], [UserDetailId], [UserName], [Password], [IsActive], [Guid]) VALUES (3, 2, 3, N'kaya12', N'1234', 1, NULL)
INSERT [dbo].[Users] ([Id], [RoleId], [UserDetailId], [UserName], [Password], [IsActive], [Guid]) VALUES (4, 2, 4, N'ugur12', N'1234', 1, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF