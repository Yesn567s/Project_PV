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
    Promo INT NOT NULL,
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
('Laptop Mini', 2500000, 3);
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
(5, 2); -- New Arrival

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
