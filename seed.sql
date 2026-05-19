-- CBS Seed Data — LAMI Church Davao
-- Safe: skips rows that already exist by name check
SET NOCOUNT ON;
BEGIN TRANSACTION;

-- ══════════════════════════════════════════════
-- VENDORS
-- ══════════════════════════════════════════════
IF NOT EXISTS (SELECT 1 FROM Vendors WHERE Name = 'Globe Telecom Davao')
    INSERT INTO Vendors (Name,Address,DateAdded,IsActive) VALUES ('Globe Telecom Davao','J.P. Laurel Ave, Davao City','2024-01-05',1);
IF NOT EXISTS (SELECT 1 FROM Vendors WHERE Name = 'Davao Light and Power Co.')
    INSERT INTO Vendors (Name,Address,DateAdded,IsActive) VALUES ('Davao Light and Power Co.','C.M. Recto St, Davao City','2024-01-05',1);
IF NOT EXISTS (SELECT 1 FROM Vendors WHERE Name = 'Metro Davao Water District')
    INSERT INTO Vendors (Name,Address,DateAdded,IsActive) VALUES ('Metro Davao Water District','Pichon St, Davao City','2024-01-05',1);
IF NOT EXISTS (SELECT 1 FROM Vendors WHERE Name = 'National Bookstore Davao')
    INSERT INTO Vendors (Name,Address,DateAdded,IsActive) VALUES ('National Bookstore Davao','SM City Davao, Quimpo Blvd','2024-01-10',1);
IF NOT EXISTS (SELECT 1 FROM Vendors WHERE Name = 'Holy Cross Church Supplies')
    INSERT INTO Vendors (Name,Address,DateAdded,IsActive) VALUES ('Holy Cross Church Supplies','Ilustre St, Davao City','2024-01-10',1);
IF NOT EXISTS (SELECT 1 FROM Vendors WHERE Name = 'Davao Transport Cooperative')
    INSERT INTO Vendors (Name,Address,DateAdded,IsActive) VALUES ('Davao Transport Cooperative','Sandawa Road, Davao City','2024-01-10',1);
IF NOT EXISTS (SELECT 1 FROM Vendors WHERE Name = 'JMV General Hardware')
    INSERT INTO Vendors (Name,Address,DateAdded,IsActive) VALUES ('JMV General Hardware','Matina, Davao City','2024-02-01',1);
IF NOT EXISTS (SELECT 1 FROM Vendors WHERE Name = 'BIR Regional Office 11')
    INSERT INTO Vendors (Name,Address,DateAdded,IsActive) VALUES ('BIR Regional Office 11','Sales St, Davao City','2024-02-01',1);
IF NOT EXISTS (SELECT 1 FROM Vendors WHERE Name = 'Feliciano Printing Press')
    INSERT INTO Vendors (Name,Address,DateAdded,IsActive) VALUES ('Feliciano Printing Press','Claveria St, Davao City','2024-03-01',1);
IF NOT EXISTS (SELECT 1 FROM Vendors WHERE Name = 'Mintal Public Market Vendors')
    INSERT INTO Vendors (Name,Address,DateAdded,IsActive) VALUES ('Mintal Public Market Vendors','Mintal, Davao City','2024-03-15',1);

DECLARE
    @globe     INT = (SELECT Id FROM Vendors WHERE Name = 'Globe Telecom Davao'),
    @dlpc      INT = (SELECT Id FROM Vendors WHERE Name = 'Davao Light and Power Co.'),
    @water     INT = (SELECT Id FROM Vendors WHERE Name = 'Metro Davao Water District'),
    @nbs       INT = (SELECT Id FROM Vendors WHERE Name = 'National Bookstore Davao'),
    @church    INT = (SELECT Id FROM Vendors WHERE Name = 'Holy Cross Church Supplies'),
    @transport INT = (SELECT Id FROM Vendors WHERE Name = 'Davao Transport Cooperative'),
    @hardware  INT = (SELECT Id FROM Vendors WHERE Name = 'JMV General Hardware'),
    @bir       INT = (SELECT Id FROM Vendors WHERE Name = 'BIR Regional Office 11'),
    @print     INT = (SELECT Id FROM Vendors WHERE Name = 'Feliciano Printing Press'),
    @market    INT = (SELECT Id FROM Vendors WHERE Name = 'Mintal Public Market Vendors');

