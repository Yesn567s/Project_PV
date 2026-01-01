CREATE DATABASE IF NOT EXISTS db_proyek_pv;
USE db_proyek_pv;

-- 1. MEMBER
CREATE TABLE IF NOT EXISTS MEMBER (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Nama VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    PASSWORD VARCHAR(50) NOT NULL,
    Tanggal_Lahir DATE NOT NULL,
    Is_Member BOOLEAN NOT NULL,
    membership_start DATE NULL,
    membership_end DATE NULL
);

-- 2. KATEGORI (NEW)
CREATE TABLE IF NOT EXISTS Kategori (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Nama VARCHAR(100) NOT NULL UNIQUE
);


-- 3. PRODUK (UPDATED)
CREATE TABLE IF NOT EXISTS Produk (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Nama VARCHAR(100) NOT NULL,
    Merk VARCHAR(100), -- Merek barang
    Harga INT NOT NULL,
    kategori_id INT NOT NULL,
    tag VARCHAR(255), -- Untuk Promo
    image_url VARCHAR(500),
    FOREIGN KEY (kategori_id) REFERENCES Kategori(ID)
);

-- 6. PROMO (Keep as reference to product name or change to FK later)
CREATE TABLE Promo (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Nama_Promo VARCHAR(100) NOT NULL,
    
    -- TARGET: Promo ini ditujukan untuk apa?
    -- Pilihannya: 'Produk', 'Kategori', 'Tag', 'Merk', 'Global'
    Target_Type ENUM('Produk', 'Kategori', 'Tag', 'Merk', 'Global') NOT NULL,
    Target_Value VARCHAR(100) NOT NULL, -- Berisi ID (Produk/Kategori) atau Nama (Merk/Tag)
    
    -- JENIS LOGIKA: Cara hitung harganya
    -- 'Harga_Jadi' : Harga langsung berubah (e.g. 12rb -> 9.900)
    -- 'Persen'     : Potongan %
    -- 'Grosir'     : Beli banyak harga paket (e.g. 3 biji 11rb)
    -- 'Bonus'      : Hadiah barang (Buy X Get Y atau Get Z)
    Jenis_Promo ENUM('Harga_Jadi', 'Persen', 'Grosir', 'Bonus') NOT NULL,
    
    -- NILAI PENDUKUNG
    Nilai_Potongan FLOAT DEFAULT 0,    -- Untuk isi % atau Nominal potongan
    Harga_Baru INT DEFAULT NULL,       -- Untuk Harga_Jadi atau Harga_Grosir_Paket
    Min_Qty INT DEFAULT 1,             -- Minimal beli untuk aktifkan promo
    
    -- KHUSUS HADIAH (Beli Kalkulator Gratis Bolpen)
    Bonus_Produk_ID INT DEFAULT NULL,  -- ID barang yang digratiskan
    Gratis_Qty INT DEFAULT 0,          -- Jumlah barang gratis
    
    START DATETIME DEFAULT CURRENT_TIMESTAMP,
    END DATETIME DEFAULT NULL,
    FOREIGN KEY (Bonus_Produk_ID) REFERENCES Produk(ID)
);

-- 7. PROMO SPECIAL (kept as is)
CREATE TABLE IF NOT EXISTS Promo_Special (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Nama_Promo VARCHAR(100) NOT NULL,
    Produk VARCHAR(100) NOT NULL,
    Kategori ENUM('Usia') NOT NULL,
    Tag VARCHAR(255),
    START DATETIME DEFAULT CURRENT_TIMESTAMP(),
    END DATETIME DEFAULT NULL
);

-- 8. HTRANSAKSI (NEW)
CREATE TABLE IF NOT EXISTS Transaksi (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    member_id INT NULL,
    Tanggal DATETIME DEFAULT CURRENT_TIMESTAMP(),
    Total INT NOT NULL DEFAULT 0
);

-- 9. DTRANSAKSI DETAIL (NEW)
CREATE TABLE IF NOT EXISTS Transaksi_Detail (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    transaksi_id INT NOT NULL,
    produk_id INT NOT NULL,
    Qty INT NOT NULL,
    Harga INT NOT NULL, -- harga saat transaksi (avoid price change issues)
    Subtotal INT GENERATED ALWAYS AS (Qty * Harga) STORED,
    FOREIGN KEY (transaksi_id) REFERENCES Transaksi(ID)
);

INSERT INTO MEMBER
(Nama, Email, PASSWORD, Tanggal_Lahir, Is_Member, membership_start, membership_end)
VALUES
-- NON MEMBERS
('Kevin Setiono', 'kevin@gmail.com', '123', '2003-08-12', FALSE, NULL, NULL),
('Budi Santoso', 'budi@gmail.com', '123', '1995-11-05', FALSE, NULL, NULL),
('Cindy Wijaya', 'cindy@gmail.com', '123', '2001-06-18', FALSE, NULL, NULL),

-- PERMANENT MEMBERS
('Evelyn Kurnia', 'evelyn@gmail.com', '123', '2004-02-14', TRUE, '2025-01-20', NULL),
('Gracia Tania', 'gracia@gmail.com', '123', '2000-01-30', TRUE, '2025-02-01', NULL),
('Indah Pertiwi', 'indah@gmail.com', '123', '2002-07-09', TRUE, '2025-03-10', NULL),

-- TEMPORARY MEMBERS (≥ 1 month)
('Alicia Putri', 'alicia@gmail.com', '123', '1999-03-22', TRUE, '2025-04-01', '2026-03-18'),
('Davin Pratama', 'davin@gmail.com', '123', '1998-12-29', TRUE, '2025-03-15', '2026-07-09'),
('Fajar Nugroho', 'fajar@gmail.com', '123', '1997-09-10', TRUE, '2025-02-01', '2026-01-27'),
('Hendra Wijaya', 'hendra@gmail.com', '123', '1996-05-25', TRUE, '2025-01-10', '2026-11-05');

INSERT INTO Kategori (Nama) VALUES
('Food & Beverage'),
('Electronic'),
('Apparel'),
('Health & Beauty'),
('Entertainment');


