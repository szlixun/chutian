ALTER TABLE `chutian`.`ct_qualitytracking`   
  CHANGE `target` `target` VARCHAR(200) CHARSET utf8 COLLATE utf8_general_ci NOT NULL  COMMENT '去向',
  CHANGE `elongation` `elongation` VARCHAR(100) CHARSET utf8 COLLATE utf8_general_ci NULL  COMMENT '原材料延伸率',
  ADD COLUMN `elongation1` VARCHAR(100) NULL  COMMENT '成品延伸率' AFTER `elongation`,
  ADD COLUMN `type` VARCHAR(50) NULL  COMMENT '类型(原材料,半成品,成品)' AFTER `elongation1`,
  ADD COLUMN `decision` VARCHAR(50) NULL  COMMENT '品质判定' AFTER `type`;
