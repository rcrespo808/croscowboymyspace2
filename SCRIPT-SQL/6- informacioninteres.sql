-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 07-02-2023 a las 05:59:25
-- Versión del servidor: 10.4.27-MariaDB
-- Versión de PHP: 8.1.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `posdb`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `informacioninteres`
--

CREATE TABLE `informacioninteres` (
  `id` char(36) NOT NULL,
  `tipo` enum('normativos','representacion','informativo') DEFAULT 'normativos',
  `logo` longtext DEFAULT 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOAAAADgCAMAAAAt85rTAAAAYFBMVEXy8vJ2dnb19fX5+flxcXFzc3Pq6urMzMyurq5ubm5+fn6FhYVra2vi4uLn5+fu7u6+vr6VlZW3t7fFxcWQkJDT09Oampre3t6Tk5N6enqhoaHY2Nirq6uKioqenp67u7tegggxAAAE4klEQVR4nO3c63qrKBgFYP0QDxCPeIjH3P9dDmhMTJM9iXtmWtmz3j9NjenDKghioY4DAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA3yD1d3Hopwu8TxqM0WkHEcZ2Jbxw7u4S5clPl3kHakbuetEO+tdxYT9d7M+Rcl1RBJ9TJee1b08jpSziY0KfYyHnNrVRyjw+rr0GfVAxbPCsCyjj+RWLq/79EGBtQIo7KUQZvOs/rA3YS4+7nJ+GN3Voa8D0fB0N2+DvE1oakProOozz7umUh+9sDVh4a8Cn0j/emlkakIX3gPHDCWke5duElgak6t5E0+37Sa3v5M6bhJYGdFK5djJqU12UTOawd05uB20N6ARiThhtK5DS6TqBqG91aG1AJ9ADoeeF2/coX2dSvF7r0N6AxBoVOJsbGfLztedx79ehvQFNjW3HA/K77Ux4TWhzwC/Cx4m8t7RS2wNuesuvjyqW65BCqwOSytMl4xB9Deh6ZsS3vAYzl0+6nigNXz2JiurU8oCZHgx5l+h87Yt8Lpe+1QFJzYM9n5zX+WwPSOr6hJTLl/HWgPY9dLoGVLdh/VdPgk1AsrYGM/H2CbfVTfSDfDYHpOCX7fIPCRh67/Mh4BH9DwJG+wJaOkxQ0YoPlL55/mZjQD2f/4id88Hx9YT3NftqUF+DzWfVNyN9L3q2KaB5XCjkDq2eUNnzF2zH8Sfdf3Ju/nT2GZeLyqaA1NTc20VmNuUzj+dVuEfW/HSJd6N0D9tWcgEAAAAAAAAAAADAn4ws2tr5W9Tk/3QR/lNp6Fq2w3qvpE/fn7Q47i+CEbH4drGZxQf3sprDZE4wx2hZdbAcT1NzeP0Eo2TZhff48UNgsqhKIbu5LVJalDK/7fokJROqhniQ5cWhfpJlMa+0J1UKUcdNHevP99M4TkFdm+NJWMruzYbD78b4KLssFLLRBeylDLOu7a5dC2VezIK2rbOwnVQ7VBc56tPisxsGqmsns5v54uZF0QlPx6LKLYtL154P1TMxTwS6FTZjzZwkPyeMWCAuy3ukeELBadDHKlcq3SgbGTpOKCrTPhUvY1Ii042cNXJwHF90vn7di+JIdciWLZ40tIyU28xFK+6LmnUNnnrzchzn/QWhdGJ5WQLkOqAJZn6KEg27iJjNO+vFkYZP5g2mOKw4MVbw+RAFfAm61KBnvmHncjmNO010XVWR6SZ6KpYwaVuxuuxnBT9gQNIB6RYweg5Ylstp3OmvAUnpgN7SHKnRAXN3XcN2sIBzx2gC6hY5FzcTtyb6HDCNxXXdiOlkynpuuTSMMevy+xK242B8DmiaKCVl7etrqJLhMr4/N1ETMB2kudZY4OprMJBhzyi+6J6KGi9z9HFfHep/d2yuQd3PizwLClnfdoZ48XMNUlOOWVUVojar9pQUUz2OlXkzFF2minw82DBxuwb1l7hrXXFZ78/0OJiYC9J5CKjv4Lq2baWqpGnRaTAMeVvPn6ly4Y7hnsWK3+B6xWzvu27vLbdqz6cxqhLTSsn8wxL9hcVjff/4kRrob1tDVEt/q28OrFqR9zEql13a+obuDw2oeGHaZTyWH0+srGJ2FoquK9tj9Zz/JmrCrhvUgSe8/xQdcJILAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABgrb8AVdFTjJLjUDkAAAAASUVORK5CYII=',
  `documento` longtext DEFAULT NULL,
  `titulo` varchar(255) NOT NULL,
  `descripcion` text DEFAULT NULL,
  `contenido` text DEFAULT NULL,
  `fecha` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `informacioninteres`
--

INSERT INTO `informacioninteres` (`id`, `tipo`, `logo`, `documento`, `titulo`, `descripcion`, `contenido`, `fecha`) VALUES
('02ba926b-a642-11ed-aae1-489ebde58e88', 'normativos', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOAAAADgCAMAAAAt85rTAAAAYFBMVEXy8vJ2dnb19fX5+flxcXFzc3Pq6urMzMyurq5ubm5+fn6FhYVra2vi4uLn5+fu7u6+vr6VlZW3t7fFxcWQkJDT09Oampre3t6Tk5N6enqhoaHY2Nirq6uKioqenp67u7tegggxAAAE4klEQVR4nO3c63qrKBgFYP0QDxCPeIjH3P9dDmhMTJM9iXtmWtmz3j9NjenDKghioY4DAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA3yD1d3Hopwu8TxqM0WkHEcZ2Jbxw7u4S5clPl3kHakbuetEO+tdxYT9d7M+Rcl1RBJ9TJee1b08jpSziY0KfYyHnNrVRyjw+rr0GfVAxbPCsCyjj+RWLq/79EGBtQIo7KUQZvOs/rA3YS4+7nJ+GN3Voa8D0fB0N2+DvE1oakProOozz7umUh+9sDVh4a8Cn0j/emlkakIX3gPHDCWke5duElgak6t5E0+37Sa3v5M6bhJYGdFK5djJqU12UTOawd05uB20N6ARiThhtK5DS6TqBqG91aG1AJ9ADoeeF2/coX2dSvF7r0N6AxBoVOJsbGfLztedx79ehvQFNjW3HA/K77Ux4TWhzwC/Cx4m8t7RS2wNuesuvjyqW65BCqwOSytMl4xB9Deh6ZsS3vAYzl0+6nigNXz2JiurU8oCZHgx5l+h87Yt8Lpe+1QFJzYM9n5zX+WwPSOr6hJTLl/HWgPY9dLoGVLdh/VdPgk1AsrYGM/H2CbfVTfSDfDYHpOCX7fIPCRh67/Mh4BH9DwJG+wJaOkxQ0YoPlL55/mZjQD2f/4id88Hx9YT3NftqUF+DzWfVNyN9L3q2KaB5XCjkDq2eUNnzF2zH8Sfdf3Ju/nT2GZeLyqaA1NTc20VmNuUzj+dVuEfW/HSJd6N0D9tWcgEAAAAAAAAAAADAn4ws2tr5W9Tk/3QR/lNp6Fq2w3qvpE/fn7Q47i+CEbH4drGZxQf3sprDZE4wx2hZdbAcT1NzeP0Eo2TZhff48UNgsqhKIbu5LVJalDK/7fokJROqhniQ5cWhfpJlMa+0J1UKUcdNHevP99M4TkFdm+NJWMruzYbD78b4KLssFLLRBeylDLOu7a5dC2VezIK2rbOwnVQ7VBc56tPisxsGqmsns5v54uZF0QlPx6LKLYtL154P1TMxTwS6FTZjzZwkPyeMWCAuy3ukeELBadDHKlcq3SgbGTpOKCrTPhUvY1Ii042cNXJwHF90vn7di+JIdciWLZ40tIyU28xFK+6LmnUNnnrzchzn/QWhdGJ5WQLkOqAJZn6KEg27iJjNO+vFkYZP5g2mOKw4MVbw+RAFfAm61KBnvmHncjmNO010XVWR6SZ6KpYwaVuxuuxnBT9gQNIB6RYweg5Ylstp3OmvAUnpgN7SHKnRAXN3XcN2sIBzx2gC6hY5FzcTtyb6HDCNxXXdiOlkynpuuTSMMevy+xK242B8DmiaKCVl7etrqJLhMr4/N1ETMB2kudZY4OprMJBhzyi+6J6KGi9z9HFfHep/d2yuQd3PizwLClnfdoZ48XMNUlOOWVUVojar9pQUUz2OlXkzFF2minw82DBxuwb1l7hrXXFZ78/0OJiYC9J5CKjv4Lq2baWqpGnRaTAMeVvPn6ly4Y7hnsWK3+B6xWzvu27vLbdqz6cxqhLTSsn8wxL9hcVjff/4kRrob1tDVEt/q28OrFqR9zEql13a+obuDw2oeGHaZTyWH0+srGJ2FoquK9tj9Zz/JmrCrhvUgSe8/xQdcJILAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABgrb8AVdFTjJLjUDkAAAAASUVORK5CYII=', 'documento', 'titulo', 'descripcion', 'contenido', '2023-02-06 17:16:36'),
('08db08c3-f5ca-4608-82e3-9258e8886ffc', 'normativos', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOAAAADgCAMAAAAt85rTAAAAYFBMVEXy8vJ2dnb19fX5+flxcXFzc3Pq6urMzMyurq5ubm5+fn6FhYVra2vi4uLn5+fu7u6+vr6VlZW3t7fFxcWQkJDT09Oampre3t6Tk5N6enqhoaHY2Nirq6uKioqenp67u7tegggxAAAE4klEQVR4nO3c63qrKBgFYP0QDxCPeIjH3P9dDmhMTJM9iXtmWtmz3j9NjenDKghioY4DAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA3yD1d3Hopwu8TxqM0WkHEcZ2Jbxw7u4S5clPl3kHakbuetEO+tdxYT9d7M+Rcl1RBJ9TJee1b08jpSziY0KfYyHnNrVRyjw+rr0GfVAxbPCsCyjj+RWLq/79EGBtQIo7KUQZvOs/rA3YS4+7nJ+GN3Voa8D0fB0N2+DvE1oakProOozz7umUh+9sDVh4a8Cn0j/emlkakIX3gPHDCWke5duElgak6t5E0+37Sa3v5M6bhJYGdFK5djJqU12UTOawd05uB20N6ARiThhtK5DS6TqBqG91aG1AJ9ADoeeF2/coX2dSvF7r0N6AxBoVOJsbGfLztedx79ehvQFNjW3HA/K77Ux4TWhzwC/Cx4m8t7RS2wNuesuvjyqW65BCqwOSytMl4xB9Deh6ZsS3vAYzl0+6nigNXz2JiurU8oCZHgx5l+h87Yt8Lpe+1QFJzYM9n5zX+WwPSOr6hJTLl/HWgPY9dLoGVLdh/VdPgk1AsrYGM/H2CbfVTfSDfDYHpOCX7fIPCRh67/Mh4BH9DwJG+wJaOkxQ0YoPlL55/mZjQD2f/4id88Hx9YT3NftqUF+DzWfVNyN9L3q2KaB5XCjkDq2eUNnzF2zH8Sfdf3Ju/nT2GZeLyqaA1NTc20VmNuUzj+dVuEfW/HSJd6N0D9tWcgEAAAAAAAAAAADAn4ws2tr5W9Tk/3QR/lNp6Fq2w3qvpE/fn7Q47i+CEbH4drGZxQf3sprDZE4wx2hZdbAcT1NzeP0Eo2TZhff48UNgsqhKIbu5LVJalDK/7fokJROqhniQ5cWhfpJlMa+0J1UKUcdNHevP99M4TkFdm+NJWMruzYbD78b4KLssFLLRBeylDLOu7a5dC2VezIK2rbOwnVQ7VBc56tPisxsGqmsns5v54uZF0QlPx6LKLYtL154P1TMxTwS6FTZjzZwkPyeMWCAuy3ukeELBadDHKlcq3SgbGTpOKCrTPhUvY1Ii042cNXJwHF90vn7di+JIdciWLZ40tIyU28xFK+6LmnUNnnrzchzn/QWhdGJ5WQLkOqAJZn6KEg27iJjNO+vFkYZP5g2mOKw4MVbw+RAFfAm61KBnvmHncjmNO010XVWR6SZ6KpYwaVuxuuxnBT9gQNIB6RYweg5Ylstp3OmvAUnpgN7SHKnRAXN3XcN2sIBzx2gC6hY5FzcTtyb6HDCNxXXdiOlkynpuuTSMMevy+xK242B8DmiaKCVl7etrqJLhMr4/N1ETMB2kudZY4OprMJBhzyi+6J6KGi9z9HFfHep/d2yuQd3PizwLClnfdoZ48XMNUlOOWVUVojar9pQUUz2OlXkzFF2minw82DBxuwb1l7hrXXFZ78/0OJiYC9J5CKjv4Lq2baWqpGnRaTAMeVvPn6ly4Y7hnsWK3+B6xWzvu27vLbdqz6cxqhLTSsn8wxL9hcVjff/4kRrob1tDVEt/q28OrFqR9zEql13a+obuDw2oeGHaZTyWH0+srGJ2FoquK9tj9Zz/JmrCrhvUgSe8/xQdcJILAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABgrb8AVdFTjJLjUDkAAAAASUVORK5CYII=', 'documento', 'titulo', 'descripcion', 'contenido', '2023-02-06 17:16:36'),
('08db08c4-05f6-41dc-8035-93e508a57c32', 'normativos', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOAAAADgCAMAAAAt85rTAAAAYFBMVEXy8vJ2dnb19fX5+flxcXFzc3Pq6urMzMyurq5ubm5+fn6FhYVra2vi4uLn5+fu7u6+vr6VlZW3t7fFxcWQkJDT09Oampre3t6Tk5N6enqhoaHY2Nirq6uKioqenp67u7tegggxAAAE4klEQVR4nO3c63qrKBgFYP0QDxCPeIjH3P9dDmhMTJM9iXtmWtmz3j9NjenDKghioY4DAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA3yD1d3Hopwu8TxqM0WkHEcZ2Jbxw7u4S5clPl3kHakbuetEO+tdxYT9d7M+Rcl1RBJ9TJee1b08jpSziY0KfYyHnNrVRyjw+rr0GfVAxbPCsCyjj+RWLq/79EGBtQIo7KUQZvOs/rA3YS4+7nJ+GN3Voa8D0fB0N2+DvE1oakProOozz7umUh+9sDVh4a8Cn0j/emlkakIX3gPHDCWke5duElgak6t5E0+37Sa3v5M6bhJYGdFK5djJqU12UTOawd05uB20N6ARiThhtK5DS6TqBqG91aG1AJ9ADoeeF2/coX2dSvF7r0N6AxBoVOJsbGfLztedx79ehvQFNjW3HA/K77Ux4TWhzwC/Cx4m8t7RS2wNuesuvjyqW65BCqwOSytMl4xB9Deh6ZsS3vAYzl0+6nigNXz2JiurU8oCZHgx5l+h87Yt8Lpe+1QFJzYM9n5zX+WwPSOr6hJTLl/HWgPY9dLoGVLdh/VdPgk1AsrYGM/H2CbfVTfSDfDYHpOCX7fIPCRh67/Mh4BH9DwJG+wJaOkxQ0YoPlL55/mZjQD2f/4id88Hx9YT3NftqUF+DzWfVNyN9L3q2KaB5XCjkDq2eUNnzF2zH8Sfdf3Ju/nT2GZeLyqaA1NTc20VmNuUzj+dVuEfW/HSJd6N0D9tWcgEAAAAAAAAAAADAn4ws2tr5W9Tk/3QR/lNp6Fq2w3qvpE/fn7Q47i+CEbH4drGZxQf3sprDZE4wx2hZdbAcT1NzeP0Eo2TZhff48UNgsqhKIbu5LVJalDK/7fokJROqhniQ5cWhfpJlMa+0J1UKUcdNHevP99M4TkFdm+NJWMruzYbD78b4KLssFLLRBeylDLOu7a5dC2VezIK2rbOwnVQ7VBc56tPisxsGqmsns5v54uZF0QlPx6LKLYtL154P1TMxTwS6FTZjzZwkPyeMWCAuy3ukeELBadDHKlcq3SgbGTpOKCrTPhUvY1Ii042cNXJwHF90vn7di+JIdciWLZ40tIyU28xFK+6LmnUNnnrzchzn/QWhdGJ5WQLkOqAJZn6KEg27iJjNO+vFkYZP5g2mOKw4MVbw+RAFfAm61KBnvmHncjmNO010XVWR6SZ6KpYwaVuxuuxnBT9gQNIB6RYweg5Ylstp3OmvAUnpgN7SHKnRAXN3XcN2sIBzx2gC6hY5FzcTtyb6HDCNxXXdiOlkynpuuTSMMevy+xK242B8DmiaKCVl7etrqJLhMr4/N1ETMB2kudZY4OprMJBhzyi+6J6KGi9z9HFfHep/d2yuQd3PizwLClnfdoZ48XMNUlOOWVUVojar9pQUUz2OlXkzFF2minw82DBxuwb1l7hrXXFZ78/0OJiYC9J5CKjv4Lq2baWqpGnRaTAMeVvPn6ly4Y7hnsWK3+B6xWzvu27vLbdqz6cxqhLTSsn8wxL9hcVjff/4kRrob1tDVEt/q28OrFqR9zEql13a+obuDw2oeGHaZTyWH0+srGJ2FoquK9tj9Zz/JmrCrhvUgSe8/xQdcJILAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABgrb8AVdFTjJLjUDkAAAAASUVORK5CYII=', 'documento', 'titulo 1', 'descripcion', 'contenido', '2023-02-06 17:16:36'),
('08db08c4-09d2-47d4-8e91-64d62320ba10', 'normativos', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOAAAADgCAMAAAAt85rTAAAAYFBMVEXy8vJ2dnb19fX5+flxcXFzc3Pq6urMzMyurq5ubm5+fn6FhYVra2vi4uLn5+fu7u6+vr6VlZW3t7fFxcWQkJDT09Oampre3t6Tk5N6enqhoaHY2Nirq6uKioqenp67u7tegggxAAAE4klEQVR4nO3c63qrKBgFYP0QDxCPeIjH3P9dDmhMTJM9iXtmWtmz3j9NjenDKghioY4DAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA3yD1d3Hopwu8TxqM0WkHEcZ2Jbxw7u4S5clPl3kHakbuetEO+tdxYT9d7M+Rcl1RBJ9TJee1b08jpSziY0KfYyHnNrVRyjw+rr0GfVAxbPCsCyjj+RWLq/79EGBtQIo7KUQZvOs/rA3YS4+7nJ+GN3Voa8D0fB0N2+DvE1oakProOozz7umUh+9sDVh4a8Cn0j/emlkakIX3gPHDCWke5duElgak6t5E0+37Sa3v5M6bhJYGdFK5djJqU12UTOawd05uB20N6ARiThhtK5DS6TqBqG91aG1AJ9ADoeeF2/coX2dSvF7r0N6AxBoVOJsbGfLztedx79ehvQFNjW3HA/K77Ux4TWhzwC/Cx4m8t7RS2wNuesuvjyqW65BCqwOSytMl4xB9Deh6ZsS3vAYzl0+6nigNXz2JiurU8oCZHgx5l+h87Yt8Lpe+1QFJzYM9n5zX+WwPSOr6hJTLl/HWgPY9dLoGVLdh/VdPgk1AsrYGM/H2CbfVTfSDfDYHpOCX7fIPCRh67/Mh4BH9DwJG+wJaOkxQ0YoPlL55/mZjQD2f/4id88Hx9YT3NftqUF+DzWfVNyN9L3q2KaB5XCjkDq2eUNnzF2zH8Sfdf3Ju/nT2GZeLyqaA1NTc20VmNuUzj+dVuEfW/HSJd6N0D9tWcgEAAAAAAAAAAADAn4ws2tr5W9Tk/3QR/lNp6Fq2w3qvpE/fn7Q47i+CEbH4drGZxQf3sprDZE4wx2hZdbAcT1NzeP0Eo2TZhff48UNgsqhKIbu5LVJalDK/7fokJROqhniQ5cWhfpJlMa+0J1UKUcdNHevP99M4TkFdm+NJWMruzYbD78b4KLssFLLRBeylDLOu7a5dC2VezIK2rbOwnVQ7VBc56tPisxsGqmsns5v54uZF0QlPx6LKLYtL154P1TMxTwS6FTZjzZwkPyeMWCAuy3ukeELBadDHKlcq3SgbGTpOKCrTPhUvY1Ii042cNXJwHF90vn7di+JIdciWLZ40tIyU28xFK+6LmnUNnnrzchzn/QWhdGJ5WQLkOqAJZn6KEg27iJjNO+vFkYZP5g2mOKw4MVbw+RAFfAm61KBnvmHncjmNO010XVWR6SZ6KpYwaVuxuuxnBT9gQNIB6RYweg5Ylstp3OmvAUnpgN7SHKnRAXN3XcN2sIBzx2gC6hY5FzcTtyb6HDCNxXXdiOlkynpuuTSMMevy+xK242B8DmiaKCVl7etrqJLhMr4/N1ETMB2kudZY4OprMJBhzyi+6J6KGi9z9HFfHep/d2yuQd3PizwLClnfdoZ48XMNUlOOWVUVojar9pQUUz2OlXkzFF2minw82DBxuwb1l7hrXXFZ78/0OJiYC9J5CKjv4Lq2baWqpGnRaTAMeVvPn6ly4Y7hnsWK3+B6xWzvu27vLbdqz6cxqhLTSsn8wxL9hcVjff/4kRrob1tDVEt/q28OrFqR9zEql13a+obuDw2oeGHaZTyWH0+srGJ2FoquK9tj9Zz/JmrCrhvUgSe8/xQdcJILAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABgrb8AVdFTjJLjUDkAAAAASUVORK5CYII=', 'documento', 'titulo 2', 'descripcion', 'contenido', '2023-02-06 17:16:36'),
('08db08c4-0cb7-44b8-8e15-f9fb994d6e32', 'normativos', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOAAAADgCAMAAAAt85rTAAAAYFBMVEXy8vJ2dnb19fX5+flxcXFzc3Pq6urMzMyurq5ubm5+fn6FhYVra2vi4uLn5+fu7u6+vr6VlZW3t7fFxcWQkJDT09Oampre3t6Tk5N6enqhoaHY2Nirq6uKioqenp67u7tegggxAAAE4klEQVR4nO3c63qrKBgFYP0QDxCPeIjH3P9dDmhMTJM9iXtmWtmz3j9NjenDKghioY4DAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA3yD1d3Hopwu8TxqM0WkHEcZ2Jbxw7u4S5clPl3kHakbuetEO+tdxYT9d7M+Rcl1RBJ9TJee1b08jpSziY0KfYyHnNrVRyjw+rr0GfVAxbPCsCyjj+RWLq/79EGBtQIo7KUQZvOs/rA3YS4+7nJ+GN3Voa8D0fB0N2+DvE1oakProOozz7umUh+9sDVh4a8Cn0j/emlkakIX3gPHDCWke5duElgak6t5E0+37Sa3v5M6bhJYGdFK5djJqU12UTOawd05uB20N6ARiThhtK5DS6TqBqG91aG1AJ9ADoeeF2/coX2dSvF7r0N6AxBoVOJsbGfLztedx79ehvQFNjW3HA/K77Ux4TWhzwC/Cx4m8t7RS2wNuesuvjyqW65BCqwOSytMl4xB9Deh6ZsS3vAYzl0+6nigNXz2JiurU8oCZHgx5l+h87Yt8Lpe+1QFJzYM9n5zX+WwPSOr6hJTLl/HWgPY9dLoGVLdh/VdPgk1AsrYGM/H2CbfVTfSDfDYHpOCX7fIPCRh67/Mh4BH9DwJG+wJaOkxQ0YoPlL55/mZjQD2f/4id88Hx9YT3NftqUF+DzWfVNyN9L3q2KaB5XCjkDq2eUNnzF2zH8Sfdf3Ju/nT2GZeLyqaA1NTc20VmNuUzj+dVuEfW/HSJd6N0D9tWcgEAAAAAAAAAAADAn4ws2tr5W9Tk/3QR/lNp6Fq2w3qvpE/fn7Q47i+CEbH4drGZxQf3sprDZE4wx2hZdbAcT1NzeP0Eo2TZhff48UNgsqhKIbu5LVJalDK/7fokJROqhniQ5cWhfpJlMa+0J1UKUcdNHevP99M4TkFdm+NJWMruzYbD78b4KLssFLLRBeylDLOu7a5dC2VezIK2rbOwnVQ7VBc56tPisxsGqmsns5v54uZF0QlPx6LKLYtL154P1TMxTwS6FTZjzZwkPyeMWCAuy3ukeELBadDHKlcq3SgbGTpOKCrTPhUvY1Ii042cNXJwHF90vn7di+JIdciWLZ40tIyU28xFK+6LmnUNnnrzchzn/QWhdGJ5WQLkOqAJZn6KEg27iJjNO+vFkYZP5g2mOKw4MVbw+RAFfAm61KBnvmHncjmNO010XVWR6SZ6KpYwaVuxuuxnBT9gQNIB6RYweg5Ylstp3OmvAUnpgN7SHKnRAXN3XcN2sIBzx2gC6hY5FzcTtyb6HDCNxXXdiOlkynpuuTSMMevy+xK242B8DmiaKCVl7etrqJLhMr4/N1ETMB2kudZY4OprMJBhzyi+6J6KGi9z9HFfHep/d2yuQd3PizwLClnfdoZ48XMNUlOOWVUVojar9pQUUz2OlXkzFF2minw82DBxuwb1l7hrXXFZ78/0OJiYC9J5CKjv4Lq2baWqpGnRaTAMeVvPn6ly4Y7hnsWK3+B6xWzvu27vLbdqz6cxqhLTSsn8wxL9hcVjff/4kRrob1tDVEt/q28OrFqR9zEql13a+obuDw2oeGHaZTyWH0+srGJ2FoquK9tj9Zz/JmrCrhvUgSe8/xQdcJILAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABgrb8AVdFTjJLjUDkAAAAASUVORK5CYII=', 'documento', 'titulo 3', 'descripcion', 'contenido', '2023-02-06 17:16:36'),
('08db08c4-0f74-4af8-8fe8-41f83aa560e2', 'normativos', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOAAAADgCAMAAAAt85rTAAAAYFBMVEXy8vJ2dnb19fX5+flxcXFzc3Pq6urMzMyurq5ubm5+fn6FhYVra2vi4uLn5+fu7u6+vr6VlZW3t7fFxcWQkJDT09Oampre3t6Tk5N6enqhoaHY2Nirq6uKioqenp67u7tegggxAAAE4klEQVR4nO3c63qrKBgFYP0QDxCPeIjH3P9dDmhMTJM9iXtmWtmz3j9NjenDKghioY4DAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA3yD1d3Hopwu8TxqM0WkHEcZ2Jbxw7u4S5clPl3kHakbuetEO+tdxYT9d7M+Rcl1RBJ9TJee1b08jpSziY0KfYyHnNrVRyjw+rr0GfVAxbPCsCyjj+RWLq/79EGBtQIo7KUQZvOs/rA3YS4+7nJ+GN3Voa8D0fB0N2+DvE1oakProOozz7umUh+9sDVh4a8Cn0j/emlkakIX3gPHDCWke5duElgak6t5E0+37Sa3v5M6bhJYGdFK5djJqU12UTOawd05uB20N6ARiThhtK5DS6TqBqG91aG1AJ9ADoeeF2/coX2dSvF7r0N6AxBoVOJsbGfLztedx79ehvQFNjW3HA/K77Ux4TWhzwC/Cx4m8t7RS2wNuesuvjyqW65BCqwOSytMl4xB9Deh6ZsS3vAYzl0+6nigNXz2JiurU8oCZHgx5l+h87Yt8Lpe+1QFJzYM9n5zX+WwPSOr6hJTLl/HWgPY9dLoGVLdh/VdPgk1AsrYGM/H2CbfVTfSDfDYHpOCX7fIPCRh67/Mh4BH9DwJG+wJaOkxQ0YoPlL55/mZjQD2f/4id88Hx9YT3NftqUF+DzWfVNyN9L3q2KaB5XCjkDq2eUNnzF2zH8Sfdf3Ju/nT2GZeLyqaA1NTc20VmNuUzj+dVuEfW/HSJd6N0D9tWcgEAAAAAAAAAAADAn4ws2tr5W9Tk/3QR/lNp6Fq2w3qvpE/fn7Q47i+CEbH4drGZxQf3sprDZE4wx2hZdbAcT1NzeP0Eo2TZhff48UNgsqhKIbu5LVJalDK/7fokJROqhniQ5cWhfpJlMa+0J1UKUcdNHevP99M4TkFdm+NJWMruzYbD78b4KLssFLLRBeylDLOu7a5dC2VezIK2rbOwnVQ7VBc56tPisxsGqmsns5v54uZF0QlPx6LKLYtL154P1TMxTwS6FTZjzZwkPyeMWCAuy3ukeELBadDHKlcq3SgbGTpOKCrTPhUvY1Ii042cNXJwHF90vn7di+JIdciWLZ40tIyU28xFK+6LmnUNnnrzchzn/QWhdGJ5WQLkOqAJZn6KEg27iJjNO+vFkYZP5g2mOKw4MVbw+RAFfAm61KBnvmHncjmNO010XVWR6SZ6KpYwaVuxuuxnBT9gQNIB6RYweg5Ylstp3OmvAUnpgN7SHKnRAXN3XcN2sIBzx2gC6hY5FzcTtyb6HDCNxXXdiOlkynpuuTSMMevy+xK242B8DmiaKCVl7etrqJLhMr4/N1ETMB2kudZY4OprMJBhzyi+6J6KGi9z9HFfHep/d2yuQd3PizwLClnfdoZ48XMNUlOOWVUVojar9pQUUz2OlXkzFF2minw82DBxuwb1l7hrXXFZ78/0OJiYC9J5CKjv4Lq2baWqpGnRaTAMeVvPn6ly4Y7hnsWK3+B6xWzvu27vLbdqz6cxqhLTSsn8wxL9hcVjff/4kRrob1tDVEt/q28OrFqR9zEql13a+obuDw2oeGHaZTyWH0+srGJ2FoquK9tj9Zz/JmrCrhvUgSe8/xQdcJILAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABgrb8AVdFTjJLjUDkAAAAASUVORK5CYII=', 'documento', 'titulo 4', 'descripcion', 'contenido', '2023-02-06 17:16:36'),
('08db08c4-1203-45b4-8480-7d2e681de46c', 'normativos', '', 'documento', 'titulo 7 xxxx', 'descripcion', 'contenido', '2023-02-06 17:16:36'),
('08db08c4-8215-4467-8f47-badd3ef1a7bf', 'normativos', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOAAAADgCAMAAAAt85rTAAAAYFBMVEXy8vJ2dnb19fX5+flxcXFzc3Pq6urMzMyurq5ubm5+fn6FhYVra2vi4uLn5+fu7u6+vr6VlZW3t7fFxcWQkJDT09Oampre3t6Tk5N6enqhoaHY2Nirq6uKioqenp67u7tegggxAAAE4klEQVR4nO3c63qrKBgFYP0QDxCPeIjH3P9dDmhMTJM9iXtmWtmz3j9NjenDKghioY4DAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA3yD1d3Hopwu8TxqM0WkHEcZ2Jbxw7u4S5clPl3kHakbuetEO+tdxYT9d7M+Rcl1RBJ9TJee1b08jpSziY0KfYyHnNrVRyjw+rr0GfVAxbPCsCyjj+RWLq/79EGBtQIo7KUQZvOs/rA3YS4+7nJ+GN3Voa8D0fB0N2+DvE1oakProOozz7umUh+9sDVh4a8Cn0j/emlkakIX3gPHDCWke5duElgak6t5E0+37Sa3v5M6bhJYGdFK5djJqU12UTOawd05uB20N6ARiThhtK5DS6TqBqG91aG1AJ9ADoeeF2/coX2dSvF7r0N6AxBoVOJsbGfLztedx79ehvQFNjW3HA/K77Ux4TWhzwC/Cx4m8t7RS2wNuesuvjyqW65BCqwOSytMl4xB9Deh6ZsS3vAYzl0+6nigNXz2JiurU8oCZHgx5l+h87Yt8Lpe+1QFJzYM9n5zX+WwPSOr6hJTLl/HWgPY9dLoGVLdh/VdPgk1AsrYGM/H2CbfVTfSDfDYHpOCX7fIPCRh67/Mh4BH9DwJG+wJaOkxQ0YoPlL55/mZjQD2f/4id88Hx9YT3NftqUF+DzWfVNyN9L3q2KaB5XCjkDq2eUNnzF2zH8Sfdf3Ju/nT2GZeLyqaA1NTc20VmNuUzj+dVuEfW/HSJd6N0D9tWcgEAAAAAAAAAAADAn4ws2tr5W9Tk/3QR/lNp6Fq2w3qvpE/fn7Q47i+CEbH4drGZxQf3sprDZE4wx2hZdbAcT1NzeP0Eo2TZhff48UNgsqhKIbu5LVJalDK/7fokJROqhniQ5cWhfpJlMa+0J1UKUcdNHevP99M4TkFdm+NJWMruzYbD78b4KLssFLLRBeylDLOu7a5dC2VezIK2rbOwnVQ7VBc56tPisxsGqmsns5v54uZF0QlPx6LKLYtL154P1TMxTwS6FTZjzZwkPyeMWCAuy3ukeELBadDHKlcq3SgbGTpOKCrTPhUvY1Ii042cNXJwHF90vn7di+JIdciWLZ40tIyU28xFK+6LmnUNnnrzchzn/QWhdGJ5WQLkOqAJZn6KEg27iJjNO+vFkYZP5g2mOKw4MVbw+RAFfAm61KBnvmHncjmNO010XVWR6SZ6KpYwaVuxuuxnBT9gQNIB6RYweg5Ylstp3OmvAUnpgN7SHKnRAXN3XcN2sIBzx2gC6hY5FzcTtyb6HDCNxXXdiOlkynpuuTSMMevy+xK242B8DmiaKCVl7etrqJLhMr4/N1ETMB2kudZY4OprMJBhzyi+6J6KGi9z9HFfHep/d2yuQd3PizwLClnfdoZ48XMNUlOOWVUVojar9pQUUz2OlXkzFF2minw82DBxuwb1l7hrXXFZ78/0OJiYC9J5CKjv4Lq2baWqpGnRaTAMeVvPn6ly4Y7hnsWK3+B6xWzvu27vLbdqz6cxqhLTSsn8wxL9hcVjff/4kRrob1tDVEt/q28OrFqR9zEql13a+obuDw2oeGHaZTyWH0+srGJ2FoquK9tj9Zz/JmrCrhvUgSe8/xQdcJILAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABgrb8AVdFTjJLjUDkAAAAASUVORK5CYII=', 'documento', 'titulo 6', 'descripcion', 'contenido', '2023-02-06 17:16:36'),
('08db08c4-8abf-4e19-848e-60b5e63d8c56', 'normativos', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOAAAADgCAMAAAAt85rTAAAAYFBMVEXy8vJ2dnb19fX5+flxcXFzc3Pq6urMzMyurq5ubm5+fn6FhYVra2vi4uLn5+fu7u6+vr6VlZW3t7fFxcWQkJDT09Oampre3t6Tk5N6enqhoaHY2Nirq6uKioqenp67u7tegggxAAAE4klEQVR4nO3c63qrKBgFYP0QDxCPeIjH3P9dDmhMTJM9iXtmWtmz3j9NjenDKghioY4DAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA3yD1d3Hopwu8TxqM0WkHEcZ2Jbxw7u4S5clPl3kHakbuetEO+tdxYT9d7M+Rcl1RBJ9TJee1b08jpSziY0KfYyHnNrVRyjw+rr0GfVAxbPCsCyjj+RWLq/79EGBtQIo7KUQZvOs/rA3YS4+7nJ+GN3Voa8D0fB0N2+DvE1oakProOozz7umUh+9sDVh4a8Cn0j/emlkakIX3gPHDCWke5duElgak6t5E0+37Sa3v5M6bhJYGdFK5djJqU12UTOawd05uB20N6ARiThhtK5DS6TqBqG91aG1AJ9ADoeeF2/coX2dSvF7r0N6AxBoVOJsbGfLztedx79ehvQFNjW3HA/K77Ux4TWhzwC/Cx4m8t7RS2wNuesuvjyqW65BCqwOSytMl4xB9Deh6ZsS3vAYzl0+6nigNXz2JiurU8oCZHgx5l+h87Yt8Lpe+1QFJzYM9n5zX+WwPSOr6hJTLl/HWgPY9dLoGVLdh/VdPgk1AsrYGM/H2CbfVTfSDfDYHpOCX7fIPCRh67/Mh4BH9DwJG+wJaOkxQ0YoPlL55/mZjQD2f/4id88Hx9YT3NftqUF+DzWfVNyN9L3q2KaB5XCjkDq2eUNnzF2zH8Sfdf3Ju/nT2GZeLyqaA1NTc20VmNuUzj+dVuEfW/HSJd6N0D9tWcgEAAAAAAAAAAADAn4ws2tr5W9Tk/3QR/lNp6Fq2w3qvpE/fn7Q47i+CEbH4drGZxQf3sprDZE4wx2hZdbAcT1NzeP0Eo2TZhff48UNgsqhKIbu5LVJalDK/7fokJROqhniQ5cWhfpJlMa+0J1UKUcdNHevP99M4TkFdm+NJWMruzYbD78b4KLssFLLRBeylDLOu7a5dC2VezIK2rbOwnVQ7VBc56tPisxsGqmsns5v54uZF0QlPx6LKLYtL154P1TMxTwS6FTZjzZwkPyeMWCAuy3ukeELBadDHKlcq3SgbGTpOKCrTPhUvY1Ii042cNXJwHF90vn7di+JIdciWLZ40tIyU28xFK+6LmnUNnnrzchzn/QWhdGJ5WQLkOqAJZn6KEg27iJjNO+vFkYZP5g2mOKw4MVbw+RAFfAm61KBnvmHncjmNO010XVWR6SZ6KpYwaVuxuuxnBT9gQNIB6RYweg5Ylstp3OmvAUnpgN7SHKnRAXN3XcN2sIBzx2gC6hY5FzcTtyb6HDCNxXXdiOlkynpuuTSMMevy+xK242B8DmiaKCVl7etrqJLhMr4/N1ETMB2kudZY4OprMJBhzyi+6J6KGi9z9HFfHep/d2yuQd3PizwLClnfdoZ48XMNUlOOWVUVojar9pQUUz2OlXkzFF2minw82DBxuwb1l7hrXXFZ78/0OJiYC9J5CKjv4Lq2baWqpGnRaTAMeVvPn6ly4Y7hnsWK3+B6xWzvu27vLbdqz6cxqhLTSsn8wxL9hcVjff/4kRrob1tDVEt/q28OrFqR9zEql13a+obuDw2oeGHaZTyWH0+srGJ2FoquK9tj9Zz/JmrCrhvUgSe8/xQdcJILAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABgrb8AVdFTjJLjUDkAAAAASUVORK5CYII=', 'documento', '', 'descripcion', 'contenido', '2023-02-06 17:16:36'),
('08db08c4-9f9a-4b10-8c3f-d3438098b1a1', 'normativos', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOAAAADgCAMAAAAt85rTAAAAYFBMVEXy8vJ2dnb19fX5+flxcXFzc3Pq6urMzMyurq5ubm5+fn6FhYVra2vi4uLn5+fu7u6+vr6VlZW3t7fFxcWQkJDT09Oampre3t6Tk5N6enqhoaHY2Nirq6uKioqenp67u7tegggxAAAE4klEQVR4nO3c63qrKBgFYP0QDxCPeIjH3P9dDmhMTJM9iXtmWtmz3j9NjenDKghioY4DAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA3yD1d3Hopwu8TxqM0WkHEcZ2Jbxw7u4S5clPl3kHakbuetEO+tdxYT9d7M+Rcl1RBJ9TJee1b08jpSziY0KfYyHnNrVRyjw+rr0GfVAxbPCsCyjj+RWLq/79EGBtQIo7KUQZvOs/rA3YS4+7nJ+GN3Voa8D0fB0N2+DvE1oakProOozz7umUh+9sDVh4a8Cn0j/emlkakIX3gPHDCWke5duElgak6t5E0+37Sa3v5M6bhJYGdFK5djJqU12UTOawd05uB20N6ARiThhtK5DS6TqBqG91aG1AJ9ADoeeF2/coX2dSvF7r0N6AxBoVOJsbGfLztedx79ehvQFNjW3HA/K77Ux4TWhzwC/Cx4m8t7RS2wNuesuvjyqW65BCqwOSytMl4xB9Deh6ZsS3vAYzl0+6nigNXz2JiurU8oCZHgx5l+h87Yt8Lpe+1QFJzYM9n5zX+WwPSOr6hJTLl/HWgPY9dLoGVLdh/VdPgk1AsrYGM/H2CbfVTfSDfDYHpOCX7fIPCRh67/Mh4BH9DwJG+wJaOkxQ0YoPlL55/mZjQD2f/4id88Hx9YT3NftqUF+DzWfVNyN9L3q2KaB5XCjkDq2eUNnzF2zH8Sfdf3Ju/nT2GZeLyqaA1NTc20VmNuUzj+dVuEfW/HSJd6N0D9tWcgEAAAAAAAAAAADAn4ws2tr5W9Tk/3QR/lNp6Fq2w3qvpE/fn7Q47i+CEbH4drGZxQf3sprDZE4wx2hZdbAcT1NzeP0Eo2TZhff48UNgsqhKIbu5LVJalDK/7fokJROqhniQ5cWhfpJlMa+0J1UKUcdNHevP99M4TkFdm+NJWMruzYbD78b4KLssFLLRBeylDLOu7a5dC2VezIK2rbOwnVQ7VBc56tPisxsGqmsns5v54uZF0QlPx6LKLYtL154P1TMxTwS6FTZjzZwkPyeMWCAuy3ukeELBadDHKlcq3SgbGTpOKCrTPhUvY1Ii042cNXJwHF90vn7di+JIdciWLZ40tIyU28xFK+6LmnUNnnrzchzn/QWhdGJ5WQLkOqAJZn6KEg27iJjNO+vFkYZP5g2mOKw4MVbw+RAFfAm61KBnvmHncjmNO010XVWR6SZ6KpYwaVuxuuxnBT9gQNIB6RYweg5Ylstp3OmvAUnpgN7SHKnRAXN3XcN2sIBzx2gC6hY5FzcTtyb6HDCNxXXdiOlkynpuuTSMMevy+xK242B8DmiaKCVl7etrqJLhMr4/N1ETMB2kudZY4OprMJBhzyi+6J6KGi9z9HFfHep/d2yuQd3PizwLClnfdoZ48XMNUlOOWVUVojar9pQUUz2OlXkzFF2minw82DBxuwb1l7hrXXFZ78/0OJiYC9J5CKjv4Lq2baWqpGnRaTAMeVvPn6ly4Y7hnsWK3+B6xWzvu27vLbdqz6cxqhLTSsn8wxL9hcVjff/4kRrob1tDVEt/q28OrFqR9zEql13a+obuDw2oeGHaZTyWH0+srGJ2FoquK9tj9Zz/JmrCrhvUgSe8/xQdcJILAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABgrb8AVdFTjJLjUDkAAAAASUVORK5CYII=', 'documento', 'werewrewr', 'descripcion', 'contenido', '2023-02-06 17:16:36'),
('08db08c6-d75c-4941-8040-d0151d3379e4', 'representacion', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOAAAADgCAMAAAAt85rTAAAAYFBMVEXy8vJ2dnb19fX5+flxcXFzc3Pq6urMzMyurq5ubm5+fn6FhYVra2vi4uLn5+fu7u6+vr6VlZW3t7fFxcWQkJDT09Oampre3t6Tk5N6enqhoaHY2Nirq6uKioqenp67u7tegggxAAAE4klEQVR4nO3c63qrKBgFYP0QDxCPeIjH3P9dDmhMTJM9iXtmWtmz3j9NjenDKghioY4DAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA3yD1d3Hopwu8TxqM0WkHEcZ2Jbxw7u4S5clPl3kHakbuetEO+tdxYT9d7M+Rcl1RBJ9TJee1b08jpSziY0KfYyHnNrVRyjw+rr0GfVAxbPCsCyjj+RWLq/79EGBtQIo7KUQZvOs/rA3YS4+7nJ+GN3Voa8D0fB0N2+DvE1oakProOozz7umUh+9sDVh4a8Cn0j/emlkakIX3gPHDCWke5duElgak6t5E0+37Sa3v5M6bhJYGdFK5djJqU12UTOawd05uB20N6ARiThhtK5DS6TqBqG91aG1AJ9ADoeeF2/coX2dSvF7r0N6AxBoVOJsbGfLztedx79ehvQFNjW3HA/K77Ux4TWhzwC/Cx4m8t7RS2wNuesuvjyqW65BCqwOSytMl4xB9Deh6ZsS3vAYzl0+6nigNXz2JiurU8oCZHgx5l+h87Yt8Lpe+1QFJzYM9n5zX+WwPSOr6hJTLl/HWgPY9dLoGVLdh/VdPgk1AsrYGM/H2CbfVTfSDfDYHpOCX7fIPCRh67/Mh4BH9DwJG+wJaOkxQ0YoPlL55/mZjQD2f/4id88Hx9YT3NftqUF+DzWfVNyN9L3q2KaB5XCjkDq2eUNnzF2zH8Sfdf3Ju/nT2GZeLyqaA1NTc20VmNuUzj+dVuEfW/HSJd6N0D9tWcgEAAAAAAAAAAADAn4ws2tr5W9Tk/3QR/lNp6Fq2w3qvpE/fn7Q47i+CEbH4drGZxQf3sprDZE4wx2hZdbAcT1NzeP0Eo2TZhff48UNgsqhKIbu5LVJalDK/7fokJROqhniQ5cWhfpJlMa+0J1UKUcdNHevP99M4TkFdm+NJWMruzYbD78b4KLssFLLRBeylDLOu7a5dC2VezIK2rbOwnVQ7VBc56tPisxsGqmsns5v54uZF0QlPx6LKLYtL154P1TMxTwS6FTZjzZwkPyeMWCAuy3ukeELBadDHKlcq3SgbGTpOKCrTPhUvY1Ii042cNXJwHF90vn7di+JIdciWLZ40tIyU28xFK+6LmnUNnnrzchzn/QWhdGJ5WQLkOqAJZn6KEg27iJjNO+vFkYZP5g2mOKw4MVbw+RAFfAm61KBnvmHncjmNO010XVWR6SZ6KpYwaVuxuuxnBT9gQNIB6RYweg5Ylstp3OmvAUnpgN7SHKnRAXN3XcN2sIBzx2gC6hY5FzcTtyb6HDCNxXXdiOlkynpuuTSMMevy+xK242B8DmiaKCVl7etrqJLhMr4/N1ETMB2kudZY4OprMJBhzyi+6J6KGi9z9HFfHep/d2yuQd3PizwLClnfdoZ48XMNUlOOWVUVojar9pQUUz2OlXkzFF2minw82DBxuwb1l7hrXXFZ78/0OJiYC9J5CKjv4Lq2baWqpGnRaTAMeVvPn6ly4Y7hnsWK3+B6xWzvu27vLbdqz6cxqhLTSsn8wxL9hcVjff/4kRrob1tDVEt/q28OrFqR9zEql13a+obuDw2oeGHaZTyWH0+srGJ2FoquK9tj9Zz/JmrCrhvUgSe8/xQdcJILAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABgrb8AVdFTjJLjUDkAAAAASUVORK5CYII=', 'documento', 'werewrewr', 'descripcion', 'contenido', '2023-02-06 17:16:36'),
('08db08c6-f2a4-4b74-81ba-54518805165a', 'representacion', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOAAAADgCAMAAAAt85rTAAAAYFBMVEXy8vJ2dnb19fX5+flxcXFzc3Pq6urMzMyurq5ubm5+fn6FhYVra2vi4uLn5+fu7u6+vr6VlZW3t7fFxcWQkJDT09Oampre3t6Tk5N6enqhoaHY2Nirq6uKioqenp67u7tegggxAAAE4klEQVR4nO3c63qrKBgFYP0QDxCPeIjH3P9dDmhMTJM9iXtmWtmz3j9NjenDKghioY4DAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA3yD1d3Hopwu8TxqM0WkHEcZ2Jbxw7u4S5clPl3kHakbuetEO+tdxYT9d7M+Rcl1RBJ9TJee1b08jpSziY0KfYyHnNrVRyjw+rr0GfVAxbPCsCyjj+RWLq/79EGBtQIo7KUQZvOs/rA3YS4+7nJ+GN3Voa8D0fB0N2+DvE1oakProOozz7umUh+9sDVh4a8Cn0j/emlkakIX3gPHDCWke5duElgak6t5E0+37Sa3v5M6bhJYGdFK5djJqU12UTOawd05uB20N6ARiThhtK5DS6TqBqG91aG1AJ9ADoeeF2/coX2dSvF7r0N6AxBoVOJsbGfLztedx79ehvQFNjW3HA/K77Ux4TWhzwC/Cx4m8t7RS2wNuesuvjyqW65BCqwOSytMl4xB9Deh6ZsS3vAYzl0+6nigNXz2JiurU8oCZHgx5l+h87Yt8Lpe+1QFJzYM9n5zX+WwPSOr6hJTLl/HWgPY9dLoGVLdh/VdPgk1AsrYGM/H2CbfVTfSDfDYHpOCX7fIPCRh67/Mh4BH9DwJG+wJaOkxQ0YoPlL55/mZjQD2f/4id88Hx9YT3NftqUF+DzWfVNyN9L3q2KaB5XCjkDq2eUNnzF2zH8Sfdf3Ju/nT2GZeLyqaA1NTc20VmNuUzj+dVuEfW/HSJd6N0D9tWcgEAAAAAAAAAAADAn4ws2tr5W9Tk/3QR/lNp6Fq2w3qvpE/fn7Q47i+CEbH4drGZxQf3sprDZE4wx2hZdbAcT1NzeP0Eo2TZhff48UNgsqhKIbu5LVJalDK/7fokJROqhniQ5cWhfpJlMa+0J1UKUcdNHevP99M4TkFdm+NJWMruzYbD78b4KLssFLLRBeylDLOu7a5dC2VezIK2rbOwnVQ7VBc56tPisxsGqmsns5v54uZF0QlPx6LKLYtL154P1TMxTwS6FTZjzZwkPyeMWCAuy3ukeELBadDHKlcq3SgbGTpOKCrTPhUvY1Ii042cNXJwHF90vn7di+JIdciWLZ40tIyU28xFK+6LmnUNnnrzchzn/QWhdGJ5WQLkOqAJZn6KEg27iJjNO+vFkYZP5g2mOKw4MVbw+RAFfAm61KBnvmHncjmNO010XVWR6SZ6KpYwaVuxuuxnBT9gQNIB6RYweg5Ylstp3OmvAUnpgN7SHKnRAXN3XcN2sIBzx2gC6hY5FzcTtyb6HDCNxXXdiOlkynpuuTSMMevy+xK242B8DmiaKCVl7etrqJLhMr4/N1ETMB2kudZY4OprMJBhzyi+6J6KGi9z9HFfHep/d2yuQd3PizwLClnfdoZ48XMNUlOOWVUVojar9pQUUz2OlXkzFF2minw82DBxuwb1l7hrXXFZ78/0OJiYC9J5CKjv4Lq2baWqpGnRaTAMeVvPn6ly4Y7hnsWK3+B6xWzvu27vLbdqz6cxqhLTSsn8wxL9hcVjff/4kRrob1tDVEt/q28OrFqR9zEql13a+obuDw2oeGHaZTyWH0+srGJ2FoquK9tj9Zz/JmrCrhvUgSe8/xQdcJILAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABgrb8AVdFTjJLjUDkAAAAASUVORK5CYII=', 'documento', 'Pedro ', 'descripcion', 'contenido', '2023-02-06 17:16:36');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `informacioninteres`
--
ALTER TABLE `informacioninteres`
  ADD PRIMARY KEY (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;