using MyLittleBluRayThequeProject.DTOs;
using Npgsql;

namespace MyLittleBluRayThequeProject.Repositories
{
    public class BluRayRepository
    {
        /// <summary>
        /// Consctructeur par défaut
        /// </summary>
        public BluRayRepository()
        {

        }

        public List<BluRay> GetListeBluRay()
        {
            NpgsqlConnection conn = null;
            List<BluRay> result = new List<BluRay>();
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=network;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("SELECT \"Id\", \"Titre\", \"Duree\", \"Version\" ,\"DateSortie\" FROM \"BluRayTheque\".\"BluRay\"", conn);

                // Execute the query and obtain a result set
                NpgsqlDataReader dr = command.ExecuteReader();

                // Output rows
                while (dr.Read())
                    result.Add(new BluRay
                    {
                        Id = long.Parse(dr[0].ToString()),
                        Titre = dr[1].ToString(),
                        Duree = long.Parse(dr[2].ToString()),
                        Version = dr[3].ToString(),
                        DateSortie = DateTime.Parse(dr[4].ToString()),
                    });

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Récupération d'un BR par son Id
        /// </summary>
        /// <param name="Id">l'Id du bluRay</param>
        /// <returns></returns>
        public BluRay GetBluRay(long Id)
        {
            NpgsqlConnection conn = null;
            BluRay result = new BluRay();
            try
            {
                List<BluRay> qryResult = new List<BluRay>();
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=network;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("SELECT \"Id\", \"Titre\", \"Duree\", \"Version\" , \"DateSortie\", \"Disponible\" FROM \"BluRayTheque\".\"BluRay\" where \"Id\" = @p", conn);
                command.Parameters.AddWithValue("p", Id);

                // Execute the query and obtain a result set
                NpgsqlDataReader dr = command.ExecuteReader();

                // Output rows
                while (dr.Read())
                    qryResult.Add(new BluRay
                    {
                        Id = long.Parse(dr[0].ToString()),
                        Titre = dr[1].ToString(),
                        Duree = long.Parse(dr[2].ToString()),
                        Version = dr[3].ToString(),
                        DateSortie = DateTime.Parse(dr[4].ToString()),
                        Disponible = Boolean.Parse(dr[5].ToString()),  
                    });

                result = qryResult.SingleOrDefault();

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return result;
        }

        public long CreateBluRay(string titre, long duree, DateTime date, string version, Boolean disponible)
        {
            NpgsqlConnection conn = null;
            long idReal = -1;
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=network;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("Insert into \"BluRayTheque\".\"BluRay\" (\"Titre\", \"Duree\",\"DateSortie\",\"Version\",\"Disponible\") values (@titre, @duree , @date, @version, @disponible) RETURNING \"Id\"", conn);

                command.Parameters.AddWithValue("titre", titre);
                command.Parameters.AddWithValue("duree", duree);
                command.Parameters.AddWithValue("date", date);
                command.Parameters.AddWithValue("version", version);
                command.Parameters.AddWithValue("disponible", disponible);
                // Execute the query and obtain a result set
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    idReal = long.Parse((dr[0]).ToString());
                }

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return idReal;
        }


        public void supprimeFilm(long Id)
        {
            NpgsqlConnection conn = null;
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=network;Database=postgres;");
                conn.Open();
               
                //Supprimer les liens avec les langues, les acteurs, les realisateurs et les scenaristes

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM \"BluRayTheque\".\"BluRay\" where \"Disponible\" = true AND \"Titre\" IS NOT NULL AND \"Emprunt\" = true AND \"Id\" = @id", conn);
                command.Parameters.AddWithValue("id", Id);


                // Execute the query and obtain a result set
                NpgsqlDataReader dr = command.ExecuteReader();

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void DeleteBluRay(long Id)
        {
            NpgsqlConnection conn = null;
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=network;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("UPDATE \"BluRayTheque\".\"BluRay\" SET \"Emprunt\" = true, \"Disponible\" = true WHERE \"Titre\" IS NOT NULL AND \"Id\" = @id", conn);
                command.Parameters.AddWithValue("id", Id);

                // Execute the query and obtain a result set
                NpgsqlDataReader dr = command.ExecuteReader();

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void Empruter(long Id)
        {
            NpgsqlConnection conn = null;
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=network;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("UPDATE \"BluRayTheque\".\"BluRay\" SET \"Emprunt\" = false, \"Disponible\" = false WHERE  \"Titre\" IS NOT NULL AND \"Disponible\" = true AND \"Id\" = @id", conn);
                command.Parameters.AddWithValue("id", Id);

                // Execute the query and obtain a result set
                NpgsqlDataReader dr = command.ExecuteReader();

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }

        public BluRay Dispo(long Id)
        {
            NpgsqlConnection conn = null;
            BluRay result = new BluRay();
            try
            {
                List<BluRay> qryResult = new List<BluRay>();
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=network;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("SELECT \"Disponible\" FROM \"BluRayTheque\".\"BluRay\" WHERE  \"Titre\" IS NOT NULL AND \"Id\" = @id", conn);
                command.Parameters.AddWithValue("id", Id);

                // Execute the query and obtain a result set
                NpgsqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                    qryResult.Add(new BluRay
                    {
                        Disponible = Boolean.Parse(dr[0].ToString()),

                    }); 

                result = qryResult.SingleOrDefault();

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return result;
        }

        public void RendreBluRay(long Id)
        {
            NpgsqlConnection conn = null;
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=network;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("UPDATE \"BluRayTheque\".\"BluRay\" SET \"Emprunt\" = true, \"Disponible\" = true WHERE  \"Titre\" IS NOT NULL AND \"Disponible\" = false AND \"Id\" = @id", conn);
                command.Parameters.AddWithValue("id", Id);

                // Execute the query and obtain a result set
                NpgsqlDataReader dr = command.ExecuteReader();

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }

        public List<Langue> GetLangues()
        {
            NpgsqlConnection conn = null;
            List<Langue> result = new List<Langue>();
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=network;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("SELECT \"Id\", \"Langue\" FROM \"BluRayTheque\".\"RefLangue\"", conn);

                // Execute the query and obtain a result set
                NpgsqlDataReader dr = command.ExecuteReader();

                // Output rows
                while (dr.Read())
                    result.Add(new Langue
                    {
                        Id = long.Parse(dr[0].ToString()),
                        Valeur = dr[1].ToString(),
                       
                    });

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return result;

        }
        public List<BluRay> GetListBluRayEmprunter()
        {
            NpgsqlConnection conn = null;
            List<BluRay> result = new List<BluRay>();
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=projet;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("SELECT \"Id\", \"Titre\", \"Duree\", \"Version\" ,\"DateSortie\" FROM \"BluRayTheque\".\"BluRay\"  WHERE \"BluRay\".\"Disponible\" = false", conn);

                // Execute the query and obtain a result set
                NpgsqlDataReader dr = command.ExecuteReader();

                // Output rows
                while (dr.Read())
                    result.Add(new BluRay
                    {
                        Id = long.Parse(dr[0].ToString()),
                        Titre = dr[1].ToString(),
                        Duree = long.Parse(dr[2].ToString()),
                        Version = dr[3].ToString(),
                        DateSortie = DateTime.Parse(dr[4].ToString()),
                    });

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return result;
        }
    }
}