INSERT INTO Produk (Nama, Merk, Harga, kategori_id, tag, image_url) VALUES
-- KATEGORI 1: Food & Beverage
('Sedapmie', 'Sedaap', 3500, 1, 'Mie Instan',''),
('Indomie', 'Indofood', 3500, 1, 'Mie Instan',''),
('Indomie Goreng', 'Indofood', 3500, 1, 'Mie Instan',''),
('Gula Rose Brand 1 kg', 'Rose Brand', 18500, 1, 'Sembako, Gula',''),
('Gula Rose Brand Hijau 1 kg', 'Rose Brand', 18500, 1, 'Sembako, Gula','https://c.alfagift.id/product/1/1_A09170277363_20200824132308167_base.jpg'),
('Minyak Goreng Hemart 900 ml', 'Hemart', 18500, 1, 'Sembako, Minyak','https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full/catalog-image/104/MTA-182275096/brd-29831_minyak-goreng-hemart-kemasan-botol-1-liter-free-packing-kardus_full07-99ee99e7.jpg'),
('Minyak Goreng Fitri 800 ml', 'Fitri', 17500, 1, 'Sembako, Minyak',''),
('Minyak Goreng 800 ml', 'Umum', 17000, 1, 'Sembako, Minyak','https://c.alfagift.id/product/1/1_A09350045270_20240122094000330_base.jpg'),
('Beras Raja Lele 5 kg', 'Raja Lele', 80000, 1, 'Sembako, Beras','https://arti-assets.sgp1.cdn.digitaloceanspaces.com/megaswalayan/products/8abfc71d-1c5a-4bca-baf0-3c5a570cd203.jpg'),
('Beras Rojo Lele 5 kg', 'Rojo Lele', 80000, 1, 'Sembako, Beras','https://arti-assets.sgp1.cdn.digitaloceanspaces.com/megaswalayan/products/8abfc71d-1c5a-4bca-baf0-3c5a570cd203.jpg'),
('Beras Cap Uduk 5 kg', 'Cap Uduk', 82000, 1, 'Sembako, Beras','https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full/catalog-image/104/MTA-177755612/nasi-uduk_beras-premium-uduk-hijau-5kg_full01.jpg'),
('Beras Anggur 5 kg', 'Anggrus', 82000, 1, 'Sembako, Beras','https://parto.id/asset/foto_produk/beras_naggur_jpg_171878826074.jpg'),
('Beras Enak 5 kg', 'Enak', 78000, 1, 'Sembako, Beras','https://supermama.co.id/wp-content/uploads/2022/09/PNG-UENAK.png'),
('Beras Enak 3 kg', 'Enak', 50000, 1, 'Sembako, Beras','https://supermama.co.id/wp-content/uploads/2022/09/PNG-UENAK.png'),
('Beras Sumo Kuning 5 kg', 'Sumo', 85000, 1, 'Sembako, Beras','https://p16-images-sign-sg.tokopedia-static.net/tos-alisg-i-aphluv4xwc-sg/img/VqbcmM/2025/7/17/d77c48f0-8629-43fd-b2e3-4796ab6816e2.jpg~tplv-aphluv4xwc-resize-jpeg:700:0.jpeg?lk3s=0ccea506&x-expires=1767027165&x-signature=fickjhtchDYMwaO8Cu%2BgIgTC3Dc%3D&x-signature-webp=yGSOIU%2Fz0j8I87pm3qC0cDqaBP8%3D'),
('Air Cleo 220 ml', 'Cleo', 2000, 1, 'Minuman, Air Mineral','https://cf.shopee.co.id/file/id-11134207-7rbk0-mam1t88zo31n0e'),
('Air Cleo 220 ml Isi 24', 'Cleo', 18500, 1, 'Minuman, Air Mineral','https://p16-images-sign-sg.tokopedia-static.net/tos-alisg-i-aphluv4xwc-sg/5a08eb1fb35e4a95b8b9ea39d295be2a~tplv-aphluv4xwc-resize-jpeg:700:0.jpeg?lk3s=0ccea506&x-expires=1767027282&x-signature=vIj4NNqqI5qYCq3EYjfK69ty6JE%3D&x-signature-webp=okJLs4ARvWFQB40C17EUU9OdbBA%3D'),
('Air Pristine 400 ml', 'Pristine', 3500, 1, 'Minuman, Air Mineral',''),
('Cleo Air Mineral 550 ml Isi 6', 'Cleo', 12000, 1, 'Minuman, Air Mineral','https://solvent-production.s3.amazonaws.com/media/images/products/2021/05/IMG20210201155138.jpg'),
('Pristine Air Mineral 600 ml', 'Pristine', 4500, 1, 'Minuman, Air Mineral',''),
('Pristine Air Mineral 1500 ml', 'Pristine', 8000, 1, 'Minuman, Air Mineral','https://c.alfagift.id/product/1/1_A8262900002167_20250516142237557_base.jpg'),
('Ameria Biscuit Columbia', 'Columbia', 28000, 1, 'Biskuit','https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTG4mww39cvVE7VzODsx7qKkZLha3gYWFDjhQ&s'),
('Biskuit Columbia Toples', 'Columbia', 15000, 1, 'Biskuit','https://www.sumberpangansukses.com/assets/produk/d98d74dd7aa145fab0216c2bdf72271e.jpg'),

-- KATEGORI 2: Electronic
('Flashdisk Lexar V40 8 GB', 'Lexar', 45000, 2, 'Flashdisk',''),
('Flashdisk Lexar V40 16 GB', 'Lexar', 55000, 2, 'Flashdisk','https://planetcomputer.co.id/wp-content/uploads/2024/08/67342744_cc42ab57-cf1a-43e9-8667-95a259c3f88d_640_640.jpg'),
('Flashdisk Lexar V40 32 GB', 'Lexar', 65000, 2, 'Flashdisk','https://planetcomputer.co.id/wp-content/uploads/2024/08/23867496_5ece3732-b278-42ae-b05d-8d026331f9ff_700_700-600x600.jpg'),
('Flashdisk Lexar V40 64 GB', 'Lexar', 75000, 2, 'Flashdisk','https://pegastore.id/media/product/produk-1737532395.jpg'),
('Kalkulator M&G Mgc-10', 'M&G', 110000, 2, 'Kalkulator',''),
('Kalkulator Scientific Casio FX-570ES Plus', 'Casio', 451500, 2, 'Kalkulator','https://www.casio.com/content/dam/casio/product-info/locales/id/id/calc/product/scientific/F/FX/FX5/fx-570esplus-2bu/assets/fx-570ES_PLUS-2BU_F.png.transform/main-visual-pc/image.png'),
('Kalkulator Scientific Casio FX-82ES Plus', 'Casio', 287000, 2, 'Kalkulator','https://www.casio.com/content/dam/casio/product-info/locales/id/id/calc/product/scientific/F/FX/FX8/fx-82ESPLUS-2/assets/fx-82ES_PLUS-2_F.png.transform/main-visual-pc/image.png'),
('Kalkulator Scientific Casio FX-991ES Plus', 'Casio', 481000, 2, 'Kalkulator','https://www.casio.com/content/dam/casio/product-info/locales/id/id/calc/product/scientific/F/FX/FX9/fx-991esplus-2pk/assets/fx-991ES_PLUS-2PK_F.png.transform/main-visual-pc/image.png'),
('Kalkulator Joyko CC-40', 'Joyko', 52000, 2, 'Kalkulator','https://www.joyko.co.id/image/cache/data/CC-40-01-650x650.jpg'),
('Kalkulator Scientific Joyko CC-25BP', 'Joyko', 45000, 2, 'Kalkulator','https://www.joyko.co.id/image/cache/data/COVER-25-650x650.jpg'),
('Jete Power Bank B7 10000 Mah', 'Jete', 350000, 2, 'Power Bank','https://jete.id/wp-content/uploads/2025/03/Desc-Powerbank-JETE-B7-10000-New-Color-18-600x600.jpg'),
('Vivan Vpb-D11 10.000 Mah', 'Vivan', 250000, 2, 'Power Bank','https://down-id.img.susercontent.com/file/id-11134207-8224x-mgdspnybrjt584'),
('Vtec Magnet Set Ms-8074', 'V-Tec', 18000, 2, 'Magnet',''),
('Kipas Portable', 'Umum', 20000, 2, 'Elektronik','https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full/catalog-image/MTA-11951965/kalno_kipas_angin_portable_mini_fan_terbaru_ada_holder_hp_-_mini_fan_usb_charging_rechargeable_l18_random_full01_hmr2qv00.jpg'),

