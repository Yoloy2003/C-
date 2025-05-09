DROP DATABASE IF EXISTS Transconnect;
CREATE DATABASE IF NOT EXISTS Transconnect;
USE Transconnect;

DROP TABLE IF EXISTS Personne;
CREATE TABLE Personne
(
    NSS VARCHAR(20) PRIMARY KEY NOT NULL,
    Nom VARCHAR(50),
    Prenom VARCHAR(50),
    DateNaissance DATE,
    AdressePostale VARCHAR(100),
    AdresseMail VARCHAR(100),
    Telephone VARCHAR(20)
);

DROP TABLE IF EXISTS Salarie;
CREATE TABLE Salarie
(
    NSS VARCHAR(20) PRIMARY KEY,
    DateEntree DATE,
    Poste VARCHAR(50),
    Salaire DECIMAL(10,2),
    FOREIGN KEY (NSS) REFERENCES Personne(NSS) ON DELETE CASCADE
);

DROP TABLE IF EXISTS Client;
CREATE TABLE Client
(
    NSS VARCHAR(20) PRIMARY KEY,
    FOREIGN KEY (NSS) REFERENCES Personne(NSS) ON DELETE CASCADE
);

DROP TABLE IF EXISTS Vehicule;
CREATE TABLE Vehicule
(
    Immatriculation VARCHAR(20) PRIMARY KEY
);

DROP TABLE IF EXISTS Voiture;
CREATE TABLE Voiture
(
    Immatriculation VARCHAR(20) PRIMARY KEY,
    NbPlace INT,
    FOREIGN KEY (Immatriculation) REFERENCES Vehicule(Immatriculation) ON DELETE CASCADE
);

DROP TABLE IF EXISTS Camionnette;
CREATE TABLE Camionnette
(
    Immatriculation VARCHAR(20) PRIMARY KEY,
    FOREIGN KEY (Immatriculation) REFERENCES Vehicule(Immatriculation) ON DELETE CASCADE
);

DROP TABLE IF EXISTS PoidsLourd;
CREATE TABLE PoidsLourd
(
    Immatriculation VARCHAR(20) PRIMARY KEY,
    VolumeTransportable DECIMAL(10,2),
    Type VARCHAR(50),
    FOREIGN KEY (Immatriculation) REFERENCES Vehicule(Immatriculation) ON DELETE CASCADE
);

DROP TABLE IF EXISTS Commande;
CREATE TABLE Commande
(
    Numero INT PRIMARY KEY AUTO_INCREMENT,
    NSS_Client VARCHAR(20),
    NSS_Chauffeur VARCHAR(20),
    Immatriculation VARCHAR(20),
    Utilisation VARCHAR(200),
    DateCommande DATE,
    VilleDepart VARCHAR(50),
    VilleArrivee VARCHAR(50),
    Prix DECIMAL(10,2),
    KmTotal DECIMAL(10,2),
    FOREIGN KEY (NSS_Client) REFERENCES Client(NSS),
    FOREIGN KEY (NSS_Chauffeur) REFERENCES Salarie(NSS),
    FOREIGN KEY (Immatriculation) REFERENCES Vehicule(Immatriculation)
);

CREATE TABLE Organigramme
(
    NSS VARCHAR(20) PRIMARY KEY,
    NSS_Superieur VARCHAR(20),
    FOREIGN KEY (NSS) REFERENCES Salarie(NSS),
    FOREIGN KEY (NSS_Superieur) REFERENCES Salarie(NSS) ON DELETE CASCADE
);


