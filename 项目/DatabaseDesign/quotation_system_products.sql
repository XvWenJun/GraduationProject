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
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `products` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `category` int(11) DEFAULT NULL,
  `describe` varchar(200) DEFAULT NULL,
  `unit` varchar(45) DEFAULT NULL,
  `cost` decimal(16,2) DEFAULT NULL,
  `img` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `categoryInfo_idx` (`category`),
  CONSTRAINT `categoryInfo` FOREIGN KEY (`category`) REFERENCES `categories` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=164 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products`
--

LOCK TABLES `products` WRITE;
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` VALUES (150,'Macbook',5,'便携','台',5000.00,NULL),(151,'MacbookPro',5,'性能强劲','台',6000.00,'/Upload/Products/151.jpg'),(152,'小米（MI）电视',6,'米家精选','台',500.00,'/Upload/Products/152.jpg'),(153,'海信（Hisense）LED55EC500U',6,'值得信赖','台',1000.00,'/Upload/Products/153.jpg'),(154,'可乐',7,'世界畅销','瓶',2.00,'/Upload/Products/154.jpg'),(155,'雪碧',7,'世界畅销','瓶',2.00,'/Upload/Products/155.jpg'),(156,'卫龙辣条',8,'好吃的板','袋',1.00,'/Upload/Products/156.jpg'),(157,'旺旺仙贝',8,'还可以','袋',1.00,'/Upload/Products/157.jpg'),(158,'INSCRIPTION',48,'精致','套',100.00,'/Upload/Products/158.jpg'),(159,'雅诚德陶瓷小清新碗碟套装',48,'小清新','套',10.00,'/Upload/Products/159.png'),(160,'夹克外套',50,'好看','件',50.00,'/Upload/Products/160.png'),(161,'七匹狼短袖T恤',50,'好贵','件',50.00,'/Upload/Products/161.jpg'),(162,'罗蒙（ROMON）西裤',51,'材料好','条',50.00,'/Upload/Products/162.jpg'),(163,'花花',51,'材料好','条',80.00,'/Upload/Products/163.jpg');
/*!40000 ALTER TABLE `products` ENABLE KEYS */;
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
