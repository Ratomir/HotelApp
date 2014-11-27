-- phpMyAdmin SQL Dump
-- version 4.0.4
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: May 28, 2014 at 04:51 AM
-- Server version: 5.6.12-log
-- PHP Version: 5.4.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `hotel`
--
CREATE DATABASE IF NOT EXISTS `hotel` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `hotel`;

-- --------------------------------------------------------

--
-- Table structure for table `gost`
--

CREATE TABLE IF NOT EXISTS `gost` (
  `id` int(10) unsigned NOT NULL,
  `brojSobe` int(11) NOT NULL,
  `imeiprezime` varchar(40) CHARACTER SET utf8 COLLATE utf8_croatian_ci NOT NULL,
  `placeno` tinyint(1) NOT NULL,
  `tipPlacanja` varchar(15) CHARACTER SET utf8 COLLATE utf8_croatian_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `gost`
--

INSERT INTO `gost` (`id`, `brojSobe`, `imeiprezime`, `placeno`, `tipPlacanja`) VALUES
(1, 105, 'Ratomir Vukadin', 0, 'Kartica'),
(2, 214, 'Mirko Popović', 0, 'Gotovina'),
(3, 305, 'Zorana Mitrović', 1, 'Ostalo'),
(4, 243, 'Vanja Mitrović', 1, 'Kartica'),
(5, 110, 'Milutin Milanković', 1, 'Ostalo'),
(6, 320, 'Ratomir Vukadin', 1, 'Vukadin');

-- --------------------------------------------------------

--
-- Table structure for table `soba`
--

CREATE TABLE IF NOT EXISTS `soba` (
  `brojSobe` int(10) unsigned NOT NULL,
  `stanje` varchar(10) CHARACTER SET utf8 COLLATE utf8_croatian_ci NOT NULL,
  `tipSobe` varchar(20) CHARACTER SET utf8 COLLATE utf8_croatian_ci NOT NULL,
  `datumPrijave` varchar(14) CHARACTER SET utf8 COLLATE utf8_croatian_ci NOT NULL,
  `datumOdjave` varchar(14) CHARACTER SET utf8 COLLATE utf8_croatian_ci NOT NULL,
  PRIMARY KEY (`brojSobe`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `soba`
--

INSERT INTO `soba` (`brojSobe`, `stanje`, `tipSobe`, `datumPrijave`, `datumOdjave`) VALUES
(101, 'Slobodna', 'Jednokrevetna', '', ''),
(102, 'Slobodna', 'Jednokrevetna', '', ''),
(103, 'Slobodna', 'Jednokrevetna', '', ''),
(105, 'Zauzeta', 'Jednokrevetna', '17.5.2014', '20.5.2014'),
(110, 'Zauzeta', 'Jednokrevetna', '15.5.2014', 'Neodređeno'),
(205, 'Slobodna', 'Dvokrevetna', '', ''),
(208, 'Slobodna', 'Dvokrevetna', '', ''),
(214, 'Zauzeta', 'Dvokrevetna', '16.5.2014', '21.5.2014'),
(243, 'Zauzeta', 'Dvokrevetna', '14.5.2014', 'Neodređeno'),
(305, 'Zauzeta', 'Apartman', '16.5.2014', '18.5.2014'),
(310, 'Slobodna', 'Apartman', '', ''),
(311, 'Slobodna', 'Apartman', '', ''),
(320, 'Zauzeta', 'Apartman', '21.5.2014', '24.5.2014'),
(381, 'Slobodna', 'Apartman', '', '');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
