# SQL TRIGGER / SQL PROC CODES ARE ; 
CREATE TRIGGER GONDEREN
ON TBLHAREKET
AFTER INSERT
AS
DECLARE @TUTAR DECIMAL(18,3)
SELECT @TUTAR = TUTAR FROM inserted
DECLARE @GONDEREN CHAR(6)
SELECT @GONDEREN = GÖNDEREN FROM TBLHAREKET
UPDATE TBLHESAP SET BAKİYE = BAKİYE - @TUTAR WHERE HESAPNO = @GONDEREN

CREATE TRIGGER ALICI
ON TBLHAREKET
AFTER INSERT
AS
DECLARE @TUTAR DECIMAL(18,3)
SELECT @TUTAR = TUTAR FROM inserted
DECLARE @ALICI CHAR(6)
SELECT @ALICI = ALICI FROM TBLHAREKET
UPDATE TBLHESAP SET BAKİYE = BAKİYE + @TUTAR WHERE HESAPNO=@ALICI

CREATE PROC GIDEN(@HESAP CHAR(6))
AS
SELECT TUTAR AS 'GÖNDERİLEN TUTAR', (AD+' '+SOYAD) AS 'ALICI' FROM TBLHAREKET
INNER JOIN TBLKİSİLER
ON TBLHAREKET.ALICI=TBLKİSİLER.HESAPNO WHERE GÖNDEREN=@HESAP

CREATE PROC GELEN(@HESAP CHAR(6))
AS
SELECT TUTAR AS 'GELEN TUTAR', (AD+' '+SOYAD) AS 'GÖNDEREN' FROM TBLHAREKET
INNER JOIN TBLKİSİLER
ON TBLHAREKET.GÖNDEREN=TBLKİSİLER.HESAPNO WHERE ALICI=@HESAP
