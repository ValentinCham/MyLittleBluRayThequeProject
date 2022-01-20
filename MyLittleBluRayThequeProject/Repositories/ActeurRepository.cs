using Microsoft.AspNetCore.Mvc;
using MyLittleBluRayThequeProject.DTOs;
using Npgsql;

namespace MyLittleBluRayThequeProject.Repositories
{
    public class ActeurRepository : Controller
    {
        public ActeurRepository(){

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
        public void createActeurs(long idBr, long idAct)
        {
            NpgsqlConnection conn = null;
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=network;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("Insert into \"BluRayTheque\".\"Acteur\" (\"IdBluRay\", \"IdActeur\") values (@idBr, @idAct)", conn);

                command.Parameters.AddWithValue("idBr", idBr);
                command.Parameters.AddWithValue("idAct", idAct);

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
        public void DeleteActeurs(long idBr)
        {
            NpgsqlConnection conn = null;
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=network;Database=postgres;");
                conn.Open();

                //Supprimer les liens avec les langues, les acteurs, les realisateurs et les scenaristes

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM \"BluRayTheque\".\"Acteur\" where \"Acteur\".\"IdBluRay\" = @id", conn);
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
