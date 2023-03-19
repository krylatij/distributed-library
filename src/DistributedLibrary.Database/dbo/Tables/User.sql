CREATE TABLE [dbo].[User] (
    [UserId]  UNIQUEIDENTIFIER NOT NULL,
    [Name]    NCHAR (100)      NOT NULL,
    [Email]   NCHAR (100)      NOT NULL,
    [Phone]   NCHAR (100)      NULL,
    [City]    NCHAR (100)      NULL,
    [Address] NCHAR (100)      NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

