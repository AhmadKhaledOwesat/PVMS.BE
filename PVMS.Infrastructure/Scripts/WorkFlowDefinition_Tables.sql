-- Workflow definition (dynamic designer). Run against PVMS database if not using EF migrations.

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'WorkFlowDefinition')
BEGIN
    CREATE TABLE [dbo].[WorkFlowDefinition] (
        [Id] uniqueidentifier NOT NULL PRIMARY KEY,
        [NameAr] nvarchar(max) NOT NULL,
        [NameOt] nvarchar(max) NOT NULL,
        [Active] int NOT NULL,
        [CreatedBy] uniqueidentifier NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [ModifiedBy] uniqueidentifier NULL,
        [ModifiedDate] datetime2 NULL
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'WorkFlowStep')
BEGIN
    CREATE TABLE [dbo].[WorkFlowStep] (
        [Id] uniqueidentifier NOT NULL PRIMARY KEY,
        [WorkFlowDefinitionId] uniqueidentifier NOT NULL,
        [StepOrder] int NOT NULL,
        [NameAr] nvarchar(max) NOT NULL,
        [NameOt] nvarchar(max) NOT NULL,
        [RequireNote] bit NOT NULL DEFAULT(0),
        [NotePrompt] nvarchar(max) NOT NULL DEFAULT(N''),
        [CreatedBy] uniqueidentifier NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [ModifiedBy] uniqueidentifier NULL,
        [ModifiedDate] datetime2 NULL,
        CONSTRAINT [FK_WorkFlowStep_WorkFlowDefinition] FOREIGN KEY ([WorkFlowDefinitionId]) REFERENCES [dbo].[WorkFlowDefinition]([Id]) ON DELETE CASCADE
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'WorkFlowDefinitionTicketType')
BEGIN
    CREATE TABLE [dbo].[WorkFlowDefinitionTicketType] (
        [Id] uniqueidentifier NOT NULL PRIMARY KEY,
        [WorkFlowDefinitionId] uniqueidentifier NOT NULL,
        [TicketTypeId] uniqueidentifier NOT NULL,
        CONSTRAINT [FK_WorkFlowDefinitionTicketType_Definition] FOREIGN KEY ([WorkFlowDefinitionId]) REFERENCES [dbo].[WorkFlowDefinition]([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_WorkFlowDefinitionTicketType_TicketType] FOREIGN KEY ([TicketTypeId]) REFERENCES [dbo].[TicketType]([Id])
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'WorkFlowStepApproveRole')
BEGIN
    CREATE TABLE [dbo].[WorkFlowStepApproveRole] (
        [Id] uniqueidentifier NOT NULL PRIMARY KEY,
        [WorkFlowStepId] uniqueidentifier NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        CONSTRAINT [FK_WorkFlowStepApproveRole_Step] FOREIGN KEY ([WorkFlowStepId]) REFERENCES [dbo].[WorkFlowStep]([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_WorkFlowStepApproveRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles]([Id])
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'WorkFlowStepSkipRole')
BEGIN
    CREATE TABLE [dbo].[WorkFlowStepSkipRole] (
        [Id] uniqueidentifier NOT NULL PRIMARY KEY,
        [WorkFlowStepId] uniqueidentifier NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        CONSTRAINT [FK_WorkFlowStepSkipRole_Step] FOREIGN KEY ([WorkFlowStepId]) REFERENCES [dbo].[WorkFlowStep]([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_WorkFlowStepSkipRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles]([Id])
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'WorkFlowStepRejectRole')
BEGIN
    CREATE TABLE [dbo].[WorkFlowStepRejectRole] (
        [Id] uniqueidentifier NOT NULL PRIMARY KEY,
        [WorkFlowStepId] uniqueidentifier NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        CONSTRAINT [FK_WorkFlowStepRejectRole_Step] FOREIGN KEY ([WorkFlowStepId]) REFERENCES [dbo].[WorkFlowStep]([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_WorkFlowStepRejectRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles]([Id])
    );
END
GO
