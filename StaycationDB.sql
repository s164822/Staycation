CREATE DATABASE  IF NOT EXISTS `StaycationDB` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `StaycationDB`;
-- MySQL dump 10.13  Distrib 5.7.17, for macos10.12 (x86_64)
--
-- Host: 127.0.0.1    Database: StaycationDB
-- ------------------------------------------------------
-- Server version	5.7.21

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
-- Table structure for table `Adresse`
--

DROP TABLE IF EXISTS `Adresse`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Adresse` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Adresse` varchar(45) NOT NULL,
  `Nummer` varchar(10) NOT NULL,
  `Etage` varchar(10) NOT NULL,
  `Post_nummer` int(11) NOT NULL,
  `By` varchar(45) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Adresse`
--

LOCK TABLES `Adresse` WRITE;
/*!40000 ALTER TABLE `Adresse` DISABLE KEYS */;
INSERT INTO `Adresse` VALUES (1,'Lyngby Hovedgade','86D','3tv',2800,'Lyngby'),(2,'Lyngby Hovedgade','86D','3tv',2800,'Lyngby'),(3,'Lyngby Hovedgade','86D','3tv',2800,'Lyngby'),(4,'Finsensvej','10b','st th',2000,'Frederiksberg'),(5,'Finsensvej','10b','st th',2000,'Frederiksberg'),(6,'Sønder Fasanvej','96','st tv',2500,'Valby'),(7,'Frederiksborgvej','16','3th',2400,'København NV'),(8,'Lyngby Hivedgade','86D','3,tv',2800,'Lyngby'),(9,'Lyngby Hivedgade','86D','3,tv',2800,'Lyngby');
/*!40000 ALTER TABLE `Adresse` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Booking`
--

DROP TABLE IF EXISTS `Booking`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Booking` (
  `Booking_nummer` int(11) NOT NULL AUTO_INCREMENT,
  `Antal_voksne` int(11) NOT NULL,
  `Antal_børn` int(11) DEFAULT NULL,
  `Tjek_ind_dato` date NOT NULL,
  `Tjek_ud_dato` date NOT NULL,
  `Total_pris` decimal(10,2) NOT NULL,
  `Værelse_type_ID` int(11) NOT NULL,
  `Status_ID` int(11) NOT NULL,
  `Kunde_ID` int(11) NOT NULL,
  PRIMARY KEY (`Booking_nummer`),
  KEY `FK_Booking_VærelseType_idx` (`Værelse_type_ID`),
  KEY `FK_Booking_BookingStatus_idx` (`Status_ID`),
  KEY `FK_Booking_Kunde_idx` (`Kunde_ID`),
  CONSTRAINT `FK_Booking_BookingStatus` FOREIGN KEY (`Status_ID`) REFERENCES `Booking_status` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Booking_Kunde` FOREIGN KEY (`Kunde_ID`) REFERENCES `Kunde` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Booking_VærelseType` FOREIGN KEY (`Værelse_type_ID`) REFERENCES `Værelse_type` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Booking`
--

