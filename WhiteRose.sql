-- MySQL dump 10.13  Distrib 5.7.20, for Linux (x86_64)
--
-- Host: localhost    Database: white_rose
-- ------------------------------------------------------
-- Server version	5.7.20

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tclientes`
--

DROP TABLE IF EXISTS `tclientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tclientes` (
  `CliRIF` varchar(15) NOT NULL,
  `CliNombre` varchar(40) DEFAULT NULL,
  `CliDir` varchar(60) DEFAULT NULL,
  `CliTlf` varchar(15) DEFAULT NULL,
  `CliEstatus` varchar(1) NOT NULL DEFAULT 'A',
  PRIMARY KEY (`CliRIF`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tclientes`
--

LOCK TABLES `tclientes` WRITE;
/*!40000 ALTER TABLE `tclientes` DISABLE KEYS */;
INSERT INTO `tclientes` VALUES ('20963281','Jesus Acevedo','Calle 49','04146326788','A'),('24567402','Sugehi Rivero','Ruezga Norte','04149506743','A'),('25919459','Gabriel Roa','17 con 17','04126635522','E'),('26002905','Anyeli Villarreal','17 c 17','04266270520','A'),('26710983','Albert Acevedo','Calle 49','04146326788','A');
/*!40000 ALTER TABLE `tclientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tdepartamentos`
--

DROP TABLE IF EXISTS `tdepartamentos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tdepartamentos` (
  `DptoCod` varchar(2) NOT NULL,
  `DptoDesc` varchar(30) DEFAULT NULL,
  `DptoEstatus` varchar(1) NOT NULL DEFAULT 'A',
  PRIMARY KEY (`DptoCod`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tdepartamentos`
--

LOCK TABLES `tdepartamentos` WRITE;
/*!40000 ALTER TABLE `tdepartamentos` DISABLE KEYS */;
INSERT INTO `tdepartamentos` VALUES ('D0','Departamento de Pruebas 1','A'),('D1','Departamento de Pruebas 2','A'),('D2','Departamento de Pruebas 3','A');
/*!40000 ALTER TABLE `tdepartamentos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tdetfactura`
--

DROP TABLE IF EXISTS `tdetfactura`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tdetfactura` (
  `DetNroFactura` int(5) NOT NULL,
  `DetCodServ` varchar(4) NOT NULL,
  `DetCantidad` int(4) DEFAULT NULL,
  `DetPrecio` decimal(12,2) DEFAULT NULL,
  `DetEstatus` varchar(1) NOT NULL DEFAULT 'A',
  PRIMARY KEY (`DetNroFactura`,`DetCodServ`),
  KEY `Servicios_idx` (`DetCodServ`),
  CONSTRAINT `Factura` FOREIGN KEY (`DetNroFactura`) REFERENCES `tfactventa` (`VenNroFact`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Servicios` FOREIGN KEY (`DetCodServ`) REFERENCES `tservicios` (`ServCod`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tdetfactura`
--

LOCK TABLES `tdetfactura` WRITE;
/*!40000 ALTER TABLE `tdetfactura` DISABLE KEYS */;
INSERT INTO `tdetfactura` VALUES (1,'S01',12,25000.00,'A'),(1,'S02',12,250000.00,'A'),(2,'S01',12,25000.00,'A'),(2,'S02',12,250000.00,'A'),(3,'S01',1,25000.00,'A'),(3,'S02',1,250000.00,'A'),(4,'E01',2,125000.00,'A'),(4,'S01',1,25000.00,'A'),(5,'S01',3,25000.00,'A'),(5,'S02',3,250000.00,'A'),(6,'S01',1,25000.00,'A'),(6,'S02',1,250000.00,'A'),(6,'S03',1,3500000.00,'A'),(9,'S01',7,25000.00,'A'),(9,'S02',7,250000.00,'A'),(11,'E02',3,2000000.00,'A'),(11,'S01',1,25000.00,'A'),(12,'S02',4,250000.00,'A'),(14,'E02',1,2000000.00,'A'),(14,'S02',4,250000.00,'A'),(15,'E02',3,2000000.00,'A'),(16,'S02',1,250000.00,'A'),(17,'E02',2,2000000.00,'A'),(18,'E02',1,2000000.00,'A'),(19,'S01',12,25000.00,'A'),(20,'S01',112,25000.00,'A'),(21,'S01',2,25000.00,'A'),(21,'S02',12,250000.00,'A'),(22,'S01',12,25000.00,'A'),(23,'S02',12,245000.00,'A'),(24,'S01',12,25000.00,'A'),(24,'S02',12,250000.00,'A'),(25,'E02',21,1500000.00,'A'),(25,'S01',12,22500.00,'A'),(26,'E02',12,1500000.00,'A'),(26,'S02',2,245000.00,'A'),(27,'E02',12,2000000.00,'A'),(27,'S01',13,25000.00,'A');
/*!40000 ALTER TABLE `tdetfactura` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tfactventa`
--

DROP TABLE IF EXISTS `tfactventa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tfactventa` (
  `VenNroFact` int(5) NOT NULL AUTO_INCREMENT,
  `VenRIFCli` varchar(15) DEFAULT NULL,
  `VenTipoVenta` varchar(1) DEFAULT NULL,
  `VenFechaFact` datetime DEFAULT NULL,
  `VenSubTotalFact` decimal(12,2) DEFAULT NULL,
  `VenPorcDesc` decimal(8,2) DEFAULT NULL,
  `VenPorcIVA` decimal(8,2) DEFAULT NULL,
  `VenEstatus` varchar(1) NOT NULL DEFAULT 'A',
  PRIMARY KEY (`VenNroFact`),
  KEY `RIFCliente_idx` (`VenRIFCli`),
  CONSTRAINT `RIFCliente` FOREIGN KEY (`VenRIFCli`) REFERENCES `tclientes` (`CliRIF`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tfactventa`
--

LOCK TABLES `tfactventa` WRITE;
/*!40000 ALTER TABLE `tfactventa` DISABLE KEYS */;
INSERT INTO `tfactventa` VALUES (1,'26710983','1','2017-12-07 21:22:18',3300000.00,0.12,0.12,'A'),(2,'26710983','1','2017-12-08 20:16:03',3300000.00,0.00,12.00,'A'),(3,'25919459','1','2017-12-09 21:26:04',1775000.00,0.00,12.00,'A'),(4,'25919459','1','2017-12-10 11:20:53',275000.00,0.00,12.00,'A'),(5,'26710983','1','2017-12-10 11:30:35',1575000.00,0.00,12.00,'A'),(6,'26002905','1','2017-12-13 16:28:47',3775000.00,15.00,25.00,'A'),(7,'25919459','2','2017-12-13 16:32:38',45000.00,0.00,25.00,'A'),(8,'26710983','1','2017-12-13 16:35:49',19250000.00,30.00,9.00,'A'),(9,'25919459','1','2017-12-13 16:37:05',1925000.00,0.00,12.00,'A'),(10,'25919459','1','2017-12-13 16:37:19',42000000.00,0.00,12.00,'A'),(11,'26002905','1','2017-12-13 21:34:00',6025000.00,15.00,25.00,'A'),(12,'26002905','1','2017-12-14 00:40:12',1000000.00,0.00,12.00,'A'),(13,'26002905','2','2017-12-14 00:40:25',9000000.00,0.00,12.00,'A'),(14,'26002905','1','2017-12-14 00:44:35',3000000.00,0.00,12.00,'A'),(15,'26002905','1','2017-12-14 00:45:25',6000000.00,0.00,12.00,'A'),(16,'26002905','1','2017-12-14 00:45:59',250000.00,0.00,12.00,'A'),(17,'26002905','1','2017-12-14 00:49:13',4000000.00,0.00,12.00,'A'),(18,'26710983','1','2017-12-14 00:51:49',2000000.00,0.00,12.00,'A'),(19,'26710983','1','2017-12-14 01:38:04',300000.00,0.00,12.00,'A'),(20,'26710983','1','2017-12-14 01:38:27',2800000.00,0.00,12.00,'A'),(21,'26710983','1','2017-12-14 01:40:25',3050000.00,0.00,12.00,'A'),(22,'26710983','1','2017-12-14 01:40:50',300000.00,0.00,12.00,'A'),(23,'26710983','2','2017-12-14 01:41:26',2940000.00,0.00,12.00,'A'),(24,'26710983','1','2017-12-14 01:44:00',3300000.00,0.00,12.00,'A'),(25,'26710983','2','2017-12-14 01:45:59',31770000.00,0.00,12.00,'A'),(26,'26710983','2','2017-12-14 01:46:29',18490000.00,0.00,12.00,'A'),(27,'26710983','1','2017-12-14 01:59:18',24325000.00,0.00,12.00,'A');
/*!40000 ALTER TABLE `tfactventa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tservicios`
--

DROP TABLE IF EXISTS `tservicios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tservicios` (
  `ServCod` varchar(4) NOT NULL,
  `ServCodpto` varchar(2) DEFAULT NULL,
  `ServDesc` varchar(45) DEFAULT NULL,
  `ServCosto` decimal(12,2) DEFAULT NULL,
  `ServPrecioDetal` decimal(12,2) DEFAULT NULL,
  `ServPrecioMayor` decimal(12,2) DEFAULT NULL,
  `ServEstatus` varchar(1) NOT NULL DEFAULT 'A',
  PRIMARY KEY (`ServCod`),
  KEY `Codpto_idx` (`ServCodpto`),
  CONSTRAINT `Codpto` FOREIGN KEY (`ServCodpto`) REFERENCES `tdepartamentos` (`DptoCod`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tservicios`
--

LOCK TABLES `tservicios` WRITE;
/*!40000 ALTER TABLE `tservicios` DISABLE KEYS */;
INSERT INTO `tservicios` VALUES ('E01','D2','Prueba de Software',66512.00,125000.00,100000.00,'E'),('E02','D2','Programación de Proyecto Universitario',150000.00,2000000.00,1500000.00,'A'),('S01','D0','Diseño de Página Web',15000.00,25000.00,22500.00,'A'),('S02','D1','Construcción de Software Automatizado',200000.00,250000.00,245000.00,'A'),('S03','D2','Diseño de Sistema de Información completo',1500000.00,3500000.00,3000000.00,'A');
/*!40000 ALTER TABLE `tservicios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tusuarios`
--

DROP TABLE IF EXISTS `tusuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tusuarios` (
  `UsuCodigo` varchar(5) NOT NULL,
  `UsuUsuario` varchar(10) DEFAULT NULL,
  `UsuContraseña` varchar(15) NOT NULL,
  `UsuNombre` varchar(30) DEFAULT NULL,
  `UsuApellido` varchar(30) DEFAULT NULL,
  `UsuCedula` varchar(9) DEFAULT NULL,
  `UsuFechaIng` date DEFAULT NULL,
  `UsuCodUsuarioAnadido` varchar(5) DEFAULT NULL,
  `UsuTipoUsuario` smallint(6) DEFAULT NULL,
  `UsuCorreo` varchar(45) DEFAULT NULL,
  `UsuPista` varchar(45) DEFAULT NULL,
  `UsuEstatus` varchar(1) NOT NULL DEFAULT 'A',
  PRIMARY KEY (`UsuCodigo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tusuarios`
--

LOCK TABLES `tusuarios` WRITE;
/*!40000 ALTER TABLE `tusuarios` DISABLE KEYS */;
INSERT INTO `tusuarios` VALUES ('A0001','admin','12345678','Administrador','del Sistema',NULL,'2017-12-05',NULL,1,'admin@whiterose.com','Números del 1 al 8','I'),('A0002','gaboroa','123','Gabriel','Roa','25919459','2017-12-05','A0001',2,'gaboroab14@gmail.com','¿Qué eres?','I'),('A0003','ayvp','12','Anyeli','Villarreal','26002905','2017-12-13','A0002',1,'ayvp@gmail.com','1','A'),('A0004','adv','123','Azucena','Del Valle','35','2017-12-14','A0003',1,'adv@gmail.com','123','I'),('A0005','adelcielo','12345787867','Alguiennn','Del Cielo','45612','2017-12-14','A0003',1,'123@gmail.com','123','I'),('A0006','ajav','123','Albert','Acevedo','26710983','2017-12-14','A0003',1,'albert06av@gmail.com','1-3','A'),('B0001','imedina','123','Isaias','Medina Angarita','1','2017-12-14','A0003',3,'imedina@gmail.com','123','I'),('B0002','t','1','Testest','Testest','444','2017-12-14','A0003',3,'t@gmail.com','1','I'),('V0001','gaboroa14','cacaHuate2','Gabriel','Roa','25919459','2017-12-05','A0001',2,'gaboroab14@gmail.com','¿Qué eres?','I'),('V0002','pinfante','123','Pedro','Infante','2','2017-12-14','A0003',2,'pinfante@gmail.com','123','I'),('V0003','ppruebita','141414','Probando','Pruebita','121231','2017-12-14','A0003',2,'1414@1414.com','1414','A');
/*!40000 ALTER TABLE `tusuarios` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-12-14  2:08:33
