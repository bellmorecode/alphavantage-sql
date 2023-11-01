-- # Install the external functions

-- NOTE: RUN EACH STEP INDIVIDUALLY

-- Step #1 :: configure .NET
exec sp_configure 'clr enabled','1'
RECONFIGURE
-- step #1b: set 'Trustworthy' Flag
Alter Database [YOUR_DATABASE_NAME_HERE] Set Trustworthy ON

-- Step #2 :: Copy files to the SQL directory
-- ===================================================================================
-- copy DLL to SQL Server Program Files directory, so that the code below will work.
-- from here: "C:\_src\bc\alphavantage-sql\stk.alphavantage\bin\Debug\stk.alphavantage.dll"

-- Step #3: Create Login to be used to run the external function
-- ===================================================================================
Create Asymmetric Key StocksAPIKey From Executable File = 'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\stk.alphavantage.dll'
Create Login StocksAPILogin From Asymmetric Key StocksAPIKey
Grant External Access Assembly To StocksAPILogin

-- Step #4A: Uninstall before Reinstall, not needed on first install.
-- ========================================================================
-- drop Function GetStocksAPIQuote
-- drop Function GetStocksAPIIntradaySeries
-- drop Assembly StocksAPIFunctions

-- Step #4B: Install the Assembly + Functions
-- ========================================================================
Create Assembly StocksAPIFunctions From 'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\stk.alphavantage.dll' with permission_set=UNSAFE
go
Create Function dbo.GetStocksAPIQuote (	@symbol as nvarchar(100)) Returns nvarchar(4000)
    as external name StocksAPIFunctions.[stk.alphavantage.StocksAPI].GetQuote
go

Create Function dbo.GetStocksAPIIntradaySeries (@symbol as nvarchar(100)) Returns nvarchar(4000)
    as external name StocksAPIFunctions.[stk.alphavantage.StocksAPI].GetIntradaySeries
go



-- set security
exec sp_changedbowner [sa]
