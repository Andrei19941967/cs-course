CREATE OR ALTER FUNCTION fn_FindReminder(
	@date DATETIMEOFFSET,
    @id  UNIQUEIDENTIFIER
)
RETURNS NVARCHAR(100)
BEGIN
	DECLARE @message NVARCHAR(100);
    
    SELECT @message = [Message]
		FROM [ReminderItem]
	WHERE [DateTime] = @date AND [Id] = @id;
    
    RETURN @message;
END;

 
