/*
SQLyog Community v13.0.1 (64 bit)
MySQL - 5.7.22-log : Database - imdb
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
/*Table structure for table `actors` */

DROP TABLE IF EXISTS `actors`;

CREATE TABLE `actors` (
  `actid` bigint(20) unsigned NOT NULL AUTO_INCREMENT COMMENT 'Actor ID',
  `actname` varchar(255) NOT NULL COMMENT 'Actor Name',
  `actsex` varchar(12) NOT NULL COMMENT 'Actor Gender',
  `actdob` date NOT NULL COMMENT 'Actor DOB',
  `actbio` text NOT NULL COMMENT 'Actor Bio',
  PRIMARY KEY (`actid`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

/*Data for the table `actors` */

insert  into `actors`(`actid`,`actname`,`actsex`,`actdob`,`actbio`) values (1,'Varun','Male','2018-11-14','Is a good Actor and have worked in good movies with actress like jacqline etc');
insert  into `actors`(`actid`,`actname`,`actsex`,`actdob`,`actbio`) values (2,'Akshay','Male','2018-11-09','Something something he is all good.');
insert  into `actors`(`actid`,`actname`,`actsex`,`actdob`,`actbio`) values (3,'Deepika','Female','2018-11-11','She has won a lot of awards');
insert  into `actors`(`actid`,`actname`,`actsex`,`actdob`,`actbio`) values (4,'Shahrukh Khan','Male','2018-11-05','He is the king of the Bollywood, Also known as the King Khan.');
insert  into `actors`(`actid`,`actname`,`actsex`,`actdob`,`actbio`) values (5,'Kirti','Female','2018-11-01','She is new in the industry but have stolen the hearts of many people.');

/*Table structure for table `movieactor` */

DROP TABLE IF EXISTS `movieactor`;

CREATE TABLE `movieactor` (
  `macid` bigint(20) NOT NULL AUTO_INCREMENT,
  `actid` bigint(20) NOT NULL,
  `movid` bigint(20) NOT NULL,
  PRIMARY KEY (`macid`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=latin1;

/*Data for the table `movieactor` */

insert  into `movieactor`(`macid`,`actid`,`movid`) values (1,1,1);
insert  into `movieactor`(`macid`,`actid`,`movid`) values (2,2,1);
insert  into `movieactor`(`macid`,`actid`,`movid`) values (3,5,1);
insert  into `movieactor`(`macid`,`actid`,`movid`) values (5,1,4);
insert  into `movieactor`(`macid`,`actid`,`movid`) values (17,2,6);
insert  into `movieactor`(`macid`,`actid`,`movid`) values (18,1,6);

/*Table structure for table `movies` */

DROP TABLE IF EXISTS `movies`;

CREATE TABLE `movies` (
  `movid` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'Movie ID',
  `proid` bigint(20) DEFAULT NULL COMMENT 'Producer ID',
  `moviname` varchar(255) NOT NULL COMMENT 'Movie Name',
  `movirelyear` int(4) NOT NULL COMMENT 'Movie Release Year',
  `moviplot` varchar(255) NOT NULL COMMENT 'Movie Plot',
  `moviposter` text NOT NULL COMMENT 'Movie Poster Url',
  PRIMARY KEY (`movid`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

/*Data for the table `movies` */

insert  into `movies`(`movid`,`proid`,`moviname`,`movirelyear`,`moviplot`,`moviposter`) values (1,1,'ABCD-2',2016,'Was on Dance','nnsdfkdskf.jpg');
insert  into `movies`(`movid`,`proid`,`moviname`,`movirelyear`,`moviplot`,`moviposter`) values (4,1,'Bombae',2018,'Short Film','http://localhost/imdbApi/img/dbfckebf.jpg');
insert  into `movies`(`movid`,`proid`,`moviname`,`movirelyear`,`moviplot`,`moviposter`) values (6,2,'fdgdf',2018,'fgdfg','ecff2000ae6c46479587ed435a91b63b.jpg');

/*Table structure for table `producers` */

DROP TABLE IF EXISTS `producers`;

CREATE TABLE `producers` (
  `proid` bigint(20) unsigned NOT NULL AUTO_INCREMENT COMMENT 'Producer ID',
  `proname` varchar(255) NOT NULL COMMENT 'Producer Name',
  `prosex` varchar(12) NOT NULL COMMENT 'Producer Gender',
  `prodob` date NOT NULL COMMENT 'Producer DOB',
  `probio` text NOT NULL COMMENT 'Producer Bio',
  PRIMARY KEY (`proid`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

/*Data for the table `producers` */

insert  into `producers`(`proid`,`proname`,`prosex`,`prodob`,`probio`) values (1,'KK Bro','Male','2018-11-14','Good Investor');
insert  into `producers`(`proid`,`proname`,`prosex`,`prodob`,`probio`) values (2,'Mio','Female','2018-11-19','Clasic Producer');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
