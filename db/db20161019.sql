ALTER TABLE `chutian`.`ct_qualitytracking`   
  CHANGE `target` `target` VARCHAR(200) CHARSET utf8 COLLATE utf8_general_ci NOT NULL  COMMENT 'ȥ��',
  CHANGE `elongation` `elongation` VARCHAR(100) CHARSET utf8 COLLATE utf8_general_ci NULL  COMMENT 'ԭ����������',
  ADD COLUMN `elongation1` VARCHAR(100) NULL  COMMENT '��Ʒ������' AFTER `elongation`,
  ADD COLUMN `type` VARCHAR(50) NULL  COMMENT '����(ԭ����,���Ʒ,��Ʒ)' AFTER `elongation1`,
  ADD COLUMN `decision` VARCHAR(50) NULL  COMMENT 'Ʒ���ж�' AFTER `type`;
