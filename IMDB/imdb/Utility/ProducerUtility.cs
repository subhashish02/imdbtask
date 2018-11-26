using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using imdb.Models;


namespace imdb.Utility
{
    public class ProducerUtility
    {
        public static List<Producer> GetProducers()
        {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            List<Producer> genList = new List<Producer>();

            try
            {
                scmd.CommandText = "SELECT * FROM producers ";
                MySqlDataReader reader = scmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producer xyz = new Producer();
                        xyz.proid = reader.GetInt32(reader.GetOrdinal("proid"));
                        xyz.proname = reader.IsDBNull(reader.GetOrdinal("proname")) ? "" : reader.GetString(reader.GetOrdinal("proname"));
                        xyz.prosex = reader.IsDBNull(reader.GetOrdinal("prosex")) ? "" : reader.GetString(reader.GetOrdinal("prosex"));
                        xyz.prodob = reader.IsDBNull(reader.GetOrdinal("prodob")) ? (DateTime?)null : Convert.ToDateTime(reader.GetString(reader.GetOrdinal("prodob")));
                        xyz.probio = reader.IsDBNull(reader.GetOrdinal("probio")) ? "" : reader.GetString(reader.GetOrdinal("probio"));
						
                        genList.Add(xyz);
                    }
                }
            }
            catch (Exception ee)
            {

            }
            finally
            {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open)
                {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return genList;
        }
        public static Producer GetProducer(int id)
        {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            Producer xyz = new Producer();

            try
            {

                scmd.CommandText = "SELECT * FROM producers where proid=@id";
                scmd.Parameters.AddWithValue("proid", id);
                scmd.Prepare();
                MySqlDataReader reader = scmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    //xyz.proid = "";
                    xyz.proname = reader.IsDBNull(reader.GetOrdinal("proname")) ? "" : reader.GetString(reader.GetOrdinal("proname"));
                    xyz.prosex = reader.IsDBNull(reader.GetOrdinal("prosex")) ? "" : reader.GetString(reader.GetOrdinal("prosex"));
                    xyz.prodob = reader.IsDBNull(reader.GetOrdinal("prodob")) ? (DateTime?)null : Convert.ToDateTime(reader.GetString(reader.GetOrdinal("prodob")));
                    xyz.probio = reader.IsDBNull(reader.GetOrdinal("probio")) ? "" : reader.GetString(reader.GetOrdinal("probio"));
					
                }
            }
            catch (Exception ee)
            {

            }
            finally
            {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open)
                {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return xyz;
        }
       
        public static BaseResponse SaveProducer(Producer value)
        {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            MySqlTransaction t = null;
            scon.Open();
            scmd.Connection = scon;
            BaseResponse br = new BaseResponse();
            br.status = "error";
            t = scon.BeginTransaction();
            scmd.Transaction = t;
            try
            {
                scmd.CommandText = "INSERT INTO producers( proname, prosex, prodob, probio) VALUES( @proname, @prosex, @prodob, @probio);";
                scmd.Parameters.AddWithValue("proname", value.proname);
                scmd.Parameters.AddWithValue("prosex", value.prosex);
                scmd.Parameters.AddWithValue("prodob", value.prodob);
                scmd.Parameters.AddWithValue("probio", value.probio);

                scmd.Prepare();
                scmd.ExecuteNonQuery();


                br.status = "success";
                br.message = "created succefully.";
                
                t.Commit();
            }
            catch (Exception ee)
            {
                t.Rollback();
                br.status = "error";
                br.message = ee.Message;
            }
            finally
            {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open)
                {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return br;
        }
        public static BaseResponse UpdateProducer(int id, Producer value)
        {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            BaseResponse br = new BaseResponse();
            br.status = "error";
            try
            {
                scmd.CommandText = "UPDATE producers SET proname=@proname, prosex=@prosex, prodob=@prodob, probio=@probio,  WHERE proid=@id";
                scmd.Parameters.AddWithValue("proname", value.proname);
                scmd.Parameters.AddWithValue("prosex", value.prosex);
                scmd.Parameters.AddWithValue("prodob", value.prodob);
                scmd.Parameters.AddWithValue("probio", value.probio);

                scmd.Prepare();
                scmd.ExecuteNonQuery();
                br.status = "success";
                br.message = "Updated Successfully.";
            }
            catch (Exception ee)
            {
                br.status = "error";
                br.message = ee.Message;
            }
            finally
            {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open)
                {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return br;
        }
		
		public static BaseResponse DeleteProducer(int id)
        {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            BaseResponse br = new BaseResponse();
            br.status = "error";
            try
            {
                scmd.CommandText = "DELETE FROM producers WHERE proid=@id";
				scmd.Parameters.AddWithValue("proid", id);
                scmd.ExecuteNonQuery();
                br.status = "success";
                br.message = "Deleted Successfully.";
            }
            catch (Exception ee)
            {
                br.status = "error";
                br.message = ee.Message;
            }
            finally
            {
                if (scmd != null)
                    scmd.Dispose();
                if (scon.State == ConnectionState.Open)
                {
                    scon.Dispose();
                    scon.Close();
                }
            }
            return br;
        }
    }
}
