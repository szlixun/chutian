ALTER TABLE `chutian`.`ct_deliveryitem`   
  ADD COLUMN `netweight` DOUBLE DEFAULT 0  NULL  COMMENT '毛重' AFTER `contractno`,
  ADD COLUMN `coreweight` DOUBLE DEFAULT 0  NULL  COMMENT '管芯重量' AFTER `netweight`;
  
ALTER TABLE `chutian`.`ct_qualitytracking`   
  ADD COLUMN `elongation` VARCHAR(100) NULL  COMMENT '延伸率' AFTER `target`;
  