-- ══════════════════════════════════════════════
-- EXPENSES
-- ExpenseTypes: CommSvc=0, Repair=1, Transport=2, Supplies=3, Rents=4,
--               LoveGifts=5, Water/Light=6, Taxes=7, Ministry=8, Other=9
-- ══════════════════════════════════════════════
IF NOT EXISTS (SELECT 1 FROM Expenses WHERE Description = 'Monthly internet & phone plan' AND MONTH(ExpenseDate)=1 AND YEAR(ExpenseDate)=2026)
BEGIN
    -- Recurring templates
    INSERT INTO Expenses (VendorId,ExpenseDate,Description,ExpenseType,Amount,IsRecurring,RecurrenceType,RecurrenceEndsOn,RecurrenceSourceId,ReceiptImage) VALUES
    (@globe, '2026-01-05','Monthly internet & phone plan',     0,  999.00,1,1,NULL,NULL,NULL),
    (@dlpc,  '2026-01-10','Electricity — church hall',         6, 5800.00,1,1,NULL,NULL,NULL),
    (@water, '2026-01-10','Water bill — church premises',      6, 1200.00,1,1,NULL,NULL,NULL),
    (@church,'2026-01-15','Venue rental — worship hall',       4, 8500.00,1,1,NULL,NULL,NULL);

    -- Jan one-time
    INSERT INTO Expenses (VendorId,ExpenseDate,Description,ExpenseType,Amount,IsRecurring,RecurrenceType,RecurrenceSourceId,ReceiptImage) VALUES
    (@nbs,       '2026-01-08','Bibles and hymnals (20 pcs)',            3, 3200.00,0,1,NULL,NULL),
    (@print,     '2026-01-12','Printed Sunday bulletins — January',     3,  650.00,0,1,NULL,NULL),
    (@hardware,  '2026-01-20','Sound system repair — main speaker',     1, 2500.00,0,1,NULL,NULL),
    (@transport, '2026-01-28','Outreach trip to Mati transport',        2, 1800.00,0,1,NULL,NULL),
    -- Feb
    (@globe,     '2026-02-05','Monthly internet & phone plan',          0,  999.00,0,1,NULL,NULL),
    (@dlpc,      '2026-02-10','Electricity — church hall',              6, 6200.00,0,1,NULL,NULL),
    (@water,     '2026-02-10','Water bill — church premises',           6, 1150.00,0,1,NULL,NULL),
    (@church,    '2026-02-15','Venue rental — worship hall',            4, 8500.00,0,1,NULL,NULL),
    (@nbs,       '2026-02-14','Valentines prayer event supplies',       3,  980.00,0,1,NULL,NULL),
    (@print,     '2026-02-10','Printed Sunday bulletins — February',    3,  650.00,0,1,NULL,NULL),
    (@market,    '2026-02-22','Fellowship lunch — food supplies',       3, 3500.00,0,1,NULL,NULL),
    (@transport, '2026-02-25','Pastor transport — outreach visits',     2,  900.00,0,1,NULL,NULL),
    -- Mar
    (@globe,     '2026-03-05','Monthly internet & phone plan',          0,  999.00,0,1,NULL,NULL),
    (@dlpc,      '2026-03-10','Electricity — church hall',              6, 5600.00,0,1,NULL,NULL),
    (@water,     '2026-03-10','Water bill — church premises',           6, 1300.00,0,1,NULL,NULL),
    (@church,    '2026-03-15','Venue rental — worship hall',            4, 8500.00,0,1,NULL,NULL),
    (@nbs,       '2026-03-02','Lent devotional booklets (30 pcs)',      3, 1500.00,0,1,NULL,NULL),
    (@print,     '2026-03-08','Holy Week posters and flyers',           3,  850.00,0,1,NULL,NULL),
    (@hardware,  '2026-03-18','Projector bulb replacement',             1, 3800.00,0,1,NULL,NULL),
    (@church,    '2026-03-20','Easter celebration ministry supplies',   8, 4200.00,0,1,NULL,NULL),
    (@transport, '2026-03-29','Holy Week outreach transport',           2, 2200.00,0,1,NULL,NULL),
    -- Apr
    (@globe,     '2026-04-05','Monthly internet & phone plan',          0,  999.00,0,1,NULL,NULL),
    (@dlpc,      '2026-04-10','Electricity — church hall',              6, 5900.00,0,1,NULL,NULL),
    (@water,     '2026-04-10','Water bill — church premises',           6, 1100.00,0,1,NULL,NULL),
    (@church,    '2026-04-15','Venue rental — worship hall',            4, 8500.00,0,1,NULL,NULL),
    (@print,     '2026-04-07','Printed Sunday bulletins — April',       3,  650.00,0,1,NULL,NULL),
    (@bir,       '2026-04-14','Annual BIR registration fees',           7, 1800.00,0,1,NULL,NULL),
    (@market,    '2026-04-19','Youth camp food and snacks',             3, 5200.00,0,1,NULL,NULL),
    (@transport, '2026-04-19','Youth camp transport — 2 jeepneys',     2, 3600.00,0,1,NULL,NULL),
    (@nbs,       '2026-04-24','Youth camp activity materials',          3, 1750.00,0,1,NULL,NULL),
    -- May
    (@globe,     '2026-05-05','Monthly internet & phone plan',          0,  999.00,0,1,NULL,NULL),
    (@dlpc,      '2026-05-10','Electricity — church hall',              6, 6100.00,0,1,NULL,NULL),
    (@water,     '2026-05-10','Water bill — church premises',           6, 1250.00,0,1,NULL,NULL),
    (@church,    '2026-05-15','Venue rental — worship hall',            4, 8500.00,0,1,NULL,NULL),
    (@print,     '2026-05-05','Mothers Day event program printing',     3,  750.00,0,1,NULL,NULL),
    (@market,    '2026-05-11','Mothers Day fellowship lunch',           3, 4800.00,0,1,NULL,NULL),
    (@church,    '2026-05-18','Pentecost Sunday ministry materials',    8, 2600.00,0,1,NULL,NULL),
    (@hardware,  '2026-05-20','Chairs repair and repainting',           1, 1400.00,0,1,NULL,NULL);
