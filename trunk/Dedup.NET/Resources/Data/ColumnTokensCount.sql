DECLARE @idx int = 1
DECLARE @tokeString nvarchar(max) = ''
DECLARE @tokenSplit nvarchar(max)
DECLARE @columnTokensCount table(Token nvarchar(max), Frequency int)
	
SELECT @tokeString = @tokeString + dbo.Tokenize(dbo.Activist.Email, ' ') from dbo.Activist
	
WHILE @idx != 0     
BEGIN
	SET @idx = CHARINDEX(';',@tokeString)
		     
	IF @idx != 0     
		SET @tokenSplit = LEFT(@tokeString, @idx - 1)     
	ELSE     
		SET @tokenSplit = @tokeString     
		
	IF (LEN(@tokenSplit) > 0)
		INSERT INTO @columnTokensCount(Token) VALUES(LOWER(@tokenSplit))
			
	SET @tokeString = RIGHT(@tokeString, LEN(@tokeString) - @idx)
		
	IF LEN(@tokeString) = 0
		BREAK
END
SELECT Token, COUNT(*) as Frequency FROM @columnTokensCount
GROUP BY Token