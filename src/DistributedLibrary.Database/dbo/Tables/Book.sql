﻿CREATE TABLE [dbo].[Book] (
    [BookId]          INT            IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (300) NOT NULL,
    [Author]          NVARCHAR (100) NULL,
    [ISBN]            NVARCHAR (100) NULL,
    [Genres]          NVARCHAR (500) NULL,
    [Tags]            NVARCHAR (500) NULL,
    [Publisher]       NVARCHAR (100) NULL,
    [PublicationDate] DATE           NULL,
    [PageCount]       INT            NULL,
    [ContributorId]   NVARCHAR (450) NULL,
    [HolderId]        NVARCHAR (450) NULL,
    [CreatedAt]       DATETIME       CONSTRAINT [DF_Book_CreatedAt] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]       NVARCHAR (450) NULL,
    [UpdatedAt]       DATETIME       NULL,
    [UpdatedBy]       NVARCHAR (450) NULL,
    CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED ([BookId] ASC),
    CONSTRAINT [FK_Book_AspNetUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Book_AspNetUsers1] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Book_User] FOREIGN KEY ([ContributorId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Book_User1] FOREIGN KEY ([HolderId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);










GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Book]
    ON [dbo].[Book]([ISBN] ASC) WHERE ([ISBN] IS NOT NULL);




GO
CREATE NONCLUSTERED INDEX [IX_Book_HolderId]
    ON [dbo].[Book]([HolderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Book_ContributorId]
    ON [dbo].[Book]([ContributorId] ASC);

