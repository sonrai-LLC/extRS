 --Find all instances of '@string' within a string (varchar/nvarchar)
 CREATE FUNCTION dbo.FindPatternLocation
 (
 	@string NVARCHAR(MAX),
  	@term   NVARCHAR(255)
 )
 RETURNS TABLE
 AS
 RETURN
 (
	SELECT pos = Number - LEN(@term)
	FROM (SELECT Number, Item = LTRIM(RTRIM(SUBSTRING(@string, Number,
	CHARINDEX(@term, @string + @term, Number) - Number)))
	FROM (SELECT ROW_NUMBER() OVER (ORDER BY [object_id])
	FROM sys.all_objects) AS n(Number)
	WHERE Number > 1 AND Number <= CONVERT(INT, LEN(@string)+1)
	AND SUBSTRING(@term + @string, Number, LEN(@term)) = @term
 ) AS y);
 GO;

 --WINDOWING for running totals, referencing related rows and row group totals
 SELECT
     SUM(xact_amt) OVER (ORDER BY xact_datetime) AS running_total
 FROM SOME_DB.dbo.SOME_TABLE

 --Get IDs from CSV of ids
 SELECT * FROM string_split('100, 101, 201, 301, 411, 414', ',')

--STUFF a series of values into one row
 SELECT playerID,
 STUFF((select '; ' + teamID
      from Managers
      where teamID like('%P%')
            group by teamID
      for xml path('')), 1, 1, '') allTeams
 FROM Managers

--DYNAMIC SQL
 DECLARE @mult bit = 0
 DECLARE @sqlToExecute varchar(500)
 DECLARE @srvWest varchar(50) = 'WestZoneSqlSrv08'
 DECLARE @srvEast varchar(50) = 'EastZoneSqlSrv08'
 IF @mult = 0
      SET @sqlToExecute = 'SELECT TOP 1 OrderId, PersonId FROM ALGO.dbo.[Order]'
 ELSE
      SET @sqlToExecute = 'SELECT TOP 1 OrderId, PersonId FROM ' + @srvEast + '.ALGO.dbo.[Order] UNION ALL SELECT TOP 1 OrderID, PersonId FROM ' + @srvWest + '.ALGO.dbo.Order' 
 EXEC(@sqlToExecute)

 --SQL day-of-month calculations
SELECT dateadd(mm, datediff(mm, 1, getDate()), 0) as FirstOfTheMonth
SELECT dateadd(ms, -3, dateadd(mm, datediff(m, 0, getDate()) + 1, 0)) as LastOfTheMonth
SELECT dateadd(qq, datediff(qq, 0, getDate()), 0) as FirstDayOfQtr
SELECT dateadd(wk, datediff(wk, 0, dateadd(dd, 6 - datepart(day, getDate()), getDate())), 0)  --(First Monday of Month)
