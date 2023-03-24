CREATE TABLE [dbo].[Loan] (
    [LoanId]   INT            IDENTITY (1, 1) NOT NULL,
    [BookId]   INT            NOT NULL,
    [UserId]   NVARCHAR (450) NOT NULL,
    [DateFrom] DATE      NOT NULL,
    [DateTo]   DATE      NULL,
    CONSTRAINT [PK_Loan] PRIMARY KEY CLUSTERED ([LoanId] ASC),
    CONSTRAINT [FK_Loan_Book] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Book] ([BookId]),
    CONSTRAINT [FK_Loan_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);






GO
CREATE NONCLUSTERED INDEX [IX_Loan_UserId]
    ON [dbo].[Loan]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Loan_BookId]
    ON [dbo].[Loan]([BookId] ASC);