-- KATEGORI 3: Apparel
('Jas Hujan Jaket Celana Elmondo', 'Elmondo', 60000, 3, 'Jas Hujan','https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRo_KMIx6pig3UZ0LNFiqukIGbPIZOxk_nPBg&s'),
('Jas Hujan Elmondo Celana Jaket 935', 'Elmondo', 45000, 3, 'Jas Hujan','https://down-id.img.susercontent.com/file/id-11134207-7rasg-m3rzjj7rj573fa'),
('Jas Hujan Cap Kapak', 'Kapak', 48000, 3, 'Jas Hujan','https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRaa9SDmJWW_WebUHj7UYtYMPQVYmBgzTA5Ng&s'),
('Jas Hujan Kiddy Rets', 'Kiddy', 44000, 3, 'Jas Hujan','https://down-id.img.susercontent.com/file/sg-11134201-22110-hd4pzlm4vsjve0'),
('Tas Sekolah', 'Umum', 150000, 3, 'Tas',''),
('Kaos Kaki Remaja', 'Umum', 12000, 3, 'Pakaian',''),
('Celana Pendek', 'Umum', 35000, 3, 'Pakaian',''),
('Topi Natal Glitter', 'Umum', 20000, 3, 'Natal',''),

-- KATEGORI 4: Health & Beauty / Home Care
('Tissue See U 250 Sheet', 'See U', 8500, 4, 'Tissue',''),
('Tissue Basah Mitu Wipes', 'Mitu', 13000, 4, 'Tissue Basah',''),
('Tissue Paseo', 'Paseo', 28000, 4, 'Tissue',''),
('Tissue Fusia Jumbo 1000 Sheet', 'Fusia', 30000, 4, 'Tissue',''),
('Hit Aerosol 600 ml', 'Hit', 38000, 4, 'Anti Nyamuk',''),
('Masker Duckbill Alkindo Isi 50', 'Alkindo', 15000, 4, 'Kesehatan',''),
('Freshcare', 'Freshcare', 14000, 4, 'Kesehatan',''),
('Kifa Sabun Cuci Piring', 'Kifa', 8500, 4, 'Sabun',''),
('Lem Tikus Gajah', 'Gajah', 18000, 4, 'Kebersihan',''),

-- KATEGORI 5: Entertainment / Stationery / Household
('Buku Tulis Kiky 38 Lembar', 'Kiky', 28000, 5, 'Buku Tulis',''),
('Buku Tulis Sinar Dunia 32 Lembar', 'Sidu', 30000, 5, 'Buku Tulis',''),
('Bolpen Sarasa', 'Zebra', 16000, 5, 'Alat Tulis',''),
('Bolpen AE7 1 Lusin', 'Standard', 22000, 5, 'Alat Tulis',''),
('Crayon Greebel 55 Warna', 'Greebel', 85000, 5, 'Alat Tulis',''),
('Magic Clay', 'Umum', 6000, 5, 'Mainan',''),
('Bubble Stick', 'Umum', 8000, 5, 'Mainan',''),
('Meja Belajar Anak', 'Umum', 65000, 5, 'Furniture',''),
('Aneka Kursi', 'Umum', 35000, 5, 'Furniture',''),
('Mangkok Sultan', 'Sultan', 12000, 5, 'Peralatan Rumah',''),
('Termos Sultan 2 Cangkir', 'Sultan', 35000, 5, 'Peralatan Rumah',''),
('Botol Minum Cetek Sedotan Mix 900 ml', 'Umum', 18000, 5, 'Botol',''),
('Gantungan Kunci', 'Umum', 5000, 5, 'Aksesoris',''),
('Jam Dinding Mayomi Type 917', 'Mayomi', 30000, 5, 'Dekorasi',''),
('HVS Sinarline A4', 'Sinarline', 40000, 5, 'Kertas',''),
('HVS Sidu 70 gsm A4', 'Sidu', 42000, 5, 'Kertas',''),
('Odner Bantex 1461', 'Bantex', 35000, 5, 'ATK','');

-- BULAN 7
INSERT INTO Promo (Nama_Promo, Target_Type, Target_Value, Jenis_Promo, Nilai_Potongan, Harga_Baru, Min_Qty, Bonus_Produk_ID, Gratis_Qty, START, END) VALUES
-- 1 Juli 2025
('Promo Biasa', 'Produk', 'Tissue See U', 'Harga_Jadi', 0, 1440, 1, NULL, 0, '2025-07-01 00:00:00', '2025-07-01 23:59:00'),
('Promo Biasa', 'Produk', 'Gantungan Kunci', 'Grosir', 0, 11000, 3, NULL, 0, '2025-07-01 00:00:00', '2025-07-01 23:59:00'),
('Promo Biasa', 'Produk', 'Beras Raja Lele 5 kg', 'Harga_Jadi', 0, 73500, 1, NULL, 0, '2025-07-01 00:00:00', '2025-07-01 23:59:00'),

-- 3 Juli 2025
('Promo Biasa', 'Produk', 'Air Minum Cleo 24 Botol', 'Harga_Jadi', 0, 3980, 1, NULL, 0, '2025-07-03 00:00:00', '2025-07-03 23:59:00'),
('Promo Biasa', 'Produk', 'Bolpoin Quantum Selusin', 'Harga_Jadi', 0, 3000, 1, NULL, 0, '2025-07-03 00:00:00', '2025-07-03 23:59:00'),
('Promo Biasa', 'Produk', 'Pensil Squeesy Selusin', 'Harga_Jadi', 0, 3000, 1, NULL, 0, '2025-07-03 00:00:00', '2025-07-03 23:59:00'),
('Promo Biasa', 'Produk', 'Bolpoin Gel Kokoro', 'Harga_Jadi', 0, 4600, 1, NULL, 0, '2025-07-03 00:00:00', '2025-07-03 23:59:00'),
('Promo Biasa', 'Produk', 'Crayon Murah Vanart', 'Harga_Jadi', 0, 6000, 1, NULL, 0, '2025-07-03 00:00:00', '2025-07-03 23:59:00'),

-- PROMO GO TO SCHOOL (3 Juli 2025)
('PROMO GO TO SCHOOL', 'Produk', 'Buku Tulis Kiky 32 Lembar', 'Harga_Jadi', 0, 21000, 1, NULL, 0, '2025-07-03 00:00:00', '2025-07-03 23:59:00'),
('PROMO GO TO SCHOOL', 'Produk', 'Buku Tulis Dg', 'Harga_Jadi', 0, 22000, 1, NULL, 0, '2025-07-03 00:00:00', '2025-07-03 23:59:00'),
('PROMO GO TO SCHOOL', 'Produk', 'Buku Tulis Skola 38 Lembar', 'Harga_Jadi', 0, 23400, 1, NULL, 0, '2025-07-03 00:00:00', '2025-07-03 23:59:00'),
('PROMO GO TO SCHOOL', 'Produk', 'Buku Tulis Sinar Dunia 32 Lembar', 'Harga_Jadi', 0, 26000, 1, NULL, 0, '2025-07-03 00:00:00', '2025-07-03 23:59:00'),
('PROMO GO TO SCHOOL', 'Produk', 'Kotak Pensil', 'Harga_Jadi', 0, 4500, 1, NULL, 0, '2025-07-03 00:00:00', '2025-07-03 23:59:00'),
('PROMO GO TO SCHOOL', 'Produk', 'Stiker Box Isi 100', 'Harga_Jadi', 0, 8000, 1, NULL, 0, '2025-07-03 00:00:00', '2025-07-03 23:59:00'),
('PROMO GO TO SCHOOL', 'Produk', 'Tissue See U 250 Sheet', 'Harga_Jadi', 0, 6888, 1, NULL, 0, '2025-07-03 00:00:00', '2025-07-03 23:59:00'),
('PROMO GO TO SCHOOL', 'Produk', 'Tas Sekolah', 'Persen', 15, NULL, 1, NULL, 0, '2025-07-03 00:00:00', '2025-07-03 23:59:00'),
('PROMO GO TO SCHOOL', 'Produk', 'Meja Belajar', 'Harga_Jadi', 0, 49900, 1, NULL, 0, '2025-07-03 00:00:00', '2025-07-03 23:59:00'),

