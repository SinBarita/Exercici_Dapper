using Dapper;
using ExempleDapper.Contracts;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExempleDapper
{
    // Esquema de la BD
    //
    //  ---------------                ---------------                ---------------
    //  |             |                |             |                |             |
    //  |    Cotxe    |--------------->|   Situació  |<---------------|   Ubicació  |
    //  |             |                |             |                |             |
    //  -------^-------                ---------------                ---------------
    //         |
    //         |
    //         |
    //  ---------------     ---------------
    //  |             |     |             |
    //  |    Model    |<----| Combustible |
    //  |             |     |             |
    //  ---------------     ---------------

    class Program
    {
        private static string connectionString;

        static void Main(string[] args)
        {
            connectionString = "Data Source =:memory:";
            using (SqliteConnection conn = new SqliteConnection(connectionString))
            {
                conn.Open();

                // Generar base de dades
                createDatabase(conn);

                // Consultar els tipus de combustible que hi ha
                IEnumerable<Combustible> combustibles = conn.Query<Combustible>("SELECT IdCombustible as Id, Codi, Descripcio FROM Combustibles");
                foreach (Combustible fuel in combustibles)
                    Console.WriteLine("Tipo de combustible: {0} - {1}", fuel.Codi, fuel.Descripcio);

                // Exercici 1
                // ----------
                // Consultar tots els cotxes matriculats al 2006 i mostrar la següent informació:
                // Marca - Model - Any (del model, no de la matrícula) - Tipus de combustible (codi)

                // Exercici 2
                // ----------
                // Consultar els cotxes que estan llogats i mostrar la següent informació:
                // Marca - Model - Matrícula - Ubicació (on es va llogar) - Data


            }
        }

        static void createDatabase(SqliteConnection conn)
        {
            conn.Execute("CREATE TABLE Combustibles(IdCombustible INTEGER PRIMARY KEY, Codi VARCHAR(20), Descripcio VARCHAR(250))");
            conn.Execute("INSERT INTO Combustibles(Codi, Descripcio) VALUES('Gasolina', 'Gasolina 95/98 octans sense plom')");
            conn.Execute("INSERT INTO Combustibles(Codi, Descripcio) VALUES('Dièsel', 'Dièsel estàndard')");
            conn.Execute("INSERT INTO Combustibles(Codi, Descripcio) VALUES('Bio-oil', 'Combustible sintètic de base vegetal')");
            conn.Execute("CREATE TABLE Models(IdModel INTEGER PRIMARY KEY, Marca VARCHAR(50), Model VARCHAR(100), Any INTEGER, Combustible INTEGER, Cilindrada FLOAT, FOREIGN KEY(Combustible) REFERENCES Combustibles(IdCombustible))");
            conn.Execute("INSERT INTO Models(Marca, Model, Any, Combustible, Cilindrada) VALUES('Ford', 'Fiesta', 1997, 1, 1.650)");
            conn.Execute("INSERT INTO Models(Marca, Model, Any, Combustible, Cilindrada) VALUES('Seat', 'Ibiza', 2002, 2, 1.600)");
            conn.Execute("INSERT INTO Models(Marca, Model, Any, Combustible, Cilindrada) VALUES('Audi', 'A3', 2001, 1, 1.700)");
            conn.Execute("INSERT INTO Models(Marca, Model, Any, Combustible, Cilindrada) VALUES('Peugeot', '307', 2003, 2, 1.550)");
            conn.Execute("INSERT INTO Models(Marca, Model, Any, Combustible, Cilindrada) VALUES('Renault', 'Clio', 2005, 1, 1.720)");
            conn.Execute("CREATE TABLE Cotxes(IdCotxe INTEGER PRIMARY KEY, Model INTEGER, Matricula VARCHAR(10), Any INTEGER, FOREIGN KEY(Model) REFERENCES Models(IdModel))");
            conn.Execute("INSERT INTO Cotxes(Model, Matricula, Any) VALUES (1, '2348GFK', 2006)");
            conn.Execute("INSERT INTO Cotxes(Model, Matricula, Any) VALUES (1, '3489GZU', 2007)");
            conn.Execute("INSERT INTO Cotxes(Model, Matricula, Any) VALUES (1, '4373GMT', 2006)");
            conn.Execute("INSERT INTO Cotxes(Model, Matricula, Any) VALUES (2, '2341HAB', 2007)");
            conn.Execute("INSERT INTO Cotxes(Model, Matricula, Any) VALUES (2, '7723GSD', 2006)");
            conn.Execute("INSERT INTO Cotxes(Model, Matricula, Any) VALUES (3, '2340HBA', 2008)");
            conn.Execute("INSERT INTO Cotxes(Model, Matricula, Any) VALUES (4, '8974GLL', 2006)");
            conn.Execute("INSERT INTO Cotxes(Model, Matricula, Any) VALUES (4, '9402GJV', 2006)");
            conn.Execute("INSERT INTO Cotxes(Model, Matricula, Any) VALUES (4, '0932HNF', 2009)");
            conn.Execute("INSERT INTO Cotxes(Model, Matricula, Any) VALUES (5, '3892HKU', 2009)");
            conn.Execute("INSERT INTO Cotxes(Model, Matricula, Any) VALUES (5, '4352HUI', 2010)");
            conn.Execute("INSERT INTO Cotxes(Model, Matricula, Any) VALUES (5, '2389HYN', 2010)");
            conn.Execute("INSERT INTO Cotxes(Model, Matricula, Any) VALUES (5, '7377HDE', 2009)");
            conn.Execute("CREATE TABLE Ubicacions(IdUbicacio INTEGER PRIMARY KEY, Nom VARCHAR(50), Adreca VARCHAR(250), Capacitat INTEGER)");
            conn.Execute("INSERT INTO Ubicacions(Nom, Adreca, Capacitat) VALUES('Aeroport Manises', 'Ctra. Aeroport Manises, s/n, 46940 Manises', 12)");
            conn.Execute("INSERT INTO Ubicacions(Nom, Adreca, Capacitat) VALUES('Estació AVE', 'C. de Sant Vicent Màrtir, 188, 190, 46007 València', 12)");
            conn.Execute("CREATE TABLE UbicacioCotxes(IdUbiCotxe INTEGER PRIMARY KEY, Cotxe INTEGER, Ubicacio INTEGER, Llogat TINYINT, Data DATETIME, FOREIGN KEY(Cotxe) REFERENCES Cotxes(IdCotxe), FOREIGN KEY(Ubicacio) REFERENCES Ubicacions(IdUbicacio))");
            conn.Execute("INSERT INTO UbicacioCotxes(Cotxe, Ubicacio, Llogat, Data) VALUES(1, 2, 1, '2015-06-03')");
            conn.Execute("INSERT INTO UbicacioCotxes(Cotxe, Ubicacio, Llogat, Data) VALUES(2, 1, 0, '2015-05-28')");
            conn.Execute("INSERT INTO UbicacioCotxes(Cotxe, Ubicacio, Llogat, Data) VALUES(3, 2, 0, '2015-06-01')");
            conn.Execute("INSERT INTO UbicacioCotxes(Cotxe, Ubicacio, Llogat, Data) VALUES(4, 2, 0, '2015-05-23')");
            conn.Execute("INSERT INTO UbicacioCotxes(Cotxe, Ubicacio, Llogat, Data) VALUES(5, 1, 0, '2015-06-02')");
            conn.Execute("INSERT INTO UbicacioCotxes(Cotxe, Ubicacio, Llogat, Data) VALUES(6, 1, 1, '2015-06-03')");
            conn.Execute("INSERT INTO UbicacioCotxes(Cotxe, Ubicacio, Llogat, Data) VALUES(7, 1, 0, '2015-05-30')");
            conn.Execute("INSERT INTO UbicacioCotxes(Cotxe, Ubicacio, Llogat, Data) VALUES(8, 2, 0, '2015-06-02')");
            conn.Execute("INSERT INTO UbicacioCotxes(Cotxe, Ubicacio, Llogat, Data) VALUES(9, 1, 1, '2015-05-24')");
            conn.Execute("INSERT INTO UbicacioCotxes(Cotxe, Ubicacio, Llogat, Data) VALUES(10, 2, 0, '2015-05-19')");
            conn.Execute("INSERT INTO UbicacioCotxes(Cotxe, Ubicacio, Llogat, Data) VALUES(11, 2, 0, '2015-05-23')");
            conn.Execute("INSERT INTO UbicacioCotxes(Cotxe, Ubicacio, Llogat, Data) VALUES(12, 1, 1, '2015-05-27')");
            conn.Execute("INSERT INTO UbicacioCotxes(Cotxe, Ubicacio, Llogat, Data) VALUES(13, 1, 0, '2015-05-29')");
        }
    }
}
