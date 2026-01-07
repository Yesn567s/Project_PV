/*
SQLyog Community v13.3.1 (64 bit)
MySQL - 10.4.32-MariaDB : Database - db_proyek_pv
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`db_proyek_pv` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;

USE `db_proyek_pv`;

/*Table structure for table `cart` */

DROP TABLE IF EXISTS `cart`;

CREATE TABLE `cart` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `member_id` INT(11) DEFAULT NULL,
  `session_id` VARCHAR(100) DEFAULT NULL,
  `created_at` DATETIME DEFAULT CURRENT_TIMESTAMP(),
  `updated_at` DATETIME DEFAULT CURRENT_TIMESTAMP() ON UPDATE CURRENT_TIMESTAMP(),
  PRIMARY KEY (`ID`),
  UNIQUE KEY `unique_member_cart` (`member_id`),
  UNIQUE KEY `unique_session_cart` (`session_id`),
  KEY `idx_cart_member` (`member_id`),
  KEY `idx_cart_session` (`session_id`),
  CONSTRAINT `cart_ibfk_1` FOREIGN KEY (`member_id`) REFERENCES `member` (`ID`) ON DELETE CASCADE
) ENGINE=INNODB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `cart` */

INSERT  INTO `cart`(`ID`,`member_id`,`session_id`,`created_at`,`updated_at`) VALUES 
(1,10,NULL,'2026-01-06 08:53:40','2026-01-06 09:39:55'),
(2,NULL,'8d97c347-4cdb-4de8-8232-f5a509743993','2026-01-06 08:59:15','2026-01-06 08:59:15'),
(3,NULL,'37223ed2-3368-4480-a7ab-3633ef4d6089','2026-01-06 09:33:37','2026-01-06 09:33:37'),
(4,NULL,'dfd63cf2-7887-4301-acec-706181e4fa46','2026-01-06 14:14:16','2026-01-06 14:14:16');

/*Table structure for table `cart_detail` */

DROP TABLE IF EXISTS `cart_detail`;

CREATE TABLE `cart_detail` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `cart_id` INT(11) NOT NULL,
  `produk_id` INT(11) NOT NULL,
  `Qty` INT(11) NOT NULL DEFAULT 1,
  `added_at` DATETIME DEFAULT CURRENT_TIMESTAMP(),
  PRIMARY KEY (`ID`),
  UNIQUE KEY `unique_cart_product` (`cart_id`,`produk_id`),
  KEY `idx_cart_detail_cart` (`cart_id`),
  KEY `idx_cart_detail_product` (`produk_id`),
  CONSTRAINT `cart_detail_ibfk_1` FOREIGN KEY (`cart_id`) REFERENCES `cart` (`ID`) ON DELETE CASCADE,
  CONSTRAINT `cart_detail_ibfk_2` FOREIGN KEY (`produk_id`) REFERENCES `produk` (`ID`) ON DELETE CASCADE
) ENGINE=INNODB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `cart_detail` */

INSERT  INTO `cart_detail`(`ID`,`cart_id`,`produk_id`,`Qty`,`added_at`) VALUES 
(1,1,16,5,'2026-01-06 08:53:55'),
(2,1,17,1,'2026-01-06 08:53:59'),
(3,1,18,1,'2026-01-06 08:54:02'),
(4,1,22,8,'2026-01-06 08:54:05'),
(5,1,25,1,'2026-01-06 09:32:22');

/*Table structure for table `kategori` */

DROP TABLE IF EXISTS `kategori`;

CREATE TABLE `kategori` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Nama` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Nama` (`Nama`)
) ENGINE=INNODB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `kategori` */

INSERT  INTO `kategori`(`ID`,`Nama`) VALUES 
(3,'Apparel'),
(2,'Electronic'),
(5,'Entertainment'),
(1,'Food & Beverage'),
(4,'Health & Beauty');

/*Table structure for table `member` */

DROP TABLE IF EXISTS `member`;

CREATE TABLE `member` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Nama` VARCHAR(100) NOT NULL,
  `Email` VARCHAR(100) NOT NULL,
  `PASSWORD` VARCHAR(50) NOT NULL,
  `Tanggal_Lahir` DATE NOT NULL,
  `Is_Member` TINYINT(1) NOT NULL,
  `membership_start` DATE DEFAULT NULL,
  `membership_end` DATE DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=INNODB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `member` */

INSERT  INTO `member`(`ID`,`Nama`,`Email`,`PASSWORD`,`Tanggal_Lahir`,`Is_Member`,`membership_start`,`membership_end`) VALUES 
(1,'Kevin Setiono','kevin@gmail.com','123','2003-08-12',0,NULL,NULL),
(2,'Budi Santoso','budi@gmail.com','123','1995-11-05',0,NULL,NULL),
(3,'Cindy Wijaya','cindy@gmail.com','123','2001-06-18',0,NULL,NULL),
(4,'Evelyn Kurnia','evelyn@gmail.com','123','2004-02-14',1,'2025-01-20',NULL),
(5,'Gracia Tania','gracia@gmail.com','123','2000-01-30',1,'2025-02-01',NULL),
(6,'Indah Pertiwi','indah@gmail.com','123','2002-07-09',1,'2025-03-10',NULL),
(7,'Alicia Putri','alicia@gmail.com','123','1999-03-22',1,'2025-04-01','2026-03-18'),
(8,'Davin Pratama','davin@gmail.com','123','1998-12-29',1,'2025-03-15','2026-07-09'),
(9,'Fajar Nugroho','fajar@gmail.com','123','1997-09-10',1,'2025-02-01','2026-01-27'),
(10,'Hendra Wijaya','hendra@gmail.com','123','1996-05-25',1,'2025-01-10','2026-11-05');

/*Table structure for table `produk` */

DROP TABLE IF EXISTS `produk`;

CREATE TABLE `produk` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Nama` VARCHAR(100) NOT NULL,
  `Merk` VARCHAR(100) DEFAULT NULL,
  `Harga` INT(11) NOT NULL,
  `kategori_id` INT(11) NOT NULL,
  `tag` VARCHAR(255) DEFAULT NULL,
  `image_url` VARCHAR(500) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `kategori_id` (`kategori_id`),
  CONSTRAINT `produk_ibfk_1` FOREIGN KEY (`kategori_id`) REFERENCES `kategori` (`ID`)
) ENGINE=INNODB AUTO_INCREMENT=72 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `produk` */