-- 4 Juli 2025
('Promo Biasa', 'Produk', 'Botol Minum 800 ml', 'Harga_Jadi', 0, 16000, 1, NULL, 0, '2025-07-04 00:00:00', '2025-07-04 23:59:00'),
('Promo Biasa', 'Produk', 'Beras Rojo Lele', 'Harga_Jadi', 0, 72500, 1, NULL, 0, '2025-07-04 00:00:00', '2025-07-04 23:59:00'),

-- 18 - 21 Juli 2025
('Promo Biasa', 'Produk', 'Cleo Air Mineral 550 ml Isi 6', 'Harga_Jadi', 0, 10000, 1, NULL, 0, '2025-07-18 00:00:00', '2025-07-21 23:59:00'),
('Promo Biasa', 'Produk', 'Buku Tulis Sinar Dunia 32 Lembar', 'Harga_Jadi', 0, 25900, 1, NULL, 0, '2025-07-18 00:00:00', '2025-07-21 23:59:00'),
('Promo Biasa', 'Produk', 'Buku Tulis Kraft Sampul Coklat 40 Lembar', 'Harga_Jadi', 0, 27900, 1, NULL, 0, '2025-07-18 00:00:00', '2025-07-21 23:59:00'),
('Promo Biasa', 'Produk', 'Buku Tulis Big Boss', 'Harga_Jadi', 0, 23000, 1, NULL, 0, '2025-07-18 00:00:00', '2025-07-21 23:59:00'),
('Promo Biasa', 'Produk', 'Buku Tulis Kiky', 'Harga_Jadi', 0, 21000, 1, NULL, 0, '2025-07-18 00:00:00', '2025-07-21 23:59:00'),
('Promo Biasa', 'Produk', 'Mainan Clay', 'Grosir', 0, 5900, 3, NULL, 0, '2025-07-18 00:00:00', '2025-07-21 23:59:00'),
('Promo Biasa', 'Produk', 'Mainan Clay', 'Harga_Jadi', 0, 6400, 1, NULL, 0, '2025-07-18 00:00:00', '2025-07-21 23:59:00'),
('Promo Biasa', 'Produk', 'Tissue Basah Mitu Wipes', 'Bonus', 0, 10900, 1, (SELECT ID FROM Produk WHERE Nama='Tissue Basah Mitu Wipes' LIMIT 1), 1, '2025-07-18 00:00:00', '2025-07-21 23:59:00'),
('Promo Biasa', 'Produk', 'Kotak Pensil Rakit', 'Grosir', 0, 9000, 3, NULL, 0, '2025-07-18 00:00:00', '2025-07-21 23:59:00'),
('Promo Biasa', 'Produk', 'Gelas Viola', 'Grosir', 0, 10000, 3, NULL, 0, '2025-07-18 00:00:00', '2025-07-21 23:59:00'),
('Promo Biasa', 'Produk', 'Gantungan Kunci', 'Grosir', 0, 11000, 3, NULL, 0, '2025-07-18 00:00:00', '2025-07-21 23:59:00'),

-- 19 Juli 2025
('Promo Biasa', 'Produk', 'Cleo Air Mineral 550 ml Isi 6', 'Harga_Jadi', 0, 10000, 1, NULL, 0, '2025-07-19 00:00:00', '2025-07-19 23:59:00'),
('Promo Biasa', 'Produk', 'Bolpoin Gel Kokoro', 'Harga_Jadi', 0, 4666, 1, NULL, 0, '2025-07-19 00:00:00', '2025-07-19 23:59:00'),
('Promo Biasa', 'Produk', 'Bolpen Kokoro Isi 13 pcs', 'Harga_Jadi', 0, 59900, 1, NULL, 0, '2025-07-19 00:00:00', '2025-07-19 23:59:00'),
('Promo Biasa', 'Produk', 'Beras Enak 5 kg', 'Harga_Jadi', 0, 73500, 1, NULL, 0, '2025-07-19 00:00:00', '2025-07-19 23:59:00'),

-- 20 Juli 2025
('Promo Biasa', 'Produk', 'Beras Enak 3 kg', 'Harga_Jadi', 0, 46000, 1, NULL, 0, '2025-07-20 00:00:00', '2025-07-20 23:59:00'),
('Promo Biasa', 'Produk', 'Beras Sumo Kuning 5 kg', 'Harga_Jadi', 0, 80000, 1, NULL, 0, '2025-07-20 00:00:00', '2025-07-20 23:59:00'),
('Promo Biasa', 'Produk', 'Indomie Goreng', 'Harga_Jadi', 0, 2900, 1, NULL, 0, '2025-07-20 00:00:00', '2025-07-20 23:59:00'),

-- 23 Juli - 31 Agustus 2025
('Promo Biasa', 'Produk', 'Toples Biskuit', 'Grosir', 0, 24000, 2, NULL, 0, '2025-07-23 00:00:00', '2025-08-31 23:59:00'),

-- 24 Juli 2025
('Promo Biasa', 'Produk', 'Parcel Mangkok Biskuit', 'Harga_Jadi', 0, 35000, 1, NULL, 0, '2025-07-24 00:00:00', '2025-07-30 23:59:00'),
('Promo Biasa', 'Produk', 'Kotak Makan', 'Harga_Jadi', 0, 6000, 1, NULL, 0, '2025-07-24 00:00:00', '2025-07-24 23:59:00'),
('Promo Biasa', 'Produk', 'Botol Minum', 'Harga_Jadi', 0, 6000, 1, NULL, 0, '2025-07-24 00:00:00', '2025-07-24 23:59:00'),

-- 28 Juli 2025
('Promo Biasa', 'Produk', 'Biskuit Columbia Toples', 'Bonus', 0, 12000, 1, (SELECT ID FROM Produk WHERE Nama='Biskuit Columbia Toples' LIMIT 1), 1, '2025-07-28 00:00:00', '2025-07-28 23:59:00'),
('Promo Biasa', 'Produk', 'Alat Tulis Serba 5000', 'Harga_Jadi', 0, 5000, 1, NULL, 0, '2025-07-28 00:00:00', '2025-07-28 23:59:00'),
('Promo Biasa', 'Produk', 'Beras Super Enak 5 kg', 'Harga_Jadi', 0, 73500, 1, NULL, 0, '2025-07-28 00:00:00', '2025-07-28 23:59:00');

