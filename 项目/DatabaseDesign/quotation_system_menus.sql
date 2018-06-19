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
-- Table structure for table `menus`
--

DROP TABLE IF EXISTS `menus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `menus` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `text` varchar(45) CHARACTER SET gb2312 NOT NULL,
  `iconCls` varchar(45) CHARACTER SET gb2312 DEFAULT NULL,
  `attributes` varchar(45) CHARACTER SET gb2312 DEFAULT NULL,
  `parid` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=102 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `menus`
--

LOCK TABLES `menus` WRITE;
/*!40000 ALTER TABLE `menus` DISABLE KEYS */;
INSERT INTO `menus` VALUES (1,'用户管理','fa fa-users','',0),(2,'产品管理','fa fa-shopping-cart',NULL,0),(3,'报价管理','fa fa-gg-circle',NULL,0),(4,'权限管理','fa fa-shield','',0),(5,'信息管理','fa fa-puzzle-piece',NULL,0),(6,'系统管理','fa fa-gears',NULL,0),(7,'用户管理','fa fa-users',NULL,1),(8,'角色管理','fa fa-users','/SystemUser/LevelIndex',7),(9,'用户信息操作','fa fa-align-center','/SystemUser/Index',7),(10,'个人资料查看','fa fa-user-o','/SystemUser/ShowUserInfo',7),(11,'产品信息','fa fa-th-list',NULL,2),(12,'报价信息','fa fa-gg-circle','',3),(13,'产品操作','fa fa-info','/SystemProduct/Index',11),(17,'报价单管理','fa fa-adjust',NULL,3),(19,'报价单操作','fa fa-align-justify','/Quotation/ShowQuotationInfo',12),(22,'系统说明','fa fa-smile-o',NULL,6),(24,'关于','fa fa-smile-o','/SystemSetting/About',22),(25,'权限管理','fa fa-shield',NULL,4),(26,'权限操作','fa  fa-key','/SystemRight/Index',25),(27,'信息管理','fa fa-puzzle-piece',NULL,5),(28,'我的信息','fa fa-envelope-o','/Notice/Index',27),(29,'代理商售价升降级','fa fa-arrow-circle-up','/Quotation/AgentLevelChangeView',17),(32,'报价单统计','fa fa-area-chart','/Quotation/ShowQuotationEchart',12),(33,'个人资料修改','fa fa-pencil','/SystemUser/ShowUserInfoEdit',7),(34,'产品售价管理','fa fa-money','/SystemProduct/ProductPriceSetting',11);
/*!40000 ALTER TABLE `menus` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-04-18 15:51:04
