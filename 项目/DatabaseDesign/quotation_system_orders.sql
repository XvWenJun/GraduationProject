-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: quotation_system
-- ------------------------------------------------------
-- Server version	8.0.3-rc-log

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
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orders` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `date` date DEFAULT NULL,
  `staffId` int(11) DEFAULT '0',
  `staffName` varchar(45) DEFAULT NULL,
  `agentId` int(11) DEFAULT '0',
  `agentName` varchar(45) DEFAULT NULL,
  `state` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `staffId_idx` (`staffId`,`agentId`)
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (23,'2018-03-26',123456,'徐',123450,'徐13','审核通过'),(24,'2018-02-26',123456,'徐',123450,'徐13','审核通过'),(25,'2018-03-26',123456,'徐',123450,'徐13','审核通过'),(26,'2018-03-26',123456,'徐',123450,'徐13','审核通过'),(29,'2018-02-04',123456,'徐',123450,'徐13','审核通过'),(30,'2018-02-04',123456,'徐',123454,'徐7','审核通过'),(40,'2018-02-10',123456,'徐',123450,'徐13','审核通过'),(41,'2018-02-09',123456,'徐',123450,'徐13','审核通过'),(42,'2018-02-10',123456,'徐',123450,'徐13','审核通过'),(43,'2018-02-11',123456,'徐',123450,'徐13','审核通过'),(44,'2018-02-11',123456,'徐',123450,'徐13','审核通过'),(45,'2018-04-11',123456,'徐',123454,'徐7','审核通过'),(46,'2018-04-11',123456,'徐',123450,'徐13','审核通过'),(47,'2018-04-11',123456,'徐',123450,'徐13','审核通过'),(48,'2018-04-11',123456,'徐',123450,'徐13','审核通过'),(49,'2018-04-11',123456,'徐',123450,'徐13','审核通过'),(53,'2018-04-17',123456,'徐',123450,'徐13','审核通过'),(55,'2018-04-17',123456,'徐',123450,'徐13','审核通过');
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-04-18 15:51:05
