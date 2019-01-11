/*
SQLyog Enterprise - MySQL GUI v4.1
Host - 5.0.27-community-nt : Database - b2e
*********************************************************************
Server version : 5.0.27-community-nt
*/


create database if not exists `b2e`;

USE `b2e`;

/*Table structure for table `tb_erros` */

drop table if exists `tb_erros`;

CREATE TABLE `tb_erros` (
  `cod_erro` int(11) NOT NULL auto_increment,
  `data_erro` datetime NOT NULL,
  `sistema` varchar(255) NOT NULL,
  `maquina` varchar(255) NOT NULL,
  `numero` int(11) NOT NULL,
  `descricao` text NOT NULL,
  `origem` text NOT NULL,
  `procedimento` text NOT NULL,
  `pilha` text NOT NULL,
  PRIMARY KEY  (`cod_erro`),
  KEY `data_erro` (`data_erro`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

/*Table structure for table `tb_historicos` */

drop table if exists `tb_historicos`;

CREATE TABLE `tb_historicos` (
  `cod_historico` int(11) NOT NULL auto_increment,
  `cod_usuario` int(11) NOT NULL,
  `cod_tipo_historico` int(11) NOT NULL,
  `codigo` varchar(30) NOT NULL,
  `valor` int(11) NOT NULL default '0',
  `data_historico` datetime NOT NULL,
  `historico` longtext NOT NULL,
  PRIMARY KEY  (`cod_historico`),
  KEY `codigo` (`codigo`),
  KEY `cod_tipo_historico` (`cod_tipo_historico`),
  KEY `valor` (`valor`),
  KEY `data_historico` (`data_historico`),
  KEY `cod_usuario` (`cod_usuario`),
  CONSTRAINT `tb_historicos_ibfk_1` FOREIGN KEY (`cod_usuario`) REFERENCES `tb_usuarios` (`cod_usuario`) ON UPDATE CASCADE,
  CONSTRAINT `tb_historicos_ibfk_2` FOREIGN KEY (`cod_tipo_historico`) REFERENCES `tb_tipos_historico` (`cod_tipo_historico`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `tb_tipos_historico` */

drop table if exists `tb_tipos_historico`;

CREATE TABLE `tb_tipos_historico` (
  `cod_tipo_historico` int(11) NOT NULL auto_increment,
  `tipo_historico` varchar(20) NOT NULL,
  `icone` int(1) NOT NULL,
  PRIMARY KEY  (`cod_tipo_historico`),
  UNIQUE KEY `tipo_historico` (`tipo_historico`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

/*Table structure for table `tb_urls` */

drop table if exists `tb_urls`;

CREATE TABLE `tb_urls` (
  `id` int(11) NOT NULL auto_increment,
  `user` int(11) NOT NULL,
  `url` varchar(255) NOT NULL,
  `shorturl` varchar(255) NOT NULL,
  `hits` int(11) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

/*Table structure for table `tb_users` */

drop table if exists `tb_users`;

CREATE TABLE `tb_users` (
  `id` int(11) NOT NULL auto_increment,
  `user` varchar(30) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
