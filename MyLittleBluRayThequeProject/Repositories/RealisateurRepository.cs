using Microsoft.AspNetCore.Mvc;
using MyLittleBluRayThequeProject.DTOs;
using Npgsql;

namespace MyLittleBluRayThequeProject.Repositories
{
    public class RealisateurRepository 
    {
        public RealisateurRepository()
        {
        }
        public List<Personne> GetRealisateurfromBluRay(long Id)
        {
            NpgsqlConnection conn = null;
            List<Personne> result = new List<Personne>();
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=projet;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("SELECT  \"Personne\".\"Nom\", \"Personne\".\"Prenom\"  FROM  \"BluRayTheque\".\"Realisateur\", \"BluRayTheque\".\"Personne\" where \"Realisateur\".\"IdBluRay\" = @i and \"Realisateur\".\"IdRealisateur\" = \"Personne\".\"Id\"", conn);
                command.Parameters.AddWithValue("i", Id);

                // Execute the query and obtain a result set
                NpgsqlDataReader dr = command.ExecuteReader();

                // Output rows
                while (dr.Read())
                    result.Add(new Personne
                    {
                        Nom = dr[0].ToString(),
                        Prenom = dr[1].ToString(),
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
        public void createRealisateur(long idBr, long idReal)
        {
            NpgsqlConnection conn = null;
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=projet;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("Insert into \"BluRayTheque\".\"Realisateur\" (\"IdBluRay\", \"IdRealisateur\") values (@idBr, @idReal)", conn);

                command.Parameters.AddWithValue("idBr", idBr);
                command.Parameters.AddWithValue("idReal", idReal);
              
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
        public void DeleteRealisateur(long idBr)
        {
            NpgsqlConnection conn = null;
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=projet;Database=postgres;");
                conn.Open();

                //Supprimer les liens avec les langues, les acteurs, les realisateurs et les scenaristes

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM \"BluRayTheque\".\"Realisateur\" where \"Realisateur\".\"IdBluRay\" = @id", conn);
                command.Parameters.AddWithValue("id", idBr);


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
    }
}
