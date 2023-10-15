-- Get all DMVs
SELECT 'sys.' + [name] 'name', [type], [type_desc]
FROM sys.system_objects
WHERE [name] LIKE 'dm_%'
ORDER BY [name]

-- Run a DMV
SELECT * FROM
sys.dm_cryptographic_provider_properties

SELECT * FROM
sys.dm_db_log_info(1)

SELECT * FROM
sys.dm_os_wait_stats;  

SELECT * FROM 
sys.dm_exec_query_stats

SELECT * FROM 
sys.dm_exec_query_stats

SELECT * FROM 
sys.dm_xe_objects

SELECT * FROM 
sys.dm_db_script_level

SELECT * FROM 
sys.dm_exec_requests

SELECT * FROM
sys.dm_database_backups

SELECT * FROM
sys.dm_db_index_usage_stats

SELECT * FROM
sys.dm_exec_connections

--— Waiting/ Suspended Tasks
SELECT * FROM sys.dm_os_waiting_tasks WHERE Session_ID > 50

--— Performance Counters
SELECT * FROM sys.dm_os_performance_counters

--— Cluster Nodes Details
SELECT * FROM sys.dm_os_cluster_nodes

--— Current Connections(User Processes only)
SELECT * FROM sys.dm_exec_connections

--— Active Sessions
SELECT * FROM sys.dm_exec_sessions

-- Currently executing requests
SELECT * FROM sys.dm_exec_requests

--— Very Important: Query Optimizer details
SELECT * FROM sys.dm_exec_query_optimizer_info

--— Very Important: Query Plan, This is DMF
SELECT TOP 1 * FROM sys.dm_exec_query_stats T1
CROSS APPLY sys.dm_exec_query_plan (T1.plan_handle )

--— Very Important: SQL Statements, this is DMF
SELECT TOP 1 * FROM sys.dm_exec_query_stats T1
CROSS APPLY sys.dm_exec_SQL_text (T1.SQL_handle )

--— Index Usability
SELECT * FROM sys.dm_db_index_usage_stats

--— Cached Plans
SELECT * FROM sys.dm_exec_cached_plans

--— Missing Indexes
SELECT * FROM sys.dm_db_missing_index_details

--— Physical Stats, this is DMF, requires DBID, ObjectID, IndexID, PartitionID and Mode
SELECT * FROM sys.dm_db_index_physical_stats(DB_ID('TESTDB'), OBJECT_ID('EMPLOYEE'), NULL, NULL , NULL) WHERE avg_fragmentation_in_percent >30

SELECT * FROM sys.dm_os_schedulers

--— Waiting/ Suspended Tasks
SELECT * FROM sys.dm_os_waiting_tasks WHERE Session_ID > 50

--— Performance Counters
SELECT * FROM sys.dm_os_performance_counters

--— Cluster Nodes Details
SELECT * FROM sys.dm_os_cluster_nodes

SELECT * FROM  sys.dm_db_rda_migration_status

SELECT * FROM  sys.dm_column_store_object_pool