-- BULAN 8
INSERT INTO Promo (Nama_Promo, Target_Type, Target_Value, Jenis_Promo, Nilai_Potongan, Harga_Baru, Min_Qty, Bonus_Produk_ID, Gratis_Qty, START, END) VALUES
-- PROMO SPESIAL KEMERDEKAAN (2 - 31 Agustus 2025)
('Promo Spesial Kemerdekaan', 'Produk', 'Aneka Hiasan Agustusan', 'Harga_Jadi', 0, 5000, 1, NULL, 0, '2025-08-02 00:00:00', '2025-08-31 23:59:00'),
('Promo Spesial Kemerdekaan', 'Produk', 'Parcel Event Agustusan', 'Harga_Jadi', 0, 0, 1, NULL, 0, '2025-08-02 00:00:00', '2025-08-31 23:59:00'), -- Cashback (Harga diatur 0 sebagai simbolik/disesuaikan sistem)
('Promo Spesial Kemerdekaan', 'Produk', 'Alat Tulis Serba 5000', 'Harga_Jadi', 0, 5000, 1, NULL, 0, '2025-08-02 00:00:00', '2025-08-31 23:59:00'),
('Promo Spesial Kemerdekaan', 'Produk', 'Koleksi Tung-Tung Sahur', 'Harga_Jadi', 0, 0, 1, NULL, 0, '2025-08-02 00:00:00', '2025-08-31 23:59:00'), -- Cashback
('Promo Spesial Kemerdekaan', 'Produk', 'Tas Sekolah', 'Persen', 15, NULL, 1, NULL, 0, '2025-08-02 00:00:00', '2025-08-31 23:59:00'),
('Promo Spesial Kemerdekaan', 'Produk', 'Meja Belajar', 'Harga_Jadi', 0, 48000, 1, NULL, 0, '2025-08-02 00:00:00', '2025-08-31 23:59:00'),
('Promo Spesial Kemerdekaan', 'Produk', 'Indomie Goreng', 'Harga_Jadi', 0, 2900, 1, NULL, 0, '2025-08-02 00:00:00', '2025-08-31 23:59:00'),
('Promo Spesial Kemerdekaan', 'Produk', 'Gula Rose Brand 1 kg', 'Harga_Jadi', 0, 17500, 1, NULL, 0, '2025-08-02 00:00:00', '2025-08-31 23:59:00'),
('Promo Spesial Kemerdekaan', 'Produk', 'Beras Super Enak 5 kg', 'Harga_Jadi', 0, 73500, 1, NULL, 0, '2025-08-02 00:00:00', '2025-08-31 23:59:00'),

-- Promo 5 Agustus 2025
('Promo Biasa', 'Produk', 'Biskuit Columbia Toples', 'Bonus', 0, 12000, 1, (SELECT ID FROM Produk WHERE Nama='Biskuit Columbia Toples' LIMIT 1), 1, '2025-08-05 00:00:00', '2025-08-05 23:59:00'),
('Promo Biasa', 'Produk', 'Indomie Goreng', 'Harga_Jadi', 0, 2900, 1, NULL, 0, '2025-08-05 00:00:00', '2025-08-05 23:59:00'),
('Promo Biasa', 'Produk', 'Minyak Goreng', 'Harga_Jadi', 0, 16500, 1, NULL, 0, '2025-08-05 00:00:00', '2025-08-05 23:59:00'),
('Promo Biasa', 'Produk', 'Termos Sultan Set', 'Harga_Jadi', 0, 29000, 1, NULL, 0, '2025-08-05 00:00:00', '2025-08-05 23:59:00'),
('Promo Biasa', 'Produk', 'Parcel Sembako', 'Harga_Jadi', 0, 50000, 1, NULL, 0, '2025-08-05 00:00:00', '2025-08-05 23:59:00'),

-- Batch 20 - 31 Agustus 2025 (Elektronik & ATK)
('Promo Biasa', 'Produk', 'Flashdisk Lexar V40 8GB', 'Harga_Jadi', 0, 37000, 1, NULL, 0, '2025-08-20 00:00:00', '2025-08-31 23:59:00'),
('Promo Biasa', 'Produk', 'Flashdisk Lexar V40 64GB', 'Harga_Jadi', 0, 59000, 1, NULL, 0, '2025-08-20 00:00:00', '2025-08-31 23:59:00'),
('Promo Biasa', 'Tag', 'Kalkulator', 'Persen', 20, NULL, 1, NULL, 0, '2025-08-20 00:00:00', '2025-08-31 23:59:00'),
('Promo Biasa', 'Tag', 'Loose Leaf', 'Persen', 5, NULL, 1, NULL, 0, '2025-08-20 00:00:00', '2025-08-31 23:59:00'),
('Promo Biasa', 'Tag', 'Binder', 'Persen', 5, NULL, 1, NULL, 0, '2025-08-20 00:00:00', '2025-08-31 23:59:00'),
('Promo Biasa', 'Produk', 'Bolpen Gel Kokoro Black 12+1', 'Bonus', 0, 60000, 12, (SELECT ID FROM Produk WHERE Nama='Bolpen Gel Kokoro Black 12+1' LIMIT 1), 1, '2025-08-20 00:00:00', '2025-08-31 23:59:00'),

-- Akhir Agustus (29 - 31 Agustus 2025)
('Promo Biasa', 'Produk', 'Cleo 220 ml Isi 24 Botol', 'Harga_Jadi', 0, 16500, 1, NULL, 0, '2025-08-29 00:00:00', '2025-08-31 23:59:00'),
('Promo Biasa', 'Tag', 'Indomie', 'Harga_Jadi', 0, 2800, 1, NULL, 0, '2025-08-29 00:00:00', '2025-08-31 23:59:00'),
('Promo Biasa', 'Produk', 'Hit Anti Nyamuk 600 ml', 'Harga_Jadi', 0, 29500, 1, NULL, 0, '2025-08-29 00:00:00', '2025-08-31 23:59:00'),
('Promo Biasa', 'Produk', 'Kertas Kado Squeesy', 'Bonus', 0, 3000, 1, (SELECT ID FROM Produk WHERE Nama='Kertas Kado Squeesy' LIMIT 1), 1, '2025-08-29 00:00:00', '2025-08-31 23:59:00');

-- BULAN 9
INSERT INTO Promo (Nama_Promo, Target_Type, Target_Value, Jenis_Promo, Nilai_Potongan, Harga_Baru, Min_Qty, START, END) VALUES
-- Batch 2 September
('Promo Biasa', 'Produk', 'Jas Hujan Plastik Jaket Celana Model', 'Harga_Jadi', 0, 6500, 1, '2025-09-02 00:00:00', '2025-09-02 23:59:00'),
('Promo Biasa', 'Produk', 'Jas Hujan Plastik Poncho Dewasa', 'Harga_Jadi', 0, 4500, 1, '2025-09-02 00:00:00', '2025-09-02 23:59:00'),
('Promo Biasa', 'Produk', 'Cleo 550 ml 6 Botol', 'Harga_Jadi', 0, 11000, 1, '2025-09-02 00:00:00', '2025-09-02 23:59:00'),

-- Batch 4 September
('Promo Biasa', 'Produk', 'Diamond Painting Diy Love', 'Harga_Jadi', 0, 10000, 1, '2025-09-04 00:00:00', '2025-09-04 23:59:00'),

-- Batch 23 September
('Promo Biasa', 'Produk', 'Jumbo Tumbler Bricks', 'Harga_Jadi', 0, 11700, 1, '2025-09-23 00:00:00', '2025-09-23 23:59:00'),
('Promo Biasa', 'Produk', 'Jumbo Mainan Bricks', 'Harga_Jadi', 0, 11700, 1, '2025-09-23 00:00:00', '2025-09-23 23:59:00'),
('Promo Biasa', 'Produk', 'Tumblr Mainan Bricks', 'Harga_Jadi', 0, 11700, 1, '2025-09-23 00:00:00', '2025-09-23 23:59:00'),
('Promo Biasa', 'Produk', 'Squeezy Empat Pensil Rakit', 'Harga_Jadi', 0, 7000, 1, '2025-09-23 00:00:00', '2025-09-23 23:59:00'),

