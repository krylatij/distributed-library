CREATE TABLE [dbo].[Reservation] (
    [ReservationId]   INT            IDENTITY (1, 1) NOT NULL,
    [BookId]          INT            NOT NULL,
    [UserId]          NVARCHAR (450) NOT NULL,
    [ReservationDate] DATETIME       NOT NULL,
    [CreatedAt]       DATETIME       CONSTRAINT [DF_Reservation_CreatedAt] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]       NVARCHAR (450) NULL,
    [UpdatedAt]       DATETIME       NULL,
    [UpdatedBy]       NVARCHAR (450) NULL,
    CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED ([ReservationId] ASC),
    CONSTRAINT [FK_Reservation_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Reservation_AspNetUsers1] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Reservation_AspNetUsers2] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Reservation_Book] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Book] ([BookId])
);



