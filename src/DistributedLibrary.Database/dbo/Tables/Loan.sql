CREATE TABLE [dbo].[Loan] (
    [LoanId]   INT              IDENTITY (1, 1) NOT NULL,
    [BookId]   INT              NOT NULL,
    [UserId]   UNIQUEIDENTIFIER NOT NULL,
    [DateFrom] DATE             NOT NULL,
    [DateTo]   NCHAR (10)       NULL,
    CONSTRAINT [PK_Loan] PRIMARY KEY CLUSTERED ([LoanId] ASC),
    CONSTRAINT [FK_Loan_Book] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Book] ([BookId]),
    CONSTRAINT [FK_Loan_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);



