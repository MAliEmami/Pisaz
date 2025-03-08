USE msdb; -- Switch to the msdb database where agent jobs and schedules are stored

DECLARE @job_id UNIQUEIDENTIFIER;
DECLARE @job_name NVARCHAR(128);
DECLARE @schedule_id INT;

-- Cursor to fetch all agent jobs
DECLARE job_cursor CURSOR FOR
SELECT job_id, name
FROM sysjobs;

OPEN job_cursor;

FETCH NEXT FROM job_cursor INTO @job_id, @job_name;

-- Loop through all jobs and delete them along with their schedules
WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Deleting Job: ' + @job_name;

    -- Delete all schedules associated with the current job
    DECLARE schedule_cursor CURSOR FOR
    SELECT schedule_id
    FROM sysjobschedules
    WHERE job_id = @job_id;

    OPEN schedule_cursor;
    FETCH NEXT FROM schedule_cursor INTO @schedule_id;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT 'Deleting Schedule ID: ' + CAST(@schedule_id AS NVARCHAR(10));
        EXEC sp_delete_schedule @schedule_id = @schedule_id, @force_delete = 1; -- Force delete the schedule
        FETCH NEXT FROM schedule_cursor INTO @schedule_id;
    END;

    CLOSE schedule_cursor;
    DEALLOCATE schedule_cursor;

    -- Delete the job itself
    EXEC sp_delete_job @job_id = @job_id;

    FETCH NEXT FROM job_cursor INTO @job_id, @job_name;
END;

CLOSE job_cursor;
DEALLOCATE job_cursor;

PRINT 'All SQL Server Agent jobs and their associated schedules have been deleted.';