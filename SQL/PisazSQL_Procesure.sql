USE msdb ;

-- creates a job to payback an specific percent of payments
GO
EXEC dbo.sp_add_job @job_name = N'VIPPayback' ;

GO
EXEC sp_add_jobstep
    @job_name = N'VIPPayback',
	@step_id = 1,
    @step_name = N'payback',
    @subsystem = N'TSQL',
    @command = 
	N'
	DECLARE @MyCursor CURSOR;
	DECLARE @MyField INT;
	BEGIN
		SET @MyCursor = CURSOR FOR SELECT ID FROM VIPClient;

		OPEN @MyCursor 
		FETCH NEXT FROM @MyCursor
		INTO @MyField

		WHILE @@FETCH_STATUS = 0
		BEGIN
			--MY ALGORITHM GOES HERE
			DECLARE @TotalPayment BIGINT;
			SELECT @TotalPayment = dbo.CalculateFinalPrice(L.ID, L.CartNumber, L.LockedNumber)
			FROM IssuedFor AS I, Transactions AS T, LockedShoppingCart AS L
			WHERE I.ID = @MyField AND 
				  I.TrackingCode = T.TrackingCode AND 
				  T.TransactionStatus = ''Successfull'' AND
				  T.TransactionTime > (DATEADD(MONTH, -1, CURRENT_TIMESTAMP)) AND
				  L.ID = I.ID AND
				  L.CartNumber = I.CartNumber AND
				  L.LockedNumber = I.CartNumber
			GROUP BY L.ID, L.CartNumber, L.LockedNumber;
		
			UPDATE Client
			SET WalletBalance = WalletBalance - @TotalPayment*0.15
			FROM Client
			WHERE ID = @MyField;

			FETCH NEXT FROM @MyCursor 
			INTO @MyField 
		END; 

		CLOSE @MyCursor ;
		DEALLOCATE @MyCursor;
	END;
	',
	@database_name = N'PsazDBTset2',
    @retry_attempts = 5,
    @retry_interval = 5 ;
GO
-- creates a schedule named VIPPayback.
EXEC sp_add_schedule
    @schedule_name = N'MonthlyJobs' ,
    @freq_type = 16, -- 16 : Monthly repeat
    @freq_interval = 1, -- 1 : Day 1 of each month
	@freq_recurrence_factor = 1,
    @active_start_time = 010000 ; -- Start at 01:00 AM
GO
-- attaches the schedule to the job UpdateStats
EXEC sp_attach_schedule
    @job_name = N'VIPPayback',
    @schedule_name = N'MonthlyJobs' ;
GO


-- creates a job to check vip expiration time
GO
EXEC dbo.sp_add_job @job_name = N'VIPValidation' ;

GO
EXEC sp_add_jobstep
    @job_name = N'VIPValidation',
	@step_id = 1,
    @step_name = N'Validation',
    @subsystem = N'TSQL',
    @command = 
	N'
	DELETE VIPClient
	WHERE ID IN (SELECT ID FROM Client WHERE SubsctiptionExpirationTime < GETDATE());
	',
	@database_name = N'Pisaz',
    @retry_attempts = 5,
    @retry_interval = 5 ;
GO
-- creates a schedule named DailyJobs
EXEC sp_add_schedule
    @schedule_name = N'DailyJobs2' ,
    @freq_type = 4, -- 4 : Daily repeat
    @freq_interval = 1, -- 1 : Everyday
	@freq_recurrence_factor = 1,
    @active_start_time = 023400 ; -- Start at 02:34 AM
GO
-- attaches the schedule to the job
EXEC sp_attach_schedule
    @job_name = N'VIPValidation',
    @schedule_name = N'DailyJobs2' ; 
GO


-- creates a job to check locked cart expiration time
GO
EXEC dbo.sp_add_job @job_name = N'LockedCartChecker' ;

GO
EXEC sp_add_jobstep
    @job_name = N'LockedCartChecker',
	@step_id = 1,
    @step_name = N'Check',
    @subsystem = N'TSQL',
    @command = 
	N'
	UPDATE ShoppingCart
	SET CartStatus = ''blocked''
	FROM (SELECT * FROM ShoppingCart WHERE CartStatus = ''Locked'') AS S JOIN LockedShoppingCart AS L ON S.ID = L.ID AND S.CartNumber = L.CartNumber
	WHERE  DATEADD(DAY, 3,LockTimestamp) < GETDATE(); 

	UPDATE ShoppingCart
	SET CartStatus = ''active''
	FROM (SELECT * FROM ShoppingCart WHERE CartStatus = ''Locked'') AS S JOIN LockedShoppingCart AS L ON S.ID = L.ID AND S.CartNumber = L.CartNumber
	WHERE  DATEADD(DAY, 7,LockTimestamp) < GETDATE(); 
	',
	@database_name = N'PsazDBTset2',
    @retry_attempts = 5,
    @retry_interval = 5 ;
GO
-- creates a schedule named DailyJobs3
EXEC sp_add_schedule
    @schedule_name = N'DailyJobs3' ,
    @freq_type = 4, -- 4 : Daily repeat
    @freq_interval = 1, -- 1 : Everyday
	@freq_recurrence_factor = 1,
    @active_start_time = 033000 ; -- Start at 03:30 AM
GO
-- attaches the schedule to the job
EXEC sp_attach_schedule
    @job_name = N'LockedCartChecker',
    @schedule_name = N'DailyJobs3' ; 
GO