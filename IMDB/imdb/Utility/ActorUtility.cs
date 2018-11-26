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
    public class ActorUtility
    {
        public static List<Actor> GetActors()
        {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            List<Actor> genList = new List<Actor>();

            try
            {
                scmd.CommandText = "SELECT * FROM actors ";
                MySqlDataReader reader = scmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Actor xyz = new Actor();
                        xyz.actid = reader.GetInt32(reader.GetOrdinal("actid"));
                        xyz.actname = reader.IsDBNull(reader.GetOrdinal("actname")) ? "" : reader.GetString(reader.GetOrdinal("actname"));
                        xyz.actsex = reader.IsDBNull(reader.GetOrdinal("actsex")) ? "" : reader.GetString(reader.GetOrdinal("actsex"));
                        xyz.actdob = reader.IsDBNull(reader.GetOrdinal("actdob")) ? (DateTime?)null : Convert.ToDateTime(reader.GetString(reader.GetOrdinal("actdob")));
                        xyz.actbio = reader.IsDBNull(reader.GetOrdinal("actbio")) ? "" : reader.GetString(reader.GetOrdinal("actbio"));
						
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
        public static Actor GetActor(int id)
        {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            Actor xyz = new Actor();

            try
            {

                scmd.CommandText = "SELECT * FROM actors where actid=@id";
                scmd.Parameters.AddWithValue("actid", id);
                scmd.Prepare();
                MySqlDataReader reader = scmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    xyz.actname = reader.IsDBNull(reader.GetOrdinal("actname")) ? "" : reader.GetString(reader.GetOrdinal("actname"));
                    xyz.actsex = reader.IsDBNull(reader.GetOrdinal("actsex")) ? "" : reader.GetString(reader.GetOrdinal("actsex"));
                    xyz.actdob = reader.IsDBNull(reader.GetOrdinal("actdob")) ? (DateTime?)null  : Convert.ToDateTime(reader.GetString(reader.GetOrdinal("actdob")));
                    xyz.actbio = reader.IsDBNull(reader.GetOrdinal("actbio")) ? "" : reader.GetString(reader.GetOrdinal("actbio"));
					
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
       
        public static BaseResponse SaveActor(Actor value)
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
                scmd.CommandText = "INSERT INTO actors( actname, actsex, actdob, actbio) VALUES( @actname, @actsex, @actdob, @actbio);";
                scmd.Parameters.AddWithValue("actname", value.actname);
                scmd.Parameters.AddWithValue("actsex", value.actsex);
                scmd.Parameters.AddWithValue("actdob", value.actdob);
                scmd.Parameters.AddWithValue("actbio", value.actbio);

                scmd.Prepare();
                scmd.ExecuteNonQuery();

                br.lid = scmd.LastInsertedId;
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
        public static BaseResponse UpdateActor(int id, Actor value)
        {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            BaseResponse br = new BaseResponse();
            br.status = "error";
            try
            {
                scmd.CommandText = "UPDATE actors SET =@, actname=@actname, actsex=@actsex, actdob=@actdob, actbio=@actbio,  WHERE actid=@id";
                scmd.Parameters.AddWithValue("actname", value.actname);
                scmd.Parameters.AddWithValue("actsex", value.actsex);
                scmd.Parameters.AddWithValue("actdob", value.actdob);
                scmd.Parameters.AddWithValue("actbio", value.actbio);

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
		
		public static BaseResponse DeleteActor(int id)
        {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            BaseResponse br = new BaseResponse();
            br.status = "error";
            try
            {
                scmd.CommandText = "DELETE FROM actors WHERE actid=@id";
				scmd.Parameters.AddWithValue("actid", id);
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
