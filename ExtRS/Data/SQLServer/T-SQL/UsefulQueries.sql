-- Script to CREATE necessary objects for CustomType operations
-- CREATE TABLE dbo.CUSTOM_TYPE (...) Commented-out useful nuggets go here.
 --SSRS-specific

        //SELECT

        //    Subscriptions.Description,
        //	Subscriptions.LastStatus,
        //	Subscriptions.EventType,
        //	Subscriptions.LastRunTime,
        //	Subscriptions.Parameters,
        //	SUBSCRIPTION_OWNER.UserName AS SubscriptionOwner,
        //	Catalog.Name AS ReportName,
        //	MODIFIED_BY.UserName AS LastModifiedBy,
        //	Subscriptions.ModifiedDate
        //FROM dbo.Subscriptions
        //INNER JOIN dbo.Users SUBSCRIPTION_OWNER
        //ON SUBSCRIPTION_OWNER.UserID = Subscriptions.OwnerID
        //INNER JOIN dbo.Catalog
        //ON Catalog.ItemID = Subscriptions.Report_OID
        //INNER JOIN dbo.Users MODIFIED_BY
        //        //ON MODIFIED_BY.UserID = Subscriptions.ModifiedByID;


        //        SELECT DISTINCT C.NAME AS[Report Name]
        //  , MAX(EL.TIMESTART) AS[LAST START TIME]
        //FROM REPORTSERVER.DBO.EXECUTIONLOG(NOLOCK) EL
        //INNER JOIN REPORTSERVER.DBO.CATALOG(NOLOCK) C ON EL.REPORTID = C.ITEMID
        //GROUP BY C.NAME
        //ORDER BY C.NAME



        //2
        //3
        //4
        //5
        //6
        //SELECT CONVERT(VARCHAR(25), TIMESTART, 101) AS[SPECIFIC DATE]  
        //   ,COUNT(*) AS[Gneerated Number]
        // FROM REPORTSERVER.DBO.EXECUTIONLOG(NOLOCK)  
        // --WHERE USERNAME NOT IN
        // GROUP BY CONVERT(VARCHAR(25), TIMESTART, 101)  
        // ORDER BY CONVERT(VARCHAR(25), TIMESTART, 101) DESC



        //            1
        //2
        //3
        //4
        //5
        //6
        //SELECT DATEPART(HOUR, TIMESTART) AS HOUR
        //   , COUNT(*) AS[Gneerated Number]
        // FROM REPORTSERVER.DBO.EXECUTIONLOG(NOLOCK)  
        // --WHERE USERNAME NOT IN
        // GROUP BY DATEPART(HOUR, TIMESTART)
        // ORDER BY DATEPART(HOUR, TIMESTART)


        //SELECT EL.USERNAME
        //   , C.NAME
        //   , COUNT(1) AS[Gneerated Number]
        // FROM REPORTSERVER.DBO.EXECUTIONLOG(NOLOCK) EL
        // INNER JOIN REPORTSERVER.DBO.CATALOG(NOLOCK) C ON EL.REPORTID = C.ITEMID
        // GROUP BY EL.USERNAME
        //   , C.NAME
        // ORDER BY EL.USERNAME
        //   , C.NAME
        //1
        //2
        //3
        //4
        //5
        //6
        //7
        //8
        //9
        //SELECT EL.USERNAME
        //   , C.NAME
        //   , COUNT(1) AS[Gneerated Number]
        // FROM REPORTSERVER.DBO.EXECUTIONLOG(NOLOCK) EL
        // INNER JOIN REPORTSERVER.DBO.CATALOG(NOLOCK) C ON EL.REPORTID = C.ITEMID
        // GROUP BY EL.USERNAME
        //   , C.NAME
        // ORDER BY EL.USERNAME
        //   , C.NAME

        //     --Scott Murray

        //     SELECT
        //  [Path]
        //, CASE[Type]
        //    WHEN 2 THEN 'Report'
        //    WHEN 5 THEN 'Data Source'    
        //  END AS TypeName
        //, CAST(CAST(content AS varbinary(max)) AS xml)
        //, [Description]
        //FROM ReportServer.dbo.[Catalog] CTG
        //WHERE
        //  [Type] IN (2, 5);

        //        SELECT* FROM dbo.ExecutionLog

        //        SELECT
        //          [ItemPath] --Path of the report
        //        , [UserName]  --Username that executed the report
        //, [RequestType] --Usually Interactive(user on the scree) or Subscription
        //, [Format] --RPL is the screen, could also indicate Excel, PDF, etc
        //, [TimeStart]--Start time of report request
        //, [TimeEnd] --Completion time of report request
        //, [TimeDataRetrieval] --Time spent running queries to obtain results
        //, [TimeProcessing] --Time spent preparing data in SSRS.Usually sorting and grouping.
        //, [TimeRendering] --Time rendering to screen
        //, [Source] --Live = query, Session = refreshed without rerunning the query, Cache = report snapshot
        //, [Status] --Self explanatory
        //, [RowCount] --Row count returned by a query
        //, [Parameters]
        //FROM ReportServer.dbo.ExecutionLog3


        //SELECT
        //  ctg.[Path]
        //, s.[Description] SubScriptionDesc
        //, sj.[description] AgentJobDesc
        //, s.LastStatus
        //, s.DeliveryExtension
        //, s.[Parameters]
        //FROM
        //  ReportServer.dbo.[Catalog] ctg
        //    INNER JOIN
        //  ReportServer.dbo.Subscriptions s on s.Report_OID = ctg.ItemID
        //    INNER JOIN
        //  ReportServer.dbo.ReportSchedule rs on rs.SubscriptionID = s.SubscriptionID
        //    INNER JOIN
        //  msdb.dbo.sysjobs sj ON CAST(rs.ScheduleID AS sysname) = sj.name
        //ORDER BY
        //  rs.ScheduleID;


        //--XML
        //SELECT
        //C.Name
        //,c.Path
        //,CONVERT(XML, CONVERT(VARBINARY(MAX), C.Content)) AS reportXML
        //, C.Content
        //FROM  ReportServer.dbo.Catalog C
        //WHERE C.Content is not null
        //AND C.Type = 2



        //WITH XMLNAMESPACES
        //(DEFAULT
        //  'http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition'
        //, 'http://schemas.microsoft.com/SQLServer/reporting/reportdesigner' AS ReportDefinition)
        //SELECT
        //CATDATA.Name AS ReportName
        //, CATDATA.Path AS ReportPathLocation
        //, xmlcolumn.value('(@Name)[1]', 'VARCHAR(250)') AS DataSetName
        //, xmlcolumn.value('(Query/DataSourceName)[1]','VARCHAR(250)') AS DataSoureName
        //, xmlcolumn.value('(Query/CommandText)[1]','VARCHAR(2500)') AS CommandText
        //FROM(
        //    SELECT C.Name

        //    , c.Path

        //    , CONVERT(XML, CONVERT(VARBINARY(MAX), C.Content)) AS reportXML

        //    FROM ReportServer.dbo.Catalog C
        //    WHERE  C.Content is not null

        //    AND C.Type = 2
        //    ) CATDATA
        //CROSS APPLY reportXML.nodes('/Report/DataSets/DataSet') xmltable(xmlcolumn )
        //WHERE
        //xmlcolumn.value('(Query/CommandText)[1]','VARCHAR(250)') LIKE '%ProductCategoryName%'
        //ORDER BY CATDATA.Name



        //WITH XMLNAMESPACES
        //(DEFAULT
        //  'http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition'
        //, 'http://schemas.microsoft.com/SQLServer/reporting/reportdesigner' AS ReportDefinition)


        //CROSS APPLY reportXML.nodes('/Report/DataSets/DataSet') xmltable(xmlcolumn )
        //WHERE
        //xmlcolumn.value('(Query/CommandText)[1]','VARCHAR(250)') LIKE '%ProductCategoryName%'
        //ORDER BY CATDATA.Name

        //SELECT
        //CATDATA.Name AS ReportName
        //,CATDATA.Path AS ReportPathLocation
        //,xmlcolumn.value('(@Name)[1]', 'VARCHAR(250)') AS DataSetName
        //, xmlcolumn.value('(Query/DataSourceName)[1]','VARCHAR(250)') AS DataSoureName
        //, xmlcolumn.value('(Query/CommandText)[1]','VARCHAR(2500)') AS CommandText
        //FROM(
        //    SELECT C.Name

        //    , c.Path

        //    , CONVERT(XML, CONVERT(VARBINARY(MAX), C.Content)) AS reportXML

        //    FROM ReportServer.dbo.Catalog C
        //    WHERE  C.Content is not null

        //    AND C.Type = 2
        //    ) CATDATA
        //CROSS APPLY reportXML.nodes('/Report/DataSets/DataSet') xmltable(xmlcolumn )



        //        --— Waiting/ Suspended Tasks*****
        //SELECT* FROM sys.dm_os_waiting_tasks WHERE Session_ID > 50

        //--— Performance Counters
        //SELECT* FROM sys.dm_os_performance_counters

        //--— Cluster Nodes Details
        //SELECT* FROM sys.dm_os_cluster_nodes

        //        --— Execution related DMV and DMF

        //--— Current Connections(User Processes only)
        //SELECT* FROM sys.dm_exec_connections

        //--— Active Sessions
        //SELECT* FROM sys.dm_exec_sessions

        //-- Currently executing requests
        //SELECT* FROM sys.dm_exec_requests

        //--— Very Important: Query Optimizer details
        //SELECT* FROM sys.dm_exec_query_optimizer_info

        //--— Very Important: Query Plan, This is DMF
        //SELECT TOP 1 * FROM sys.dm_exec_query_stats T1
        //CROSS APPLY sys.dm_exec_query_plan (T1.plan_handle )

        //--— Very Important: SQL Statements, this is DMF
        //SELECT TOP 1 * FROM sys.dm_exec_query_stats T1
        //CROSS APPLY sys.dm_exec_SQL_text (T1.SQL_handle )

        //--— Index related Dynamic Management Objects
        //--— Index Usability
        //SELECT* FROM sys.dm_db_index_usage_stats

        //--— Cached Plans
        //SELECT* FROM sys.dm_exec_cached_plans

        //--— Missing Indexes
        //SELECT* FROM sys.dm_db_missing_index_details

        //--— Physical Stats, this is DMF, requires DBID, ObjectID, IndexID, PartitionID and Mode
        //SELECT * FROM sys.dm_db_index_physical_stats(DB_ID('TESTDB'), OBJECT_ID('EMPLOYEE'), NULL, NULL , NULL) WHERE avg_fragmentation_in_percent >30

        //--— SQL Server Operating system related Dynamic Management Objects
        //--— Wait Stats
        //SELECT* FROM sys.dm_os_wait_stats

        //--— Scheduler
        //SELECT* FROM sys.dm_os_schedulers

        //--— Waiting/ Suspended Tasks
        //SELECT* FROM sys.dm_os_waiting_tasks WHERE Session_ID > 50

        //--— Performance Counters
        //SELECT* FROM sys.dm_os_performance_counters

        //--— Cluster Nodes Details
        //SELECT* FROM sys.dm_os_cluster_nodes

        //      
        //
        //select* from  sys.dm_db_rda_migration_status
        //select* from  sys.dm_column_store_object_pool
        //
    }
}