END

-- ══════════════════════════════════════════════
-- MEMBERS
-- Area: Mintal=0, Downtown=1, Calinan=2, Mati=3, Toril=4
-- ══════════════════════════════════════════════
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Maria'      AND LastName='Santos')     INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Maria',     'Santos',    '', 'maria.santos@gmail.com',       1,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Jose'       AND LastName='Reyes')      INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Jose',      'Reyes',     'Jr.','jose.reyes@yahoo.com',       1,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Ana'        AND LastName='Gonzales')   INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Ana',       'Gonzales',  '', 'ana.gonzales@gmail.com',        1,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Pedro'      AND LastName='Villanueva') INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Pedro',     'Villanueva','', 'pedro.villanueva@outlook.com',  1,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Rosa'       AND LastName='Fernandez')  INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Rosa',      'Fernandez', '', 'rosa.fernandez@gmail.com',      1,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Miguel'     AND LastName='Bautista')   INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Miguel',    'Bautista',  '', 'miguel.bautista@gmail.com',     0,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Carmen'     AND LastName='Aquino')     INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Carmen',    'Aquino',    '', 'carmen.aquino@yahoo.com',       0,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Antonio'    AND LastName='Ramos')      INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Antonio',   'Ramos',     'Sr.','antonio.ramos@gmail.com',    0,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Luz'        AND LastName='Mendoza')    INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Luz',       'Mendoza',   '', 'luz.mendoza@gmail.com',         0,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Eduardo'    AND LastName='Cruz')       INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Eduardo',   'Cruz',      '', 'eduardo.cruz@outlook.com',      0,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Gloria'     AND LastName='Pascual')    INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Gloria',    'Pascual',   '', 'gloria.pascual@gmail.com',      2,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Roberto'    AND LastName='Flores')     INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Roberto',   'Flores',    '', 'roberto.flores@yahoo.com',      2,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Josefa'     AND LastName='Morales')    INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Josefa',    'Morales',   '', 'josefa.morales@gmail.com',      2,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Ernesto'    AND LastName='Navarro')    INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Ernesto',   'Navarro',   'II','ernesto.navarro@gmail.com',   2,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Maricel'    AND LastName='Domingo')    INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Maricel',   'Domingo',   '', 'maricel.domingo@outlook.com',   2,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Rodrigo'    AND LastName='Magno')      INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Rodrigo',   'Magno',     '', 'rodrigo.magno@gmail.com',       3,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Corazon'    AND LastName='Salazar')    INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Corazon',   'Salazar',   '', 'corazon.salazar@yahoo.com',     3,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Alfredo'    AND LastName='Castillo')   INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Alfredo',   'Castillo',  '', 'alfredo.castillo@gmail.com',    3,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Teresita'   AND LastName='Gomez')      INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Teresita',  'Gomez',     '', 'teresita.gomez@gmail.com',      3,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Benjamin'   AND LastName='Torres')     INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Benjamin',  'Torres',    '', 'benjamin.torres@outlook.com',   3,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Lourdes'    AND LastName='Aguilar')    INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Lourdes',   'Aguilar',   '', 'lourdes.aguilar@gmail.com',     4,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Francisco'  AND LastName='Lim')        INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Francisco', 'Lim',       '', 'francisco.lim@yahoo.com',       4,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Concepcion' AND LastName='Uy')         INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Concepcion','Uy',        '', 'concepcion.uy@gmail.com',       4,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Arsenio'    AND LastName='Tan')        INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Arsenio',   'Tan',       '', 'arsenio.tan@gmail.com',         4,1);
IF NOT EXISTS (SELECT 1 FROM Members WHERE FirstName='Pilar'      AND LastName='Chua')       INSERT INTO Members (FirstName,LastName,Suffix,Email,Area,IsActive) VALUES ('Pilar',     'Chua',      '', 'pilar.chua@outlook.com',        4,1);

