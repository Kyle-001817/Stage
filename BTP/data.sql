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

INSERT INTO service(nom) VALUES ('Main d''oeuvre');
INSERT INTO service(nom) VALUES ('Maitre de chantier');
INSERT INTO service(nom) VALUES ('Chef d''equipe');

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

CREATE OR REPLACE VIEW v_salaire_personnel as
WITH JourExecutionService001 AS (
    SELECT 
        detail_bdq.id_detail_dbq, 
        CEIL((detail_bdq.quantite / personnel.rendement) / personnel.nb_main_oeuvre) AS jour_execution_service001
    FROM 
        personnel 
    JOIN 
        detail_bdq 
    ON 
        detail_bdq.id_detail_dbq = personnel.id_detail_dbq
    WHERE 
        personnel.id_service = 'SERVICE001'
)
SELECT 
    personnel.*, 
    detail_bdq.quantite AS quantite,
    COALESCE(jour_execution_service001.jour_execution_service001, CEIL((detail_bdq.quantite / personnel.rendement) / personnel.nb_main_oeuvre)) AS jour_execution,
    ((personnel.salaire_par_heure * personnel.heure_travail) * personnel.nb_main_oeuvre) * COALESCE(jour_execution_service001.jour_execution_service001, CEIL((detail_bdq.quantite / personnel.rendement) / personnel.nb_main_oeuvre)) AS salaire_personnel
FROM 
    personnel 
JOIN 
    detail_bdq 
ON 
    detail_bdq.id_detail_dbq = personnel.id_detail_dbq
LEFT JOIN 
    JourExecutionService001 AS jour_execution_service001 
ON 
    detail_bdq.id_detail_dbq = jour_execution_service001.id_detail_dbq;