-- Batch 25 September (Calculators)
('Promo Biasa', 'Produk', 'M&G Scientific Calculator 552 Function', 'Harga_Jadi', 0, 335250, 1, '2025-09-25 00:00:00', '2025-09-25 23:59:00'),
('Promo Biasa', 'Produk', 'M&G 12 Digits Scientist Calculator MG-991ES', 'Harga_Jadi', 0, 202050, 1, '2025-09-25 00:00:00', '2025-09-25 23:59:00'),
('Promo Biasa', 'Produk', 'M&G Scientific Calculator 401 Function', 'Harga_Jadi', 0, 145530, 1, '2025-09-25 00:00:00', '2025-09-25 23:59:00'),
('Promo Biasa', 'Produk', 'M&G Scientific Calculator 240 Function', 'Harga_Jadi', 0, 90000, 1, '2025-09-25 00:00:00', '2025-09-25 23:59:00'),
('Promo Biasa', 'Produk', 'M&G Scientific Calculator Sakura Rain', 'Harga_Jadi', 0, 151740, 1, '2025-09-25 00:00:00', '2025-09-25 23:59:00'),

-- Batch 27 September (REGOHOREG)
('REGOHOREG', 'Produk', 'Kiky Buku Tulis 32 Lembar', 'Harga_Jadi', 0, 20000, 1, '2025-09-27 00:00:00', '2025-09-27 23:59:00'),
('REGOHOREG', 'Produk', 'Kiky Buku Tulis 38 Lembar', 'Harga_Jadi', 0, 23900, 1, '2025-09-27 00:00:00', '2025-09-27 23:59:00'),
('REGOHOREG', 'Produk', 'Sedaap Mie Goreng', 'Harga_Jadi', 0, 2800, 1, '2025-09-27 00:00:00', '2025-09-27 23:59:00'),
('REGOHOREG', 'Produk', 'Indomie Mie Goreng', 'Harga_Jadi', 0, 2900, 1, '2025-09-27 00:00:00', '2025-09-27 23:59:00'),
('REGOHOREG', 'Produk', 'Anggur Beras 5 kg', 'Harga_Jadi', 0, 74900, 1, '2025-09-27 00:00:00', '2025-09-27 23:59:00'),
('REGOHOREG', 'Produk', 'Pinpin Beras 5 kg', 'Harga_Jadi', 0, 79900, 1, '2025-09-27 00:00:00', '2025-09-27 23:59:00'),
('REGOHOREG', 'Produk', 'Pristine Air Mineral 600 ml Dus', 'Harga_Jadi', 0, 80100, 1, '2025-09-27 00:00:00', '2025-09-27 23:59:00'),
('REGOHOREG', 'Produk', 'Pristine Air Mineral 400 ml Dus', 'Harga_Jadi', 0, 56000, 1, '2025-09-27 00:00:00', '2025-09-27 23:59:00'),

