ALTER TABLE `chutian`.`ct_deliveryitem`   
  ADD COLUMN `netweight` DOUBLE DEFAULT 0  NULL  COMMENT 'ë��' AFTER `contractno`,
  ADD COLUMN `coreweight` DOUBLE DEFAULT 0  NULL  COMMENT '��о����' AFTER `netweight`;
  
ALTER TABLE `chutian`.`ct_qualitytracking`   
  ADD COLUMN `elongation` VARCHAR(100) NULL  COMMENT '������' AFTER `target`;
  