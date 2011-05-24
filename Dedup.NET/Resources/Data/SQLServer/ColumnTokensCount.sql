SELECT Token, COUNT(*) as Count FROM ###relationName### AS TargetTable CROSS APPLY dbo.TokenSet(###relationName###.###columnName###, ###stopChars###) AS TokenSet GROUP BY TokenSet.Token
