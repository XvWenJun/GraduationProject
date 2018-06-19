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
-- Table structure for table `notices`
--

DROP TABLE IF EXISTS `notices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `notices` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `receiver` int(11) DEFAULT NULL,
  `title` varchar(45) DEFAULT NULL,
  `message` varchar(200) DEFAULT NULL,
  `url` varchar(200) DEFAULT NULL,
  `datetime` datetime DEFAULT NULL,
  `state` tinyint(2) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=115 DEFAULT CHARSET=gb2312;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notices`
--

LOCK TABLES `notices` WRITE;
/*!40000 ALTER TABLE `notices` DISABLE KEYS */;
INSERT INTO `notices` VALUES (62,123454,'订单审核结果','你的订单号为40的订单审核通过；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-11 16:24:58',1),(63,123454,'订单审核结果','你的订单号为42的订单审核通过；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-11 16:25:06',1),(64,123454,'订单审核结果','你的订单号为43的订单审核失败；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-11 16:25:07',1),(65,123454,'订单审核结果','你的订单号为44的订单审核失败；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-11 16:25:09',1),(66,123454,'上月报表出炉','2018年3月的报表已经出炉；点击下方链接查看上一个月的销售情况。','/Quotation/ShowQuotationEchart','2018-04-11 16:25:33',1),(67,0,'报价单有改动','代理商已修改43报价单；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-11 16:25:58',1),(69,123454,'用户升级','恭喜你升级到一级代理商；由于你销售情况优秀，相较于之前的产品报价，你将获得更低的产品售价，希望你能继续保持，再接再厉，也祝我们合作愉快！点击下方链接查看新的售价。','/SystemProduct/Index','2018-04-11 16:29:26',1),(70,0,'有新报价单','代理商已添加报价单；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-11 19:25:59',1),(71,123454,'订单审核结果','你的订单号为43的订单审核通过；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-11 19:28:51',1),(74,123456,'订单审核结果','你的订单号为30的订单审核通过；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-11 19:37:31',0),(76,123454,'订单审核结果','你的订单号为45的订单审核通过；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-11 19:48:40',1),(77,0,'报价单有改动','代理商已修改44报价单；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-11 19:55:08',1),(78,0,'有新报价单','代理商已添加报价单；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-11 19:59:55',1),(79,0,'报价单有改动','代理商已修改44报价单；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-11 20:00:36',1),(80,123454,'订单审核结果','你的订单号为44的订单审核通过；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-11 20:01:14',1),(82,123454,'订单审核结果','你的订单号为47的订单审核通过；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-11 20:17:08',0),(83,123454,'订单审核结果','你的订单号为48的订单审核通过；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-11 20:34:58',0),(86,123454,'订单审核结果','你的订单号为49的订单审核通过；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-12 11:30:05',0),(87,123454,'订单审核结果','你的订单号为50的订单审核失败；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-12 11:30:07',0),(90,123456,'用户降级','很遗憾你降级到三级代理商；由于你销售情况不理想，相较于之前的产品报价，你将获得更高的产品售价，希望你今后能有一个好的销售情况，也祝我们合作愉快！点击下方链接查看新的售价。','/SystemProduct/Index','2018-04-16 16:47:47',0),(91,123456,'用户降级','很遗憾你降级到四级代理商；由于你销售情况不理想，相较于之前的产品报价，你将获得更高的产品售价，希望你今后能有一个好的销售情况，也祝我们合作愉快！点击下方链接查看新的售价。','/SystemProduct/Index','2018-04-16 16:50:44',0),(92,123456,'用户降级','很遗憾你降级到五级代理商；由于你销售情况不理想，相较于之前的产品报价，你将获得更高的产品售价，希望你今后能有一个好的销售情况，也祝我们合作愉快！点击下方链接查看新的售价。','/SystemProduct/Index','2018-04-16 16:50:52',0),(93,123456,'用户降级','很遗憾你降级到六级代理商；由于你销售情况不理想，相较于之前的产品报价，你将获得更高的产品售价，希望你今后能有一个好的销售情况，也祝我们合作愉快！点击下方链接查看新的售价。','/SystemProduct/Index','2018-04-16 16:52:16',0),(94,123456,'用户降级','很遗憾你降级到六级代理商；由于你销售情况不理想，相较于之前的产品报价，你将获得更高的产品售价，希望你今后能有一个好的销售情况，也祝我们合作愉快！点击下方链接查看新的售价。','/SystemProduct/Index','2018-04-16 17:05:14',0),(95,123456,'用户降级','很遗憾你降级到六级代理商；由于你销售情况不理想，相较于之前的产品报价，你将获得更高的产品售价，希望你今后能有一个好的销售情况，也祝我们合作愉快！点击下方链接查看新的售价。','/SystemProduct/Index','2018-04-16 17:07:20',0),(96,0,'有新报价单','代理商已添加报价单；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-17 21:22:50',1),(97,0,'有新报价单','代理商已添加报价单；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-17 21:29:38',1),(98,0,'有新报价单','代理商已添加报价单；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-17 21:33:03',1),(99,0,'报价单有改动','代理商已修改54报价单；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-17 21:34:02',1),(100,0,'有新报价单','代理商已添加报价单；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-17 21:34:56',1),(101,123450,'订单审核结果','你的订单号为53的订单审核通过；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-17 21:36:55',1),(102,123450,'订单审核结果','你的订单号为55的订单审核失败；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-17 21:36:58',1),(103,0,'报价单有改动','代理商已修改55报价单；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-17 21:46:42',1),(104,123450,'用户升级','恭喜你升级到一级代理商；由于你销售情况优秀，相较于之前的产品报价，你将获得更低的产品售价，希望你能继续保持，再接再厉，也祝我们合作愉快！点击下方链接查看新的售价。','/SystemProduct/Index','2018-04-17 23:50:51',1),(105,123450,'用户升级','恭喜你升级到一级代理商；由于你销售情况优秀，相较于之前的产品报价，你将获得更低的产品售价，希望你能继续保持，再接再厉，也祝我们合作愉快！点击下方链接查看新的售价。','/SystemProduct/Index','2018-04-17 23:53:44',1),(106,123450,'用户降级','很遗憾你降级到三级代理商；由于你销售情况不理想，相较于之前的产品报价，你将获得更高的产品售价，希望你今后能有一个好的销售情况，也祝我们合作愉快！点击下方链接查看新的售价。','/SystemProduct/Index','2018-04-17 23:59:49',1),(107,123450,'上月报表出炉','2018年3月的报表已经出炉；点击下方链接查看上一个月的销售情况。','/Quotation/ShowQuotationEchart','2018-04-18 00:02:31',1),(110,123450,'上月报表出炉','2018年3月的报表已经出炉；点击下方链接查看上一个月的销售情况。','/Quotation/ShowQuotationEchart','2018-04-18 00:17:34',1),(111,0,'上月报表出炉','2018年3月的报表已经出炉；点击下方链接查看上一个月的销售情况。','/Quotation/ShowQuotationEchart','2018-04-18 00:19:06',0),(112,0,'上月报表出炉','2018年3月的报表已经出炉；点击下方链接查看上一个月的销售情况。','/Quotation/ShowQuotationEchart','2018-04-18 00:19:38',0),(113,123450,'上月报表出炉','2018年3月的报表已经出炉；点击下方链接查看上一个月的销售情况。','/Quotation/ShowQuotationEchart','2018-04-18 00:19:44',1),(114,123450,'订单审核结果','你的订单号为23的订单审核通过；点击下方链接查看报价单。','/Quotation/ShowQuotationInfo','2018-04-18 00:20:01',0);
/*!40000 ALTER TABLE `notices` ENABLE KEYS */;
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