INSERT  INTO `produk`(`ID`,`Nama`,`Merk`,`Harga`,`kategori_id`,`tag`,`image_url`) VALUES 
(1,'Sedapmie','Sedaap',3500,1,'Mie Instan','https://solvent-production.s3.amazonaws.com/media/images/products/2021/04/2499a.jpg'),
(2,'Indomie','Indofood',3500,1,'Mie Instan','https://solvent-production.s3.amazonaws.com/media/images/products/2021/04/2494a.jpg'),
(3,'Indomie Goreng','Indofood',3500,1,'Mie Instan','https://solvent-production.s3.amazonaws.com/media/images/products/2021/04/2494a.jpg'),
(4,'Gula Rose Brand 1 kg','Rose Brand',18500,1,'Sembako, Gula','https://solvent-production.s3.amazonaws.com/media/images/products/2022/08/DSC_0829.JPG'),
(5,'Gula Rose Brand Hijau 1 kg','Rose Brand',18500,1,'Sembako, Gula','https://c.alfagift.id/product/1/1_A09170277363_20200824132308167_base.jpg'),
(6,'Minyak Goreng Hemart 900 ml','Hemart',18500,1,'Sembako, Minyak','https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full/catalog-image/104/MTA-182275096/brd-29831_minyak-goreng-hemart-kemasan-botol-1-liter-free-packing-kardus_full07-99ee99e7.jpg'),
(7,'Minyak Goreng Fitri 800 ml','Fitri',17500,1,'Sembako, Minyak','https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full/catalog-image/MTA-7097935/fitri_smg-jog-solo_-_fitri_minyak_goreng_sawit_pouch_1800_ml_tbkt_full01_facchq2o.jpg'),
(8,'Minyak Goreng 800 ml','Umum',17000,1,'Sembako, Minyak','https://c.alfagift.id/product/1/1_A09350045270_20240122094000330_base.jpg'),
(9,'Beras Raja Lele 5 kg','Raja Lele',80000,1,'Sembako, Beras','https://arti-assets.sgp1.cdn.digitaloceanspaces.com/megaswalayan/products/8abfc71d-1c5a-4bca-baf0-3c5a570cd203.jpg'),
(10,'Beras Rojo Lele 5 kg','Rojo Lele',80000,1,'Sembako, Beras','https://arti-assets.sgp1.cdn.digitaloceanspaces.com/megaswalayan/products/8abfc71d-1c5a-4bca-baf0-3c5a570cd203.jpg'),
(11,'Beras Cap Uduk 5 kg','Cap Uduk',82000,1,'Sembako, Beras','https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full/catalog-image/104/MTA-177755612/nasi-uduk_beras-premium-uduk-hijau-5kg_full01.jpg'),
(12,'Beras Anggur 5 kg','Anggrus',82000,1,'Sembako, Beras','https://parto.id/asset/foto_produk/beras_naggur_jpg_171878826074.jpg'),
(13,'Beras Enak 5 kg','Enak',78000,1,'Sembako, Beras','https://supermama.co.id/wp-content/uploads/2022/09/PNG-UENAK.png'),
(14,'Beras Enak 3 kg','Enak',50000,1,'Sembako, Beras','https://supermama.co.id/wp-content/uploads/2022/09/PNG-UENAK.png'),
(15,'Beras Sumo Kuning 5 kg','Sumo',85000,1,'Sembako, Beras','https://p16-images-sign-sg.tokopedia-static.net/tos-alisg-i-aphluv4xwc-sg/img/VqbcmM/2025/7/17/d77c48f0-8629-43fd-b2e3-4796ab6816e2.jpg~tplv-aphluv4xwc-resize-jpeg:700:0.jpeg?lk3s=0ccea506&x-expires=1767027165&x-signature=fickjhtchDYMwaO8Cu%2BgIgTC3Dc%3D&x-signature-webp=yGSOIU%2Fz0j8I87pm3qC0cDqaBP8%3D'),
(16,'Air Cleo 220 ml','Cleo',2000,1,'Minuman, Air Mineral','https://cf.shopee.co.id/file/id-11134207-7rbk0-mam1t88zo31n0e'),
(17,'Air Cleo 220 ml Isi 24','Cleo',18500,1,'Minuman, Air Mineral','https://p16-images-sign-sg.tokopedia-static.net/tos-alisg-i-aphluv4xwc-sg/5a08eb1fb35e4a95b8b9ea39d295be2a~tplv-aphluv4xwc-resize-jpeg:700:0.jpeg?lk3s=0ccea506&x-expires=1767027282&x-signature=vIj4NNqqI5qYCq3EYjfK69ty6JE%3D&x-signature-webp=okJLs4ARvWFQB40C17EUU9OdbBA%3D'),
(18,'Air Pristine 400 ml','Pristine',3500,1,'Minuman, Air Mineral','https://c.alfagift.id/product/1/1_A8262900002167_20250516142237557_base.jpg'),
(19,'Cleo Air Mineral 550 ml Isi 6','Cleo',12000,1,'Minuman, Air Mineral','https://solvent-production.s3.amazonaws.com/media/images/products/2021/05/IMG20210201155138.jpg'),
(20,'Pristine Air Mineral 600 ml','Pristine',4500,1,'Minuman, Air Mineral','https://c.alfagift.id/product/1/1_A8262900002167_20250516142237557_base.jpg'),
(21,'Pristine Air Mineral 1500 ml','Pristine',8000,1,'Minuman, Air Mineral','https://c.alfagift.id/product/1/1_A8262900002167_20250516142237557_base.jpg'),
(22,'Ameria Biscuit Columbia','Columbia',28000,1,'Biskuit','https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTG4mww39cvVE7VzODsx7qKkZLha3gYWFDjhQ&s'),
(23,'Biskuit Columbia Toples','Columbia',15000,1,'Biskuit','https://www.sumberpangansukses.com/assets/produk/d98d74dd7aa145fab0216c2bdf72271e.jpg'),
(24,'Flashdisk Lexar V40 8 GB','Lexar',45000,2,'Flashdisk','https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT0VnRs0D_hBSf4J1p7ISP8faBF7-dJ2-TDVw&s'),
(25,'Flashdisk Lexar V40 16 GB','Lexar',55000,2,'Flashdisk','https://planetcomputer.co.id/wp-content/uploads/2024/08/67342744_cc42ab57-cf1a-43e9-8667-95a259c3f88d_640_640.jpg'),
(26,'Flashdisk Lexar V40 32 GB','Lexar',65000,2,'Flashdisk','https://planetcomputer.co.id/wp-content/uploads/2024/08/23867496_5ece3732-b278-42ae-b05d-8d026331f9ff_700_700-600x600.jpg'),
(27,'Flashdisk Lexar V40 64 GB','Lexar',75000,2,'Flashdisk','https://pegastore.id/media/product/produk-1737532395.jpg'),
(28,'Kalkulator M&G Mgc-10','M&G',110000,2,'Kalkulator','https://halimonline.com/20997-large_default/calculator-mg-12-digits-mgc-10-adg98779.jpg'),
(29,'Kalkulator Scientific Casio FX-570ES Plus','Casio',451500,2,'Kalkulator','https://www.casio.com/content/dam/casio/product-info/locales/id/id/calc/product/scientific/F/FX/FX5/fx-570esplus-2bu/assets/fx-570ES_PLUS-2BU_F.png.transform/main-visual-pc/image.png'),
(30,'Kalkulator Scientific Casio FX-82ES Plus','Casio',287000,2,'Kalkulator','https://www.casio.com/content/dam/casio/product-info/locales/id/id/calc/product/scientific/F/FX/FX8/fx-82ESPLUS-2/assets/fx-82ES_PLUS-2_F.png.transform/main-visual-pc/image.png'),
(31,'Kalkulator Scientific Casio FX-991ES Plus','Casio',481000,2,'Kalkulator','https://www.casio.com/content/dam/casio/product-info/locales/id/id/calc/product/scientific/F/FX/FX9/fx-991esplus-2pk/assets/fx-991ES_PLUS-2PK_F.png.transform/main-visual-pc/image.png'),
(32,'Kalkulator Joyko CC-40','Joyko',52000,2,'Kalkulator','https://www.joyko.co.id/image/cache/data/CC-40-01-650x650.jpg'),
(33,'Kalkulator Scientific Joyko CC-25BP','Joyko',45000,2,'Kalkulator','https://www.joyko.co.id/image/cache/data/COVER-25-650x650.jpg'),
(34,'Jete Power Bank B7 10000 Mah','Jete',350000,2,'Power Bank','https://jete.id/wp-content/uploads/2025/03/Desc-Powerbank-JETE-B7-10000-New-Color-18-600x600.jpg'),
(35,'Vivan Vpb-D11 10.000 Mah','Vivan',250000,2,'Power Bank','https://down-id.img.susercontent.com/file/id-11134207-8224x-mgdspnybrjt584'),
(36,'Vtec Magnet Set Ms-8074','V-Tec',18000,2,'Magnet','https://images.tokopedia.net/img/cache/500-square/VqbcmM/2025/1/8/8d066233-8265-4282-b5bf-12a44b3f96fd.png'),
(37,'Kipas Portable','Umum',20000,2,'Elektronik','https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full/catalog-image/MTA-11951965/kalno_kipas_angin_portable_mini_fan_terbaru_ada_holder_hp_-_mini_fan_usb_charging_rechargeable_l18_random_full01_hmr2qv00.jpg'),
(38,'Jas Hujan Jaket Celana Elmondo','Elmondo',60000,3,'Jas Hujan','https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRo_KMIx6pig3UZ0LNFiqukIGbPIZOxk_nPBg&s'),
(39,'Jas Hujan Elmondo Celana Jaket 935','Elmondo',45000,3,'Jas Hujan','https://down-id.img.susercontent.com/file/id-11134207-7rasg-m3rzjj7rj573fa'),
(40,'Jas Hujan Cap Kapak','Kapak',48000,3,'Jas Hujan','https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRaa9SDmJWW_WebUHj7UYtYMPQVYmBgzTA5Ng&s'),
(41,'Jas Hujan Kiddy Rets','Kiddy',44000,3,'Jas Hujan','https://down-id.img.susercontent.com/file/sg-11134201-22110-hd4pzlm4vsjve0'),
(42,'Tas Sekolah','Umum',150000,3,'Tas','https://torch.id/cdn/shop/files/SamataBlack2.jpg?v=1757300726&width=1445'),
(43,'Kaos Kaki Remaja','Umum',12000,3,'Pakaian','https://img.lazcdn.com/g/p/0d9154f2d8d9c0d0feda2b4a6c29aedb.jpg_720x720q80.jpg'),
(44,'Celana Pendek','Umum',35000,3,'Pakaian','https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full//catalog-image/102/MTA-142959621/no_brands_celana_pendek_cowo_boardshort_pantai_boxer_full03_tn4sw094.jpg'),
(45,'Topi Natal Glitter','Umum',20000,3,'Natal','https://cdn.ruparupa.io/fit-in/400x400/filters:format(webp)/filters:quality(90)/ruparupa-com/image/upload/Products/10590765_3.jpg'),
(46,'Tissue See U 250 Sheet','See U',8500,4,'Tissue','https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full//91/MTA-7277202/see-u_see-u_classic_facial_tissue_-250_sheet-_full02_hfmvmlh9.jpg'),
(47,'Tissue Basah Mitu Wipes','Mitu',13000,4,'Tissue Basah','https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full//97/MTA-16704537/mitu_mitu_baby_wipes_tisu_basah_-_pink_-50_sheets-_full01_eim6q5h0.jpg'),
(48,'Tissue Paseo','Paseo',28000,4,'Tissue','https://image.astronauts.cloud/product-images/2024/1/PaseoSmartFacialSoftPack3_894fbed8-4077-47a2-bdc1-f85ddce9f8dc_900x897.jpg'),
(49,'Tissue Fusia Jumbo 1000 Sheet','Fusia',30000,4,'Tissue','https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full//105/MTA-71747925/peony_tisu_facial_lembut_isi_180s_2ply_peony_-_fusia_full05_n68nk82k.jpg'),
(50,'Hit Aerosol 600 ml','Hit',38000,4,'Anti Nyamuk','https://down-id.img.susercontent.com/file/id-11134207-7r98x-lw09ywibwduy3e'),
(51,'Masker Duckbill Alkindo Isi 50','Alkindo',15000,4,'Kesehatan','https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full//catalog-image/96/MTA-129630009/alkindo_masker_duckbill_alkindo_4ply_isi_50_pcs_emboss_disposable_50pcs_full05_utpl9yiu.jpg'),
(52,'Freshcare','Freshcare',14000,4,'Kesehatan','https://res-1.cloudinary.com/dk0z4ums3/image/upload/c_scale,h_500,w_500/v1/production/pharmacy/products/1732609444_freshcare_hot_10_ml'),
(53,'Kifa Sabun Cuci Piring','Kifa',8500,4,'Sabun','https://img.klikindogrosir.com/images/products/1782090.png'),
(54,'Lem Tikus Gajah','Gajah',18000,4,'Kebersihan','https://image.astronauts.cloud/product-images/2024/6/13_8f896e40-4e8a-4d90-a19c-14151f692f26_900x900.jpg'),
(55,'Buku Tulis Kiky 38 Lembar','Kiky',28000,5,'Buku Tulis','https://down-id.img.susercontent.com/file/id-11134207-7r98z-lwuwzzz7o7xm77'),
(56,'Buku Tulis Sinar Dunia 32 Lembar','Sidu',30000,5,'Buku Tulis','https://siplah.blibli.com/data/images/SMAH-0008-00596/500b8241-be5a-4e8a-b436-4eddc6c11f0c.jpg'),
(57,'Bolpen Sarasa','Zebra',16000,5,'Alat Tulis','https://img.lazcdn.com/g/ff/kf/S88b74865074c434ab59a51dd427e6414a.jpg_720x720q80.jpg'),
(58,'Bolpen AE7 1 Lusin','Standard',22000,5,'Alat Tulis','https://smb-padiumkm-images-public-prod.oss-ap-southeast-5.aliyuncs.com/product/image/02102023/64d1c58da8f6af718485e746/651a7331419f30ab8982fb88/774c70b9cc7e3c11762aeeb810f325.jpeg?x-oss-process=image/resize,m_pad,w_432,h_432/quality,Q_70'),
(59,'Crayon Greebel 55 Warna','Greebel',85000,5,'Alat Tulis','https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full/catalog-image/MTA-138785705/greebel_greebel_crayon_krayon_kids_oil_pastel_55_warna_-55c-_tdk_berdebu_cerah_utk_anak_sekolah_art-_tdk_beracun_full02_uh0jkusp.jpg'),
(60,'Magic Clay','Umum',6000,5,'Mainan','https://img.lazcdn.com/g/ff/kf/S26912b104acb4c8fa39df2d2b7b857eeZ.jpg_720x720q80.jpg'),
(61,'Bubble Stick','Umum',8000,5,'Mainan','https://filebroker-cdn.lazada.co.id/kf/S1e6283810b614e09a671fe89f055beb40.jpg'),
(62,'Meja Belajar Anak','Umum',65000,5,'Furniture','https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full//catalog-image/94/MTA-150402881/no_brand_set_meja_belajar_anak_meja_tulis_anak_type_k11-_full01_btwcm98d.jpg'),
(63,'Aneka Kursi','Umum',35000,5,'Furniture','https://sc04.alicdn.com/kf/H4e974323b1004c458f8cb92e99fd73145.jpg'),
(64,'Mangkok Sultan','Sultan',12000,5,'Peralatan Rumah','https://img.lazcdn.com/g/p/06d793599c817a1fca28924dffe38e0e.jpg_720x720q80.jpg'),
(65,'Termos Sultan 2 Cangkir','Sultan',35000,5,'Peralatan Rumah','https://www.static-src.com/wcsstore/Indraprastha/images/catalog/full//catalog-image/97/MTA-108135267/tidak_ada_merk_termos_sultan_vacuum_flask_set_botol_minum_cangkir_gift_premium_500_ml_full06_dcg1pyxn.jpg'),
(66,'Botol Minum Cetek Sedotan Mix 900 ml','Umum',18000,5,'Botol','https://down-id.img.susercontent.com/file/id-11134207-7ras9-m0zi1zuixrc48c'),
(67,'Gantungan Kunci','Umum',5000,5,'Aksesoris','https://img.lazcdn.com/g/p/c9a8ef327b35b36cc41804c985144888.jpg_720x720q80.jpg'),
(68,'Jam Dinding Mayomi Type 917','Mayomi',30000,5,'Dekorasi','https://images.tokopedia.net/img/cache/700/VqbcmM/2022/3/19/85f657ce-4c4a-4709-a71d-a1c0422d6939.png'),
(69,'HVS Sinarline A4','Sinarline',40000,5,'Kertas','https://siplah.blibli.com/data/images/SASH-0240-00438/fc352501-23df-4501-a6a7-9b2ae402a86a.jpg'),
(70,'HVS Sidu 70 gsm A4','Sidu',42000,5,'Kertas','https://sidu.id/documents/287278/309575/SDC_A470_500_Green+depan.png/bb0d88f9-56a4-4ab4-5bdf-7a57f62b04c0?t=1706770983130'),
(71,'Odner Bantex 1461','Bantex',35000,5,'ATK','https://images.tokopedia.net/img/cache/700/product-1/2020/7/16/98480860/98480860_d7d28a1d-dea7-4a1a-805f-6626e47d7611_600_600');