-- Insertion des personnes correspondantes aux clients
INSERT INTO Personne (NSS, Nom, Prenom, DateNaissance, AdressePostale, AdresseMail, Telephone) VALUES
('1001', 'Dupont', 'Lucie', '1985-03-12', '12 rue des Lilas, Lyon', 'lucie.dupont@mail.com', '0601020304'),
('1002', 'Martin', 'Paul', '1990-07-25', '45 avenue Victor Hugo, Paris', 'paul.martin@mail.com', '0602030405'),
('1003', 'Bernard', 'Sophie', '1978-01-05', '88 boulevard Haussmann, Paris', 'sophie.bernard@mail.com', '0603040506'),
('1004', 'Morel', 'Antoine', '1992-09-15', '10 impasse des Cerisiers, Marseille', 'antoine.morel@mail.com', '0604050607'),
('1005', 'Girard', 'Emma', '1980-05-30', '5 place Bellecour, Lyon', 'emma.girard@mail.com', '0605060708'),
('1006', 'Fournier', 'Hugo', '1987-11-22', '23 rue Lafayette, Toulouse', 'hugo.fournier@mail.com', '0606070809'),
('1007', 'Lambert', 'Julie', '1995-04-10', '77 avenue des Champs, Bordeaux', 'julie.lambert@mail.com', '0607080910'),
('1008', 'Bonnet', 'Thomas', '1983-08-19', '14 rue de la République, Lille', 'thomas.bonnet@mail.com', '0608091011'),
('1009', 'Francois', 'Camille', '1975-12-02', '8 avenue de la Paix, Nice', 'camille.francois@mail.com', '0609101112'),
('1010', 'Rousseau', 'Maxime', '1991-06-14', '3 rue Nationale, Montpellier', 'maxime.rousseau@mail.com', '0610111213');

-- Insertion des clients liés aux personnes
INSERT INTO Client (NSS) VALUES
('1001'),
('1002'),
('1003'),
('1004'),
('1005'),
('1006'),
('1007'),
('1008'),
('1009'),
('1010');

-- Insertion des personnes correspondantes aux salarié
INSERT INTO Personne (NSS, Nom, Prenom, DateNaissance, AdressePostale, AdresseMail, Telephone) VALUES
('2001', 'Dupond', 'Jean', '1970-05-12', '1 avenue de la République, Paris', 'jean.dupond@mail.com', '0600000001'),
('2002', 'Fiesta', 'Claire', '1980-03-18', '2 rue des Lilas, Lyon', 'claire.fiesta@mail.com', '0600000002'),
('2003', 'Forge', 'Nicolas', '1990-07-25', '3 place Bellecour, Lyon', 'nicolas.forge@mail.com', '0600000003'),
('2004', 'Fermi', 'Elise', '1992-09-15', '4 avenue Victor Hugo, Paris', 'elise.fermi@mail.com', '0600000004'),
('2005', 'Fetard', 'Marc', '1975-11-30', '5 rue Lafayette, Toulouse', 'marc.fetard@mail.com', '0600000005'),
('2006', 'Royal', 'Antoine', '1985-01-22', '6 rue Nationale, Montpellier', 'antoine.royal@mail.com', '0600000006'),
('2007', 'Romu', 'Adrien', '1993-10-10', '7 avenue des Champs, Bordeaux', 'adrien.romu@mail.com', '0600000007'),
('2008', 'Romi', 'Lucas', '1994-04-14', '8 avenue de la Paix, Nice', 'lucas.romi@mail.com', '0600000008'),
('2009', 'Roma', 'Julie', '1995-06-12', '9 rue de la République, Lille', 'julie.roma@mail.com', '0600000009'),
('2010', 'Prince', 'Alice', '1987-08-05', '10 avenue de la Paix, Nice', 'alice.prince@mail.com', '0600000010'),
('2011', 'Rome', 'Vincent', '1991-08-22', '11 rue Nationale, Montpellier', 'vincent.rome@mail.com', '0600000011'),
('2012', 'Rimou', 'Maxime', '1992-10-02', '12 rue de la République, Lille', 'maxime.rimou@mail.com', '0600000012'),
('2013', 'Joyeuse', 'Camille', '1978-12-30', '13 boulevard Haussmann, Paris', 'camille.joyeuse@mail.com', '0600000013'),
('2014', 'Couleur', 'Pauline', '1988-09-21', '14 impasse des Cerisiers, Marseille', 'pauline.couleur@mail.com', '0600000014'),
('2015', 'ToutleMonde', 'Emma', '1993-11-18', '15 avenue Victor Hugo, Paris', 'emma.toutemonde@mail.com', '0600000015'),
('2016', 'GripSous', 'Georges', '1974-06-14', '16 rue des Lilas, Lyon', 'georges.gripsous@mail.com', '0600000016'),
('2017', 'Picsou', 'Luc', '1969-04-08', '17 avenue de la République, Paris', 'luc.picsou@mail.com', '0600000017'),
('2018', 'Fournier', 'Sylvie', '1983-07-23', '18 rue Lafayette, Toulouse', 'sylvie.fournier@mail.com', '0600000018'),
('2019', 'Gautier', 'Nathalie', '1986-05-11', '19 place Bellecour, Lyon', 'nathalie.gautier@mail.com', '0600000019'),
('2020', 'GrosSous', 'Olivier', '1972-02-20', '20 avenue Victor Hugo, Paris', 'olivier.grossous@mail.com', '0600000020');

