CREATE DATABASE IF NOT EXISTS db_proyek_pv;
USE db_proyek_pv;

-- 1. MEMBER
CREATE TABLE IF NOT EXISTS MEMBER (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Nama VARCHAR(100) NOT NULL,
    Tanggal_Lahir DATE NOT NULL
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
    FOREIGN KEY (kategori_id) REFERENCES Kategori(ID)
);

-- 6. PROMO (Keep as reference to product name or change to FK later)
CREATE TABLE IF NOT EXISTS Promo (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Nama_Promo VARCHAR(100) NOT NULL,
    Produk VARCHAR(100) NOT NULL, -- (You can convert this to produk_id INT for FK)
    Jenis ENUM('Persen', 'Nominal') NOT NULL,
    Promo FLOAT NOT NULL,
    Tag VARCHAR(255),
    START DATETIME DEFAULT CURRENT_TIMESTAMP(),
    END DATETIME DEFAULT NULL
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

INSERT INTO MEMBER (Nama, Tanggal_Lahir) VALUES
('Kevin Setiono', '2003-08-12'),
('Alicia Putri', '1999-03-22'),
('Budi Santoso', '1995-11-05'),
('Cindy Wijaya', '2001-06-18'),
('Davin Pratama', '1998-12-29'),
('Evelyn Kurnia', '2004-02-14'),
('Fajar Nugroho', '1997-09-10'),
('Gracia Tania', '2000-01-30'),
('Hendra Wijaya', '1996-05-25'),
('Indah Pertiwi', '2002-07-09');

INSERT INTO Kategori (Nama) VALUES
('Minuman'),
('Makanan'),
('Elektronik'),
('Aksesoris'),
('Other');

INSERT INTO Produk (Nama, Merk, Harga, kategori_id, tag) VALUES
-- MAKANAN
('Mie Sedaap Goreng', 'Sedaap', 3000, 2, 'makanan'),
('Indomie Goreng', 'Indomie', 3500, 2, 'makanan'),
('Gula Rose Brand 1kg', 'Rose Brand', 18500, 2, 'sembako'),
('Biskuit Columbia', 'Columbia', 25000, 2, 'biskuit'),

-- MINUMAN
('Air Cleo 220ml', 'Cleo', 2000, 1, 'minuman'),

-- SEMBAKO & RUMAH TANGGA
('Minyak Goreng 800ml', 'Generic', 17500, 5, 'sembako'),
('Beras Cap Uduk 5kg', 'Cap Uduk', 78000, 5, 'sembako'),
('Tissue See-U', 'See-U', 9000, 5, 'tissue'),
('Tissue Fusia Jumbo', 'Fusia', 25000, 5, 'tissue'),
('Tissue Basah Mitu', 'Mitu', 12000, 5, 'tissue'),
('Nit Aerosol 600ml', 'Baygon', 34000, 5, 'insektisida'),

-- ATK
('HVS Sinarline 75gsm A4', 'Sinarline', 36000, 5, 'atk'),
('HVS Sinarline 75gsm F4', 'Sinarline', 42000, 5, 'atk'),
('Odner Bantex 1401', 'Bantex', 18000, 5, 'atk'),
('HVS Sidu 70gsm A4', 'Sidu', 42000, 5, 'atk'),
('HVS Sidu 70gsm F4', 'Sidu', 47000, 5, 'atk'),
('Bolpen Ae7 1 Lusin', 'AE7', 20000, 5, 'atk'),
('Nota Kontan Borneo 2ply Isi 10', 'Borneo', 32000, 5, 'atk'),
('Lakban Borneo Core Merah', 'Borneo', 6500, 5, 'atk'),
('Opp Nachi 2inch Ecer', 'Nachi', 9000, 5, 'atk'),
('Opp Nachi 2inch Dus', 'Nachi', 550000, 5, 'atk'),
('Bubble Wrap Putih 2kg', 'Generic', 75000, 5, 'packing'),

-- BUKU & ALAT TULIS
('Buku Tulis Kiky Okey 32l', 'Kiky', 23000, 5, 'buku'),
('Buku Tulis Kiky Okey 38l', 'Kiky', 27000, 5, 'buku'),
('Buku Tulis Big Boss 42l', 'Big Boss', 26000, 5, 'buku'),
('Bolpen Sarasa', 'Zebra', 17000, 5, 'atk'),
('Bolpen Trendee', 'Trendee', 7000, 5, 'atk'),
('Crayon Greebel 55w', 'Greebel', 75000, 5, 'alat gambar'),

-- ELEKTRONIK
('Kalkulator', 'Casio', 120000, 3, 'elektronik'),
('Produk JETE', 'JETE', 150000, 3, 'elektronik'),

-- AKSESORIS & LAINNYA
('Map L Benefit Isi 12pcs', 'Benefit', 13000, 5, 'map'),
('Magic Clay', 'Generic', 7000, 5, 'mainan'),
('Bubble Stick', 'Generic', 7000, 5, 'mainan'),
('Gantungan Kunci', 'Generic', 5000, 4, 'aksesoris'),
('Masker', 'Alkindo', 15000, 5, 'kesehatan'),
('Lem Tikus', 'Generic', 16000, 5, 'rumah'),
('Jas Hujan Cap Kapak', 'Cap Kapak', 45000, 5, 'jas hujan'),
('Payung Golf Jumbo', 'Generic', 55000, 4, 'payung'),
('Hanger Hitam 12pcs', 'Generic', 9000, 5, 'rumah'),
('Kursi', 'Generic', 30000, 5, 'furniture'),
('Lunch Box Ginza', 'Ginza', 13000, 5, 'peralatan makan'),
('Jam Dinding Mayomi 917', 'Mayomi', 25000, 5, 'jam'),
('Keset Handuk Jumbo', 'Generic', 22000, 5, 'rumah'),
('Handuk 70x140', 'Generic', 42000, 5, 'handuk'),

-- SABUN & PEMBERSIH
('Kifa Pencuci Piring 650ml', 'Kifa', 8000, 5, 'sabun'),
('Kifa Karbol Wangi 700ml', 'Kifa', 11000, 5, 'sabun'),

-- PERLENGKAPAN
('Kertas Kado', 'Generic', 22000, 5, 'natal'),
('Hiasan Natal', 'Generic', 30000, 5, 'natal'),
('Box Container Black 50l', 'Generic', 60000, 5, 'storage'),

-- PERALATAN MAKAN
('Mangkok Sultan Isi 1', 'Sultan', 12000, 5, 'peralatan makan'),
('Mangkok Sultan Isi 2', 'Sultan', 20000, 5, 'peralatan makan'),
('Mangkok Sultan Isi 4', 'Sultan', 40000, 5, 'peralatan makan'),
('Gelas Voila 3pcs', 'Voila', 13000, 5, 'peralatan makan'),
('Mangkok Ayam 3pcs', 'Generic', 13000, 5, 'peralatan makan'),
('Termos Sultan + 2 Cangkir', 'Sultan', 30000, 5, 'termos'),
('Termos Serba', 'Generic', 38000, 5, 'termos'),

-- LAIN-LAIN
('Botol Minum Xiao Oval', 'Xiao', 37000, 5, 'botol'),
('Botol Stainless', 'Generic', 56000, 5, 'botol'),
('Cooler Bag', 'Generic', 13000, 5, 'tas'),
('Celana Pendek', 'Generic', 32000, 5, 'pakaian'),
('Kaos Kaki Remaja', 'Generic', 10000, 5, 'pakaian'),
('Meja Belajar', 'Generic', 48000, 5, 'furniture'),
('Tas Sekolah', 'Generic', 175000, 4, 'tas'),
('Mainan', 'Generic', 60000, 5, 'mainan');


INSERT INTO Promo (Nama_Promo, Produk, Jenis, Promo, Tag) VALUES
('REGO NEKADDD DISAMBER AJA', 'Mie Sedaap', 'Nominal', 2900, NULL),
('REGO NEKADDD DISAMBER AJA', 'Indomie', 'Nominal', 3000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Gula Rose Brand', 'Nominal', 17000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Air Cleo 220ml', 'Nominal', 16900, NULL),
('REGO NEKADDD DISAMBER AJA', 'Minyak Goreng 800ml', 'Nominal', 15900, NULL),
('REGO NEKADDD DISAMBER AJA', 'Beras Cap Uduk 5kg', 'Nominal', 74000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Tissue See-U', 'Nominal', 7000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Tissue Fusia Jumbo', 'Nominal', 22500, NULL),
('REGO NEKADDD DISAMBER AJA', 'Tissue Basah Mitu', 'Nominal', 10500, NULL),
('REGO NEKADDD DISAMBER AJA', 'Nit Aerosol 600ml', 'Nominal', 30900, NULL),
('REGO NEKADDD DISAMBER AJA', 'Hvs Sinarline 75gsm A4', 'Nominal', 34500, NULL),
('REGO NEKADDD DISAMBER AJA', 'Hvs Sinarline 75gsm F4', 'Nominal', 39900, NULL),
('REGO NEKADDD DISAMBER AJA', 'Odner Bantex 1401', 'Nominal', 16000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Hvs Sidu 70gsm A4', 'Nominal', 39900, NULL),
('REGO NEKADDD DISAMBER AJA', 'Hvs Sidu 70gsm F4', 'Nominal', 45400, NULL),
('REGO NEKADDD DISAMBER AJA', 'Bolpen Ae7 1 Lusin', 'Nominal', 18000, NULL),

('REGO NEKADDD DISAMBER AJA', 'Biskuit Columbia', 'Nominal', 23000, 'Biskuit Columbia'),

('REGO NEKADDD DISAMBER AJA', 'Nota Kontan 2ply Borneo Isi 10pcs', 'Nominal', 29000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Lakban Borneo Core Merah', 'Nominal', 5733, NULL),
('REGO NEKADDD DISAMBER AJA', 'Opp Nachi 2inch Ecer', 'Nominal', 7700, NULL),
('REGO NEKADDD DISAMBER AJA', 'Opp Nachi 2inch Dus', 'Nominal', 529200, NULL),
('REGO NEKADDD DISAMBER AJA', 'Bubble Wrap Putih 2kg', 'Nominal', 69900, NULL),

('REGO NEKADDD DISAMBER AJA', 'Buku Tulis Kiky Okey 32l', 'Nominal', 21000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Buku Tulis Kiky Okey 38l', 'Nominal', 25000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Buku Tulis Big Boss 42l', 'Nominal', 24000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Bolpen Sarasa', 'Nominal', 14900, NULL),
('REGO NEKADDD DISAMBER AJA', 'Bolpen Trendee', 'Nominal', 5500, NULL),
('REGO NEKADDD DISAMBER AJA', 'Crayon Greebel 55w', 'Nominal', 69900, NULL),

('REGO NEKADDD DISAMBER AJA', 'Kalkulator', 'Persen', 15, 'Canon Joyko Casio M&g Target Deli'),
('REGO NEKADDD DISAMBER AJA', 'Jete', 'Persen', 2.5, 'Jete'),

('REGO NEKADDD DISAMBER AJA', 'Map L Benefit Isi 12pcs', 'Nominal', 11000, NULL),

('REGO NEKADDD DISAMBER AJA', 'Magic Clay', 'Nominal', 5000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Bubble Stick', 'Nominal', 5000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Gantungan Kunci', 'Nominal', 3333, NULL),
('REGO NEKADDD DISAMBER AJA', 'Masker', 'Nominal', 13333, 'Alkindo Mouson'),
('REGO NEKADDD DISAMBER AJA', 'Lem Tikus', 'Nominal', 14500, NULL),
('REGO NEKADDD DISAMBER AJA', 'Jas Hujan Cap Kapak', 'Nominal', 41000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Payung Golf Jumbo', 'Nominal', 49000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Hanger Hitam 12pcs', 'Nominal', 7000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Kursi', 'Nominal', 25000, 'Kursi'),
('REGO NEKADDD DISAMBER AJA', 'Lunch Box Ginza', 'Nominal', 11000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Jam Dinding Mayomi Type 917', 'Nominal', 21333, NULL),
('REGO NEKADDD DISAMBER AJA', 'Keset Handuk Jumbo', 'Nominal', 19000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Handuk 70x140', 'Nominal', 39000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Kifa Pencuci Piring 650ml', 'Nominal', 6500, NULL),
('REGO NEKADDD DISAMBER AJA', 'Kifa Karbil Wangi 700ml', 'Nominal', 9000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Kertas Kado', 'Nominal', 19000, 'Natal'),
('REGO NEKADDD DISAMBER AJA', 'Hiasan', 'Persen', 2.5, 'Natal'),
('REGO NEKADDD DISAMBER AJA', 'Box Container Black 50l', 'Nominal', 55000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Mangkok Sultan Isi 1', 'Nominal', 9900, NULL),
('REGO NEKADDD DISAMBER AJA', 'Mangkok Sultan Isi 2', 'Nominal', 18000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Mangkok Sultan Isi 4', 'Nominal', 37000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Gelas Voila 3pcs', 'Nominal', 11000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Mangkok Ayam 3pcs', 'Nominal', 11000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Termos Sultan + 2 Cangkir', 'Nominal', 26333, NULL),
('REGO NEKADDD DISAMBER AJA', 'Termos Serba', 'Nominal', 35000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Botol Minum Xiao Oval', 'Nominal', 34000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Botol Stainless', 'Nominal', 52500, NULL),
('REGO NEKADDD DISAMBER AJA', 'Cooler Bag', 'Nominal', 11000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Celana Pendek', 'Nominal', 29000, NULL),
('REGO NEKADDD DISAMBER AJA', 'Kaos Kaki Remaja', 'Nominal', 8500, NULL),
('REGO NEKADDD DISAMBER AJA', 'Meja Belajar', 'Nominal', 42900, NULL),
('REGO NEKADDD DISAMBER AJA', 'Tas Sekolah', 'Persen', 10, NULL),
('REGO NEKADDD DISAMBER AJA', 'Mainan', 'Persen', 10, 'Mainan');

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
