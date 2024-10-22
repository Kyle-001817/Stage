INSERT INTO genre(nom) values ('Homme');
INSERT INTO genre(nom) values ('Femme');

INSERT INTO unite(nom) VALUES ('U');
INSERT INTO unite(nom) VALUES ('m3');
INSERT INTO unite(nom) VALUES ('sac');
INSERT INTO unite(nom) VALUES ('barre');
INSERT INTO unite(nom) VALUES ('kg');
INSERT INTO unite(nom) VALUES ('feuille');
INSERT INTO unite(nom) VALUES ('L');
INSERT INTO unite(nom) VALUES ('Fft');
INSERT INTO unite(nom) VALUES ('m2');

CREATE VIEW v_bdq AS
SELECT 
    dbq.id_detail_dbq,
    dbq.designation,
    dbq.id_serie_travaux,
    dbq.id_bdq,
    u1.nom AS nom_unite_dbq,
    dbq.quantite AS quantite_dbq,
    m.nom AS nom_materiaux,
    u2.nom AS nom_unite_materiaux,
    dm.quantite AS quantite_materiaux,
    m.prix_unitaire,
    dm.quantite * m.prix_unitaire AS montant
FROM 
    detail_bdq dbq
LEFT JOIN 
    detail_materiaux dm ON dbq.id_detail_dbq = dm.id_detail_dbq
LEFT JOIN 
    materiaux m ON dm.id_materiaux = m.id_materiaux
LEFT JOIN 
    unite u1 ON dbq.id_unite = u1.id_unite
LEFT JOIN 
    unite u2 ON m.id_unite = u2.id_unite
ORDER BY 
    dbq.id_detail_dbq;

CREATE view v_bde as
select 
    detail_bdq.designation,
    detail_bdq.id_unite,
    detail_bdq.quantite,
    detail_bde.prix_unitaire,
    detail_bdq.id_serie_travaux,
    detail_bdq.id_bdq,
    (detail_bde.prix_unitaire * detail_bdq.quantite) as montant 
from detail_bde 
join detail_bdq on detail_bdq.id_detail_dbq = detail_bde.id_detail_dbq;