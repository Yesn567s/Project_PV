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

-- 3. PRODUK
CREATE TABLE IF NOT EXISTS Produk (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Nama VARCHAR(100) NOT NULL,
    Merk VARCHAR(100), -- Merek barang
    Harga INT NOT NULL,
    kategori_id INT NOT NULL,
    tag VARCHAR(255), -- Untuk Promo
    image_url VARCHAR(500), -- image from internet
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
('Minuman'),
('Makanan'),
('Elektronik'),
('Aksesoris'),
('Other');

INSERT INTO Produk (Nama, Merk, Harga, kategori_id, tag, image_url) VALUES
-- MAKANAN
('Mie Sedaap Goreng', 'Sedaap', 3000, 2, 'makanan', 'https://solvent-production.s3.amazonaws.com/media/images/products/2021/04/2499a.jpg'),
('Indomie Goreng', 'Indomie', 3500, 2, 'makanan', 'https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full//108/MTA-5856786/indomie_indomie_goreng_85gr_full02_b3o9d3ep.jpg'),
('Gula Rose Brand 1kg', 'Rose Brand', 18500, 2, 'sembako', 'https://titanproduct.s3-ap-southeast-1.amazonaws.com/watermarkimg/8993093665497.jpg'),
('Biskuit Columbia', 'Columbia', 25000, 2, 'biskuit', 'https://down-id.img.susercontent.com/file/id-11134207-7rasb-m5nyfkvqm70m01'),

-- MINUMAN
('Air Cleo 220ml', 'Cleo', 2000, 1, 'minuman', 'https://cf.shopee.co.id/file/id-11134207-7rbk0-mam1t88zo31n0e'),

-- SEMBAKO & RUMAH TANGGA
('Minyak Goreng 800ml', 'Generic', 17500, 5, 'sembako', 'https://cdn.ralali.id/assets/img/Libraries/100000176816001_FETTA_Minyak_Goreng_Botol_12_x_800_ml-1653f5ff0f08976ce718a5463947f6bb-1.jpg'),
('Beras Cap Uduk 5kg', 'Cap Uduk', 78000, 5, 'sembako', 'https://arti-assets.sgp1.cdn.digitaloceanspaces.com/renyswalayanku/products/77490e2e-14e0-436e-9b5e-75fb0ebc07ad.jpg'),
('Tissue See-U', 'See-U', 9000, 5, 'tissue', 'https://id-live-01.slatic.net/p/98c935c09a46f4b54bfb12e22f8be3f4.jpg'),
('Tissue Fusia Jumbo', 'Fusia', 25000, 5, 'tissue', 'https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full//105/MTA-71747925/peony_tisu_facial_lembut_isi_180s_2ply_peony_-_fusia_full05_n68nk82k.jpg'),
('Tissue Basah Mitu', 'Mitu', 12000, 5, 'tissue', 'https://image.astronauts.cloud/product-images/2024/9/vdgzb_2fab9e32-cf67-4fd5-bb8f-ddf2fa3251cd_900x900.jpg'),
('Hit Aerosol 600ml', 'Baygon', 34000, 5, 'insektisida', 'https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full//catalog-image/96/MTA-148030086/hit_hit-aerosol-600ml-75ml_full01.jpg'),

-- ATK
('HVS Sinarline 75gsm A4', 'Sinarline', 36000, 5, 'atk', 'https://img.lazcdn.com/g/p/91c270107d4b65fa6823ec136edcd9f9.png_720x720q80.png'),
('HVS Sinarline 75gsm F4', 'Sinarline', 42000, 5, 'atk', 'https://down-id.img.susercontent.com/file/id-11134207-81ztp-mfc3s31lp8gc4b'),
('Odner Bantex 1401', 'Bantex', 18000, 5, 'atk', 'https://storage.googleapis.com/eezee-product-images/ordner-f4-karton-bantex-1401-7-cm-2ajm_600.png'),
('HVS Sidu 70gsm A4', 'Sidu', 42000, 5, 'atk', 'https://sidu.id/documents/287278/309575/SDC_A470_500_Green+depan.png/bb0d88f9-56a4-4ab4-5bdf-7a57f62b04c0?t=1706770983130'),
('HVS Sidu 70gsm F4', 'Sidu', 47000, 5, 'atk', 'https://siopen.balangankab.go.id/storage/merchant/products/2024/04/04/e32ec0d0cbc3925c0a9cc4fcada3e467.jpg'),
('Bolpen Ae7 1 Lusin', 'AE7', 20000, 5, 'atk', 'https://cdn.eurekabookhouse.co.id/ebh/product/all/PULPEN_STANDARD_0,51.jpeg'),
('Nota Kontan Borneo 2ply Isi 10', 'Borneo', 32000, 5, 'atk', 'https://img.lazcdn.com/g/p/35b4fca0b651bd282f7b39f269f3bfe9.png_720x720q80.png'),
('Lakban Borneo Core Merah', 'Borneo', 6500, 5, 'atk', 'https://img.lazcdn.com/g/p/4eb51dc59f1b89466d0dfc490879e419.jpg_720x720q80.jpg'),
('Opp Nachi 2inch Ecer', 'Nachi', 9000, 5, 'atk', 'https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full//catalog-image/101/MTA-148683034/no-brand_lakban-opp-bening-coklat-nachi-2-x-90-yard-48mm-tebal_full9.jpg'),
('Opp Nachi 2inch Dus', 'Nachi', 550000, 5, 'atk', 'https://images.tokopedia.net/img/cache/200-square/VqbcmM/2022/9/24/cc37041a-53e3-42a8-b255-a11c94f9caec.jpg'),
('Bubble Wrap Putih 2kg', 'Generic', 75000, 5, 'packing', 'https://down-id.img.susercontent.com/file/368585cf7f78a0d6e8ec0a7d54e04400'),

-- BUKU & ALAT TULIS
('Buku Tulis Kiky Okey 32l', 'Kiky', 23000, 5, 'buku', 'https://filebroker-cdn.lazada.co.id/kf/S3bd5227d2925453fb0cb512fb7f2576cz.jpg'),
('Buku Tulis Kiky Okey 38l', 'Kiky', 27000, 5, 'buku', 'https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full//catalog-image/113/MTA-148682759/no-brand_buku-tulis-kiky-38-lembar_full04.jpg'),
('Buku Tulis Big Boss 42l', 'Big Boss', 26000, 5, 'buku', 'https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full/catalog-image/101/MTA-35635736/br-m036969-09262_nokida-buku-tulis-kiky-boxy-42-lembar-1-pack-buku-tulis-sekolah-1-pak-murah-buku-kuliah-notebook-alat-tulis-sekolah_full05.jpg'),
('Bolpen Sarasa', 'Zebra', 17000, 5, 'atk', 'https://down-id.img.susercontent.com/file/sg-11134201-22110-ayg8szvt7gkv51'),
('Bolpen Trendee', 'Trendee', 7000, 5, 'atk', 'https://tokodaring.balimall.id/sftp/file/uploads/products/4rxpvn/55ac731b30dd576496224ca9affda1c4-ibOadRoy.jpg'),
('Crayon Greebel 55w', 'Greebel', 75000, 5, 'alat gambar', 'https://down-id.img.susercontent.com/file/94952b2d6075e8b4aa6a571196e042bd'),

-- ELEKTRONIK
('Kalkulator', 'Casio', 120000, 3, 'elektronik', 'https://images.tokopedia.net/img/cache/500-square/product-1/2015/12/18/230011/230011_dfe97530-c3a8-4c40-aac7-f25fb310e036.jpg'),
('Produk JETE', 'JETE', 150000, 3, 'elektronik', 'https://doran.id/wp-content/uploads/2025/02/AM5-17.jpg'),

-- AKSESORIS & LAINNYA
('Map L Benefit Isi 12pcs', 'Benefit', 13000, 5, 'map', 'https://www.produktkdn.co.id/wp-content/uploads/2023/01/2248-.jpg'),
('Magic Clay', 'Generic', 7000, 5, 'mainan', 'https://images-cdn.ubuy.co.id/63767cf33301a0745e0937d9-modeling-clay-kit-24-colors-air-dry.jpg'),
('Bubble Stick', 'Generic', 7000, 5, 'mainan', 'https://filebroker-cdn.lazada.co.id/kf/S1e6283810b614e09a671fe89f055beb40.jpg'),
('Gantungan Kunci', 'Generic', 5000, 4, 'aksesoris', 'https://img.lazcdn.com/g/p/c9a8ef327b35b36cc41804c985144888.jpg_720x720q80.jpg'),
('Masker', 'Alkindo', 15000, 5, 'kesehatan', 'https://siopen.balangankab.go.id/storage/merchant/products/2024/08/24/36c9acb93d77c44912d786c9e623c4b5.jpg'),
('Lem Tikus', 'Generic', 16000, 5, 'rumah', 'https://image.astronauts.cloud/product-images/2024/6/13_8f896e40-4e8a-4d90-a19c-14151f692f26_900x900.jpg'),
('Jas Hujan Cap Kapak', 'Cap Kapak', 45000, 5, 'jas hujan', 'https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full/catalog-image/92/MTA-177136020/brd-46010_jas-hujan-ponco-cap-kapak_full01-cd811e71.jpg'),
('Payung Golf Jumbo', 'Generic', 55000, 4, 'payung', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTgBehJaUKbnyY3Bv4UTjxZefgdGM3EkIr8FA&s'),
('Hanger Hitam 12pcs', 'Generic', 9000, 5, 'rumah', 'https://media.monotaro.id/mid01/big/Alat%20%26%20Kebutuhan%20Kebersihan/Perlengkapan%20Binatu/Gantungan%20Laundry/Indokurnia%20Hanger%20Kayu%20Dewasa%20Polos/Indokurnia%20Hanger%20Kayu%20Dewasa%20Polos%20Hitam%201set(12pcs)/oeS034323878-1.jpg'),
('Kursi', 'Generic', 30000, 5, 'furniture', 'https://d2xjmi1k71iy2m.cloudfront.net/dairyfarm/id/images/198/1319859_PE941002_S4.jpg'),
('Lunch Box Ginza', 'Ginza', 13000, 5, 'peralatan makan', 'https://down-id.img.susercontent.com/file/id-11134207-7r98p-lwpzkeqia29p8e'),
('Jam Dinding Mayomi 917', 'Mayomi', 25000, 5, 'jam', 'https://down-id.img.susercontent.com/file/id-11134207-7qul1-lj6myb0oc9er76'),
('Keset Handuk Jumbo', 'Generic', 22000, 5, 'rumah', 'https://img.lazcdn.com/g/p/137ff5474c08eb74f9ffa81832c3a3e2.png_720x720q80.png'),
('Handuk 70x140', 'Generic', 42000, 5, 'handuk', 'https://static.jakmall.id/2021/01/images/products/3baef8/thumbnail/handuk-polos-besar-70-x-140-handuk-besar.jpg'),

-- SABUN & PEMBERSIH
('Kifa Pencuci Piring 650ml', 'Kifa', 8000, 5, 'sabun', 'https://cf.shopee.co.id/file/sg-11134201-23010-inibocr7ivlv07'),
('Kifa Karbol Wangi 700ml', 'Kifa', 11000, 5, 'sabun', 'https://www.mirotakampus.com/resources/assets/images/product_images/1671196995.super%20kifa.jpg'),

-- PERLENGKAPAN
('Kertas Kado', 'Generic', 22000, 5, 'natal', 'https://squeezy.co.id/wp-content/uploads/2024/06/KERTAS-KADO-KFH-173BBC.jpg'),
('Hiasan Natal', 'Generic', 30000, 5, 'natal', 'https://d2xjmi1k71iy2m.cloudfront.net/dairyfarm/id/pageImages/page__en_us_15747432270.jpeg'),
('Box Container Black 50l', 'Generic', 60000, 5, 'storage', 'https://down-id.img.susercontent.com/file/id-11134207-7qul4-licqqk3khxg681'),

-- PERALATAN MAKAN
('Mangkok Sultan Isi 1', 'Sultan', 12000, 5, 'peralatan makan', 'https://img.lazcdn.com/g/p/06d793599c817a1fca28924dffe38e0e.jpg_720x720q80.jpg'),
('Mangkok Sultan Isi 2', 'Sultan', 20000, 5, 'peralatan makan', 'https://img.lazcdn.com/g/p/06d793599c817a1fca28924dffe38e0e.jpg_720x720q80.jpg'),
('Mangkok Sultan Isi 4', 'Sultan', 40000, 5, 'peralatan makan', 'https://img.lazcdn.com/g/p/06d793599c817a1fca28924dffe38e0e.jpg_720x720q80.jpg'),
('Gelas Voila 3pcs', 'Voila', 13000, 5, 'peralatan makan', 'https://rajaperabotan.co.id/wp-content/uploads/2019/06/Viera-TMS99-147-6pcs-Gelas-Kaca-Set-Polkadot-240ml-1.jpg'),
('Mangkok Ayam 3pcs', 'Generic', 13000, 5, 'peralatan makan', 'https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full//87/MTA-20179112/mgm_mangkok-sambal-ayam-3inch-keramik-sni-ayam-jago-tempat-sambal_full01.jpg'),
('Termos Sultan + 2 Cangkir', 'Sultan', 30000, 5, 'termos', 'https://down-id.img.susercontent.com/file/id-11134207-23020-xhumyw53tmnv11'),
('Termos Serba', 'Generic', 38000, 5, 'termos', 'https://img.lazcdn.com/g/p/0d6644e42e3ab6a295f22cd614338a3b.jpg_360x360q80.jpg'),

-- LAIN-LAIN
('Botol Minum Xiao Oval', 'Xiao', 37000, 5, 'botol', 'https://images.tokopedia.net/img/cache/250-square/aphluv/1997/1/1/9808fc04e4194f8fa25f5681042e9148~.jpeg?ect=4g'),
('Botol Stainless', 'Generic', 56000, 5, 'botol', 'https://upload.jaknot.com/2025/08/images/products/38321c/original/baowenbei-botol-minum-termos-air-panas-dingin-stainless-steel-500ml-a1a0.jpg'),
('Cooler Bag', 'Generic', 13000, 5, 'tas', 'https://contents.mediadecathlon.com/p1739673/k$d33c1cb195c68272634f4f67552f7d8d/kotak-es-berkemah-cooler-bag-35-l-tahan-dingin-17-jam-quechua-8572258.jpg?f=1920x0&format=auto'),
('Celana Pendek', 'Generic', 32000, 5, 'pakaian', 'https://d29c1z66frfv6c.cloudfront.net/pub/media/catalog/product/zoom/9f2be9552ca69299bafe86de68db4712dcc35b3f_xxl-1.jpg'),
('Kaos Kaki Remaja', 'Generic', 10000, 5, 'pakaian', 'https://parto.id/asset/foto_produk/kaos_kaki.jpg'),
('Meja Belajar', 'Generic', 48000, 5, 'furniture', 'https://www.homarindo.com/wp-content/uploads/2024/08/Meja-Belajar-Minimalis-Penyimpanan-3-Laci-Murah-1.jpg'),
('Tas Sekolah', 'Generic', 175000, 4, 'tas', 'https://torch.id/cdn/shop/files/SamataBlack2.jpg?v=1757300726&width=1445'),
('Mainan', 'Generic', 60000, 5, 'mainan', 'https://cdn.outleap.de/o3Rp9vZwL176EQ4UVUo0_Nc-hJezQPi4wdtyqOaxl3U/rs:fit:239:239::1/dpr:2/bg:FFF/da:1/czM6Ly9jZG4tb3V0/bGVhcC9ndW5maW5k/ZXIvcHJvZHVjdHMv/QmlsZHNjaGlybWZv/dG9fMjAyMi0wMS0w/M191bV8xOS4wOS4x/NV9NQnFZcC1RZzRI/WS5wbmc?ck=1753295001');

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
