/**************************************************************/
/* Copyright (c) Microsoft.  All rights reserved.             */
/**************************************************************/
-- !!! This assumes the database is created and the user is either a dbo or is added to the RSExecRole
-- !!! Please run setup to create the database, users, role !!!

SET ANSI_NULLS ON
GO

SET ANSI_PADDING ON
GO

SET ANSI_WARNINGS ON
GO

SET ARITHABORT ON
GO

SET CONCAT_NULL_YIELDS_NULL ON
GO

SET NUMERIC_ROUNDABORT OFF
GO

SET QUOTED_IDENTIFIER ON
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetDBVersion]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetDBVersion]
GO

CREATE PROCEDURE [dbo].[GetDBVersion]
@DBVersion nvarchar(32) OUTPUT
AS
set @DBVersion = 'T.0.9.45'
GO

GRANT EXECUTE ON [dbo].[GetDBVersion] TO RSExecRole
GO

-- drop foreign keys

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_SnapshotChunkMappingSnapshotDataId]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[SegmentedChunk] DROP CONSTRAINT [FK_SnapshotChunkMappingSnapshotDataId]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ChunkSegmentMappingChunkId]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ChunkSegmentMapping] DROP CONSTRAINT [FK_ChunkSegmentMappingChunkId]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ChunkSegmentMappingSegmentId]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ChunkSegmentMapping] DROP CONSTRAINT [FK_ChunkSegmentMappingSegmentId]
GO

-------------------------------------------------------

exec sp_addrolemember 'db_owner', 'RSExecRole'
GO

-------------------------------------------------------
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SessionLock]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[SessionLock]
GO

CREATE TABLE [dbo].[SessionLock] (
    [SessionID] varchar(32) NOT NULL
)
GO

GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON [dbo].[SessionLock] TO RSExecRole
GO

CREATE UNIQUE CLUSTERED INDEX [IDX_SessionLock] ON [dbo].[SessionLock]([SessionID])
GO

-------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SessionData]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[SessionData]
GO

CREATE TABLE [dbo].[SessionData] (
    [SessionID] varchar(32) NOT NULL,
    [CompiledDefinition] uniqueidentifier NULL, -- when session starts from definition, this holds compiled definition snapshot id
    [SnapshotDataID] uniqueidentifier NULL,
    [IsPermanentSnapshot] bit NULL,
    [ReportPath] nvarchar(424) NULL, -- Report Path is empty string ("") when session starts from definition
    [Timeout] int NOT NULL,
    [AutoRefreshSeconds] int NULL, -- How often data should be refreshed
    [Expiration] datetime NOT NULL,
    [ShowHideInfo] image NULL,
    [DataSourceInfo] image NULL,
    [OwnerID] uniqueidentifier NOT NULL,
    [EffectiveParams] ntext NULL,
    [CreationTime] DateTime NOT NULL,
    [HasInteractivity] bit NULL,
    [SnapshotExpirationDate] datetime NULL, -- when this snapshot expires - cache or exec snapshot
    [HistoryDate] datetime NULL, -- if this is not null, session was started from history
    [PageHeight] float NULL,  -- page properties are only populated for temporary reports.
    [PageWidth] float NULL,
    [TopMargin] float NULL,
    [BottomMargin] float NULL,
    [LeftMargin] float NULL,
    [RightMargin] float NULL,
    [AwaitingFirstExecution] bit NULL	-- when rendering, this flag tells us if we are rendering
										-- within the session snapshot, or if we are rendering
										-- as if the session is new (first execution vs. subsequent execution)
)
GO

IF (SERVERPROPERTY('EngineEdition') <> 8)
BEGIN
    EXEC sp_tableoption N'[dbo].[SessionData]', 'text in row', 'ON'
END
GO

GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON [dbo].[SessionData] TO RSExecRole
GO

CREATE UNIQUE CLUSTERED INDEX [IDX_SessionData] ON [dbo].[SessionData]([SessionID])
GO

CREATE INDEX [IX_SessionCleanup] ON [dbo].[SessionData]([Expiration])
GO

CREATE INDEX [IX_SessionSnapshotID] ON [dbo].[SessionData]([SnapshotDataID])
GO

-------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ExecutionCache]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ExecutionCache]
GO

