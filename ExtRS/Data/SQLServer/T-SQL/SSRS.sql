-- Script to CREATE necessary objects for CustomType operations
-- CREATE TABLE dbo.CUSTOM_TYPE (...) Commented-out useful nuggets go here.
--SSRS-specific



SELECT
  ItemID -- Unique Identifier
, [Path] --Path including object name
, [Name] --Just the objectd name
, ParentID --The ItemID of the folder in which it resides
, CASE [Type] --Type, an int which can be converted using this case statement.
    WHEN 1 THEN 'Folder'
    WHEN 2 THEN 'Report'
    WHEN 3 THEN 'File'
    WHEN 4 THEN 'Linked Report'
    WHEN 5 THEN 'Data Source'
    WHEN 6 THEN 'Report Model - Rare'
    WHEN 7 THEN 'Report Part - Rare'
    WHEN 8 THEN 'Shared Data Set - Rare'
    WHEN 9 THEN 'Image'
    ELSE CAST(Type as varchar(100))
  END AS TypeName
--, content
, LinkSourceID --If a linked report then this is the ItemID of the actual report.
, [Description] --This is the same information as can be found in the GUI
, [Hidden] --Is the object hidden on the screen or not
, CreatedBy.UserName CreatedBy
, CreationDate
, ModifiedBy.UserName ModifiedBy
, ModifiedDate
FROM 
  ReportServer.dbo.[Catalog] CTG
    INNER JOIN 
  ReportServer.dbo.Users CreatedBy ON CTG.CreatedByID = CreatedBy.UserID
    INNER JOIN
  ReportServer.dbo.Users ModifiedBy ON CTG.ModifiedByID = ModifiedBy.UserID;

    SELECT Subscriptions.Description,
        Subscriptions.LastStatus,
        Subscriptions.EventType,
        Subscriptions.LastRunTime,
        Subscriptions.Parameters,
        SUBSCRIPTION_OWNER.UserName AS SubscriptionOwner,
        Catalog.Name AS ReportName,
        MODIFIED_BY.UserName AS LastModifiedBy,
        Subscriptions.ModifiedDate
    FROM dbo.Subscriptions
    INNER JOIN dbo.Users SUBSCRIPTION_OWNER
    ON SUBSCRIPTION_OWNER.UserID = Subscriptions.OwnerID
    INNER JOIN dbo.Catalog
    ON Catalog.ItemID = Subscriptions.Report_OID
    INNER JOIN dbo.Users MODIFIED_BY
            ON MODIFIED_BY.UserID = Subscriptions.ModifiedByID;

  SELECT DISTINCT C.NAME AS[Report Name]
        , MAX(EL.TIMESTART) AS[LAST START TIME]
    FROM REPORTSERVER.DBO.EXECUTIONLOG(NOLOCK) EL
    INNER JOIN REPORTSERVER.DBO.CATALOG(NOLOCK) C ON EL.REPORTID = C.ITEMID
    GROUP BY C.NAME
    ORDER BY C.NAME

    SELECT CONVERT(VARCHAR(25), TIMESTART, 101) AS[SPECIFIC DATE]  
        ,COUNT(*) AS[Gneerated Number]
        FROM REPORTSERVER.DBO.EXECUTIONLOG(NOLOCK)  
        --WHERE USERNAME NOT IN
        GROUP BY CONVERT(VARCHAR(25), TIMESTART, 101)  
        ORDER BY CONVERT(VARCHAR(25), TIMESTART, 101) DESC

    SELECT DATEPART(HOUR, TIMESTART) AS HOUR
        , COUNT(*) AS[Gneerated Number]
        FROM REPORTSERVER.DBO.EXECUTIONLOG(NOLOCK)  
        --WHERE USERNAME NOT IN
        GROUP BY DATEPART(HOUR, TIMESTART)
        ORDER BY DATEPART(HOUR, TIMESTART)

    SELECT EL.USERNAME
        , C.NAME
        , COUNT(1) AS[Gneerated Number]
        FROM REPORTSERVER.DBO.EXECUTIONLOG(NOLOCK) EL
        INNER JOIN REPORTSERVER.DBO.CATALOG(NOLOCK) C ON EL.REPORTID = C.ITEMID
        GROUP BY EL.USERNAME
        , C.NAME
        ORDER BY EL.USERNAME
        , C.NAME

SELECT [Path]
    , CASE[Type]
        WHEN 2 THEN 'Report'
        WHEN 5 THEN 'Data Source'    
        END AS TypeName
    , CAST(CAST(content AS varbinary(max)) AS xml)
    , [Description]
    FROM ReportServer.dbo.[Catalog] CTG
    WHERE [Type] IN (2, 5);


