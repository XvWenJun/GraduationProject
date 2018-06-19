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
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `password` varchar(45) NOT NULL,
  `name` varchar(45) NOT NULL,
  `company` varchar(45) DEFAULT NULL,
  `level` int(11) DEFAULT '0',
  `tel` varchar(15) DEFAULT NULL,
  `province` varchar(45) DEFAULT NULL,
  `city` varchar(45) DEFAULT NULL,
  `region` varchar(45) DEFAULT NULL,
  `area` varchar(100) DEFAULT NULL,
  `avatarPath` varchar(200) DEFAULT NULL,
  `active` varchar(15) DEFAULT '启用',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=826001 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (123450,'4QrcOUm6Wau+VuBX8g+IPg==','徐13','太阳伞公司',6,'13511111111','山东省','威海市','文登市','三开放苏坑反动刚来；',NULL,'启用'),(123451,'4QrcOUm6Wau+VuBX8g+IPg==','徐14','太阳伞公司',6,'13511111111','山东省','威海市','文登市','三开放苏坑反动刚来；',NULL,'启用'),(123452,'4QrcOUm6Wau+VuBX8g+IPg==','徐2','太阳伞公司',6,'13511111111','山东省','威海市','文登市','三开放苏坑反动刚来；',NULL,'启用'),(123453,'4QrcOUm6Wau+VuBX8g+IPg==','徐6','太阳伞公司',6,'13511111111','山东省','威海市','文登市','三开放苏坑反动刚来；',NULL,'禁用'),(123454,'4QrcOUm6Wau+VuBX8g+IPg==','徐7','太阳伞公司',4,'18112138026','山东省','威海市','文登市','三开放苏坑反动刚来；',NULL,'启用'),(123456,'4QrcOUm6Wau+VuBX8g+IPg==','徐','太阳伞公司',2,'13511111111','黑龙江省','哈尔滨市','依兰县','你管我在na','/Upload/Users/123456.jpg','启用'),(123458,'ZwsUcorZkCrsujLiL6T2vQ==','好玩吧','2621512152',6,'13551312124','安徽省','亳州市','谯城区','三开放苏坑反动刚来；',NULL,'启用'),(123459,'ZwsUcorZkCrsujLiL6T2vQ==','1321','13513',6,'13551328026','湖北省','仙桃市','仙桃市','132158215',NULL,'启用'),(825969,'ZwsUcorZkCrsujLiL6T2vQ==','小徐','1232112121212',5,'13551329026','湖南省','湘潭市','湘乡市','1215215','/Upload/Users/825969.jpg','启用'),(825984,'ZwsUcorZkCrsujLiL6T2vQ==','徐许','世界联合国际公司',6,'13551324564','湖北省','仙桃市','仙桃市','12152','/Upload/Users/825984.jpg','启用'),(825996,'ZwsUcorZkCrsujLiL6T2vQ==','默认','太阳山',4,'13551324521','广东省','潮州市','潮安县','sfsfsffdsf',NULL,'启用'),(825997,'ZwsUcorZkCrsujLiL6T2vQ==','默认','sfsdf',2,'13551351225','广东省','东莞市','东莞市','dfds',NULL,'启用'),(825999,'ZwsUcorZkCrsujLiL6T2vQ==','默认',NULL,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(826000,'ZwsUcorZkCrsujLiL6T2vQ==','默认',NULL,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
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
