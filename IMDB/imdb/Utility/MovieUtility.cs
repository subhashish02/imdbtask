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
    public class MovieUtility
    {
        public static List<Movie> GetMovies()
        {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            List<Movie> genList = new List<Movie>();

            try
            {
                scmd.CommandText = "SELECT m.*,p.proname FROM movies m inner join producers p on m.proid=p.proid ";
                MySqlDataReader reader = scmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Movie xyz = new Movie();
                        xyz.movid = reader.GetUInt32(reader.GetOrdinal("movid"));
                        xyz.moviname = reader.IsDBNull(reader.GetOrdinal("moviname")) ? "" : reader.GetString(reader.GetOrdinal("moviname"));
                        xyz.movirelyear = reader.IsDBNull(reader.GetOrdinal("movirelyear")) ? 0 : Convert.ToInt32(reader.GetString(reader.GetOrdinal("movirelyear")));
                        xyz.moviplot = reader.IsDBNull(reader.GetOrdinal("moviplot")) ? "" : reader.GetString(reader.GetOrdinal("moviplot"));
                        xyz.moviposter = reader.IsDBNull(reader.GetOrdinal("moviposter")) ? "" : reader.GetString(reader.GetOrdinal("moviposter"));
                        if (!string.IsNullOrWhiteSpace(xyz.moviposter))
                        {
                            xyz.moviposter = "http://localhost/imdbApi/img/" + xyz.moviposter;
                        }
                        xyz.producer = new Producer() {proname= reader.IsDBNull(reader.GetOrdinal("proname")) ? "" : reader.GetString(reader.GetOrdinal("proname")) };
                        xyz.actors = GetMovieActors(xyz.movid);
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
        public static List<Actor> GetMovieActors(long movid)
        {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            List<Actor> genList = new List<Actor>();

            try
            {
                scmd.CommandText = "SELECT a.* FROM actors a inner join movieactor ma on a.actid=ma.actid where ma.movid=@movid ";
                scmd.Parameters.AddWithValue("movid", movid);
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
        public static Movie GetMovie(int id)
        {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            Movie xyz = new Movie();

            try
            {

                scmd.CommandText = "SELECT m.*,p.proname FROM movies m inner join producers p on m.proid=p.proid where m.movid=@movid";
                scmd.Parameters.AddWithValue("movid", id);
                scmd.Prepare();
                MySqlDataReader reader = scmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    xyz.movid = reader.GetUInt32(reader.GetOrdinal("movid"));
                    xyz.moviname = reader.IsDBNull(reader.GetOrdinal("moviname")) ? "" : reader.GetString(reader.GetOrdinal("moviname"));
                    xyz.movirelyear = reader.IsDBNull(reader.GetOrdinal("movirelyear")) ? 0 : Convert.ToInt32(reader.GetString(reader.GetOrdinal("movirelyear")));
                    xyz.moviplot = reader.IsDBNull(reader.GetOrdinal("moviplot")) ? "" : reader.GetString(reader.GetOrdinal("moviplot"));
                    xyz.moviposter = reader.IsDBNull(reader.GetOrdinal("moviposter")) ? "" : reader.GetString(reader.GetOrdinal("moviposter"));
                    if(!string.IsNullOrWhiteSpace(xyz.moviposter))
                    {
                        xyz.moviposter = "http://localhost/imdbApi/img/" + xyz.moviposter;
                    }
                    xyz.producer = new Producer() { proname = reader.IsDBNull(reader.GetOrdinal("proname")) ? "" : reader.GetString(reader.GetOrdinal("proname")) };
                    xyz.actors = GetMovieActors(xyz.movid);

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
       
        public static BaseResponse SaveMovie(Movie value)
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
                var photoName = "";
                if (!string.IsNullOrWhiteSpace(value.posterImg))
                {
                    var localFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/img")+ @"\";
                    photoName = Guid.NewGuid().ToString().Replace("-","") + ".jpg";
                    byte[] imageBytes = Convert.FromBase64String(value.posterImg.Replace("data:image/jpeg;base64,", String.Empty));
                    string imgPath = Path.Combine(localFilePath, photoName);
                    File.WriteAllBytes(imgPath, imageBytes);
                }
                scmd.CommandText = "INSERT INTO movies(  proid, moviname, movirelyear, moviplot, moviposter) VALUES(@proid, @moviname, @movirelyear, @moviplot, @moviposter);";
                scmd.Parameters.AddWithValue("proid", value.producer.proid);
                scmd.Parameters.AddWithValue("moviname", value.moviname);
                scmd.Parameters.AddWithValue("movirelyear", value.movirelyear);
                scmd.Parameters.AddWithValue("moviplot", value.moviplot);
                scmd.Parameters.AddWithValue("moviposter", string.IsNullOrWhiteSpace(photoName)?"": photoName);
                scmd.Prepare();
                scmd.ExecuteNonQuery();
                var movid = scmd.LastInsertedId;
                foreach (var actor in value.actors)
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.AddWithValue("movid", movid);
                    scmd.Parameters.AddWithValue("actid", actor.actid);
                    scmd.CommandText = "INSERT INTO movieactor( actid, movid) VALUES(@actid, @movid)";
                    scmd.ExecuteNonQuery();
                }


                br.lid = movid;
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
        public static BaseResponse UpdateMovie(int id, Movie value)
        {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            BaseResponse br = new BaseResponse();
            br.status = "error";
            try
            {
                var photoName = "";
                if (!string.IsNullOrWhiteSpace(value.posterImg))
                {
                    var localFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/img") + @"\";
                    photoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                    byte[] imageBytes = Convert.FromBase64String(value.posterImg.Replace("data:image/jpeg;base64,", String.Empty));
                    string imgPath = Path.Combine(localFilePath, photoName);
                    File.WriteAllBytes(imgPath, imageBytes);
                }

                scmd.CommandText = "UPDATE movies SET proid=@proid, moviname=@moviname, movirelyear=@movirelyear, moviplot=@moviplot"+ ( !string.IsNullOrWhiteSpace(photoName)? ",moviposter =@moviposter":"")+"  WHERE movid=@movid";
                scmd.Parameters.AddWithValue("proid", value.producer.proid);
                scmd.Parameters.AddWithValue("moviname", value.moviname);
                scmd.Parameters.AddWithValue("movirelyear", value.movirelyear);
                scmd.Parameters.AddWithValue("moviplot", value.moviplot);
                scmd.Parameters.AddWithValue("moviposter", string.IsNullOrWhiteSpace(photoName) ? "" : photoName);
                scmd.Parameters.AddWithValue("movid", value.movid);
                scmd.Prepare();
                scmd.ExecuteNonQuery();

                scmd.CommandText = "delete from movieactor where movid=@movid";
                scmd.ExecuteNonQuery();

                foreach (var actor in value.actors)
                {
                    scmd.Parameters.Clear();
                    scmd.Parameters.AddWithValue("movid", value.movid);
                    scmd.Parameters.AddWithValue("actid", actor.actid);
                    scmd.CommandText = "INSERT INTO movieactor( actid, movid) VALUES(@actid, @movid)";
                    scmd.ExecuteNonQuery();
                }


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
		
		public static BaseResponse DeleteMovie(int id)
        {
            MySqlConnection scon = new MySqlConnection(WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString);
            MySqlCommand scmd = new MySqlCommand();
            scon.Open();
            scmd.Connection = scon;
            BaseResponse br = new BaseResponse();
            br.status = "error";
            try
            {
                scmd.CommandText = "DELETE FROM movies WHERE movid=@id";
				scmd.Parameters.AddWithValue("movid", id);
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