SELECT [ItemPath] --Path of the report
, [UserName]  --Username that executed the report
, [RequestType] --Usually Interactive(user on the scree) or Subscription
, [Format] --RPL is the screen, could also indicate Excel, PDF, etc
, [TimeStart]--Start time of report request
, [TimeEnd] --Completion time of report request
, [TimeDataRetrieval] --Time spent running queries to obtain results
, [TimeProcessing] --Time spent preparing data in SSRS.Usually sorting and grouping.
, [TimeRendering] --Time rendering to screen
, [Source] --Live = query, Session = refreshed without rerunning the query, Cache = report snapshot
, [Status] --Self explanatory
, [RowCount] --Row count returned by a query
, [Parameters]
FROM ReportServer.dbo.ExecutionLog3


    SELECT ctg.[Path]
    , s.[Description] SubScriptionDesc
    , sj.[description] AgentJobDesc
    , s.LastStatus
    , s.DeliveryExtension
    , s.[Parameters]
    FROM
        ReportServer.dbo.[Catalog] ctg
        INNER JOIN
        ReportServer.dbo.Subscriptions s on s.Report_OID = ctg.ItemID
        INNER JOIN
        ReportServer.dbo.ReportSchedule rs on rs.SubscriptionID = s.SubscriptionID
        INNER JOIN
        msdb.dbo.sysjobs sj ON CAST(rs.ScheduleID AS sysname) = sj.name
    ORDER BY
        rs.ScheduleID;

   -- XML
   SELECT
  [Path]
, CASE [Type]
    WHEN 2 THEN 'Report'
    WHEN 5 THEN 'Data Source'    
  END AS TypeName
, CAST(CAST(content AS varbinary(max)) AS xml)
, [Description]
FROM ReportServer.dbo.[Catalog] CTG
WHERE
[Type] IN (2, 5);

   SELECT
    CATDATA.Name AS ReportName
    , CATDATA.Path AS ReportPathLocation
    , xmlcolumn.value('(@Name)[1]', 'VARCHAR(250)') AS DataSetName
    , xmlcolumn.value('(Query/DataSourceName)[1]','VARCHAR(250)') AS DataSoureName
    , xmlcolumn.value('(Query/CommandText)[1]','VARCHAR(2500)') AS CommandText
    FROM(
        SELECT C.Name
        , c.Path
        , CONVERT(XML, CONVERT(VARBINARY(MAX), C.Content)) AS reportXML
        FROM ReportServer.dbo.Catalog C
        WHERE  C.Content is not null
        AND C.Type = 2
        ) CATDATA
    CROSS APPLY reportXML.nodes('/Report/DataSets/DataSet') xmltable(xmlcolumn )
    WHERE
    xmlcolumn.value('(Query/CommandText)[1]','VARCHAR(250)') LIKE '%ProductCategoryName%'
    ORDER BY CATDATA.Name
-- Script to CREATE necessary objects for CustomType operations
-- CREATE TABLE dbo.CUSTOM_TYPE (...) Commented-out useful nuggets go here.
--SSRS-specific

    SELECT Subscriptions.Description,
        Subscriptions.LastStatus,
        Subscriptions.EventType,
        Subscriptions.LastRunTime,
        Subscriptions.Parameters,
        SUBSCRIPTION_OWNER.UserName AS SubscriptionOwner,
        Catalog.Name AS ReportName,
        MODIFIED_BY.UserName AS LastModifiedBy,
        Subscriptions.ModifiedDate
    FROM dbo.Subscriptions
    INNER JOIN dbo.Users SUBSCRIPTION_OWNER
    ON SUBSCRIPTION_OWNER.UserID = Subscriptions.OwnerID
    INNER JOIN dbo.Catalog
    ON Catalog.ItemID = Subscriptions.Report_OID
    INNER JOIN dbo.Users MODIFIED_BY
            ON MODIFIED_BY.UserID = Subscriptions.ModifiedByID;

  SELECT DISTINCT C.NAME AS[Report Name]
        , MAX(EL.TIMESTART) AS[LAST START TIME]
    FROM REPORTSERVER.DBO.EXECUTIONLOG(NOLOCK) EL
    INNER JOIN REPORTSERVER.DBO.CATALOG(NOLOCK) C ON EL.REPORTID = C.ITEMID
    GROUP BY C.NAME
    ORDER BY C.NAME

    SELECT CONVERT(VARCHAR(25), TIMESTART, 101) AS[SPECIFIC DATE]  
        ,COUNT(*) AS[Gneerated Number]
        FROM REPORTSERVER.DBO.EXECUTIONLOG(NOLOCK)  
        --WHERE USERNAME NOT IN
        GROUP BY CONVERT(VARCHAR(25), TIMESTART, 101)  
        ORDER BY CONVERT(VARCHAR(25), TIMESTART, 101) DESC

    SELECT DATEPART(HOUR, TIMESTART) AS HOUR
        , COUNT(*) AS[Gneerated Number]
        FROM REPORTSERVER.DBO.EXECUTIONLOG(NOLOCK)  
        --WHERE USERNAME NOT IN
        GROUP BY DATEPART(HOUR, TIMESTART)
        ORDER BY DATEPART(HOUR, TIMESTART)

    SELECT EL.USERNAME
        , C.NAME
        , COUNT(1) AS[Gneerated Number]
        FROM REPORTSERVER.DBO.EXECUTIONLOG(NOLOCK) EL
        INNER JOIN REPORTSERVER.DBO.CATALOG(NOLOCK) C ON EL.REPORTID = C.ITEMID
        GROUP BY EL.USERNAME
        , C.NAME
        ORDER BY EL.USERNAME
        , C.NAME