-- ══════════════════════════════════════════════
-- FUNDS / CONTRIBUTIONS
-- Only insert if these members have no contributions yet
-- FundTypes: Offering=0, Tithe=1, Donation=2, OtherIncome=3
-- ══════════════════════════════════════════════
DECLARE
    @m1  INT=(SELECT Id FROM Members WHERE FirstName='Maria'      AND LastName='Santos'),
    @m2  INT=(SELECT Id FROM Members WHERE FirstName='Jose'       AND LastName='Reyes'),
    @m3  INT=(SELECT Id FROM Members WHERE FirstName='Ana'        AND LastName='Gonzales'),
    @m4  INT=(SELECT Id FROM Members WHERE FirstName='Pedro'      AND LastName='Villanueva'),
    @m5  INT=(SELECT Id FROM Members WHERE FirstName='Rosa'       AND LastName='Fernandez'),
    @m6  INT=(SELECT Id FROM Members WHERE FirstName='Miguel'     AND LastName='Bautista'),
    @m7  INT=(SELECT Id FROM Members WHERE FirstName='Carmen'     AND LastName='Aquino'),
    @m8  INT=(SELECT Id FROM Members WHERE FirstName='Antonio'    AND LastName='Ramos'),
    @m9  INT=(SELECT Id FROM Members WHERE FirstName='Luz'        AND LastName='Mendoza'),
    @m10 INT=(SELECT Id FROM Members WHERE FirstName='Eduardo'    AND LastName='Cruz'),
    @m11 INT=(SELECT Id FROM Members WHERE FirstName='Gloria'     AND LastName='Pascual'),
    @m12 INT=(SELECT Id FROM Members WHERE FirstName='Roberto'    AND LastName='Flores'),
    @m13 INT=(SELECT Id FROM Members WHERE FirstName='Josefa'     AND LastName='Morales'),
    @m14 INT=(SELECT Id FROM Members WHERE FirstName='Ernesto'    AND LastName='Navarro'),
    @m15 INT=(SELECT Id FROM Members WHERE FirstName='Maricel'    AND LastName='Domingo'),
    @m16 INT=(SELECT Id FROM Members WHERE FirstName='Rodrigo'    AND LastName='Magno'),
    @m17 INT=(SELECT Id FROM Members WHERE FirstName='Corazon'    AND LastName='Salazar'),
    @m18 INT=(SELECT Id FROM Members WHERE FirstName='Alfredo'    AND LastName='Castillo'),
    @m19 INT=(SELECT Id FROM Members WHERE FirstName='Teresita'   AND LastName='Gomez'),
    @m20 INT=(SELECT Id FROM Members WHERE FirstName='Benjamin'   AND LastName='Torres'),
    @m21 INT=(SELECT Id FROM Members WHERE FirstName='Lourdes'    AND LastName='Aguilar'),
    @m22 INT=(SELECT Id FROM Members WHERE FirstName='Francisco'  AND LastName='Lim'),
    @m23 INT=(SELECT Id FROM Members WHERE FirstName='Concepcion' AND LastName='Uy'),
    @m24 INT=(SELECT Id FROM Members WHERE FirstName='Arsenio'    AND LastName='Tan'),
    @m25 INT=(SELECT Id FROM Members WHERE FirstName='Pilar'      AND LastName='Chua');

