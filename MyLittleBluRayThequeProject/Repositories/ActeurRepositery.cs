using Microsoft.AspNetCore.Mvc;
using MyLittleBluRayThequeProject.DTOs;
using Npgsql;

namespace MyLittleBluRayThequeProject.Repositories
{
    public class ActeurRepositery : Controller
    {
        public ActeurRepositery(){

            }
        public List<Personne> GetActorsfromBluRay(long Id)
        {
            NpgsqlConnection conn = null;
            List<Personne> result = new List<Personne>();
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=network;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("SELECT  \"Personne\".\"Nom\", \"Personne\".\"Prenom\"  FROM  \"BluRayTheque\".\"Acteur\", \"BluRayTheque\".\"Personne\" where \"Acteur\".\"IdBluRay\" = @i and \"Acteur\".\"IdActeur\" = \"Personne\".\"Id\"", conn);
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
    }
}
