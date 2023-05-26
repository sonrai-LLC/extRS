-- Get all DMVs
SELECT 'sys.' +[[name] 'name', [type], [type_desc]
FROM sys.system_objects
WHERE [name] LIKE 'dm_%'
ORDER BY [name]


-- Run a DMV
select * from
sys.dm_cryptographic_provider_properties

select * from
sys.dm_db_log_info(1)

select * from
sys.dm_os_wait_stats;  

select * from 
sys.dm_exec_query_stats

select * from 
sys.dm_exec_query_stats

select * from 
sys.dm_xe_objects

select * from 
sys.dm_db_script_level

select * from 
sys.dm_exec_requests

select * from
sys.dm_database_backups

select * from
sys.dm_db_index_usage_stats

select * from
sys.dm_exec_connections