/*Table structure for table `promo` */

DROP TABLE IF EXISTS `promo`;

CREATE TABLE `promo` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Nama_Promo` VARCHAR(100) NOT NULL,
  `Target_Type` ENUM('Produk','Kategori','Tag','Merk','Global') NOT NULL,
  `Target_Value` VARCHAR(100) NOT NULL,
  `Jenis_Promo` ENUM('Harga_Jadi','Persen','Grosir','Bonus') NOT NULL,
  `Nilai_Potongan` FLOAT DEFAULT 0,
  `Harga_Baru` INT(11) DEFAULT NULL,
  `Min_Qty` INT(11) DEFAULT 1,
  `Bonus_Produk_ID` INT(11) DEFAULT NULL,
  `Gratis_Qty` INT(11) DEFAULT 0,
  `START` DATETIME DEFAULT CURRENT_TIMESTAMP(),
  `END` DATETIME DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `Bonus_Produk_ID` (`Bonus_Produk_ID`),
  CONSTRAINT `promo_ibfk_1` FOREIGN KEY (`Bonus_Produk_ID`) REFERENCES `produk` (`ID`)
) ENGINE=INNODB AUTO_INCREMENT=221 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `promo` */

INSERT  INTO `promo`(`ID`,`Nama_Promo`,`Target_Type`,`Target_Value`,`Jenis_Promo`,`Nilai_Potongan`,`Harga_Baru`,`Min_Qty`,`Bonus_Produk_ID`,`Gratis_Qty`,`START`,`END`) VALUES 
(1,'Promo Biasa','Produk','Tissue See U','Harga_Jadi',0,1440,1,NULL,0,'2025-07-01 00:00:00','2025-07-01 23:59:00'),
(2,'Promo Biasa','Produk','Gantungan Kunci','Grosir',0,11000,3,NULL,0,'2025-07-01 00:00:00','2025-07-01 23:59:00'),
(3,'Promo Biasa','Produk','Beras Raja Lele 5 kg','Harga_Jadi',0,73500,1,NULL,0,'2025-07-01 00:00:00','2025-07-01 23:59:00'),
(4,'Promo Biasa','Produk','Air Minum Cleo 24 Botol','Harga_Jadi',0,3980,1,NULL,0,'2025-07-03 00:00:00','2025-07-03 23:59:00'),
(5,'Promo Biasa','Produk','Bolpoin Quantum Selusin','Harga_Jadi',0,3000,1,NULL,0,'2025-07-03 00:00:00','2025-07-03 23:59:00'),
(6,'Promo Biasa','Produk','Pensil Squeesy Selusin','Harga_Jadi',0,3000,1,NULL,0,'2025-07-03 00:00:00','2025-07-03 23:59:00'),
(7,'Promo Biasa','Produk','Bolpoin Gel Kokoro','Harga_Jadi',0,4600,1,NULL,0,'2025-07-03 00:00:00','2025-07-03 23:59:00'),
(8,'Promo Biasa','Produk','Crayon Murah Vanart','Harga_Jadi',0,6000,1,NULL,0,'2025-07-03 00:00:00','2025-07-03 23:59:00'),
(9,'PROMO GO TO SCHOOL','Produk','Buku Tulis Kiky 32 Lembar','Harga_Jadi',0,21000,1,NULL,0,'2025-07-03 00:00:00','2025-07-03 23:59:00'),
(10,'PROMO GO TO SCHOOL','Produk','Buku Tulis Dg','Harga_Jadi',0,22000,1,NULL,0,'2025-07-03 00:00:00','2025-07-03 23:59:00'),
(11,'PROMO GO TO SCHOOL','Produk','Buku Tulis Skola 38 Lembar','Harga_Jadi',0,23400,1,NULL,0,'2025-07-03 00:00:00','2025-07-03 23:59:00'),
(12,'PROMO GO TO SCHOOL','Produk','Buku Tulis Sinar Dunia 32 Lembar','Harga_Jadi',0,26000,1,NULL,0,'2025-07-03 00:00:00','2025-07-03 23:59:00'),
(13,'PROMO GO TO SCHOOL','Produk','Kotak Pensil','Harga_Jadi',0,4500,1,NULL,0,'2025-07-03 00:00:00','2025-07-03 23:59:00'),
(14,'PROMO GO TO SCHOOL','Produk','Stiker Box Isi 100','Harga_Jadi',0,8000,1,NULL,0,'2025-07-03 00:00:00','2025-07-03 23:59:00'),
(15,'PROMO GO TO SCHOOL','Produk','Tissue See U 250 Sheet','Harga_Jadi',0,6888,1,NULL,0,'2025-07-03 00:00:00','2025-07-03 23:59:00'),
(16,'PROMO GO TO SCHOOL','Produk','Tas Sekolah','Persen',15,NULL,1,NULL,0,'2025-07-03 00:00:00','2025-07-03 23:59:00'),
(17,'PROMO GO TO SCHOOL','Produk','Meja Belajar','Harga_Jadi',0,49900,1,NULL,0,'2025-07-03 00:00:00','2025-07-03 23:59:00'),
(18,'Promo Biasa','Produk','Botol Minum 800 ml','Harga_Jadi',0,16000,1,NULL,0,'2025-07-04 00:00:00','2025-07-04 23:59:00'),
(19,'Promo Biasa','Produk','Beras Rojo Lele','Harga_Jadi',0,72500,1,NULL,0,'2025-07-04 00:00:00','2025-07-04 23:59:00'),
(20,'Promo Biasa','Produk','Cleo Air Mineral 550 ml Isi 6','Harga_Jadi',0,10000,1,NULL,0,'2025-07-18 00:00:00','2025-07-21 23:59:00'),
(21,'Promo Biasa','Produk','Buku Tulis Sinar Dunia 32 Lembar','Harga_Jadi',0,25900,1,NULL,0,'2025-07-18 00:00:00','2025-07-21 23:59:00'),
(22,'Promo Biasa','Produk','Buku Tulis Kraft Sampul Coklat 40 Lembar','Harga_Jadi',0,27900,1,NULL,0,'2025-07-18 00:00:00','2025-07-21 23:59:00'),
(23,'Promo Biasa','Produk','Buku Tulis Big Boss','Harga_Jadi',0,23000,1,NULL,0,'2025-07-18 00:00:00','2025-07-21 23:59:00'),
(24,'Promo Biasa','Produk','Buku Tulis Kiky','Harga_Jadi',0,21000,1,NULL,0,'2025-07-18 00:00:00','2025-07-21 23:59:00'),
(25,'Promo Biasa','Produk','Mainan Clay','Grosir',0,5900,3,NULL,0,'2025-07-18 00:00:00','2025-07-21 23:59:00'),
(26,'Promo Biasa','Produk','Mainan Clay','Harga_Jadi',0,6400,1,NULL,0,'2025-07-18 00:00:00','2025-07-21 23:59:00'),
(27,'Promo Biasa','Produk','Tissue Basah Mitu Wipes','Bonus',0,10900,1,47,1,'2025-07-18 00:00:00','2025-07-21 23:59:00'),
(28,'Promo Biasa','Produk','Kotak Pensil Rakit','Grosir',0,9000,3,NULL,0,'2025-07-18 00:00:00','2025-07-21 23:59:00'),
(29,'Promo Biasa','Produk','Gelas Viola','Grosir',0,10000,3,NULL,0,'2025-07-18 00:00:00','2025-07-21 23:59:00'),
(30,'Promo Biasa','Produk','Gantungan Kunci','Grosir',0,11000,3,NULL,0,'2025-07-18 00:00:00','2025-07-21 23:59:00'),
(31,'Promo Biasa','Produk','Cleo Air Mineral 550 ml Isi 6','Harga_Jadi',0,10000,1,NULL,0,'2025-07-19 00:00:00','2025-07-19 23:59:00'),
(32,'Promo Biasa','Produk','Bolpoin Gel Kokoro','Harga_Jadi',0,4666,1,NULL,0,'2025-07-19 00:00:00','2025-07-19 23:59:00'),
(33,'Promo Biasa','Produk','Bolpen Kokoro Isi 13 pcs','Harga_Jadi',0,59900,1,NULL,0,'2025-07-19 00:00:00','2025-07-19 23:59:00'),
(34,'Promo Biasa','Produk','Beras Enak 5 kg','Harga_Jadi',0,73500,1,NULL,0,'2025-07-19 00:00:00','2025-07-19 23:59:00'),
(35,'Promo Biasa','Produk','Beras Enak 3 kg','Harga_Jadi',0,46000,1,NULL,0,'2025-07-20 00:00:00','2025-07-20 23:59:00'),
(36,'Promo Biasa','Produk','Beras Sumo Kuning 5 kg','Harga_Jadi',0,80000,1,NULL,0,'2025-07-20 00:00:00','2025-07-20 23:59:00'),
(37,'Promo Biasa','Produk','Indomie Goreng','Harga_Jadi',0,2900,1,NULL,0,'2025-07-20 00:00:00','2025-07-20 23:59:00'),
(38,'Promo Biasa','Produk','Toples Biskuit','Grosir',0,24000,2,NULL,0,'2025-07-23 00:00:00','2025-08-31 23:59:00'),
(39,'Promo Biasa','Produk','Parcel Mangkok Biskuit','Harga_Jadi',0,35000,1,NULL,0,'2025-07-24 00:00:00','2025-07-30 23:59:00'),
(40,'Promo Biasa','Produk','Kotak Makan','Harga_Jadi',0,6000,1,NULL,0,'2025-07-24 00:00:00','2025-07-24 23:59:00'),
(41,'Promo Biasa','Produk','Botol Minum','Harga_Jadi',0,6000,1,NULL,0,'2025-07-24 00:00:00','2025-07-24 23:59:00'),
(42,'Promo Biasa','Produk','Biskuit Columbia Toples','Bonus',0,12000,1,23,1,'2025-07-28 00:00:00','2025-07-28 23:59:00'),
(43,'Promo Biasa','Produk','Alat Tulis Serba 5000','Harga_Jadi',0,5000,1,NULL,0,'2025-07-28 00:00:00','2025-07-28 23:59:00'),
(44,'Promo Biasa','Produk','Beras Super Enak 5 kg','Harga_Jadi',0,73500,1,NULL,0,'2025-07-28 00:00:00','2025-07-28 23:59:00'),
(45,'Promo Spesial Kemerdekaan','Produk','Aneka Hiasan Agustusan','Harga_Jadi',0,5000,1,NULL,0,'2025-08-02 00:00:00','2025-08-31 23:59:00'),
(46,'Promo Spesial Kemerdekaan','Produk','Parcel Event Agustusan','Harga_Jadi',0,0,1,NULL,0,'2025-08-02 00:00:00','2025-08-31 23:59:00'),
(47,'Promo Spesial Kemerdekaan','Produk','Alat Tulis Serba 5000','Harga_Jadi',0,5000,1,NULL,0,'2025-08-02 00:00:00','2025-08-31 23:59:00'),
(48,'Promo Spesial Kemerdekaan','Produk','Koleksi Tung-Tung Sahur','Harga_Jadi',0,0,1,NULL,0,'2025-08-02 00:00:00','2025-08-31 23:59:00'),
(49,'Promo Spesial Kemerdekaan','Produk','Tas Sekolah','Persen',15,NULL,1,NULL,0,'2025-08-02 00:00:00','2025-08-31 23:59:00'),
(50,'Promo Spesial Kemerdekaan','Produk','Meja Belajar','Harga_Jadi',0,48000,1,NULL,0,'2025-08-02 00:00:00','2025-08-31 23:59:00'),
(51,'Promo Spesial Kemerdekaan','Produk','Indomie Goreng','Harga_Jadi',0,2900,1,NULL,0,'2025-08-02 00:00:00','2025-08-31 23:59:00'),
(52,'Promo Spesial Kemerdekaan','Produk','Gula Rose Brand 1 kg','Harga_Jadi',0,17500,1,NULL,0,'2025-08-02 00:00:00','2025-08-31 23:59:00'),
(53,'Promo Spesial Kemerdekaan','Produk','Beras Super Enak 5 kg','Harga_Jadi',0,73500,1,NULL,0,'2025-08-02 00:00:00','2025-08-31 23:59:00'),
(54,'Promo Biasa','Produk','Biskuit Columbia Toples','Bonus',0,12000,1,23,1,'2025-08-05 00:00:00','2025-08-05 23:59:00'),
(55,'Promo Biasa','Produk','Indomie Goreng','Harga_Jadi',0,2900,1,NULL,0,'2025-08-05 00:00:00','2025-08-05 23:59:00'),
(56,'Promo Biasa','Produk','Minyak Goreng','Harga_Jadi',0,16500,1,NULL,0,'2025-08-05 00:00:00','2025-08-05 23:59:00'),
(57,'Promo Biasa','Produk','Termos Sultan Set','Harga_Jadi',0,29000,1,NULL,0,'2025-08-05 00:00:00','2025-08-05 23:59:00'),
(58,'Promo Biasa','Produk','Parcel Sembako','Harga_Jadi',0,50000,1,NULL,0,'2025-08-05 00:00:00','2025-08-05 23:59:00'),
(59,'Promo Biasa','Produk','Flashdisk Lexar V40 8GB','Harga_Jadi',0,37000,1,NULL,0,'2025-08-20 00:00:00','2025-08-31 23:59:00'),
(60,'Promo Biasa','Produk','Flashdisk Lexar V40 64GB','Harga_Jadi',0,59000,1,NULL,0,'2025-08-20 00:00:00','2025-08-31 23:59:00'),
(61,'Promo Biasa','Tag','Kalkulator','Persen',20,NULL,1,NULL,0,'2025-08-20 00:00:00','2025-08-31 23:59:00'),
(62,'Promo Biasa','Tag','Loose Leaf','Persen',5,NULL,1,NULL,0,'2025-08-20 00:00:00','2025-08-31 23:59:00'),
(63,'Promo Biasa','Tag','Binder','Persen',5,NULL,1,NULL,0,'2025-08-20 00:00:00','2025-08-31 23:59:00'),
(64,'Promo Biasa','Produk','Bolpen Gel Kokoro Black 12+1','Bonus',0,60000,12,NULL,1,'2025-08-20 00:00:00','2025-08-31 23:59:00'),
(65,'Promo Biasa','Produk','Cleo 220 ml Isi 24 Botol','Harga_Jadi',0,16500,1,NULL,0,'2025-08-29 00:00:00','2025-08-31 23:59:00'),
(66,'Promo Biasa','Tag','Indomie','Harga_Jadi',0,2800,1,NULL,0,'2025-08-29 00:00:00','2025-08-31 23:59:00'),
(67,'Promo Biasa','Produk','Hit Anti Nyamuk 600 ml','Harga_Jadi',0,29500,1,NULL,0,'2025-08-29 00:00:00','2025-08-31 23:59:00'),
(68,'Promo Biasa','Produk','Kertas Kado Squeesy','Bonus',0,3000,1,NULL,1,'2025-08-29 00:00:00','2025-08-31 23:59:00'),
(69,'Promo Biasa','Produk','Jas Hujan Plastik Jaket Celana Model','Harga_Jadi',0,6500,1,NULL,0,'2025-09-02 00:00:00','2025-09-02 23:59:00'),
(70,'Promo Biasa','Produk','Jas Hujan Plastik Poncho Dewasa','Harga_Jadi',0,4500,1,NULL,0,'2025-09-02 00:00:00','2025-09-02 23:59:00'),
(71,'Promo Biasa','Produk','Cleo 550 ml 6 Botol','Harga_Jadi',0,11000,1,NULL,0,'2025-09-02 00:00:00','2025-09-02 23:59:00'),
(72,'Promo Biasa','Produk','Diamond Painting Diy Love','Harga_Jadi',0,10000,1,NULL,0,'2025-09-04 00:00:00','2025-09-04 23:59:00'),
(73,'Promo Biasa','Produk','Jumbo Tumbler Bricks','Harga_Jadi',0,11700,1,NULL,0,'2025-09-23 00:00:00','2025-09-23 23:59:00'),
(74,'Promo Biasa','Produk','Jumbo Mainan Bricks','Harga_Jadi',0,11700,1,NULL,0,'2025-09-23 00:00:00','2025-09-23 23:59:00'),
(75,'Promo Biasa','Produk','Tumblr Mainan Bricks','Harga_Jadi',0,11700,1,NULL,0,'2025-09-23 00:00:00','2025-09-23 23:59:00'),
(76,'Promo Biasa','Produk','Squeezy Empat Pensil Rakit','Harga_Jadi',0,7000,1,NULL,0,'2025-09-23 00:00:00','2025-09-23 23:59:00'),
(77,'Promo Biasa','Produk','M&G Scientific Calculator 552 Function','Harga_Jadi',0,335250,1,NULL,0,'2025-09-25 00:00:00','2025-09-25 23:59:00'),
(78,'Promo Biasa','Produk','M&G 12 Digits Scientist Calculator MG-991ES','Harga_Jadi',0,202050,1,NULL,0,'2025-09-25 00:00:00','2025-09-25 23:59:00'),
(79,'Promo Biasa','Produk','M&G Scientific Calculator 401 Function','Harga_Jadi',0,145530,1,NULL,0,'2025-09-25 00:00:00','2025-09-25 23:59:00'),
(80,'Promo Biasa','Produk','M&G Scientific Calculator 240 Function','Harga_Jadi',0,90000,1,NULL,0,'2025-09-25 00:00:00','2025-09-25 23:59:00'),
(81,'Promo Biasa','Produk','M&G Scientific Calculator Sakura Rain','Harga_Jadi',0,151740,1,NULL,0,'2025-09-25 00:00:00','2025-09-25 23:59:00'),
(82,'REGOHOREG','Produk','Kiky Buku Tulis 32 Lembar','Harga_Jadi',0,20000,1,NULL,0,'2025-09-27 00:00:00','2025-09-27 23:59:00'),
(83,'REGOHOREG','Produk','Kiky Buku Tulis 38 Lembar','Harga_Jadi',0,23900,1,NULL,0,'2025-09-27 00:00:00','2025-09-27 23:59:00'),
(84,'REGOHOREG','Produk','Sedaap Mie Goreng','Harga_Jadi',0,2800,1,NULL,0,'2025-09-27 00:00:00','2025-09-27 23:59:00'),
(85,'REGOHOREG','Produk','Indomie Mie Goreng','Harga_Jadi',0,2900,1,NULL,0,'2025-09-27 00:00:00','2025-09-27 23:59:00'),
(86,'REGOHOREG','Produk','Anggur Beras 5 kg','Harga_Jadi',0,74900,1,NULL,0,'2025-09-27 00:00:00','2025-09-27 23:59:00'),
(87,'REGOHOREG','Produk','Pinpin Beras 5 kg','Harga_Jadi',0,79900,1,NULL,0,'2025-09-27 00:00:00','2025-09-27 23:59:00'),
(88,'REGOHOREG','Produk','Pristine Air Mineral 600 ml Dus','Harga_Jadi',0,80100,1,NULL,0,'2025-09-27 00:00:00','2025-09-27 23:59:00'),
(89,'REGOHOREG','Produk','Pristine Air Mineral 400 ml Dus','Harga_Jadi',0,56000,1,NULL,0,'2025-09-27 00:00:00','2025-09-27 23:59:00'),
(90,'REGOHOREG','Produk','Squeezy Bolpen Gel SQ04','Harga_Jadi',0,11000,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(91,'REGOHOREG','Produk','Sidu HVS 80 Gsm A4','Harga_Jadi',0,46000,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(92,'REGOHOREG','Produk','Binder Note A5 Pastel','Harga_Jadi',0,19900,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(93,'REGOHOREG','Produk','Nachi Lakban','Harga_Jadi',0,7600,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(94,'REGOHOREG','Produk','Fitri Minyak Goreng','Grosir',0,46000,3,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(95,'REGOHOREG','Produk','Sultan Termos','Harga_Jadi',0,27000,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(96,'REGOHOREG','Produk','Sarasa Bolpen Gel','Harga_Jadi',0,13900,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(97,'REGOHOREG','Produk','Frixion Bolpen Bisa Dihapus','Harga_Jadi',0,22500,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(98,'REGOHOREG','Produk','Capybara Botol Minum 900 ml','Harga_Jadi',0,13900,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(99,'REGOHOREG','Produk','Kifa Sabun Cuci Piring','Harga_Jadi',0,6500,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(100,'REGOHOREG','Produk','Cleo Air Minum 220 ml','Harga_Jadi',0,16700,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(101,'REGOHOREG','Produk','Jete Power Bank B7 10000 Mah','Harga_Jadi',0,249000,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(102,'REGOHOREG','Produk','See U Tissue','Harga_Jadi',0,6900,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(103,'REGOHOREG','Produk','Lexar Flashdisk V40 64 Gb','Harga_Jadi',0,59000,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(104,'REGOHOREG','Produk','Beras Enak 5 kg + Minyak Goreng','Harga_Jadi',0,91000,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(105,'REGOHOREG','Produk','Hit Spray Anti Nyamuk 600 ml','Harga_Jadi',0,29000,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(106,'REGOHOREG','Produk','Fusia Tissue Jumbo 1000 Sheet','Harga_Jadi',0,23000,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(107,'REGOHOREG','Produk','Joyko Correction Tape CT 522','Harga_Jadi',0,4400,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(108,'REGOHOREG','Produk','Kokoro Bolpen Black','Harga_Jadi',0,4900,1,NULL,0,'2025-09-30 00:00:00','2025-09-30 23:59:00'),
(109,'Promo Biasa','Produk','Jas Hujan Jaket Celana Elmondo','Persen',2.5,58500,1,NULL,0,'2024-10-31 00:00:00','2024-10-31 23:59:00'),
(110,'Promo Biasa','Produk','Jas Hujan Kiddy Rets','Persen',2.5,42900,1,NULL,0,'2024-10-31 00:00:00','2024-10-31 23:59:00'),
(111,'Promo Biasa','Produk','Tissue Amour 360S','Harga_Jadi',0,8000,1,NULL,0,'2024-10-24 00:00:00','2024-10-24 23:59:00'),
(112,'Promo Biasa','Produk','Jas Hujan Jaket Celana Big Kid','Persen',2.5,47775,1,NULL,0,'2024-10-24 00:00:00','2024-10-24 23:59:00'),
(113,'Promo Biasa','Produk','Jas Hujan Trend Jaket Celana','Persen',2.5,69225,1,NULL,0,'2024-10-24 00:00:00','2024-10-24 23:59:00'),
(114,'Promo Biasa','Produk','Jas Hujan Trend Jaket Celana Premium Ibex','Persen',2.5,69225,1,NULL,0,'2024-10-24 00:00:00','2024-10-24 23:59:00'),
(115,'Promo Biasa','Produk','Jas Hujan Trend Jaket Celana','Persen',2.5,79950,1,NULL,0,'2024-10-24 00:00:00','2024-10-24 23:59:00'),
(116,'Promo Biasa','Produk','Jas Hujan Andalan 502','Harga_Jadi',0,63400,1,NULL,0,'2024-10-24 00:00:00','2024-10-24 23:59:00'),
(117,'Hore 95.000 Followers','Produk','Sedapmie','Harga_Jadi',0,2800,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(118,'Hore 95.000 Followers','Produk','Indomie','Harga_Jadi',0,2900,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(119,'Hore 95.000 Followers','Produk','Minyak Goreng Fitri 800 ml','Harga_Jadi',0,15300,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(120,'Hore 95.000 Followers','Produk','Beras Anggur','Harga_Jadi',0,74900,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(121,'Hore 95.000 Followers','Produk','Air Cleo 220 ml','Harga_Jadi',0,16700,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(122,'Hore 95.000 Followers','Produk','Gula Rose Brand 1 kg','Harga_Jadi',0,16900,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(123,'Hore 95.000 Followers','Produk','Air Pristine 400 ml','Harga_Jadi',0,2600,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(124,'Hore 95.000 Followers','Produk','Tissue See-U 250S','Harga_Jadi',0,7700,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(125,'Hore 95.000 Followers','Produk','Tissue Fusia Jumbo','Harga_Jadi',0,25000,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(126,'Hore 95.000 Followers','Produk','Tissue Basah So Bonus So','Harga_Jadi',0,10300,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(127,'Hore 95.000 Followers','Produk','Tissue Nice Jumbo','Harga_Jadi',0,34900,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(128,'Hore 95.000 Followers','Produk','Freshcare','Harga_Jadi',0,11500,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(129,'Hore 95.000 Followers','Produk','Tolak Angin','Harga_Jadi',0,4000,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(130,'Hore 95.000 Followers','Produk','Kipas Portable','Harga_Jadi',0,14900,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(131,'Hore 95.000 Followers','Produk','Magic Clay','Harga_Jadi',0,5500,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(132,'Hore 95.000 Followers','Produk','Bolpen Sarasa','Harga_Jadi',0,13900,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(133,'Hore 95.000 Followers','Produk','Bolpen Quantum','Harga_Jadi',0,5200,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(134,'Hore 95.000 Followers','Produk','Bolpen AE7','Harga_Jadi',0,17500,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(135,'Hore 95.000 Followers','Produk','Bolpen Squeezy SQ04','Harga_Jadi',0,1000,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(136,'Hore 95.000 Followers','Produk','Buku Tulis Kiky Okey 38L','Harga_Jadi',0,24000,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(137,'Hore 95.000 Followers','Produk','Buku Tulis Bis Rose 42L','Harga_Jadi',0,23500,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(138,'Hore 95.000 Followers','Produk','Buku Tulis Sidu 32L','Harga_Jadi',0,24000,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(139,'Hore 95.000 Followers','Produk','Buku Tulis Kraft 40L','Harga_Jadi',0,27000,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(140,'Hore 95.000 Followers','Merk','Top','Persen',13,NULL,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(141,'Hore 95.000 Followers','Produk','Pensil Agatis Neopex 2B','Harga_Jadi',0,8000,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(142,'Hore 95.000 Followers','Produk','Crayon Pop 1 12 Warna','Harga_Jadi',0,5500,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(143,'Hore 95.000 Followers','Produk','Bolpen Trendee','Harga_Jadi',0,5000,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(144,'Hore 95.000 Followers','Produk','Crayon Creebel 55 Warna','Harga_Jadi',0,78500,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(145,'Hore 95.000 Followers','Produk','Map L Benefit','Harga_Jadi',0,1000,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(146,'Hore 95.000 Followers','Produk','Lakban Borneo Core Merah','Harga_Jadi',0,5900,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(147,'Hore 95.000 Followers','Produk','Ordner Bantex 1401','Harga_Jadi',0,17000,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(148,'Hore 95.000 Followers','Produk','OPP Nachi 2 Inch','Harga_Jadi',0,7500,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(149,'Hore 95.000 Followers','Produk','Bubble Wrap 2 kg','Harga_Jadi',0,69900,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(150,'Hore 95.000 Followers','Produk','HVS Sinarline 75 GSM A4','Harga_Jadi',0,34900,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(151,'Hore 95.000 Followers','Produk','HVS Sinarline 75 GSM F4','Harga_Jadi',0,39900,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(152,'Hore 95.000 Followers','Produk','HVS Sidu 70 GSM A4','Harga_Jadi',0,39130,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(153,'Hore 95.000 Followers','Produk','HVS Sidu 70 GSM F4','Harga_Jadi',0,45500,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(154,'Hore 95.000 Followers','Produk','Mangkok Set 1','Harga_Jadi',0,9900,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(155,'Hore 95.000 Followers','Produk','Mangkok Set 2','Harga_Jadi',0,17900,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(156,'Hore 95.000 Followers','Produk','Botol Minum','Harga_Jadi',0,6000,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(157,'Hore 95.000 Followers','Produk','Lunch Box Ginza','Harga_Jadi',0,9900,1,NULL,0,'2024-10-21 00:00:00','2024-10-21 23:59:00'),
(158,'Promo Biasa','Produk','Tissue Facial Amigo 1000gr','Harga_Jadi',0,30300,1,NULL,0,'2024-10-17 00:00:00','2024-10-17 23:59:00'),
(159,'Promo Biasa','Produk','Pack Bolpen Quantum','Harga_Jadi',0,5500,1,NULL,0,'2024-10-14 00:00:00','2024-10-14 23:59:00'),
(160,'Promo Biasa','Produk','Kalkulator M&G MGC-10','Harga_Jadi',0,95000,1,NULL,0,'2024-10-08 00:00:00','2024-10-08 23:59:00'),
(161,'Promo Biasa','Produk','Kalkulator Scientific Casio FX-570ES Plus','Persen',20,361200,1,NULL,0,'2024-10-04 00:00:00','2024-10-04 23:59:00'),
(162,'Promo Biasa','Produk','Kalkulator Scientific Casio FX-82ES Plus','Persen',20,229600,1,NULL,0,'2024-10-04 00:00:00','2024-10-04 23:59:00'),
(163,'Promo Biasa','Produk','Kalkulator Scientific Casio FX-991ES Plus','Persen',20,384800,1,NULL,0,'2024-10-04 00:00:00','2024-10-04 23:59:00'),
(164,'Promo Biasa','Produk','Kalkulator Casio DM-1400F','Persen',20,330400,1,NULL,0,'2024-10-04 00:00:00','2024-10-04 23:59:00'),
(165,'Promo Biasa','Produk','Kalkulator Casio MJ-120D Plus','Persen',20,160800,1,NULL,0,'2024-10-04 00:00:00','2024-10-04 23:59:00'),
(166,'Promo Biasa','Produk','Kalkulator Casio MX-120B','Persen',20,120000,1,NULL,0,'2024-10-04 00:00:00','2024-10-04 23:59:00'),
(167,'Promo Biasa','Produk','Kalkulator Casio MX-12B','Persen',20,82000,1,NULL,0,'2024-10-04 00:00:00','2024-10-04 23:59:00'),
(168,'Promo Biasa','Produk','Kalkulator Casio MJ-100D Plus','Persen',20,148800,1,NULL,0,'2024-10-04 00:00:00','2024-10-04 23:59:00'),
(169,'Promo Biasa','Produk','Kalkulator Casio DH-12BK','Persen',20,155200,1,NULL,0,'2024-10-04 00:00:00','2024-10-04 23:59:00'),
(170,'Promo Biasa','Produk','Kalkulator Joyko CC-40','Persen',15,44200,1,NULL,0,'2024-10-03 00:00:00','2024-10-03 23:59:00'),
(171,'Promo Biasa','Produk','Kalkulator Joyko CC-57CO','Persen',15,38250,1,NULL,0,'2024-10-03 00:00:00','2024-10-03 23:59:00'),
(172,'Promo Biasa','Produk','Kalkulator Joyko CC-27','Persen',15,68000,1,NULL,0,'2024-10-03 00:00:00','2024-10-03 23:59:00'),
(173,'Promo Biasa','Produk','Kalkulator Joyko CC-15A','Persen',15,41650,1,NULL,0,'2024-10-03 00:00:00','2024-10-03 23:59:00'),
(174,'Promo Biasa','Produk','Kalkulator Scientific Joyko CC-25','Persen',15,55250,1,NULL,0,'2024-10-03 00:00:00','2024-10-03 23:59:00'),
(175,'Promo Biasa','Produk','Kalkulator Scientific Joyko CC-23BP','Persen',15,35700,1,NULL,0,'2024-10-03 00:00:00','2024-10-03 23:59:00'),
(176,'Promo Biasa','Produk','Kalkulator Scientific Joyko CC-78','Persen',15,29750,1,NULL,0,'2024-10-03 00:00:00','2024-10-03 23:59:00'),
(177,'Promo Biasa','Produk','Kalkulator Scientific Joyko CC-23','Persen',15,35700,1,NULL,0,'2024-10-03 00:00:00','2024-10-03 23:59:00'),
(178,'Promo Biasa','Produk','Kalkulator Scientific Joyko CC-67','Persen',15,112200,1,NULL,0,'2024-10-03 00:00:00','2024-10-03 23:59:00'),
(179,'Promo Biasa','Produk','Kalkulator Scientific Joyko CC-29A','Persen',15,164050,1,NULL,0,'2024-10-03 00:00:00','2024-10-03 23:59:00'),
(180,'Promo Biasa','Produk','Kalkulator Scientific Joyko CC-25CO','Persen',15,55250,1,NULL,0,'2024-10-03 00:00:00','2024-10-03 23:59:00'),
(181,'Promo Biasa','Produk','Tumbler Jumbo','Persen',2.5,NULL,1,NULL,0,'2025-11-30 00:00:00','2025-11-30 23:59:00'),
(182,'Promo Biasa','Produk','Botol Minum Cetek Sedotan Mix 900ml','Harga_Jadi',0,14900,1,NULL,0,'2025-11-29 00:00:00','2025-11-29 23:59:00'),
(183,'Rego Nekaddd Disamber Aja','Produk','Sedapmie','Harga_Jadi',0,2900,1,NULL,0,'2025-11-26 00:00:00','2025-11-26 23:59:00'),
(184,'Rego Nekaddd Disamber Aja','Produk','Indomie','Harga_Jadi',0,3000,1,NULL,0,'2025-11-26 00:00:00','2025-11-26 23:59:00'),
(185,'Rego Nekaddd Disamber Aja','Produk','Gula Rose Brand 1kg','Harga_Jadi',0,17000,1,NULL,0,'2025-11-26 00:00:00','2025-11-26 23:59:00'),
(186,'Rego Nekaddd Disamber Aja','Produk','Air Cleo 220ml','Harga_Jadi',0,16900,1,NULL,0,'2025-11-26 00:00:00','2025-11-26 23:59:00'),
(187,'Rego Nekaddd Disamber Aja','Produk','Minyak Goreng 800ml','Harga_Jadi',0,15000,1,NULL,0,'2025-11-26 00:00:00','2025-11-26 23:59:00'),
(188,'Rego Nekaddd Disamber Aja','Produk','Beras Cap Uduk 5kg','Harga_Jadi',0,74000,1,NULL,0,'2025-11-26 00:00:00','2025-11-26 23:59:00'),
(189,'Rego Nekaddd Disamber Aja','Produk','Tissu See-U 250s','Harga_Jadi',0,7000,1,NULL,0,'2025-11-26 00:00:00','2025-11-26 23:59:00'),
(190,'Rego Nekaddd Disamber Aja','Produk','Tissu Basah Mitu','Harga_Jadi',0,10500,1,NULL,0,'2025-11-26 00:00:00','2025-11-26 23:59:00'),
(191,'Rego Nekaddd Disamber Aja','Produk','Tissu Paseo','Harga_Jadi',0,22500,1,NULL,0,'2025-11-26 00:00:00','2025-11-26 23:59:00'),
(192,'Rego Nekaddd Disamber Aja','Produk','Hit Aerosol 600ml','Harga_Jadi',0,30900,1,NULL,0,'2025-11-26 00:00:00','2025-11-26 23:59:00'),
(193,'Rego Nekaddd Disamber Aja','Tag','Kalkulator','Persen',15,NULL,1,NULL,0,'2025-11-26 00:00:00','2025-11-26 23:59:00'),
(194,'Rego Nekaddd Disamber Aja','Produk','Gantungan Kunci','Grosir',0,11000,3,NULL,0,'2025-11-26 00:00:00','2025-11-26 23:59:00'),
(195,'Rego Nekaddd Disamber Aja','Produk','Jedai Kamboja','Grosir',0,11000,3,NULL,0,'2025-11-26 00:00:00','2025-11-26 23:59:00'),
(196,'Rego Nekaddd Disamber Aja','Produk','Hanger Hitam','Grosir',0,7000,12,NULL,0,'2025-11-26 00:00:00','2025-11-26 23:59:00'),
(197,'Rego Nekaddd Disamber Aja','Tag','Hiasan Natal','Persen',2.5,NULL,1,NULL,0,'2025-11-26 00:00:00','2025-11-26 23:59:00'),
(198,'Rego Nekaddd Disamber Aja','Tag','Bags & Toys','Persen',10,NULL,1,NULL,0,'2025-11-26 00:00:00','2025-11-26 23:59:00'),
(199,'Promo Biasa','Produk','Bando Natal','Persen',3.33,12567,1,NULL,0,'2025-11-22 00:00:00','2025-11-22 23:59:00'),
(200,'Promo Biasa','Produk','Topi Natal Glitter','Persen',3.33,17400,1,NULL,0,'2025-11-22 00:00:00','2025-11-22 23:59:00'),
(201,'Promo Biasa','Produk','Jas Hujan Elmondo Celana Jaket 935','Persen',2.5,34125,1,NULL,0,'2025-11-21 00:00:00','2025-11-21 23:59:00'),
(202,'Promo Biasa','Produk','Indomie','Persen',11,2933,1,NULL,0,'2025-11-11 00:00:00','2025-11-11 23:59:00'),
(203,'Promo Biasa','Produk','Beras Uduk','Persen',11,73999,1,NULL,0,'2025-11-11 00:00:00','2025-11-11 23:59:00'),
(204,'Promo Biasa','Produk','Vivan Vpb-D11 10.000 Mah','Harga_Jadi',0,190000,1,NULL,0,'2025-11-05 00:00:00','2025-11-05 23:59:00'),
(205,'Horeg Nekaddd 13est Years of Ultah TBMO','Produk','Gula Rose Brand 1kg','Harga_Jadi',0,17000,1,NULL,0,'2025-11-01 00:00:00','2025-11-13 23:59:00'),
(206,'Horeg Nekaddd 13est Years of Ultah TBMO','Produk','Tissue See-U 250s','Grosir',0,13333,2,NULL,0,'2025-11-01 00:00:00','2025-11-13 23:59:00'),
(207,'Horeg Nekaddd 13est Years of Ultah TBMO','Produk','Tissue Amor 360','Grosir',0,7700,2,NULL,0,'2025-11-01 00:00:00','2025-11-13 23:59:00'),
(208,'Horeg Nekaddd 13est Years of Ultah TBMO','Produk','Tissue Basah Mitu','Bonus',0,10333,1,NULL,1,'2025-11-01 00:00:00','2025-11-13 23:59:00'),
(209,'Horeg Nekaddd 13est Years of Ultah TBMO','Produk','Hanger Hitam','Grosir',0,13333,12,NULL,0,'2025-11-01 00:00:00','2025-11-13 23:59:00'),
(210,'Horeg Nekaddd 13est Years of Ultah TBMO','Produk','Bolpen Trendee','Grosir',0,5333,12,NULL,0,'2025-11-01 00:00:00','2025-11-13 23:59:00'),
(211,'Horeg Nekaddd 13est Years of Ultah TBMO','Tag','Kalkulator','Persen',13.33,NULL,1,NULL,0,'2025-11-01 00:00:00','2025-11-13 23:59:00'),
(212,'Horeg Nekaddd 13est Years of Ultah TBMO','Tag','Snack','Persen',3.33,NULL,1,NULL,0,'2025-11-01 00:00:00','2025-11-13 23:59:00'),
(213,'Horeg Nekaddd 13est Years of Ultah TBMO','Tag','Minuman','Persen',3.33,NULL,1,NULL,0,'2025-11-01 00:00:00','2025-11-13 23:59:00'),
(216,'Promo Biasa','Produk','Air Cleo 220 ml','Harga_Jadi',0,1500,1,NULL,0,'2026-01-06 00:00:00','2026-01-07 00:00:00'),
(217,'Promo Biasa','Kategori','Electronic','Persen',5,NULL,1,NULL,0,'2026-01-06 00:00:00','2026-01-07 00:00:00'),
(218,'Promo Biasa','Produk','Air Cleo 220 ml Isi 24','Bonus',0,NULL,1,16,1,'2026-01-06 00:00:00','2026-01-07 00:00:00'),
(220,'Promo Biasa','Produk','Ameria Biscuit Columbia','Grosir',0,70000,3,NULL,0,'2026-01-06 00:00:00','2026-01-07 00:00:00');

/*Table structure for table `promo_special` */

DROP TABLE IF EXISTS `promo_special`;

CREATE TABLE `promo_special` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Nama_Promo` VARCHAR(100) NOT NULL,
  `Kategori` ENUM('YesNo','Input') NOT NULL,
  `Keterangan` VARCHAR(255) NOT NULL,
  `START` DATETIME DEFAULT CURRENT_TIMESTAMP(),
  `END` DATETIME DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=INNODB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `promo_special` */

