-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: candidateSystem
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `assess`
--

DROP TABLE IF EXISTS `assess`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `assess` (
  `AssessID` int NOT NULL AUTO_INCREMENT,
  `isPass` tinyint DEFAULT NULL,
  `DenyReason` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Comment` varchar(300) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `InterviewMeetingDetailID` int DEFAULT NULL,
  PRIMARY KEY (`AssessID`),
  KEY `fk_interviewmeetingdetailid` (`InterviewMeetingDetailID`),
  CONSTRAINT `fk_interviewmeetingdetailid` FOREIGN KEY (`InterviewMeetingDetailID`) REFERENCES `interviewmeetingdetail` (`InterviewMeetingDetailID`)
) ENGINE=InnoDB AUTO_INCREMENT=155 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `assess`
--

LOCK TABLES `assess` WRITE;
/*!40000 ALTER TABLE `assess` DISABLE KEYS */;
/*!40000 ALTER TABLE `assess` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `assessdetail`
--

DROP TABLE IF EXISTS `assessdetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `assessdetail` (
  `AssessDetailId` int NOT NULL AUTO_INCREMENT,
  `AssessID` int DEFAULT NULL,
  `InterviewMeetingDetailID` int DEFAULT NULL,
  PRIMARY KEY (`AssessDetailId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `assessdetail`
--

LOCK TABLES `assessdetail` WRITE;
/*!40000 ALTER TABLE `assessdetail` DISABLE KEYS */;
/*!40000 ALTER TABLE `assessdetail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `candidate`
--

DROP TABLE IF EXISTS `candidate`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `candidate` (
  `CandidateID` int NOT NULL AUTO_INCREMENT,
  `candidateName` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `candidateDateOfBirth` datetime DEFAULT NULL,
  `candidateAddress` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `candidateNumber` varchar(15) DEFAULT NULL,
  `candidateCV` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `candidateEmail` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `resource` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `jobtitleID` int NOT NULL,
  `jobpositionID` int NOT NULL,
  `HasApplyBefore` tinyint DEFAULT NULL,
  `DelFlag` tinyint DEFAULT NULL,
  `CandidateStatusID` tinyint NOT NULL DEFAULT '0',
  `cancontactid` int DEFAULT NULL,
  `denyreason` varchar(100) DEFAULT NULL,
  `hasEmailFlag` tinyint DEFAULT NULL,
  PRIMARY KEY (`CandidateID`),
  KEY `FK_JobTitle_idx` (`jobtitleID`),
  CONSTRAINT `FK_JobTitle` FOREIGN KEY (`jobtitleID`) REFERENCES `candidateproperty` (`PropertyID`)
) ENGINE=InnoDB AUTO_INCREMENT=146 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `candidate`
--

LOCK TABLES `candidate` WRITE;
/*!40000 ALTER TABLE `candidate` DISABLE KEYS */;
INSERT INTO `candidate` VALUES (128,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_04_2023_NodeJS_Fresher_Toan Khanh Le_123123123','toanf21@gmail.com','',2,4,0,1,10,22,'',0),(129,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_NodeJS_Fresher_Toan Khanh Le_123123123','toanf21@gmail.com','Le Khanh Chi',2,4,1,1,10,22,'',0),(130,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_NodeJS_Fresher_Toan Khanh Le_123123123','toanf21@gmail.com','Toan Khanh Le',2,4,1,1,10,22,'',1),(131,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_NodeJS_Fresher_Toan Khanh Le_123123123','toanf21@gmail.com','Le Khanh Chi',2,4,1,1,10,22,'',1),(132,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_NodeJS_Fresher_Toan Khanh Le_123123123','toanf21@gmail.com','Toan Khanh Chi',2,4,1,1,10,22,'',0),(133,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_Python_Intern_Toan Khanh Le_123123123','toanf21@gmail.com','',1,3,1,1,9,21,'',0),(134,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_Python_Intern_Toan Khanh Le_123123123','toanf21@gmail.com','',1,3,1,1,9,21,'',0),(135,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_Python_Intern_Toan Khanh Le_123123123','toanf21@gmail.com','',1,3,1,0,7,21,'',0),(136,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_Python_Intern_Toan Khanh Le_123123123','toanf21@gmail.com','',1,3,1,0,7,21,'',0),(137,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_Python_Intern_Toan Khanh Le_123123123','toanf21@gmail.com','',1,3,1,0,7,21,'',0),(138,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_Python_Intern_Toan Khanh Le_123123123','toanf21@gmail.com','',1,3,1,0,7,21,'',0),(139,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_Python_Intern_Toan Khanh Le_123123123','toanf21@gmail.com','',1,3,1,0,7,21,'',0),(140,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_Python_Intern_Toan Khanh Le_123123123','toanf21@gmail.com','',1,3,1,0,7,21,'',0),(141,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_C#_Fresher_Toan Khanh Le_123123123','toanf21@gmail.com','',6,4,1,0,7,21,'ReceiveCV',0),(142,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_C#_Fresher_Toan Khanh Le_123123123','toanf21@gmail.com','',6,4,1,0,7,21,'ReceiveCV',0),(143,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_C#_Fresher_Toan Khanh Le_123123123','toanf21@gmail.com','',6,4,1,0,7,21,'ReceiveCV',0),(144,'Khanh Toan Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_NodeJS_Staff_Khanh Toan Le_123123123','toanf21@gmail.com','',2,5,1,1,7,21,'',0),(145,'Toan Khanh Le','0001-01-01 00:00:00','Paper Bridge','123123123','05_11_2023_Python_Fresher_Toan Khanh Le_123123123','toanf21@gmail.com','',1,4,1,1,7,21,'',0);
/*!40000 ALTER TABLE `candidate` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `candidateproperty`
--

DROP TABLE IF EXISTS `candidateproperty`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `candidateproperty` (
  `PropertyID` int NOT NULL AUTO_INCREMENT,
  `PropertyName` varchar(100) DEFAULT NULL,
  `PropertyType` varchar(45) DEFAULT NULL,
  `delFlag` bit(1) DEFAULT NULL,
  PRIMARY KEY (`PropertyID`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `candidateproperty`
--

LOCK TABLES `candidateproperty` WRITE;
/*!40000 ALTER TABLE `candidateproperty` DISABLE KEYS */;
INSERT INTO `candidateproperty` VALUES (1,'Python','JobTitle',NULL),(2,'NodeJS','JobTitle',NULL),(3,'Intern','JobPosition',NULL),(4,'Fresher','JobPosition',NULL),(5,'Staff','JobPosition',NULL),(6,'C#','JobTitle',NULL),(7,'Receive cv','CandidateStatus',NULL),(8,'Deny cv','CandidateStatus',NULL),(9,'Accept cv','CandidateStatus',NULL),(10,'Invited test','CandidateStatus',NULL),(11,'Test OK','CandidateStatus',NULL),(12,'Invited Interview round 1','CandidateStatus',NULL),(13,'Passed Interview round 1','CandidateStatus',NULL),(14,'Invited Interview round 2','CandidateStatus',NULL),(15,'Passed Interview round 2','CandidateStatus',NULL),(16,'Sent Offer','CandidateStatus',NULL),(17,'Accept Offer','CandidateStatus',NULL),(18,'Deny Offer','CandidateStatus',NULL),(19,'Sent Form','CandidateStatus',NULL),(20,'Completed','CandidateStatus',NULL),(21,'not yet','cancontact',_binary ''),(22,'can contact','cancontact',_binary ''),(23,'can not contact','cancontact',_binary '');
/*!40000 ALTER TABLE `candidateproperty` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `email`
--

DROP TABLE IF EXISTS `email`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `email` (
  `emailid` int NOT NULL AUTO_INCREMENT,
  `emailsubject` varchar(100) DEFAULT NULL,
  `emailcontent` varchar(500) DEFAULT NULL,
  `delFlag` tinyint DEFAULT NULL,
  `emailfile` varchar(100) DEFAULT NULL,
  `candidateid` int DEFAULT NULL,
  `invitedPlace` varchar(300) DEFAULT NULL,
  `invitedDate` datetime DEFAULT NULL,
  `CandidateStatusID` int DEFAULT NULL,
  `EmailAddress` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`emailid`),
  KEY `candidateid_idx` (`candidateid`)
) ENGINE=InnoDB AUTO_INCREMENT=171 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `email`
--

LOCK TABLES `email` WRITE;
/*!40000 ALTER TABLE `email` DISABLE KEYS */;
INSERT INTO `email` VALUES (164,'Saishunkan System VietNam_Take A Test Invitation \n','Dear Toan Khanh Le \nWe are a recuitment Deparment of Saishunkai System VietNam. \nThanks for Take an attention in NodeJS Fresher position of our company \nWe have a plan to test NodeJS in the time and location: \n-Date:5/4/2023 11:13:49 AM\n-Location: Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi\nThanks!',1,'',128,'Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-04 11:13:49',9,'toanf21@gmail.com'),(165,'Saishunkan System VietNam_Take A Test Invitation \n','Dear Toan Khanh Le \nWe are a recuitment Deparment of Saishunkai System VietNam. \nThanks for Take an attention in NodeJS Fresher position of our company \nWe have a plan to test NodeJS in the time and location: \n-Date:5/11/2023 1:52:18 PM\n-Location: Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi\nThanks!',1,'filled',129,'Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-16 00:00:00',9,'toanf21@gmail.com'),(166,'Saishunkan System VietNam_Take A Test Invitation \n','Dear Toan Khanh Le \nWe are a recuitment Deparment of Saishunkai System VietNam. \nThanks for Take an attention in NodeJS Fresher position of our company \nWe have a plan to test NodeJS in the time and location: \n-Date:5/11/2023 2:14:39 PM\n-Location: Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi\nThanks!',1,'',130,'Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-11 14:14:39',9,'toanf21@gmail.com'),(167,'Saishunkan System VietNam_Take A Test Invitation \n','Dear Toan Khanh Le \nWe are a recuitment Deparment of Saishunkai System VietNam. \nThanks for Take an attention in NodeJS Fresher position of our company \nWe have a plan to test NodeJS in the time and location: \n-Date:5/11/2023 2:14:39 PM\n-Location: Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi\nThanks!',1,'',131,'Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-11 14:14:39',9,'toanf21@gmail.com'),(168,'','Dear Toan Khanh Le \nWe are a recuitment Deparment of Saishunkai System VietNam. \nThanks for Take an attention in NodeJS Fresher position of our company \n-Date:5/11/2023 2:15:21 PM\n-Location: Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi\nThanks!',1,'',130,'Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-11 14:15:21',10,'toanf21@gmail.com'),(169,'','Dear Toan Khanh Le \nWe are a recuitment Deparment of Saishunkai System VietNam. \nThanks for Take an attention in NodeJS Fresher position of our company \n-Date:5/11/2023 2:15:21 PM\n-Location: Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi\nThanks!',1,'',131,'Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-11 14:15:21',10,'toanf21@gmail.com'),(170,'Saishunkan System VietNam_Take A Test Invitation \n','Dear Toan Khanh Le \nWe are a recuitment Deparment of Saishunkai System VietNam. \nThanks for Take an attention in NodeJS Fresher position of our company \nWe have a plan to test NodeJS in the time and location: \n-Date:5/11/2023 2:18:23 PM\n-Location: Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi\nThanks!',1,'filled',132,'Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-25 00:00:00',9,'toanf21@gmail.com');
/*!40000 ALTER TABLE `email` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `interviewer`
--

DROP TABLE IF EXISTS `interviewer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `interviewer` (
  `interviewerID` int NOT NULL,
  `interviewerName` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `interviewerdepartment` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `interviewerEmail` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  PRIMARY KEY (`interviewerID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `interviewer`
--

LOCK TABLES `interviewer` WRITE;
/*!40000 ALTER TABLE `interviewer` DISABLE KEYS */;
INSERT INTO `interviewer` VALUES (1,'Le Khanh Toan','System','khanhtoan.le.6555@gmail.com'),(2,'Toan Le Toan','Hardware','khanhtoan.le.6555@gmail.com'),(4,'Khanh Le Khanh','Marketing','khanhtoan.le.6555@gmail.com');
/*!40000 ALTER TABLE `interviewer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `interviewmeeting`
--

DROP TABLE IF EXISTS `interviewmeeting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `interviewmeeting` (
  `interviewmeetingid` int NOT NULL AUTO_INCREMENT,
  `InterviewMeetingHeader` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `InterviewMeetingPlace` varchar(300) DEFAULT NULL,
  `InterviewMeetingDate` datetime DEFAULT NULL,
  `InterviewMeetingRoomID` int DEFAULT NULL,
  `CandidateID` int DEFAULT NULL,
  `isHaveAssess` tinyint DEFAULT NULL,
  PRIMARY KEY (`interviewmeetingid`),
  KEY `InterviewMeetingRoomID` (`InterviewMeetingRoomID`),
  KEY `fk_candidate_id` (`CandidateID`),
  CONSTRAINT `fk_candidate_id` FOREIGN KEY (`CandidateID`) REFERENCES `candidate` (`CandidateID`),
  CONSTRAINT `interviewmeeting_ibfk_1` FOREIGN KEY (`InterviewMeetingRoomID`) REFERENCES `interviewproperty` (`propertyid`)
) ENGINE=InnoDB AUTO_INCREMENT=135 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `interviewmeeting`
--

LOCK TABLES `interviewmeeting` WRITE;
/*!40000 ALTER TABLE `interviewmeeting` DISABLE KEYS */;
INSERT INTO `interviewmeeting` VALUES (119,'The Interview with Toan Khanh Le','Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-04 00:00:00',1,128,0),(120,'The Interview with Toan Khanh Le','Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-04 00:00:00',1,128,0),(121,'The Interview with Toan Khanh Le','Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-04 00:00:00',2,128,0),(122,'The Interview with Toan Khanh Le','Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-04 00:00:00',2,128,0),(123,'The Interview with Toan Khanh Le','Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-04 00:00:00',1,128,0),(124,'The Interview with Toan Khanh Le','Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-17 00:00:00',2,128,0),(125,'The Interview with Toan Khanh Le','Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-16 00:00:00',2,129,0),(126,'The Interview with Toan Khanh Le','Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-16 00:00:00',1,129,0),(127,'The Interview with Toan Khanh Le','Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-11 00:00:00',1,130,0),(128,'The Interview with Toan Khanh Le','Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-11 00:00:00',1,130,0),(129,'The Interview with Toan Khanh Le','Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-11 00:00:00',1,130,0),(130,'The Interview with Toan Khanh Le','Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-11 00:00:00',1,131,0),(131,'The Interview with Toan Khanh Le','Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-04 00:00:00',1,128,0),(132,'The Interview with Toan Khanh Le','Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-11 00:00:00',1,131,0),(133,'The Interview with Toan Khanh Le','Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-11 00:00:00',1,130,0),(134,'The Interview with Toan Khanh Le','Saishunkan System Vietnam \n 14th Floor, Thai Binh Tower, 106 Hoang Quoc Viet, Nghia Do, Cau Giay, Ha Noi','2023-05-11 00:00:00',1,130,0);
/*!40000 ALTER TABLE `interviewmeeting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `interviewmeetingdetail`
--

DROP TABLE IF EXISTS `interviewmeetingdetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `interviewmeetingdetail` (
  `InterviewMeetingDetailID` int NOT NULL AUTO_INCREMENT,
  `InterviewMeetingId` int DEFAULT NULL,
  `InterviewerID` int DEFAULT NULL,
  PRIMARY KEY (`InterviewMeetingDetailID`),
  KEY `InterviewMeetingId` (`InterviewMeetingId`),
  KEY `InterviewerID` (`InterviewerID`),
  CONSTRAINT `interviewmeetingdetail_ibfk_1` FOREIGN KEY (`InterviewMeetingId`) REFERENCES `interviewmeeting` (`interviewmeetingid`),
  CONSTRAINT `interviewmeetingdetail_ibfk_2` FOREIGN KEY (`InterviewerID`) REFERENCES `interviewer` (`interviewerID`)
) ENGINE=InnoDB AUTO_INCREMENT=270 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `interviewmeetingdetail`
--

LOCK TABLES `interviewmeetingdetail` WRITE;
/*!40000 ALTER TABLE `interviewmeetingdetail` DISABLE KEYS */;
INSERT INTO `interviewmeetingdetail` VALUES (251,119,2),(252,119,1),(253,120,1),(254,120,2),(255,121,1),(256,121,2),(257,122,1),(258,122,2),(259,123,4),(260,123,2),(261,124,4),(262,124,2),(263,124,1),(264,125,4),(265,125,1),(266,126,1),(267,126,2),(268,134,1),(269,134,2);
/*!40000 ALTER TABLE `interviewmeetingdetail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `interviewproperty`
--

DROP TABLE IF EXISTS `interviewproperty`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `interviewproperty` (
  `propertyid` int NOT NULL,
  `propertyname` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `propertyDetail` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `propertytype` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  PRIMARY KEY (`propertyid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `interviewproperty`
--

LOCK TABLES `interviewproperty` WRITE;
/*!40000 ALTER TABLE `interviewproperty` DISABLE KEYS */;
INSERT INTO `interviewproperty` VALUES (1,'Meeting room','ha noi branch','room'),(2,'Pantry','ha noi branch','room'),(3,'Sport Area','ha noi branch','room');
/*!40000 ALTER TABLE `interviewproperty` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-05-11 17:19:31
