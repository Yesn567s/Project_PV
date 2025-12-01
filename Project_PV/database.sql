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

-- 3. TAG (NEW)
CREATE TABLE IF NOT EXISTS Tag (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Nama VARCHAR(100) NOT NULL UNIQUE
);

-- 4. PRODUK (UPDATED)
CREATE TABLE IF NOT EXISTS Produk (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Nama VARCHAR(100) NOT NULL,
    Harga INT NOT NULL,
    kategori_id INT NOT NULL,
    FOREIGN KEY (kategori_id) REFERENCES Kategori(ID)
);

-- 5. PRODUK_TAG (Bridge for Many-to-Many)
CREATE TABLE IF NOT EXISTS Produk_Tag (
    produk_id INT NOT NULL,
    tag_id INT NOT NULL,
    PRIMARY KEY(produk_id, tag_id),
    FOREIGN KEY (produk_id) REFERENCES Produk(ID),
    FOREIGN KEY (tag_id) REFERENCES Tag(ID)
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

-- 8. TRANSAKSI (NEW)
CREATE TABLE IF NOT EXISTS Transaksi (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    member_id INT NULL,
    Tanggal DATETIME DEFAULT CURRENT_TIMESTAMP(),
    Total INT NOT NULL DEFAULT 0,
    FOREIGN KEY (member_id) REFERENCES MEMBER(ID)
);

-- 9. TRANSAKSI DETAIL (NEW)
CREATE TABLE IF NOT EXISTS Transaksi_Detail (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    transaksi_id INT NOT NULL,
    produk_id INT NOT NULL,
    Qty INT NOT NULL,
    Harga INT NOT NULL, -- harga saat transaksi (avoid price change issues)
    Subtotal INT GENERATED ALWAYS AS (Qty * Harga) STORED,
    FOREIGN KEY (transaksi_id) REFERENCES Transaksi(ID),
    FOREIGN KEY (produk_id) REFERENCES Produk(ID)
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
('Aksesoris');

INSERT INTO Tag (Nama) VALUES
('Best Seller'),
('New Arrival'),
('Diskon'),
('Premium'),
('Limited');

INSERT INTO Produk (Nama, Harga, kategori_id) VALUES
('Teh Botol', 5000, 1),
('Nasi Goreng', 20000, 2),
('Headset Bluetooth', 150000, 3),
('Gelang Kayu', 12000, 4),
('Laptop Mini', 2500000, 3),
('Air Mineral', 3000, 1),
('Kopi Hitam', 8000, 1),
('Mie Goreng', 15000, 2),
('Burger Mini', 25000, 2),
('Smartwatch Lite', 350000, 3),
('Mouse Wireless', 85000, 3),
('Kalung Perak', 45000, 4),
('Topi Rajut', 18000, 4),
('Camera Pocket', 975000, 3),
('Charger Fast 20W', 120000, 3),
('Cincin Titanium', 65000, 4),
('Soda Lemon', 7000, 1),
('Ayam Geprek', 22000, 2),
('Kipas Mini USB', 55000, 3),
('Tas Selempang', 90000, 4);



INSERT INTO Produk_Tag (produk_id, tag_id) VALUES
-- Teh Botol
(1, 1), -- Best Seller
(1, 3), -- Diskon

-- Nasi Goreng
(2, 1), -- Best Seller
(2, 2), -- New Arrival

-- Headset Bluetooth
(3, 4), -- Premium
(3, 3), -- Diskon

-- Gelang Kayu
(4, 5), -- Limited Edition

-- Laptop Mini
(5, 4), -- Premium
(5, 2), -- New Arrival

-- Air Mineral (ID 6)
(6, 1),
(6, 2),

-- Kopi Hitam (ID 7)
(7, 1),
(7, 4),

-- Mie Goreng (ID 8)
(8, 1),
(8, 3),

-- Burger Mini (ID 9)
(9, 2),
(9, 5),

-- Smartwatch Lite (ID 10)
(10, 4),
(10, 2),
(10, 3),

-- Mouse Wireless (ID 11)
(11, 3),
(11, 2),

-- Kalung Perak (ID 12)
(12, 4),
(12, 5),

-- Topi Rajut (ID 13)
(13, 5),

-- Camera Pocket (ID 14)
(14, 4),
(14, 2),

-- Charger Fast 20W (ID 15)
(15, 3),
(15, 2),

-- Cincin Titanium (ID 16)
(16, 4),
(16, 5),

-- Soda Lemon (ID 17)
(17, 1),
(17, 2),

-- Ayam Geprek (ID 18)
(18, 1),
(18, 5),

-- Kipas Mini USB (ID 19)
(19, 3),
(19, 2),

-- Tas Selempang (ID 20)
(20, 5),
(20, 4);


INSERT INTO Promo (Nama_Promo, Produk, Jenis, Promo, Tag) VALUES
('REGO NEKADDD DISAMBER AJA', 'Sedaapmie', 'Nominal', 2900, NULL),
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
