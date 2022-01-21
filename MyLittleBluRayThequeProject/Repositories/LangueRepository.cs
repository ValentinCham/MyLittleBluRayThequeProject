using Microsoft.AspNetCore.Mvc;
using MyLittleBluRayThequeProject.DTOs;
using Npgsql;
namespace MyLittleBluRayThequeProject.Repositories
{
    public class LangueRepository
    {

        public LangueRepository()
        {
        }
        public List<Langue> GetLanguefromBluRay(long Id)
        {
            NpgsqlConnection conn = null;
            List<Langue> result = new List<Langue>();
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=projet;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("SELECT  \"RefLangue\".\"Id\", \"RefLangue\".\"Langue\"  FROM  \"BluRayTheque\".\"RefLangue\", \"BluRayTheque\".\"BluRayLangue\" where \"BluRayLangue\".\"IdBluRay\" = @i and \"RefLangue\".\"Id\" = \"BluRayLangue\".\"IdLangue\"", conn);
                command.Parameters.AddWithValue("i", Id);

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
        public void createLangue(long idBr, long idLangue)
        {
            NpgsqlConnection conn = null;
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=projet;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("Insert into \"BluRayTheque\".\"BluRayLangue\" (\"IdBluRay\", \"IdLangue\") values (@idBr, @idLangue)", conn);

                command.Parameters.AddWithValue("idBr", idBr);
                command.Parameters.AddWithValue("idLangue", idLangue);

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
        public void DeleteLangues(long idBr)
        {
            NpgsqlConnection conn = null;
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=projet;Database=postgres;");
                conn.Open();

                //Supprimer les liens avec les langues, les acteurs, les realisateurs et les scenaristes

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM \"BluRayTheque\".\"BluRayLangue\" where \"BluRayLangue\".\"IdBluRay\" = @id", conn);
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