LOCK TABLES `Booking` WRITE;
/*!40000 ALTER TABLE `Booking` DISABLE KEYS */;
INSERT INTO `Booking` VALUES (1,2,0,'2020-09-02','2020-09-04',800.00,1,2,7),(2,1,8,'2020-08-27','2020-08-29',800.00,1,1,1),(3,1,8,'2020-08-27','2020-08-29',800.00,1,1,1),(7,2,3,'2020-12-27','2021-01-02',800.00,1,1,4),(8,2,3,'2020-12-27','2021-01-02',800.00,1,1,4),(9,2,0,'2020-07-04','2021-07-08',800.00,1,1,5),(10,8,8,'2020-06-20','2020-06-28',8765.00,2,1,9),(14,5,NULL,'2020-06-12','2020-06-28',678.00,1,1,1),(15,2,NULL,'2020-06-17','2020-06-20',123.00,1,1,1),(16,2,NULL,'2020-06-21','2020-06-28',123.00,1,1,1),(17,2,NULL,'2020-06-21','2020-06-28',123.00,1,1,1);
/*!40000 ALTER TABLE `Booking` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Booking_status`
--

DROP TABLE IF EXISTS `Booking_status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Booking_status` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Status` varchar(45) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Booking_status`
--

LOCK TABLES `Booking_status` WRITE;
/*!40000 ALTER TABLE `Booking_status` DISABLE KEYS */;
INSERT INTO `Booking_status` VALUES (1,'Ikke ankommet'),(2,'Tjekket ind'),(3,'Tjekket ud');
/*!40000 ALTER TABLE `Booking_status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Kunde`
--

DROP TABLE IF EXISTS `Kunde`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Kunde` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Email` varchar(100) NOT NULL,
  `Adresse_ID` int(11) NOT NULL,
  `Telefon_nummer` int(11) NOT NULL,
  `Fornavn` varchar(45) NOT NULL,
  `Efternavn` varchar(45) NOT NULL,
  `Fødselsdagsdato` date NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_Kunde_Adresse_idx` (`Adresse_ID`),
  CONSTRAINT `FK_Kunde_Adresse` FOREIGN KEY (`Adresse_ID`) REFERENCES `Adresse` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Kunde`
--

LOCK TABLES `Kunde` WRITE;
/*!40000 ALTER TABLE `Kunde` DISABLE KEYS */;
INSERT INTO `Kunde` VALUES (1,'Juliesofiemoller@gmail.com',1,41692705,'Julie','Møller','1995-05-27'),(4,'Juliesofiemoller@gmail.com',1,41692705,'Julie','Møller','1995-05-27'),(5,'Juliesofiemoller@gmail.com',1,41692705,'Julie','Møller','1995-05-27'),(6,'Sigurdholmblad@gmail.com',1,21464895,'Sigurd','Holmblad','1995-10-08'),(7,'Majagregersen@gmail.com',1,27838417,'Maja','Gregersen','1994-06-10'),(8,'Majagregersen@gmail.com',1,27838417,'Maja','Gregersen','1994-06-10'),(9,'Abangild@gmail.com',1,26165715,'Alexander','Bangild','1994-06-08');
/*!40000 ALTER TABLE `Kunde` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Værelse_type`
--

DROP TABLE IF EXISTS `Værelse_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Værelse_type` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Type` varchar(45) NOT NULL,
  `Pris` decimal(10,2) NOT NULL,
  `Beskrivelse` varchar(200) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Værelse_type`
--

LOCK TABLES `Værelse_type` WRITE;
/*!40000 ALTER TABLE `Værelse_type` DISABLE KEYS */;
INSERT INTO `Værelse_type` VALUES (1,'Suite',1800.00,'Suite'),(2,'Double',1500.00,'Double'),(3,'Single',1300.00,'Single'),(4,'Suite',1500.00,'dkej'),(5,'Suite',1800.00,'dkej');
/*!40000 ALTER TABLE `Værelse_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'StaycationDB'
--
/*!50003 DROP PROCEDURE IF EXISTS `createUserAndBooking` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `createUserAndBooking`()
BEGIN

Insert into Adresse (Adresse, Nummer, Etage, Post_nummer, Adresse.By) values ('Lyngby Hovedgade', '86D', '3tv', 2800, 'Lyngby');

Insert into Kunde (Email, Adresse_ID, Telefon_nummer, Pasnummer, Fornavn, Efternavn, Fødselsdagsdato) values ('rkh@hotmail.com', 1, '12345678', '8083432', 'Rasmus', 'Hjorth', DATE("1996-08-20"));

insert into Booking (Antal_voksne, Antal_børn, Tjek_ind_dato, Tjek_ud_dato, Total_pris, Værelse_type_ID, Status_ID, Kunde_ID) values (1, 8, DATE("2020-08-27"), DATE("2020-08-29"), 800, 1, 1, 1);


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-06-18 22:40:02
