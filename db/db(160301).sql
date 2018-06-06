ALTER TABLE `chutian`.`ct_deliverynote`   
  ADD COLUMN `description1` VARCHAR(500) NULL  COMMENT '备注1' AFTER `loginid`,
  ADD COLUMN `sdate` DATETIME NULL  COMMENT '录入时间' AFTER `description1`;

update chutian.ct_deliverynote set sdate=now(),description1='';