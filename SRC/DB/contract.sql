/*
Navicat MySQL Data Transfer

Source Server         : mysql56
Source Server Version : 50611
Source Host           : localhost:3306
Source Database       : contract

Target Server Type    : MYSQL
Target Server Version : 50611
File Encoding         : 65001

Date: 2015-01-11 16:01:34
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for t_contract
-- ----------------------------
DROP TABLE IF EXISTS `t_contract`;
CREATE TABLE `t_contract` (
  `contractid` int(11) NOT NULL AUTO_INCREMENT,
  `seq` varchar(45) DEFAULT NULL COMMENT '序号',
  `contractnum` varchar(45) NOT NULL COMMENT '合同编号',
  `contractname` varchar(128) DEFAULT NULL COMMENT '合同名称',
  `contractstate` varchar(45) DEFAULT NULL COMMENT '合同状态',
  `unshelve` varchar(45) DEFAULT NULL COMMENT '是否下架',
  `projectnum` varchar(45) NOT NULL COMMENT '项目编号',
  `projectname` varchar(100) NOT NULL COMMENT '项目名称',
  `projectmanager` varchar(45) DEFAULT NULL COMMENT '项目负责人',
  `tel` varchar(100) DEFAULT NULL COMMENT '联系方式',
  `depart` varchar(45) DEFAULT NULL COMMENT '分管部门',
  `linker` varchar(45) DEFAULT NULL COMMENT '联系人',
  `paymethod` varchar(45) DEFAULT NULL COMMENT '付款方式',
  `money` varchar(45) DEFAULT '0.00' COMMENT '中标金额（万元）',
  `contractplace` varchar(45) DEFAULT NULL COMMENT '存放位置',
  `contractrfid` varchar(45) DEFAULT NULL COMMENT 'rfid标签id',
  `bcompany` varchar(45) DEFAULT NULL COMMENT '中标公司',
  `signingdate` varchar(45) DEFAULT NULL COMMENT '签订日期',
  `packageName` varchar(100) DEFAULT NULL COMMENT '分包名称',
  `packageBudget` varchar(45) DEFAULT '0.00' COMMENT '分包预算（万元）',
  `tendarNum` varchar(45) DEFAULT NULL COMMENT '招标编号',
  `tendarCompany` varchar(100) DEFAULT NULL COMMENT '招标公司',
  `tendarStartTime` varchar(45) DEFAULT NULL COMMENT '开标时间',
  `phone` varchar(45) DEFAULT NULL COMMENT '手机号码',
  `deliveryTime` varchar(45) DEFAULT NULL COMMENT '交货时间',
  `inspection` varchar(128) DEFAULT NULL COMMENT '验货情况',
  `progress` varchar(45) DEFAULT NULL COMMENT '进度',
  `isPayAll` varchar(20) DEFAULT NULL COMMENT '是否支付全款',
  `isArmoured` varchar(20) DEFAULT NULL COMMENT '是否押款',
  `isRefund` varchar(20) DEFAULT NULL,
  `remark` varchar(256) DEFAULT NULL COMMENT '备注',
  `operatorId` varchar(45) DEFAULT NULL COMMENT '操作员id',
  `operatorName` varchar(45) DEFAULT NULL COMMENT '操作员姓名',
  `createtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modifytime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`contractid`)
) ENGINE=InnoDB AUTO_INCREMENT=7723 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_contract
-- ----------------------------

-- ----------------------------
-- Table structure for t_user
-- ----------------------------
DROP TABLE IF EXISTS `t_user`;
CREATE TABLE `t_user` (
  `userid` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `sex` varchar(255) DEFAULT NULL,
  `password` varchar(45) NOT NULL,
  `link` varchar(45) DEFAULT NULL,
  `enable` int(11) DEFAULT '1',
  `createtime` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `modifytime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `address` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`userid`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of t_user
-- ----------------------------
INSERT INTO `t_user` VALUES ('1', 'admin', '男', '123456', '', '1', '2015-01-04 19:43:40', '2015-01-04 19:43:40', '杭州');