-- Batch 30 September (REGOHOREG)
('REGOHOREG', 'Produk', 'Squeezy Bolpen Gel SQ04', 'Harga_Jadi', 0, 11000, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Sidu HVS 80 Gsm A4', 'Harga_Jadi', 0, 46000, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Binder Note A5 Pastel', 'Harga_Jadi', 0, 19900, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Nachi Lakban', 'Harga_Jadi', 0, 7600, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Fitri Minyak Goreng', 'Grosir', 0, 46000, 3, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Sultan Termos', 'Harga_Jadi', 0, 27000, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Sarasa Bolpen Gel', 'Harga_Jadi', 0, 13900, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Frixion Bolpen Bisa Dihapus', 'Harga_Jadi', 0, 22500, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Capybara Botol Minum 900 ml', 'Harga_Jadi', 0, 13900, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Kifa Sabun Cuci Piring', 'Harga_Jadi', 0, 6500, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Cleo Air Minum 220 ml', 'Harga_Jadi', 0, 16700, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Jete Power Bank B7 10000 Mah', 'Harga_Jadi', 0, 249000, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'See U Tissue', 'Harga_Jadi', 0, 6900, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Lexar Flashdisk V40 64 Gb', 'Harga_Jadi', 0, 59000, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Beras Enak 5 kg + Minyak Goreng', 'Harga_Jadi', 0, 91000, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Hit Spray Anti Nyamuk 600 ml', 'Harga_Jadi', 0, 29000, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Fusia Tissue Jumbo 1000 Sheet', 'Harga_Jadi', 0, 23000, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Joyko Correction Tape CT 522', 'Harga_Jadi', 0, 4400, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00'),
('REGOHOREG', 'Produk', 'Kokoro Bolpen Black', 'Harga_Jadi', 0, 4900, 1, '2025-09-30 00:00:00', '2025-09-30 23:59:00');


-- BULAN 10
INSERT INTO Promo (Nama_Promo, Target_Type, Target_Value, Jenis_Promo, Nilai_Potongan, Harga_Baru, Min_Qty, START, END) VALUES
-- Batch 31 Oktober
('Promo Biasa', 'Produk', 'Jas Hujan Jaket Celana Elmondo', 'Persen', 2.5, 58500, 1, '2024-10-31 00:00:00', '2024-10-31 23:59:00'),
('Promo Biasa', 'Produk', 'Jas Hujan Kiddy Rets', 'Persen', 2.5, 42900, 1, '2024-10-31 00:00:00', '2024-10-31 23:59:00'),
-- Batch 24 Oktober
('Promo Biasa', 'Produk', 'Tissue Amour 360S', 'Harga_Jadi', 0, 8000, 1, '2024-10-24 00:00:00', '2024-10-24 23:59:00'),
('Promo Biasa', 'Produk', 'Jas Hujan Jaket Celana Big Kid', 'Persen', 2.5, 47775, 1, '2024-10-24 00:00:00', '2024-10-24 23:59:00'),
('Promo Biasa', 'Produk', 'Jas Hujan Trend Jaket Celana', 'Persen', 2.5, 69225, 1, '2024-10-24 00:00:00', '2024-10-24 23:59:00'),
('Promo Biasa', 'Produk', 'Jas Hujan Trend Jaket Celana Premium Ibex', 'Persen', 2.5, 69225, 1, '2024-10-24 00:00:00', '2024-10-24 23:59:00'),
('Promo Biasa', 'Produk', 'Jas Hujan Trend Jaket Celana', 'Persen', 2.5, 79950, 1, '2024-10-24 00:00:00', '2024-10-24 23:59:00'),
('Promo Biasa', 'Produk', 'Jas Hujan Andalan 502', 'Harga_Jadi', 0, 63400, 1, '2024-10-24 00:00:00', '2024-10-24 23:59:00'),
-- Batch 21 Oktober (HORE 95.000 FOLLOWERS)
('Hore 95.000 Followers', 'Produk', 'Sedapmie', 'Harga_Jadi', 0, 2800, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Indomie', 'Harga_Jadi', 0, 2900, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Minyak Goreng Fitri 800 ml', 'Harga_Jadi', 0, 15300, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Beras Anggur', 'Harga_Jadi', 0, 74900, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Air Cleo 220 ml', 'Harga_Jadi', 0, 16700, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Gula Rose Brand 1 kg', 'Harga_Jadi', 0, 16900, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Air Pristine 400 ml', 'Harga_Jadi', 0, 2600, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Tissue See-U 250S', 'Harga_Jadi', 0, 7700, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Tissue Fusia Jumbo', 'Harga_Jadi', 0, 25000, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Tissue Basah So Bonus So', 'Harga_Jadi', 0, 10300, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Tissue Nice Jumbo', 'Harga_Jadi', 0, 34900, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Freshcare', 'Harga_Jadi', 0, 11500, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Tolak Angin', 'Harga_Jadi', 0, 4000, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Kipas Portable', 'Harga_Jadi', 0, 14900, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Magic Clay', 'Harga_Jadi', 0, 5500, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Bolpen Sarasa', 'Harga_Jadi', 0, 13900, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Bolpen Quantum', 'Harga_Jadi', 0, 5200, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Bolpen AE7', 'Harga_Jadi', 0, 17500, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Bolpen Squeezy SQ04', 'Harga_Jadi', 0, 1000, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Buku Tulis Kiky Okey 38L', 'Harga_Jadi', 0, 24000, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Buku Tulis Bis Rose 42L', 'Harga_Jadi', 0, 23500, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Buku Tulis Sidu 32L', 'Harga_Jadi', 0, 24000, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Buku Tulis Kraft 40L', 'Harga_Jadi', 0, 27000, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Merk', 'Top', 'Persen', 13, NULL, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Pensil Agatis Neopex 2B', 'Harga_Jadi', 0, 8000, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Crayon Pop 1 12 Warna', 'Harga_Jadi', 0, 5500, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Bolpen Trendee', 'Harga_Jadi', 0, 5000, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Crayon Creebel 55 Warna', 'Harga_Jadi', 0, 78500, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Map L Benefit', 'Harga_Jadi', 0, 1000, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Lakban Borneo Core Merah', 'Harga_Jadi', 0, 5900, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Ordner Bantex 1401', 'Harga_Jadi', 0, 17000, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'OPP Nachi 2 Inch', 'Harga_Jadi', 0, 7500, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Bubble Wrap 2 kg', 'Harga_Jadi', 0, 69900, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'HVS Sinarline 75 GSM A4', 'Harga_Jadi', 0, 34900, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'HVS Sinarline 75 GSM F4', 'Harga_Jadi', 0, 39900, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'HVS Sidu 70 GSM A4', 'Harga_Jadi', 0, 39130, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'HVS Sidu 70 GSM F4', 'Harga_Jadi', 0, 45500, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Mangkok Set 1', 'Harga_Jadi', 0, 9900, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Mangkok Set 2', 'Harga_Jadi', 0, 17900, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Botol Minum', 'Harga_Jadi', 0, 6000, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
('Hore 95.000 Followers', 'Produk', 'Lunch Box Ginza', 'Harga_Jadi', 0, 9900, 1, '2024-10-21 00:00:00', '2024-10-21 23:59:00'),
-- Batch Lainnya
('Promo Biasa', 'Produk', 'Tissue Facial Amigo 1000gr', 'Harga_Jadi', 0, 30300, 1, '2024-10-17 00:00:00', '2024-10-17 23:59:00'),
('Promo Biasa', 'Produk', 'Pack Bolpen Quantum', 'Harga_Jadi', 0, 5500, 1, '2024-10-14 00:00:00', '2024-10-14 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator M&G MGC-10', 'Harga_Jadi', 0, 95000, 1, '2024-10-08 00:00:00', '2024-10-08 23:59:00'),
-- Batch Kalkulator Casio (4 Okt)
('Promo Biasa', 'Produk', 'Kalkulator Scientific Casio FX-570ES Plus', 'Persen', 20, 361200, 1, '2024-10-04 00:00:00', '2024-10-04 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Scientific Casio FX-82ES Plus', 'Persen', 20, 229600, 1, '2024-10-04 00:00:00', '2024-10-04 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Scientific Casio FX-991ES Plus', 'Persen', 20, 384800, 1, '2024-10-04 00:00:00', '2024-10-04 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Casio DM-1400F', 'Persen', 20, 330400, 1, '2024-10-04 00:00:00', '2024-10-04 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Casio MJ-120D Plus', 'Persen', 20, 160800, 1, '2024-10-04 00:00:00', '2024-10-04 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Casio MX-120B', 'Persen', 20, 120000, 1, '2024-10-04 00:00:00', '2024-10-04 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Casio MX-12B', 'Persen', 20, 82000, 1, '2024-10-04 00:00:00', '2024-10-04 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Casio MJ-100D Plus', 'Persen', 20, 148800, 1, '2024-10-04 00:00:00', '2024-10-04 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Casio DH-12BK', 'Persen', 20, 155200, 1, '2024-10-04 00:00:00', '2024-10-04 23:59:00'),
-- Batch Kalkulator Joyko (3 Okt)
('Promo Biasa', 'Produk', 'Kalkulator Joyko CC-40', 'Persen', 15, 44200, 1, '2024-10-03 00:00:00', '2024-10-03 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Joyko CC-57CO', 'Persen', 15, 38250, 1, '2024-10-03 00:00:00', '2024-10-03 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Joyko CC-27', 'Persen', 15, 68000, 1, '2024-10-03 00:00:00', '2024-10-03 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Joyko CC-15A', 'Persen', 15, 41650, 1, '2024-10-03 00:00:00', '2024-10-03 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Scientific Joyko CC-25', 'Persen', 15, 55250, 1, '2024-10-03 00:00:00', '2024-10-03 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Scientific Joyko CC-23BP', 'Persen', 15, 35700, 1, '2024-10-03 00:00:00', '2024-10-03 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Scientific Joyko CC-78', 'Persen', 15, 29750, 1, '2024-10-03 00:00:00', '2024-10-03 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Scientific Joyko CC-23', 'Persen', 15, 35700, 1, '2024-10-03 00:00:00', '2024-10-03 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Scientific Joyko CC-67', 'Persen', 15, 112200, 1, '2024-10-03 00:00:00', '2024-10-03 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Scientific Joyko CC-29A', 'Persen', 15, 164050, 1, '2024-10-03 00:00:00', '2024-10-03 23:59:00'),
('Promo Biasa', 'Produk', 'Kalkulator Scientific Joyko CC-25CO', 'Persen', 15, 55250, 1, '2024-10-03 00:00:00', '2024-10-03 23:59:00');


-- BULAN 11
INSERT INTO Promo 
    (Nama_Promo, Target_Type, Target_Value, Jenis_Promo, Nilai_Potongan, Harga_Baru, Min_Qty, Bonus_Produk_ID, Gratis_Qty, START, END) 
VALUES 
    -- Batch Akhir November
    ('Promo Biasa', 'Produk', 'Tumbler Jumbo', 'Persen', 2.5, NULL, 1, NULL, 0, '2025-11-30 00:00:00', '2025-11-30 23:59:00'),
    ('Promo Biasa', 'Produk', 'Botol Minum Cetek Sedotan Mix 900ml', 'Harga_Jadi', 0, 14900, 1, NULL, 0, '2025-11-29 00:00:00', '2025-11-29 23:59:00'),
    
    -- Batch REGO NEKADDD DISAMBER AJA (26 November)
    ('Rego Nekaddd Disamber Aja', 'Produk', 'Sedapmie', 'Harga_Jadi', 0, 2900, 1, NULL, 0, '2025-11-26 00:00:00', '2025-11-26 23:59:00'),
    ('Rego Nekaddd Disamber Aja', 'Produk', 'Indomie', 'Harga_Jadi', 0, 3000, 1, NULL, 0, '2025-11-26 00:00:00', '2025-11-26 23:59:00'),
    ('Rego Nekaddd Disamber Aja', 'Produk', 'Gula Rose Brand 1kg', 'Harga_Jadi', 0, 17000, 1, NULL, 0, '2025-11-26 00:00:00', '2025-11-26 23:59:00'),
    ('Rego Nekaddd Disamber Aja', 'Produk', 'Air Cleo 220ml', 'Harga_Jadi', 0, 16900, 1, NULL, 0, '2025-11-26 00:00:00', '2025-11-26 23:59:00'),
    ('Rego Nekaddd Disamber Aja', 'Produk', 'Minyak Goreng 800ml', 'Harga_Jadi', 0, 15000, 1, NULL, 0, '2025-11-26 00:00:00', '2025-11-26 23:59:00'),
    ('Rego Nekaddd Disamber Aja', 'Produk', 'Beras Cap Uduk 5kg', 'Harga_Jadi', 0, 74000, 1, NULL, 0, '2025-11-26 00:00:00', '2025-11-26 23:59:00'),
    ('Rego Nekaddd Disamber Aja', 'Produk', 'Tissu See-U 250s', 'Harga_Jadi', 0, 7000, 1, NULL, 0, '2025-11-26 00:00:00', '2025-11-26 23:59:00'),
    ('Rego Nekaddd Disamber Aja', 'Produk', 'Tissu Basah Mitu', 'Harga_Jadi', 0, 10500, 1, NULL, 0, '2025-11-26 00:00:00', '2025-11-26 23:59:00'),
    ('Rego Nekaddd Disamber Aja', 'Produk', 'Tissu Paseo', 'Harga_Jadi', 0, 22500, 1, NULL, 0, '2025-11-26 00:00:00', '2025-11-26 23:59:00'),
    ('Rego Nekaddd Disamber Aja', 'Produk', 'Hit Aerosol 600ml', 'Harga_Jadi', 0, 30900, 1, NULL, 0, '2025-11-26 00:00:00', '2025-11-26 23:59:00'),
    ('Rego Nekaddd Disamber Aja', 'Tag', 'Kalkulator', 'Persen', 15.0, NULL, 1, NULL, 0, '2025-11-26 00:00:00', '2025-11-26 23:59:00'),
    ('Rego Nekaddd Disamber Aja', 'Produk', 'Gantungan Kunci', 'Grosir', 0, 11000, 3, NULL, 0, '2025-11-26 00:00:00', '2025-11-26 23:59:00'),
    ('Rego Nekaddd Disamber Aja', 'Produk', 'Jedai Kamboja', 'Grosir', 0, 11000, 3, NULL, 0, '2025-11-26 00:00:00', '2025-11-26 23:59:00'),
    ('Rego Nekaddd Disamber Aja', 'Produk', 'Hanger Hitam', 'Grosir', 0, 7000, 12, NULL, 0, '2025-11-26 00:00:00', '2025-11-26 23:59:00'),
    ('Rego Nekaddd Disamber Aja', 'Tag', 'Hiasan Natal', 'Persen', 2.5, NULL, 1, NULL, 0, '2025-11-26 00:00:00', '2025-11-26 23:59:00'),
    ('Rego Nekaddd Disamber Aja', 'Tag', 'Bags & Toys', 'Persen', 10.0, NULL, 1, NULL, 0, '2025-11-26 00:00:00', '2025-11-26 23:59:00'),
    
    -- Batch Spesial November
    ('Promo Biasa', 'Produk', 'Bando Natal', 'Persen', 3.33, 12567, 1, NULL, 0, '2025-11-22 00:00:00', '2025-11-22 23:59:00'),
    ('Promo Biasa', 'Produk', 'Topi Natal Glitter', 'Persen', 3.33, 17400, 1, NULL, 0, '2025-11-22 00:00:00', '2025-11-22 23:59:00'),
    ('Promo Biasa', 'Produk', 'Jas Hujan Elmondo Celana Jaket 935', 'Persen', 2.5, 34125, 1, NULL, 0, '2025-11-21 00:00:00', '2025-11-21 23:59:00'),
    ('Promo Biasa', 'Produk', 'Indomie', 'Persen', 11.0, 2933, 1, NULL, 0, '2025-11-11 00:00:00', '2025-11-11 23:59:00'),
    ('Promo Biasa', 'Produk', 'Beras Uduk', 'Persen', 11.0, 73999, 1, NULL, 0, '2025-11-11 00:00:00', '2025-11-11 23:59:00'),
    ('Promo Biasa', 'Produk', 'Vivan Vpb-D11 10.000 Mah', 'Harga_Jadi', 0, 190000, 1, NULL, 0, '2025-11-05 00:00:00', '2025-11-05 23:59:00'),

    -- Batch HOREG NEKADDD 13est Years (1-13 November)
    ('Horeg Nekaddd 13est Years of Ultah TBMO', 'Produk', 'Gula Rose Brand 1kg', 'Harga_Jadi', 0, 17000, 1, NULL, 0, '2025-11-01 00:00:00', '2025-11-13 23:59:00'),
    ('Horeg Nekaddd 13est Years of Ultah TBMO', 'Produk', 'Tissue See-U 250s', 'Grosir', 0, 13333, 2, NULL, 0, '2025-11-01 00:00:00', '2025-11-13 23:59:00'),
    ('Horeg Nekaddd 13est Years of Ultah TBMO', 'Produk', 'Tissue Amor 360', 'Grosir', 0, 7700, 2, NULL, 0, '2025-11-01 00:00:00', '2025-11-13 23:59:00'),
    ('Horeg Nekaddd 13est Years of Ultah TBMO', 'Produk', 'Tissue Basah Mitu', 'Bonus', 0, 10333, 1, (SELECT ID FROM Produk WHERE Nama='Tissue Basah Mitu' LIMIT 1), 1, '2025-11-01 00:00:00', '2025-11-13 23:59:00'),
    ('Horeg Nekaddd 13est Years of Ultah TBMO', 'Produk', 'Hanger Hitam', 'Grosir', 0, 13333, 12, NULL, 0, '2025-11-01 00:00:00', '2025-11-13 23:59:00'),
    ('Horeg Nekaddd 13est Years of Ultah TBMO', 'Produk', 'Bolpen Trendee', 'Grosir', 0, 5333, 12, NULL, 0, '2025-11-01 00:00:00', '2025-11-13 23:59:00'),
    ('Horeg Nekaddd 13est Years of Ultah TBMO', 'Tag', 'Kalkulator', 'Persen', 13.33, NULL, 1, NULL, 0, '2025-11-01 00:00:00', '2025-11-13 23:59:00'),
    ('Horeg Nekaddd 13est Years of Ultah TBMO', 'Tag', 'Snack', 'Persen', 3.33, NULL, 1, NULL, 0, '2025-11-01 00:00:00', '2025-11-13 23:59:00'),
    ('Horeg Nekaddd 13est Years of Ultah TBMO', 'Tag', 'Minuman', 'Persen', 3.33, NULL, 1, NULL, 0, '2025-11-01 00:00:00', '2025-11-13 23:59:00');

INSERT INTO Transaksi (member_id, Tanggal, Total)
VALUES
(1, '2025-12-01 10:15:00', 35000),
(2, '2025-12-01 11:22:00', 78000),
(NULL, '2025-12-02 12:40:00', 15000),
(3, '2025-12-01 09:05:00', 112000),
(1, '2025-11-30 13:55:00', 54000);

INSERT INTO Transaksi_Detail (transaksi_id, produk_id, Qty, Harga)
VALUES
-- transaksi 1
(1, 3, 2, 5000),      -- contoh produk ID 3 harga 5000
(1, 5, 1, 25000),

-- transaksi 2
(2, 2, 1, 30000),
(2, 4, 2, 24000),

-- transaksi 3 (tanpa member)
(3, 1, 1, 15000),

-- transaksi 4
(4, 6, 1, 70000),
(4, 7, 2, 21000),

-- transaksi 5
(5, 3, 3, 5000),
(5, 8, 1, 39000);