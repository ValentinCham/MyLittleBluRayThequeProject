using MyLittleBluRayThequeProject.DTOs;
using Npgsql;
using System.Data;

namespace MyLittleBluRayThequeProject.Repositories
{
    public class PersonneRepository
    {

        public PersonneRepository() { }


        public Personne GetRealisateurBr(long idBr)
        {

            NpgsqlConnection conn = null;
            Personne personne = null;
            NpgsqlTransaction tran = null;
            try
            {

                List<Personne> qryResult = new List<Personne>();
                // Connect to a PostgreSQL database

                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=projet;Database=postgres;");
                conn.Open();
                tran = conn.BeginTransaction();
                using (var cmd = new NpgsqlCommand("Select per.\"Id\", per.\"Nom\", per.\"Prenom\", per.\"DateNaissance\", per.\"Nationalite\" from \"BluRayTheque\".\"Realisateur\" as r INNER JOIN \"BluRayTheque\".\"Personne\" as per ON per.\"Id\" = r.\"IdRealisateur\" where r.\"IdBluRay\" = @brid; ", conn))

                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("brid", idBr);
                    cmd.Parameters.Add(new NpgsqlParameter("@cur", NpgsqlTypes.NpgsqlDbType.Refcursor) { Direction = ParameterDirection.InputOutput, Value = "cur" });


                    using (var reader = cmd.ExecuteReader())
                    {

                        // Output rows
                        while (reader.Read())
                            qryResult.Add(new Personne
                            {
                                Id = reader.GetInt32(0),
                                Nom = reader.GetString(1),
                                Prenom = reader.GetString(2),
                                DateNaissance = reader.GetDateTime(3),
                                Nationalite = reader.GetString(4),
                            });
                    }
                }

                personne = qryResult.SingleOrDefault();
            }
            finally
            {
                if (conn != null)
                {
                    tran.Commit();
                    conn.Close();
                }
            }
            return personne;
        }

        public List<Personne> GetActeursBr(long idBr)
        {
            NpgsqlConnection conn = null;
            List<Personne> acteurs = new List<Personne>();
            NpgsqlTransaction tran = null;
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=projet;Database=postgres;");
                conn.Open();
                tran = conn.BeginTransaction();
                using (var cmd = new NpgsqlCommand("Select per.\"Id\", per.\"Nom\", per.\"Prenom\", per.\"DateNaissance\", per.\"Nationalite\" from \"BluRayTheque\".\"Acteur\" as a INNER JOIN \"BluRayTheque\".\"Personne\" as per ON per.\"Id\" = a.\"IdActeur\" where a.\"IdBluRay\" = @brid; ", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("brid", idBr);
                    cmd.Parameters.Add(new NpgsqlParameter("@cur", NpgsqlTypes.NpgsqlDbType.Refcursor) { Direction = ParameterDirection.InputOutput, Value = "cur" });

                    using (var reader = cmd.ExecuteReader())
                    {

                        // Output rows
                        while (reader.Read())
                            acteurs.Add(new Personne
                            {
                                Id = reader.GetInt32(0),
                                Nom = reader.GetString(1),
                                Prenom = reader.GetString(2),
                                DateNaissance = reader.GetDateTime(3),
                                Nationalite = reader.GetString(4),
                            });
                    }
                }
            }
            finally
            {
                if (conn != null)
                {
                    tran.Commit();
                    conn.Close();
                }
            }
            return acteurs;
        }
    }
}