CREATE TABLE [dbo].[ExecutionCache] (
    [ExecutionCacheID] uniqueidentifier NOT NULL,
    [ReportID] uniqueidentifier NOT NULL,
    [ExpirationFlags] int NOT NULL,
    [AbsoluteExpiration] datetime NULL,
    [RelativeExpiration] int NULL,
    [SnapshotDataID] uniqueidentifier NOT NULL
)
GO

GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON [dbo].[ExecutionCache] TO RSExecRole
GO

ALTER TABLE [dbo].[ExecutionCache] ADD 
    CONSTRAINT [PK_ExecutionCache] PRIMARY KEY NONCLUSTERED
    (
         [ExecutionCacheID]
    )
GO

CREATE UNIQUE CLUSTERED INDEX [IX_ExecutionCache] ON [dbo].[ExecutionCache] ([AbsoluteExpiration] DESC, [ReportID], [SnapshotDataID]) 
GO

CREATE INDEX [IX_SnapshotDataID] ON [dbo].[ExecutionCache] ([SnapshotDataID])
GO

-------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SnapshotData]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[SnapshotData]
GO

CREATE TABLE [dbo].[SnapshotData]  (
    [SnapshotDataID] uniqueidentifier NOT NULL,
    [CreatedDate] datetime NOT NULL,
    [ParamsHash] int NULL, -- Hash of values of parameters that are used in query
    [QueryParams] ntext NULL, -- Values of parameters that are used in query
    [EffectiveParams] ntext NULL, -- Full set of effective parameters
    [Description] nvarchar(512) NULL,
    [DependsOnUser] bit NULL,
    [PermanentRefcount] int NOT NULL, -- this counts only permanent references, NOT SESSIONS!!!
    [TransientRefcount] int NOT NULL, -- this is to count sessions, may be more than expected
    [ExpirationDate] datetime NOT NULL, -- Expired snapshots should be erased regardless of TransiendRefcount
    [PageCount] int NULL,
    [HasDocMap] bit NULL,
    [Machine] nvarchar(512) NOT NULL, 
    [PaginationMode] smallint NULL,	 -- 0/NULL = total, 1 = estimate, 2 = progressive
    [ProcessingFlags] int NULL,
    [IsCached] bit default(0)	-- indicates if the report is cached 
)
GO

IF (SERVERPROPERTY('EngineEdition') <> 8)
BEGIN
    EXEC sp_tableoption N'[dbo].[SnapshotData]', 'text in row', 'ON'
END
GO

GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON [dbo].[SnapshotData] TO RSExecRole
GO

ALTER TABLE [dbo].[SnapshotData] ADD 
    CONSTRAINT [PK_SnapshotData] PRIMARY KEY CLUSTERED
    (
         [SnapshotDataID]
    )
GO

CREATE INDEX [IX_SnapshotCleaning] ON [dbo].[SnapshotData]([PermanentRefcount], [TransientRefcount]) INCLUDE ([Machine])
GO

CREATE INDEX [IS_SnapshotExpiration] ON [dbo].[SnapshotData]([PermanentRefcount], [ExpirationDate])
GO

-------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ChunkData]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ChunkData]
GO

CREATE TABLE [dbo].[ChunkData] (
    [ChunkID]  uniqueidentifier NOT NULL,
    [SnapshotDataID] uniqueidentifier NOT NULL,
    [ChunkFlags] tinyint NULL,
    [ChunkName] nvarchar(260), -- Name of the chunk
    [ChunkType] int, -- internal type of the chunk
    [Version] smallint NULL, -- version of the chunk    
    [MimeType] nvarchar(260), -- mime type of the content of the chunk
    [Content] image -- content of the chunk
)
GO

GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON [dbo].[ChunkData] TO RSExecRole
GO

ALTER TABLE [dbo].[ChunkData] WITH NOCHECK ADD 
    CONSTRAINT [PK_ChunkData] PRIMARY KEY NONCLUSTERED 
    (
        [ChunkID]
    )  ON [PRIMARY] 
GO

CREATE UNIQUE CLUSTERED INDEX [IX_ChunkData] ON [dbo].[ChunkData]([SnapshotDataID], [ChunkType], [ChunkName]) ON [PRIMARY]
GO

-------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PersistedStream]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[PersistedStream]
GO

