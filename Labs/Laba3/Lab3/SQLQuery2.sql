
UPDATE dbo.Venues SET Map = (SELECT * FROM OPENROWSET(BULK 'c:\1up-mushroom.png', SINGLE_BLOB) AS bytearray);
GO