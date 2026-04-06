-- User <-> Workflow Definition mapping table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'UserWorkFlowDefinition')
BEGIN
    CREATE TABLE [dbo].[UserWorkFlowDefinition] (
        [Id] uniqueidentifier NOT NULL PRIMARY KEY,
        [UserId] uniqueidentifier NOT NULL,
        [WorkFlowDefinitionId] uniqueidentifier NOT NULL,
        [CreatedBy] uniqueidentifier NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [ModifiedBy] uniqueidentifier NULL,
        [ModifiedDate] datetime2 NULL,
        CONSTRAINT [FK_UserWorkFlowDefinition_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserWorkFlowDefinition_WorkFlowDefinition] FOREIGN KEY ([WorkFlowDefinitionId]) REFERENCES [dbo].[WorkFlowDefinition]([Id])
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'UX_UserWorkFlowDefinition_UserId_WorkFlowDefinitionId' AND object_id = OBJECT_ID('dbo.UserWorkFlowDefinition'))
BEGIN
    CREATE UNIQUE INDEX [UX_UserWorkFlowDefinition_UserId_WorkFlowDefinitionId]
        ON [dbo].[UserWorkFlowDefinition]([UserId], [WorkFlowDefinitionId]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_UserWorkFlowDefinition_UserId' AND object_id = OBJECT_ID('dbo.UserWorkFlowDefinition'))
BEGIN
    CREATE INDEX [IX_UserWorkFlowDefinition_UserId]
        ON [dbo].[UserWorkFlowDefinition]([UserId]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_UserWorkFlowDefinition_WorkFlowDefinitionId' AND object_id = OBJECT_ID('dbo.UserWorkFlowDefinition'))
BEGIN
    CREATE INDEX [IX_UserWorkFlowDefinition_WorkFlowDefinitionId]
        ON [dbo].[UserWorkFlowDefinition]([WorkFlowDefinitionId]);
END
GO
