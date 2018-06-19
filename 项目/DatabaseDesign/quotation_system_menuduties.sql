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
-- Table structure for table `menuduties`
--

DROP TABLE IF EXISTS `menuduties`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `menuduties` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `keyCode` varchar(45) DEFAULT NULL,
  `iconCls` varchar(45) DEFAULT NULL,
  `type` varchar(45) DEFAULT NULL,
  `menuId` int(11) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `menuduties`
--

LOCK TABLES `menuduties` WRITE;
/*!40000 ALTER TABLE `menuduties` DISABLE KEYS */;
INSERT INTO `menuduties` VALUES (1,'创建','Create','fa fa-user-plus','btn',8),(2,'修改','Edit','fa fa-pencil','btn',8),(3,'详情','Detail','fa fa-list','btn',8),(4,'删除','Delete','fa fa-trash','btn',8),(5,'创建','Create','fa fa-plus','btn',13),(6,'修改','Edit','fa fa-pencil','btn',13),(7,'详情','Detail','fa fa-list','btn',13),(8,'删除','Delete','fa fa-trash','btn',13),(9,'修改产品分类','EditProductCategories',NULL,'combotree',13),(10,'导入Excel','Import','fa fa-level-down','btn',13),(11,'编辑分类明细','EditCategories',NULL,'result',13),(12,'订单详情','Detail','fa fa-list','btn',19),(13,'审核通过','CheckPass','fa fa-plus','btn',19),(14,'审核退回','CheckFail','fa fa-close','btn',19),(18,'生成订单','Create','fa fa-server','btn',19),(19,'编辑订单','Edit','fa fa-pencil','btn',19),(20,'删除订单','Delete','fa fa-trash','btn',19),(21,'下载订单','Download','fa fa-download','btn',19),(22,'修改权限','Edit',NULL,'btn',26),(24,'修改规则','Edit','fa fa-save','btn',29),(25,'创建','Create','fa fa-plus','btn',9),(26,'修改','Edit','fa fa-pencil','btn',9),(27,'详情','Detail','fa fa-list','btn',9),(28,'删除','Delete','fa fa-trash','btn',9);
/*!40000 ALTER TABLE `menuduties` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-04-18 15:51:03
