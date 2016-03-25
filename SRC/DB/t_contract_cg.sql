/*
Navicat MySQL Data Transfer

Source Server         : 127.0.0.1
Source Server Version : 50624
Source Host           : 127.0.0.1:3306
Source Database       : contract

Target Server Type    : MYSQL
Target Server Version : 50624
File Encoding         : 65001

Date: 2016-03-25 20:38:00
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for t_contract_cg
-- ----------------------------
DROP TABLE IF EXISTS `t_contract_cg`;
CREATE TABLE `t_contract_cg` (
  `contractid` int(11) NOT NULL AUTO_INCREMENT,
  `seq` varchar(50) DEFAULT NULL COMMENT '采购编号',
  `type` varchar(100) DEFAULT NULL COMMENT '采购类型',
  `content` varchar(255) DEFAULT NULL COMMENT '采购内容',
  `price` decimal(10,2) DEFAULT '0.00' COMMENT '单价',
  `count` int(11) DEFAULT '0' COMMENT '数量',
  `subtotal` decimal(12,2) DEFAULT '0.00' COMMENT '小计',
  `total` decimal(12,2) DEFAULT '0.00' COMMENT '合计',
  `contractnum` varchar(255) DEFAULT NULL COMMENT '合同编号',
  `department` varchar(255) DEFAULT NULL COMMENT '采购部门',
  `linker` varchar(255) DEFAULT NULL COMMENT '采购联系人',
  `tel` varchar(255) DEFAULT NULL COMMENT '联系电话',
  `projectnum` varchar(50) DEFAULT NULL COMMENT '项目编号',
  `budgetamount` varchar(50) DEFAULT NULL COMMENT '预算金额',
  `fundsource` varchar(255) DEFAULT NULL COMMENT '经费来源',
  `super` varchar(255) DEFAULT NULL COMMENT '供货单位',
  `superlinker` varchar(255) DEFAULT NULL COMMENT '供货联系人',
  `supertel` varchar(255) DEFAULT NULL COMMENT '供货联系电话',
  `settleamount` varchar(255) DEFAULT NULL COMMENT '结算金额',
  `freecontent` varchar(255) DEFAULT NULL COMMENT '赠送内容',
  `freevalue` varchar(255) DEFAULT NULL COMMENT '赠送价值',
  `validate` varchar(255) DEFAULT NULL COMMENT '验收情况',
  `remark` varchar(255) DEFAULT NULL COMMENT '备注',
  `payprogress` varchar(255) DEFAULT NULL COMMENT '支付进度',
  `chargedepartment` varchar(255) DEFAULT NULL COMMENT '分管部门',
  `place` varchar(255) DEFAULT NULL COMMENT '存放位置',
  `operatorId` varchar(255) DEFAULT NULL,
  `operatorName` varchar(255) DEFAULT NULL,
  `createtime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `modifytime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `buytime` varchar(255) DEFAULT NULL COMMENT '采购时间',
  PRIMARY KEY (`contractid`)
) ENGINE=InnoDB AUTO_INCREMENT=71 DEFAULT CHARSET=utf8;
