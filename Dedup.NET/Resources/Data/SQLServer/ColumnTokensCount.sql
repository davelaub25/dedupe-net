SELECT Token, COUNT(*) as Count FROM {0} AS TargetTable CROSS APPLY dbo.TokenSet(TargetTable.{1}, '{2}') AS TokenSet GROUP BY TokenSet.Token
