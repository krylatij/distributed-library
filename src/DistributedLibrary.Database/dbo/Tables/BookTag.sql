CREATE TABLE [dbo].[BookTag] (
    [BookTagId] INT         IDENTITY (1, 1) NOT NULL,
    [BookId]    INT         NULL,
    [Name]      NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_BookTag] PRIMARY KEY CLUSTERED ([BookTagId] ASC),
    CONSTRAINT [FK_BookTag_Book] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Book] ([BookId])
);






GO
CREATE NONCLUSTERED INDEX [IX_BookTag_BookId]
    ON [dbo].[BookTag]([BookId] ASC);

