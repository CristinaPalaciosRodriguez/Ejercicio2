﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataAccess
{
    public class DataAccess
    {
        string databaseConnection = ConfigurationManager.ConnectionStrings["EscuelaBD"].ConnectionString;
        public void SaveAlumno(Alumno alumno)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(databaseConnection))
                {
                    SqlCommand command = new SqlCommand("[Escuela].[SaveAlumno]", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter nombreParameter = new SqlParameter();
                    nombreParameter.ParameterName = "Nombre";
                    nombreParameter.Value = alumno.Nombre;

                    SqlParameter apellidosParameter = new SqlParameter();
                    apellidosParameter.ParameterName = "Apellidos";
                    apellidosParameter.Value = alumno.Apellidos;

                    SqlParameter promedioParameter = new SqlParameter();
                    promedioParameter.ParameterName = "Promedio";
                    promedioParameter.Value = alumno.Promedio;

                    command.Parameters.Add(nombreParameter);
                    command.Parameters.Add(apellidosParameter);
                    command.Parameters.Add(promedioParameter);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public List<Alumno> GetAlumnosList()
        {
            List<Alumno> resultado = new List<Alumno>();
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnection))
                    
                {
                    SqlCommand command = new SqlCommand("[Escuela].[GetAlumnoList]", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Alumno alumno = new Alumno();
                        alumno.Id = int.Parse(reader["Id"].ToString());
                        alumno.Nombre = reader["Nombre"].ToString();
                        alumno.Apellidos = reader["Apellidos"].ToString();
                        alumno.Promedio = decimal.Parse(reader["Promedio"].ToString());
                        resultado.Add(alumno);
                    }

                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return resultado;
        }

        public void UpdAlumno(Alumno alumno)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnection))
                {
                    SqlCommand command = new SqlCommand("[Escuela].[UpdAlumno]", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter idParameter = new SqlParameter();
                    idParameter.ParameterName = "Id";
                    idParameter.Value = alumno.Id;

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter nombreParameter = new SqlParameter();
                    nombreParameter.ParameterName = "Nombre";
                    nombreParameter.Value = alumno.Nombre;

                    SqlParameter apellidosParameter = new SqlParameter();
                    apellidosParameter.ParameterName = "Apellidos";
                    apellidosParameter.Value = alumno.Apellidos;

                    SqlParameter promedioParameter = new SqlParameter();
                    promedioParameter.ParameterName = "Promedio";
                    promedioParameter.Value = alumno.Promedio;

                    command.Parameters.Add(idParameter);
                    command.Parameters.Add(nombreParameter);
                    command.Parameters.Add(apellidosParameter);
                    command.Parameters.Add(promedioParameter);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void DelAlumno(Alumno alumno)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnection))
                {
                    SqlCommand command = new SqlCommand("[Escuela].[DelAlumno]", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter idParameter = new SqlParameter();
                    idParameter.ParameterName = "Id";
                    idParameter.Value = alumno.Id;

                    command.Parameters.Add(idParameter);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
