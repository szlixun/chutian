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
CREATE DATABASE /*!32312 IF NOT EXISTS*/`chutian` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `chutian`;

/*Table structure for table `ct_customer` */

DROP TABLE IF EXISTS `ct_customer`;

CREATE TABLE `ct_customer` (
  `cid` int(11) NOT NULL AUTO_INCREMENT,
  `customer` varchar(200) NOT NULL COMMENT '客户名称',
  `cyear` int(11) NOT NULL COMMENT '送货单年度',
  `sequence` int(11) NOT NULL COMMENT '送货单流水号',
  PRIMARY KEY (`cid`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;

/*Data for the table `ct_customer` */

insert  into `ct_customer`(`cid`,`customer`,`cyear`,`sequence`) values (14,'湖北烽火博鑫电缆有限公司',2016,47),(15,'湖北楚天电缆实业有限公司',2016,109),(16,'烽火通信科技股份有限公司',2016,15),(17,'金信诺光纤光缆(赣州)有限公司',2016,0),(18,'湖北凯乐科技股份有限公司',2016,15),(19,'湖南神通光电科技有限责任公司',2016,2),(20,'南京华信藤仓光通信有限公司',2016,11),(21,'武汉宇通光缆有限公司',2016,32),(22,'西安北方光通信有限责任公司',2016,2);

/*Table structure for table `ct_deliveryitem` */

DROP TABLE IF EXISTS `ct_deliveryitem`;

CREATE TABLE `ct_deliveryitem` (
  `itemid` int(11) NOT NULL AUTO_INCREMENT,
  `noteid` int(11) NOT NULL COMMENT 'noteId',
  `jiannum` varchar(50) NOT NULL COMMENT '件号',
  `specifications` varchar(50) NOT NULL COMMENT '规格',
  `lenght` int(11) NOT NULL COMMENT '长度',
  `discnum` int(11) NOT NULL COMMENT '盘数',
  `weight` double NOT NULL COMMENT '净重',
  `price` float DEFAULT NULL COMMENT '单价',
  `totalprice` double DEFAULT NULL COMMENT '金额',
  `contractno` varchar(100) DEFAULT NULL COMMENT '合同号',
  PRIMARY KEY (`itemid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ct_deliveryitem` */

/*Table structure for table `ct_deliverynote` */

DROP TABLE IF EXISTS `ct_deliverynote`;

CREATE TABLE `ct_deliverynote` (
  `noteid` int(11) NOT NULL AUTO_INCREMENT,
  `deliverid` int(11) NOT NULL COMMENT '送货单号',
  `customer` varchar(150) NOT NULL COMMENT '客户名称',
  `model` varchar(100) DEFAULT NULL COMMENT '型号',
  `deliverdate` datetime NOT NULL COMMENT '发货时间',
  `goodname` varchar(100) DEFAULT NULL COMMENT '货物名称',
  `batch` varchar(100) DEFAULT NULL COMMENT '出厂批号',
  `description` varchar(100) DEFAULT NULL COMMENT '备注',
  `loginid` varchar(50) NOT NULL COMMENT '登陆名',
  PRIMARY KEY (`noteid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ct_deliverynote` */

/*Table structure for table `ct_qualitytracking` */

DROP TABLE IF EXISTS `ct_qualitytracking`;

CREATE TABLE `ct_qualitytracking` (
  `qtid` int(11) NOT NULL AUTO_INCREMENT,
  `qtdate` date NOT NULL COMMENT '时间',
  `category` varchar(50) DEFAULT NULL COMMENT '品种',
  `batch` varchar(50) DEFAULT NULL COMMENT '批次',
  `specifications` varchar(50) DEFAULT NULL COMMENT '规格',
  `length` int(11) DEFAULT NULL COMMENT '长度',
  `volume` varchar(50) DEFAULT NULL COMMENT '卷号',
  `stripping1` varchar(100) DEFAULT NULL COMMENT '剥离1',
  `stripping2` varchar(100) DEFAULT NULL COMMENT '剥离2',
  `sample11` varchar(50) DEFAULT NULL COMMENT '样品1-1',
  `sample12` varchar(50) DEFAULT NULL COMMENT '样品1-2',
  `sample13` varchar(50) DEFAULT NULL COMMENT '样品1-3',
  `sample21` varchar(50) DEFAULT NULL COMMENT '样品2-1',
  `sample22` varchar(50) DEFAULT NULL COMMENT '样品2-2',
  `sample23` varchar(50) DEFAULT NULL COMMENT '样品2-3',
  `baseheight` varchar(50) DEFAULT NULL COMMENT '基带厚度',
  `measuredheight` varchar(50) DEFAULT NULL COMMENT '实测厚度',
  `compositeheight` varchar(50) DEFAULT NULL COMMENT '复合厚度',
  `cutheight` varchar(50) DEFAULT NULL COMMENT '分切厚度',
  `bubblewater1` varchar(50) DEFAULT NULL COMMENT '泡水1',
  `bubblewater2` varchar(50) DEFAULT NULL COMMENT '泡水2',
  `bubbleoil` varchar(50) DEFAULT NULL COMMENT '泡油',
  `descrtiption` varchar(500) DEFAULT NULL COMMENT '备注',
  `loginid` varchar(50) NOT NULL COMMENT '登陆名',
  PRIMARY KEY (`qtid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ct_qualitytracking` */

/*Table structure for table `ct_sys_dictionary` */

DROP TABLE IF EXISTS `ct_sys_dictionary`;

CREATE TABLE `ct_sys_dictionary` (
  `dicid` int(11) NOT NULL AUTO_INCREMENT,
  `dictype` enum('qtspec','qtcategory','model','goodname','deliveryspec') NOT NULL COMMENT '字典类型',
  `dictvalue` varchar(100) NOT NULL COMMENT '内容',
  PRIMARY KEY (`dicid`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8;

/*Data for the table `ct_sys_dictionary` */

insert  into `ct_sys_dictionary`(`dicid`,`dictype`,`dictvalue`) values (1,'model','2'),(2,'goodname','3'),(3,'deliveryspec','0.23*25'),(4,'qtspec','3'),(5,'qtcategory','1'),(6,'model','88'),(7,'goodname','88'),(8,'deliveryspec','88'),(9,'model','55'),(10,'goodname','555'),(11,'model','66'),(12,'goodname','666'),(13,'deliveryspec','0.25*25'),(14,'qtspec','2'),(15,'qtcategory','2'),(16,'model','11'),(17,'goodname','11'),(18,'model','22'),(19,'goodname','22'),(20,'qtspec','1'),(21,'qtcategory','3'),(22,'qtspec','4'),(23,'qtcategory','4'),(24,'model','1'),(25,'goodname','1'),(26,'goodname','2'),(27,'model','3'),(28,'model','4'),(29,'goodname','4');

/*Table structure for table `ct_user` */

DROP TABLE IF EXISTS `ct_user`;

CREATE TABLE `ct_user` (
  `userid` int(11) NOT NULL AUTO_INCREMENT,
  `loginid` varchar(50) NOT NULL COMMENT '登陆名',
  `username` varchar(50) NOT NULL COMMENT '真实姓名',
  `password` varchar(50) DEFAULT NULL COMMENT '密码MD5',
  `dodelivery` tinyint(1) DEFAULT NULL COMMENT '是否可以使用送货单',
  `dotracking` tinyint(1) DEFAULT NULL COMMENT '是否可以使用质量管理',
  `dousermanage` tinyint(1) DEFAULT NULL COMMENT '是否可以使用用户管理',
  PRIMARY KEY (`userid`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

/*Data for the table `ct_user` */

insert  into `ct_user`(`userid`,`loginid`,`username`,`password`,`dodelivery`,`dotracking`,`dousermanage`) values (3,'admin','管理员','666666',1,1,1);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
