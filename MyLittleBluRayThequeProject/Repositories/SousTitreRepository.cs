using Microsoft.AspNetCore.Mvc;
using MyLittleBluRayThequeProject.DTOs;
using Npgsql;
namespace MyLittleBluRayThequeProject.Repositories
{
    public class SousTitreRepository
    {
        public SousTitreRepository()
        {
        }
        public List<Langue> GetSousTitrefromBluRay(long Id)
        {
            NpgsqlConnection conn = null;
            List<Langue> result = new List<Langue>();
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=network;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("SELECT  \"RefLangue\".\"Id\", \"RefLangue\".\"Langue\"  FROM  \"BluRayTheque\".\"RefLangue\", \"BluRayTheque\".\"BluRaySsTitre\" where \"BluRaySsTitre\".\"IdBluRay\" = @i and \"RefLangue\".\"Id\" = \"BluRaySsTitre\".\"IdssTitreLangue\"", conn);
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
        public void createSousTitre(long idBr, long idss)
        {
            NpgsqlConnection conn = null;
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=network;Database=postgres;");
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("Insert into \"BluRayTheque\".\"BluRaySsTitre\" (\"IdBluRay\", \"IdssTitreLangue\") values (@idBr, @idss)", conn);

                command.Parameters.AddWithValue("idBr", idBr);
                command.Parameters.AddWithValue("idss", idss);

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
        public void DeleteSousTitre(long idBr)
        {
            NpgsqlConnection conn = null;
            try
            {
                // Connect to a PostgreSQL database
                conn = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;Password=network;Database=postgres;");
                conn.Open();


                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM \"BluRayTheque\".\"BluRaySsTitre\" where \"BluRaySsTitre\".\"IdBluRay\" = @id", conn);
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
