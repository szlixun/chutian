ALTER TABLE `chutian`.`ct_deliverynote`   
  ADD COLUMN `description1` VARCHAR(500) NULL  COMMENT '��ע1' AFTER `loginid`,
  ADD COLUMN `sdate` DATETIME NULL  COMMENT '¼��ʱ��' AFTER `description1`;

update chutian.ct_deliverynote set sdate=now(),description1='';