-- Insertion dans Salarie
INSERT INTO Salarie (NSS, DateEntree, Poste, Salaire) VALUES
('2001', '2000-01-01', 'Directeur Général', 7000),
('2002', '2005-03-01', 'Directrice Commerciale', 5500),
('2003', '2010-06-01', 'Commercial', 3500),
('2004', '2015-04-01', 'Commerciale', 3200),
('2005', '2004-02-01', 'Directeur des opérations', 6000),
('2006', '2007-08-01', 'Chef d\'équipe', 4500),
('2007', '2018-07-01', 'Chauffeur', 2700),
('2008', '2019-05-01', 'Chauffeur', 2700),
('2009', '2019-09-01', 'Chauffeuse', 2700),
('2010', '2008-11-01', 'Chef d\'équipe', 4500),
('2011', '2019-03-01', 'Chauffeur', 2700),
('2012', '2017-01-01', 'Chauffeur', 2700),
('2013', '2003-10-01', 'Directrice des RH', 5500),
('2014', '2010-05-01', 'Formation', 3300),
('2015', '2015-02-01', 'Contrats', 3100),
('2016', '2001-07-01', 'Directeur Financier', 6500),
('2017', '1999-01-01', 'Direction comptable', 6000),
('2018', '2010-03-01', 'Comptable', 3200),
('2019', '2012-06-01', 'Comptable', 3100),
('2020', '2002-09-01', 'Contrôleur de Gestion', 6300);

INSERT INTO Organigramme (NSS, NSS_Superieur) VALUES
('2001', NULL),
('2002', '2001'),
('2003', '2002'),
('2004', '2002'),
('2005', '2001'),
('2006', '2005'),
('2007', '2006'),
('2008', '2006'),
('2009', '2006'),
('2010', '2005'),
('2011', '2010'),
('2012', '2010'),
('2013', '2001'),
('2014', '2013'),
('2015', '2013'),
('2016', '2001'),
('2017', '2016'),
('2018', '2017'),
('2019', '2017'),
('2020', '2016');

INSERT INTO Vehicule (Immatriculation) VALUES 
('AA-123-AA'),
('BB-234-BB'),
('CC-345-CC'),
('DD-456-DD'),
('EE-567-EE'),
('FF-678-FF'),
('GG-789-GG'),
('HH-890-HH'),
('II-901-II'),
('JJ-012-JJ'),
('KK-123-KK'),
('LL-234-LL'),
('MM-345-MM'),
('NN-456-NN'),
('OO-567-OO');

INSERT INTO Voiture (Immatriculation, NbPlace) VALUES 
('AA-123-AA', 5),
('BB-234-BB', 4),
('CC-345-CC', 2),
('DD-456-DD', 7),
('EE-567-EE', 5);

INSERT INTO Camionnette (Immatriculation) VALUES 
('FF-678-FF'),
('GG-789-GG'),
('HH-890-HH'),
('II-901-II'),
('JJ-012-JJ');

INSERT INTO PoidsLourd (Immatriculation, VolumeTransportable, Type) VALUES 
('KK-123-KK', 30.50, 'Frigorifique'),
('LL-234-LL', 25.00, 'Citerne'),
('MM-345-MM', 40.00, 'Benne'),
('NN-456-NN', 20.00, 'Citerne'),
('OO-567-OO', 35.75, 'Frigorifique');

Select * from PoidsLourd;