/*Table structure for table `transaksi` */

DROP TABLE IF EXISTS `transaksi`;

CREATE TABLE `transaksi` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `member_id` INT(11) DEFAULT NULL,
  `Tanggal` DATETIME DEFAULT CURRENT_TIMESTAMP(),
  `Harga_Terpotong` INT(11) DEFAULT 0,
  `Total` INT(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`)
) ENGINE=INNODB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `transaksi` */

INSERT  INTO `transaksi`(`ID`,`member_id`,`Tanggal`,`Harga_Terpotong`,`Total`) VALUES 
(1,1,'2025-12-01 10:15:00',0,35000),
(2,2,'2025-12-01 11:22:00',0,78000),
(3,NULL,'2025-12-02 12:40:00',0,15000),
(4,3,'2025-12-01 09:05:00',0,112000),
(5,1,'2025-11-30 13:55:00',0,54000);

/*Table structure for table `transaksi_detail` */

DROP TABLE IF EXISTS `transaksi_detail`;

CREATE TABLE `transaksi_detail` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `transaksi_id` INT(11) NOT NULL,
  `produk_id` INT(11) NOT NULL,
  `Qty` INT(11) NOT NULL,
  `Harga` INT(11) NOT NULL,
  `Diskon` INT(11) DEFAULT NULL,
  `Diskon_Spesial` INT(11) DEFAULT NULL,
  `Subtotal` INT(11) GENERATED ALWAYS AS (`Qty` * `Harga`) STORED,
  PRIMARY KEY (`ID`),
  KEY `transaksi_id` (`transaksi_id`),
  CONSTRAINT `transaksi_detail_ibfk_1` FOREIGN KEY (`transaksi_id`) REFERENCES `transaksi` (`ID`)
) ENGINE=INNODB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `transaksi_detail` */

INSERT  INTO `transaksi_detail`(`ID`,`transaksi_id`,`produk_id`,`Qty`,`Harga`,`Diskon`,`Diskon_Spesial`) VALUES 
(1,1,3,2,5000,NULL,NULL),
(2,1,5,1,25000,NULL,NULL),
(3,2,2,1,30000,NULL,NULL),
(4,2,4,2,24000,NULL,NULL),
(5,3,1,1,15000,NULL,NULL),
(6,4,6,1,70000,NULL,NULL),
(7,4,7,2,21000,NULL,NULL),
(8,5,3,3,5000,NULL,NULL),
(9,5,8,1,39000,NULL,NULL);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;