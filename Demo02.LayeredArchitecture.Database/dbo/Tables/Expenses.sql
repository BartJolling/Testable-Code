CREATE TABLE [dbo].[Expenses] (
    [EmployeeId]  INT              NOT NULL,
    [FiscalYear]  INT              NOT NULL,
    [Category]    VARCHAR (25)     NOT NULL,
	[Amount]      NUMERIC (18, 2)  NOT NULL,
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC)
);

