CREATE TABLE [dbo].[Book] (
    [BookId]          INT              IDENTITY (1, 1) NOT NULL,
    [Title]           NCHAR (100)      NOT NULL,
    [Author]          NCHAR (100)      NULL,
    [ISBN]            NCHAR (100)      NULL,
    [Publisher]       NCHAR (100)      NULL,
    [PublicationDate] DATE             NULL,
    [PageCount]       INT              NULL,
    [ContributorId]   UNIQUEIDENTIFIER NULL,
    [HolderId]        UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED ([BookId] ASC),
    CONSTRAINT [FK_Book_User] FOREIGN KEY ([ContributorId]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [FK_Book_User1] FOREIGN KEY ([HolderId]) REFERENCES [dbo].[User] ([UserId])
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Book]
    ON [dbo].[Book]([ISBN] ASC);