CREATE TABLE [dbo].[PersistedStream] (
    [SessionID] varchar(32) NOT NULL,
    [Index] int NOT NULL,
    [Content] image NULL,
    [Name] nvarchar(260) NULL,
    [MimeType] nvarchar(260) NULL,
    [Extension] nvarchar(260) NULL,
    [Encoding] nvarchar(260) NULL,
    [Error] nvarchar(512) NULL,
    [RefCount] int NOT NULL,
    [ExpirationDate] datetime NOT NULL
)  ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON [dbo].[PersistedStream] TO RSExecRole
GO

ALTER TABLE [dbo].[PersistedStream] ADD
    CONSTRAINT [PK_PersistedStream] PRIMARY KEY CLUSTERED 
    (
        [SessionID],
        [Index]
    ) ON [PRIMARY]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SnapshotChunkMappingExtendedView]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[SnapshotChunkMappingExtendedView]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SegmentedChunk]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[SegmentedChunk]
GO

create table [dbo].[SegmentedChunk] (
    [SegmentedChunkId]  bigint identity(1,1) not null,
	[ChunkId]			uniqueidentifier default newsequentialid() not null,
	[SnapshotDataId]	uniqueidentifier not null, 
	[ChunkFlags]		tinyint NULL,
	[ChunkName]			nvarchar(260), 
	[ChunkType]			int,
	[Version]			smallint null, 	
	[MimeType]			nvarchar(260),	
	[Machine]			nvarchar(512) not null,
	constraint [PK_SegmentedChunk] primary key clustered (SegmentedChunkId)	
	-- Don't use foreign key constraint here to maintain consistency... we need to manually
	-- clean up this table to ensure that in a farm the machine which allocates the chunk is the 
	-- one that eventually will clean it up, and this is not necessarily the machine which 
	-- allocated the snapshot (consider when a different machine creates a chunk in a pre-existing snapshot) 
	-- constraint [FK_SnapshotChunkMappingSnapshotDataId] foreign key ([SnapshotDataId]) references [SnapshotData]([SnapshotDataID]) on delete cascade
	)
GO

create unique nonclustered index [UNIQ_SnapshotChunkMapping]
	on [dbo].[SegmentedChunk] ([SnapshotDataId], [ChunkType], [ChunkName])
	include ([ChunkFlags], [ChunkId])
GO

create nonclustered index [IX_ChunkId_SnapshotDataId]
    on [dbo].[SegmentedChunk] ([ChunkId], [SnapshotDataId])
GO
	
GRANT SELECT, UPDATE, INSERT, DELETE, REFERENCES ON [dbo].[SegmentedChunk] TO RSExecRole
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Segment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Segment]
GO

create table [dbo].[Segment] (
	SegmentId			uniqueidentifier default newsequentialid() not null,	
	Content				varbinary(max) not null,	
	constraint [PK_Segment] primary key clustered ([SegmentId])	
	) 
GO

create unique nonclustered index [IX_SegmentMetadata] on [dbo].[Segment](SegmentId)
GO

GRANT SELECT, UPDATE, INSERT, DELETE, REFERENCES ON [dbo].[Segment] TO RSExecRole
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ChunkSegmentMapping]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ChunkSegmentMapping]
GO

create table [dbo].[ChunkSegmentMapping] (
	ChunkId			    uniqueidentifier not null,
	SegmentId		    uniqueidentifier not null, 
	StartByte		    bigint not null, 
	LogicalByteCount    int not null, 
	ActualByteCount     int not null
	constraint [PK_ChunkSegmentMapping] primary key clustered (ChunkId, SegmentId),
	constraint [Positive_LogicalByteCount] check (LogicalByteCount >= 0),
	constraint [Positive_ActualByteCount] check (ActualByteCount >= 0),
	constraint [Positive_StartByte] check (StartByte >= 0)	
	)
GO

create nonclustered index [IX_ChunkSegmentMapping_SegmentId] on [dbo].[ChunkSegmentMapping]([SegmentId])
GO

create unique nonclustered index [UNIQ_ChunkId_StartByte]
on [dbo].[ChunkSegmentMapping]([ChunkId], [StartByte]) include ([ActualByteCount], [LogicalByteCount])
GO

GRANT SELECT, UPDATE, INSERT, DELETE, REFERENCES ON [dbo].[ChunkSegmentMapping] TO RSExecRole
GO
