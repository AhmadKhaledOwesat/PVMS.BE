IF COL_LENGTH('dbo.Ticket', 'WorkFlowDefinitionId') IS NULL
BEGIN
    ALTER TABLE [dbo].[Ticket]
    ADD [WorkFlowDefinitionId] uniqueidentifier NULL;
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = 'FK_Ticket_WorkFlowDefinition'
)
BEGIN
    ALTER TABLE [dbo].[Ticket]
    ADD CONSTRAINT [FK_Ticket_WorkFlowDefinition]
        FOREIGN KEY ([WorkFlowDefinitionId]) REFERENCES [dbo].[WorkFlowDefinition]([Id]);
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Ticket_WorkFlowDefinitionId'
      AND object_id = OBJECT_ID('dbo.Ticket')
)
BEGIN
    CREATE INDEX [IX_Ticket_WorkFlowDefinitionId]
        ON [dbo].[Ticket]([WorkFlowDefinitionId]);
END
GO
