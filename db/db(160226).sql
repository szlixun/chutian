/*
SQLyog Ultimate v11.24 (32 bit)
MySQL - 5.1.50-community : Database - chutian
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


USE `chutian`;

truncate table `ct_customer`;

insert  into `ct_customer`(`cid`,`customer`,`cyear`,`sequence`) values (14,'湖北烽火博鑫电缆有限公司',2016,47),(15,'湖北楚天电缆实业有限公司',2016,109),(16,'烽火通信科技股份有限公司',2016,15),(17,'金信诺光纤光缆(赣州)有限公司',2016,0),(18,'湖北凯乐科技股份有限公司',2016,15),(19,'湖南神通光电科技有限责任公司',2016,2),(20,'南京华信藤仓光通信有限公司',2016,11),(21,'武汉宇通光缆有限公司',2016,32),(22,'西安北方光通信有限责任公司',2016,2);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

truncate table `ct_deliveryitem`;
truncate table `ct_deliverynote`;
truncate table `ct_qualitytracking`;