SELECT [Path]
    , CASE[Type]
        WHEN 2 THEN 'Report'
        WHEN 5 THEN 'Data Source'    
        END AS TypeName
    , CAST(CAST(content AS varbinary(max)) AS xml)
    , [Description]
    FROM ReportServer.dbo.[Catalog] CTG
    WHERE [Type] IN (2, 5);


SELECT [ItemPath] --Path of the report
, [UserName]  --Username that executed the report
, [RequestType] --Usually Interactive(user on the scree) or Subscription
, [Format] --RPL is the screen, could also indicate Excel, PDF, etc
, [TimeStart]--Start time of report request
, [TimeEnd] --Completion time of report request
, [TimeDataRetrieval] --Time spent running queries to obtain results
, [TimeProcessing] --Time spent preparing data in SSRS.Usually sorting and grouping.
, [TimeRendering] --Time rendering to screen
, [Source] --Live = query, Session = refreshed without rerunning the query, Cache = report snapshot
, [Status] --Self explanatory
, [RowCount] --Row count returned by a query
, [Parameters]
FROM ReportServer.dbo.ExecutionLog3


    SELECT ctg.[Path]
    , s.[Description] SubScriptionDesc
    , sj.[description] AgentJobDesc
    , s.LastStatus
    , s.DeliveryExtension
    , s.[Parameters]
    FROM
        ReportServer.dbo.[Catalog] ctg
        INNER JOIN
        ReportServer.dbo.Subscriptions s on s.Report_OID = ctg.ItemID
        INNER JOIN
        ReportServer.dbo.ReportSchedule rs on rs.SubscriptionID = s.SubscriptionID
        INNER JOIN
        msdb.dbo.sysjobs sj ON CAST(rs.ScheduleID AS sysname) = sj.name
    ORDER BY
        rs.ScheduleID;

-- XML
SELECT
[Path]
, CASE [Type]
    WHEN 2 THEN 'Report'
    WHEN 5 THEN 'Data Source'    
  END AS TypeName
, CAST(CAST(content AS varbinary(max)) AS xml)
, [Description]
FROM ReportServer.dbo.[Catalog] CTG
WHERE
[Type] IN (2, 5);

   SELECT
    CATDATA.Name AS ReportName
    , CATDATA.Path AS ReportPathLocation
    , xmlcolumn.value('(@Name)[1]', 'VARCHAR(250)') AS DataSetName
    , xmlcolumn.value('(Query/DataSourceName)[1]','VARCHAR(250)') AS DataSoureName
    , xmlcolumn.value('(Query/CommandText)[1]','VARCHAR(2500)') AS CommandText
    FROM(
        SELECT C.Name
        , c.Path
        , CONVERT(XML, CONVERT(VARBINARY(MAX), C.Content)) AS reportXML
        FROM ReportServer.dbo.Catalog C
        WHERE  C.Content is not null
        AND C.Type = 2
        ) CATDATA
    CROSS APPLY reportXML.nodes('/Report/DataSets/DataSet') xmltable(xmlcolumn )
    WHERE
    xmlcolumn.value('(Query/CommandText)[1]','VARCHAR(250)') LIKE '%ProductCategoryName%'
    ORDER BY CATDATA.Name
