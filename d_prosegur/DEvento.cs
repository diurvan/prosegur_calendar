using e_prosegur;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace d_prosegur
{
    public class DEvento
    {
        public List<EventModel> listaEventos(EventModel obj)
        {
            List<EventModel> response = new List<EventModel>();
            try {
                using (SqlConnection cn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Parameters.AddWithValue("@Accion", "S");
                        cmd.Parameters.AddWithValue("@Id", obj.id);
                        cmd.Parameters.AddWithValue("@Correo", obj.email ?? "");
                        cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.Connection = cn;
                        cmd.CommandText = "USP_CRUD_EVENTO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cn.Open();

                        SqlDataReader lst = cmd.ExecuteReader();

                        if (lst.HasRows)
                        {
                            while (lst.Read())
                            {
                                EventModel objretorno = new EventModel()
                                {
                                    id = Convert.ToInt32(lst["Id"].ToString()),
                                    email = lst["Email"].ToString(),
                                    nombre = lst["Nombre"].ToString(),
                                    //start = lst["Fecha"].ToString(),
                                    start = DateTime.Parse(lst["Fecha"].ToString()).ToString("MM/dd/yyyy"),
                                    evaluacion = int.Parse(lst["Evaluacion"].ToString())
                                };
                                response.Add(objretorno);
                            }
                        }
                    }
                }
            }
            catch (Exception ex){
                throw ex;
            }
            return response;
        }

        public int Create(EventModel obj)
        {
            int iRetorno = -1;
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Parameters.AddWithValue("@Accion", "C");
                cmd.Parameters.AddWithValue("@Fecha", DateTime.Parse(obj.start).ToString("yyyy-MM-dd"));
                //cmd.Parameters.AddWithValue("@Fecha", obj.start);
                cmd.Parameters.AddWithValue("@Correo", obj.email);
                cmd.Parameters.AddWithValue("@Nombre", obj.nombre);
                cmd.Parameters.AddWithValue("@Evaluacion", obj.evaluacion);
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.CommandTimeout = 0;
                ExecuteNonQuery("USP_CRUD_EVENTO", cmd, 0);
                iRetorno = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRetorno;
        }





        public int ExecuteNonQuery(string sSPName, SqlCommand cmd, int iTipoConexion)
        {
            SqlConnection cn = GetConnection();
            try
            {
                if (cmd == null) { cmd = new SqlCommand(); }
                cmd.Connection = cn;
                cmd.CommandText = sSPName;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                int response = cmd.ExecuteNonQuery();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open) { cn.Close(); cn.Dispose(); }
                cmd.Dispose();
            }
        }
        public SqlConnection GetConnection()
        {
            try
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["bbdd_prosegur"].ToString();
                return cn;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