IF NOT EXISTS (SELECT 1 FROM Funds WHERE MemberId = @m1)
BEGIN
    -- ── January 2026 ──
    INSERT INTO Funds (MemberId,Amount,ContributionType,ContributionDate,Month) VALUES
    (@m1, 2000.00,1,'2026-01-05',1),(@m1,  800.00,0,'2026-01-05',1),
    (@m2, 1500.00,1,'2026-01-05',1),(@m2,  600.00,0,'2026-01-05',1),
    (@m3, 1200.00,1,'2026-01-05',1),(@m3,  500.00,0,'2026-01-05',1),
    (@m4, 2500.00,1,'2026-01-05',1),(@m4,  700.00,0,'2026-01-05',1),
    (@m5, 1000.00,1,'2026-01-05',1),(@m5,  400.00,0,'2026-01-05',1),
    (@m6, 3000.00,1,'2026-01-05',1),(@m6,  900.00,0,'2026-01-05',1),
    (@m7, 1800.00,1,'2026-01-12',1),(@m7,  500.00,0,'2026-01-12',1),
    (@m8, 2200.00,1,'2026-01-12',1),(@m8,  600.00,0,'2026-01-12',1),
    (@m9,  900.00,1,'2026-01-12',1),(@m9,  350.00,0,'2026-01-12',1),
    (@m10,4000.00,1,'2026-01-12',1),(@m10, 800.00,0,'2026-01-12',1),
    (@m11, 500.00,0,'2026-01-12',1),(@m12, 700.00,0,'2026-01-12',1),
    (@m13, 400.00,0,'2026-01-12',1),(@m14, 600.00,0,'2026-01-12',1),
    (@m15, 500.00,0,'2026-01-19',1),(@m16, 800.00,0,'2026-01-19',1),
    (@m17, 300.00,0,'2026-01-19',1),(@m18, 600.00,0,'2026-01-19',1),
    (@m19, 700.00,0,'2026-01-19',1),(@m20, 500.00,0,'2026-01-19',1),
    (@m21, 400.00,0,'2026-01-26',1),(@m22, 600.00,0,'2026-01-26',1),
    (@m23, 500.00,0,'2026-01-26',1),(@m24, 300.00,0,'2026-01-26',1),
    (@m25, 700.00,0,'2026-01-26',1),
    (@m4, 5000.00,2,'2026-01-15',1),(@m10,10000.00,2,'2026-01-20',1);

    -- ── February 2026 ──
    INSERT INTO Funds (MemberId,Amount,ContributionType,ContributionDate,Month) VALUES
    (@m1, 2000.00,1,'2026-02-02',2),(@m1,  800.00,0,'2026-02-02',2),
    (@m2, 1500.00,1,'2026-02-02',2),(@m2,  600.00,0,'2026-02-02',2),
    (@m3, 1200.00,1,'2026-02-02',2),(@m3,  500.00,0,'2026-02-02',2),
    (@m4, 2500.00,1,'2026-02-02',2),(@m4,  700.00,0,'2026-02-02',2),
    (@m5, 1000.00,1,'2026-02-02',2),(@m5,  400.00,0,'2026-02-02',2),
    (@m6, 3000.00,1,'2026-02-02',2),(@m6,  900.00,0,'2026-02-02',2),
    (@m7,  500.00,0,'2026-02-09',2),(@m8,  600.00,0,'2026-02-09',2),
    (@m9,  350.00,0,'2026-02-09',2),(@m10,4500.00,1,'2026-02-09',2),
    (@m11, 500.00,0,'2026-02-09',2),(@m12, 700.00,0,'2026-02-09',2),
    (@m13, 400.00,0,'2026-02-16',2),(@m14, 600.00,0,'2026-02-16',2),
    (@m15, 500.00,0,'2026-02-16',2),(@m16, 800.00,0,'2026-02-16',2),
    (@m17, 300.00,0,'2026-02-16',2),(@m18, 600.00,0,'2026-02-16',2),
    (@m19, 700.00,0,'2026-02-23',2),(@m20, 500.00,0,'2026-02-23',2),
    (@m21, 400.00,0,'2026-02-23',2),(@m22, 600.00,0,'2026-02-23',2),
    (@m23, 500.00,0,'2026-02-23',2),(@m24, 300.00,0,'2026-02-23',2),
    (@m25, 700.00,0,'2026-02-23',2),
    (@m1, 2000.00,2,'2026-02-14',2),(@m6, 3000.00,2,'2026-02-14',2);

    -- ── March 2026 ──
    INSERT INTO Funds (MemberId,Amount,ContributionType,ContributionDate,Month) VALUES
    (@m1, 2000.00,1,'2026-03-02',3),(@m1,  800.00,0,'2026-03-02',3),
    (@m2, 1500.00,1,'2026-03-02',3),(@m2,  600.00,0,'2026-03-02',3),
    (@m3, 1200.00,1,'2026-03-02',3),(@m3,  500.00,0,'2026-03-02',3),
    (@m4, 2500.00,1,'2026-03-02',3),(@m4,  700.00,0,'2026-03-02',3),
    (@m5, 1000.00,1,'2026-03-02',3),(@m5,  400.00,0,'2026-03-02',3),
    (@m6, 3000.00,1,'2026-03-02',3),(@m6,  900.00,0,'2026-03-09',3),
    (@m7,  500.00,0,'2026-03-09',3),(@m8,  600.00,0,'2026-03-09',3),
    (@m9,  350.00,0,'2026-03-09',3),(@m10,4000.00,1,'2026-03-09',3),
    (@m11, 500.00,0,'2026-03-09',3),(@m12, 700.00,0,'2026-03-16',3),
    (@m13, 400.00,0,'2026-03-16',3),(@m14, 600.00,0,'2026-03-16',3),
    (@m15, 500.00,0,'2026-03-16',3),(@m16, 800.00,0,'2026-03-23',3),
    (@m17, 300.00,0,'2026-03-23',3),(@m18, 600.00,0,'2026-03-23',3),
    (@m19, 700.00,0,'2026-03-23',3),(@m20, 500.00,0,'2026-03-30',3),
    (@m21, 400.00,0,'2026-03-30',3),(@m22, 600.00,0,'2026-03-30',3),
    (@m23, 500.00,0,'2026-03-30',3),(@m24, 300.00,0,'2026-03-30',3),
    (@m25, 700.00,0,'2026-03-30',3),
    (@m4, 1500.00,0,'2026-03-29',3),(@m10,2000.00,0,'2026-03-29',3),
    (@m1, 8000.00,2,'2026-03-20',3);

    -- ── April 2026 ──
    INSERT INTO Funds (MemberId,Amount,ContributionType,ContributionDate,Month) VALUES
    (@m1, 2000.00,1,'2026-04-06',4),(@m1,  800.00,0,'2026-04-06',4),
    (@m2, 1500.00,1,'2026-04-06',4),(@m2,  600.00,0,'2026-04-06',4),
    (@m3, 1200.00,1,'2026-04-06',4),(@m3,  500.00,0,'2026-04-06',4),
    (@m4, 2500.00,1,'2026-04-06',4),(@m4,  700.00,0,'2026-04-06',4),
    (@m5, 1000.00,1,'2026-04-06',4),(@m5,  400.00,0,'2026-04-06',4),
    (@m6, 3000.00,1,'2026-04-06',4),(@m6,  900.00,0,'2026-04-13',4),
    (@m7,  500.00,0,'2026-04-13',4),(@m8,  600.00,0,'2026-04-13',4),
    (@m9,  350.00,0,'2026-04-13',4),(@m10,4000.00,1,'2026-04-13',4),
    (@m11, 500.00,0,'2026-04-13',4),(@m12, 700.00,0,'2026-04-20',4),
    (@m13, 400.00,0,'2026-04-20',4),(@m14, 600.00,0,'2026-04-20',4),
    (@m15, 500.00,0,'2026-04-20',4),(@m16, 800.00,0,'2026-04-20',4),
    (@m17, 300.00,0,'2026-04-27',4),(@m18, 600.00,0,'2026-04-27',4),
    (@m19, 700.00,0,'2026-04-27',4),(@m20, 500.00,0,'2026-04-27',4),
    (@m21, 400.00,0,'2026-04-27',4),(@m22, 600.00,0,'2026-04-27',4),
    (@m23, 500.00,0,'2026-04-27',4),(@m24, 300.00,0,'2026-04-27',4),
    (@m25, 700.00,0,'2026-04-27',4),
    (@m10,5000.00,2,'2026-04-15',4),(@m4, 3000.00,2,'2026-04-15',4);

    -- ── May 2026 ──
    INSERT INTO Funds (MemberId,Amount,ContributionType,ContributionDate,Month) VALUES
    (@m1, 2000.00,1,'2026-05-04',5),(@m1,  800.00,0,'2026-05-04',5),
    (@m2, 1500.00,1,'2026-05-04',5),(@m2,  600.00,0,'2026-05-04',5),
    (@m3, 1200.00,1,'2026-05-04',5),(@m3,  500.00,0,'2026-05-04',5),
    (@m4, 2500.00,1,'2026-05-04',5),(@m4,  700.00,0,'2026-05-04',5),
    (@m5, 1000.00,1,'2026-05-04',5),(@m5,  400.00,0,'2026-05-04',5),
    (@m6, 3000.00,1,'2026-05-04',5),(@m6,  900.00,0,'2026-05-11',5),
    (@m7,  500.00,0,'2026-05-11',5),(@m8,  600.00,0,'2026-05-11',5),
    (@m9,  350.00,0,'2026-05-11',5),(@m10,4000.00,1,'2026-05-11',5),
    (@m11, 500.00,0,'2026-05-11',5),(@m12, 700.00,0,'2026-05-11',5),
    (@m13, 400.00,0,'2026-05-11',5),(@m14, 600.00,0,'2026-05-18',5),
    (@m15, 500.00,0,'2026-05-18',5),(@m16, 800.00,0,'2026-05-18',5),
    (@m17, 300.00,0,'2026-05-18',5),(@m18, 600.00,0,'2026-05-18',5),
    (@m19, 700.00,0,'2026-05-18',5),(@m20, 500.00,0,'2026-05-18',5),
    (@m21, 400.00,0,'2026-05-18',5),(@m22, 600.00,0,'2026-05-18',5),
    (@m23, 500.00,0,'2026-05-18',5),(@m24, 300.00,0,'2026-05-18',5),
    (@m25, 700.00,0,'2026-05-18',5),
    (@m1, 1000.00,0,'2026-05-11',5),(@m3, 1500.00,0,'2026-05-11',5),
    (@m5,  800.00,0,'2026-05-11',5),(@m7, 1200.00,0,'2026-05-11',5),
    (@m10,5000.00,2,'2026-05-15',5);
END

COMMIT TRANSACTION;

SELECT 'Members'  AS [Table], COUNT(*) AS [Rows] FROM Members
UNION ALL SELECT 'Vendors',   COUNT(*) FROM Vendors
UNION ALL SELECT 'Expenses',  COUNT(*) FROM Expenses
UNION ALL SELECT 'Funds',     COUNT(*) FROM Funds;
