\c postgres
DROP DATABASE btp;
CREATE DATABASE btp;
\c btp

CREATE SEQUENCE genre_seq;
CREATE TABLE genre(
   id_genre VARCHAR(50) PRIMARY KEY DEFAULT CONCAT('GENRE', LPAD(nextval('genre_seq')::TEXT, 3, '0')),
   nom VARCHAR(50)  NOT NULL
);

CREATE SEQUENCE tm_seq;
CREATE TABLE type_materiel(
   id_type_materiel VARCHAR(50) PRIMARY KEY DEFAULT CONCAT('TM', LPAD(nextval('tm_seq')::TEXT, 3, '0')),
   nom VARCHAR(250)  NOT NULL
);
CREATE SEQUENCE unite_seq;
CREATE TABLE unite(
   id_unite VARCHAR(50) PRIMARY KEY DEFAULT CONCAT('UNITE', LPAD(nextval('unite_seq')::TEXT, 3, '0')),
   nom VARCHAR(250) NOT NULL
);
CREATE SEQUENCE tb_seq;
CREATE TABLE type_bordereau(
   id_type_bordereau VARCHAR(50) PRIMARY KEY DEFAULT CONCAT('TB', LPAD(nextval('tb_seq')::TEXT, 3, '0')),
   nom VARCHAR(250)  NOT NULL
);
CREATE SEQUENCE st_seq;
CREATE TABLE serie_travaux(
   id_serie_travaux VARCHAR(50) PRIMARY KEY DEFAULT CONCAT('ST', LPAD(nextval('st_seq')::TEXT, 3, '0')),
   nom VARCHAR(250)  NOT NULL,
   id_type_bordereau VARCHAR(50)  NOT NULL,
   FOREIGN KEY(id_type_bordereau) REFERENCES type_bordereau(id_type_bordereau)
);
CREATE SEQUENCE bdq_seq;
CREATE TABLE bdq(
   id_bdq VARCHAR(50) PRIMARY KEY DEFAULT CONCAT('BDQ', LPAD(nextval('bdq_seq')::TEXT, 3, '0')),
   titre VARCHAR(250)  NOT NULL,
   id_type_bordereau VARCHAR(50)  NOT NULL,
   FOREIGN KEY(id_type_bordereau) REFERENCES type_bordereau(id_type_bordereau)
);
CREATE SEQUENCE dbdq_seq;
CREATE TABLE detail_bdq(
   id_detail_dbq VARCHAR(50) PRIMARY KEY DEFAULT CONCAT('DBDQ', LPAD(nextval('dbdq_seq')::TEXT, 3, '0')),
   designation TEXT NOT NULL,
   quantite NUMERIC(15,2)   NOT NULL,
   id_serie_travaux VARCHAR(50)  NOT NULL,
   id_bdq VARCHAR(50)  NOT NULL,
   id_unite VARCHAR(50)  NOT NULL,
   FOREIGN KEY(id_serie_travaux) REFERENCES serie_travaux(id_serie_travaux),
   FOREIGN KEY(id_bdq) REFERENCES bdq(id_bdq),
   FOREIGN KEY(id_unite) REFERENCES unite(id_unite)
);
CREATE SEQUENCE profil_seq;
CREATE TABLE profil(
   id_profil VARCHAR(50) PRIMARY KEY DEFAULT CONCAT('PROFIL', LPAD(nextval('profil_seq')::TEXT, 3, '0')),
   nom VARCHAR(250)  NOT NULL,
   prenom VARCHAR(250)  NOT NULL,
   date_naissance TIMESTAMP NOT NULL,
   adresse VARCHAR(250)  NOT NULL,
   id_genre VARCHAR(50)  NOT NULL,
   FOREIGN KEY(id_genre) REFERENCES genre(id_genre)
);
CREATE SEQUENCE user_seq;
CREATE TABLE utilisateur(
   id_utilisateur VARCHAR(50) PRIMARY KEY DEFAULT CONCAT('USER', LPAD(nextval('user_seq')::TEXT, 3, '0')),
   email VARCHAR(250)  NOT NULL,
   mdp VARCHAR(250)  NOT NULL,
   id_profil VARCHAR(50)  NOT NULL,
   FOREIGN KEY(id_profil) REFERENCES profil(id_profil)
);
CREATE SEQUENCE matx_seq;
CREATE TABLE materiaux(
   id_materiaux VARCHAR(50) PRIMARY KEY DEFAULT CONCAT('MATX', LPAD(nextval('matx_seq')::TEXT, 3, '0')),
   nom VARCHAR(250)  NOT NULL,
   prix_unitaire NUMERIC(15,2)   NOT NULL,
   id_unite VARCHAR(50)  NOT NULL,
   id_type_materiel VARCHAR(50)  NOT NULL,
   FOREIGN KEY(id_unite) REFERENCES unite(id_unite),
   FOREIGN KEY(id_type_materiel) REFERENCES type_materiel(id_type_materiel)
);
CREATE TABLE detail_materiaux(
   id_materiaux VARCHAR(50),
   id_detail_dbq VARCHAR(50),
   quantite NUMERIC(15,2) NOT NULL,
   FOREIGN KEY(id_materiaux) REFERENCES materiaux(id_materiaux),
   FOREIGN KEY(id_detail_dbq) REFERENCES detail_bdq(id_detail_dbq)
);

