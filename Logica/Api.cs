using FacturasEnel.Logica;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Enel.Logica
{
    public class Api
    {
        string API = "http://sion.defensoria.gov.co:81/";
        public dynamic Token;



        public async Task<string> RecibirToken()
        {
            try
            {
                string UrlCompleta = API + "~spinzon/visionweb/cac2/mobil/key-dp.php";

                var request = (HttpWebRequest)WebRequest.Create(UrlCompleta);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

                using (Stream strReader = response.GetResponseStream())
                {

                    if (strReader == null)
                    {
                        return "\"No se obtuvo conexion con el servidor\"";
                    }
                    else
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string respuestatoken = objReader.ReadToEndAsync().Result;

                            //Token = JsonConvert.DeserializeObject(respuestatoken);

                            return respuestatoken;

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                // Manejar excepciones
                Consumo.Logger.Error(ex);
                return "\"No se obtuvo conexion con el servidor\"";
            }
        }



        public async Task<string> ObtenerDocumentos()
        {
            try
            {
                string UrlCompleta = API + "~spinzon/visionweb/cac2/mobil/tipo_id.json";

                var request = (HttpWebRequest)WebRequest.Create(UrlCompleta);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

                using (Stream strReader = response.GetResponseStream())
                {

                    if (strReader == null)
                    {
                        return "\"No se obtuvo conexion con el servidor\"";
                    }
                    else
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string respuestatoken = objReader.ReadToEndAsync().Result;

                            //Token = JsonConvert.DeserializeObject(respuestatoken);

                            return respuestatoken;

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                // Manejar excepciones
                Consumo.Logger.Error(ex);
                return "\"No se obtuvo conexion con el servidor\"";
            }
        }




        public async Task<string> ObtenerSexo( string token)
        {
            try
            {
                string UrlCompleta = API + "~spinzon/visionweb/cac2/mobil/genero.json?key_dp="+token;

                var request = (HttpWebRequest)WebRequest.Create(UrlCompleta);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

                using (Stream strReader = response.GetResponseStream())
                {

                    if (strReader == null)
                    {
                        return "\"No se obtuvo conexion con el servidor\"";
                    }
                    else
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string respuestatoken = objReader.ReadToEndAsync().Result;

                            Token = JsonConvert.DeserializeObject(respuestatoken);

                            return respuestatoken;

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                // Manejar excepciones
                Consumo.Logger.Error(ex);
                return "\"No se obtuvo conexion con el servidor\"";
            }



        }


        public async Task<string> ObtenerDepartamento(string token)
        {
            try
            {
                string UrlCompleta = API + "~spinzon/visionweb/cac2/mobil/departamento.php?key_dp=" + token;

                var request = (HttpWebRequest)WebRequest.Create(UrlCompleta);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

                using (Stream strReader = response.GetResponseStream())
                {

                    if (strReader == null)
                    {
                        return "\"No se obtuvo conexion con el servidor\"";
                    }
                    else
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string respuestatoken = objReader.ReadToEndAsync().Result;

                            Token = JsonConvert.DeserializeObject(respuestatoken);

                            return respuestatoken;

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                // Manejar excepciones
                Consumo.Logger.Error(ex);
                return "\"No se obtuvo conexion con el servidor\"";
            }



        }


        public async Task<string> ObtenerMunicipio(string token, string departamento)
        {
            try
            {
                string UrlCompleta = API + "~spinzon/visionweb/cac2/mobil/municipio.php?cod_dpa="+departamento+"&key_dp=" + token;

                var request = (HttpWebRequest)WebRequest.Create(UrlCompleta);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

                using (Stream strReader = response.GetResponseStream())
                {

                    if (strReader == null)
                    {
                        return "\"No se obtuvo conexion con el servidor\"";
                    }
                    else
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string respuestatoken = objReader.ReadToEndAsync().Result;

                            Token = JsonConvert.DeserializeObject(respuestatoken);

                            return respuestatoken;

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                // Manejar excepciones
                Consumo.Logger.Error(ex);
                return "\"No se obtuvo conexion con el servidor\"";
            }



        }



    }




}
