USE msdb; -- Switch to the msdb database where schedules are stored

DECLARE @schedule_id INT;
DECLARE @schedule_name NVARCHAR(128);

-- Cursor to fetch all schedules
DECLARE schedule_cursor CURSOR FOR
SELECT schedule_id, name
FROM sysschedules;

OPEN schedule_cursor;

FETCH NEXT FROM schedule_cursor INTO @schedule_id, @schedule_name;

-- Loop through all schedules and delete them
WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Deleting Schedule: ' + @schedule_name;
    EXEC sp_delete_schedule @schedule_id = @schedule_id, @force_delete = 1; -- Force delete the schedule
    FETCH NEXT FROM schedule_cursor INTO @schedule_id, @schedule_name;
END;

CLOSE schedule_cursor;
DEALLOCATE schedule_cursor;

PRINT 'All SQL Server Agent schedules have been deleted.';