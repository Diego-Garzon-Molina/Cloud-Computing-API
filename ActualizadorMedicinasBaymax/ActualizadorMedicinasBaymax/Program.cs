using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace ActualizadorMedicinasBaymax
{
    class Program
    {
        public static XmlDocument doc2;
        public static XmlDocument doc3;
       

        static void Main(string[] args)
        {
            string filePath2 = "C:/Users/diego.garzon.BABEL-SI/Downloads/prescripcion/DICCIONARIO_UNIDAD_CONTENIDO.xml";
            doc2 = new XmlDocument();
            doc2.Load(filePath2);
            string filePath3 = "C:/Users/diego.garzon.BABEL-SI/Downloads/prescripcion/DICCIONARIO_PRINCIPIOS_ACTIVOS.xml";
            doc3 = new XmlDocument();
            doc3.Load(filePath3);
            string filePath = "C:/Users/diego.garzon.BABEL-SI/Downloads/prescripcion/Prescripcion.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
             {
                 listar(node.ChildNodes);

             }
         
        }
        public static void listar(XmlNodeList lista)
        {
            Medicina medicina = new Medicina();
            foreach (XmlNode node in lista)
            {
                if (node.Name.Equals("des_nomco"))
                {
                    medicina.NombreMedicina = node.InnerText;
                }
                if (node.Name.Equals("cod_nacion"))
                {
                    medicina.CodigoNacional = float.Parse(node.InnerText);
                }
                if (node.Name.Equals("contenido"))
                {
                   medicina.Contenido =node.InnerText;
                    medicina.Contenido = medicina.Contenido.Replace(",", "");

                }
                if (node.Name.Equals("unid_contenido"))
                {
                    medicina.UnidadContenido = buscarUnidadContenido(node.InnerText);
                }
                if (node.Name.Equals("formasfarmaceuticas"))
                {
                    medicina.PrincipiosActivos = listarPrincipiosActivos(node.ChildNodes);
                    medicina.PrincipiosActivos = decodificarPrincipiosActivos(medicina.PrincipiosActivos);
                    if (medicina.PrincipiosActivos.Length == 0) medicina.PrincipiosActivos = new String[1];
                   
                }
            }
            if(medicina.CodigoNacional!=0) postMedicina(medicina);

        }
        public static string[] listarPrincipiosActivos(XmlNodeList lista)
        {
            List<string> principiosActivosArrayList = new List<string>();
            foreach (XmlNode node in lista)
            {
                string pa = buscarPrincipiosActivos(node);
                 if(!pa.Equals(""))principiosActivosArrayList.Add(pa);
            }
            return principiosActivosArrayList.ToArray();
            ;
        }
        public static string buscarPrincipiosActivos(XmlNode node)
        {
            string pa = "";
            if (node.Name.Equals("cod_principio_activo")) pa = node.InnerText;
            else
            {
                foreach (XmlNode nodeAux in node.ChildNodes)
                {
                    return buscarPrincipiosActivos(nodeAux);
                }
            }
            return pa;
        }
        public static string buscarUnidadContenido(string codigo)
        {
           
            foreach (XmlNode node in doc2.DocumentElement.ChildNodes)
            {
                if (node.FirstChild.InnerText.Equals(codigo)) return node.ChildNodes.Item(1).InnerText;
            }
            return "";
        }
        public static string[] decodificarPrincipiosActivos(string[] codigos)
        {
            string[] decodificado = new string[codigos.Length];
            for(int i = 0; i<codigos.Length;i++)
            {
                foreach (XmlNode node in doc3.DocumentElement.ChildNodes)
                {
                    if (node.FirstChild.InnerText.Equals(codigos[i])) decodificado[i] = node.ChildNodes.Item(2).InnerText;
                }
              
            }
            return decodificado;
        }
        public static void postMedicina(Medicina medicina)
        {
            //http://restserviceapibaymax.azurewebsites.net/api/Medicinas
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://restserviceapibaymax.azurewebsites.net/api/Medicinas");
            Encoding utf8 = Encoding.UTF8;
            
            request.Method = "POST";
            request.ContentType = "application/json";
            string json = "{\"NombreMedicina\" : \"" + medicina.NombreMedicina + "\",\"PrincipiosActivos\":";
             if(medicina.PrincipiosActivos.Length == 1)  json += "\"['" + medicina.PrincipiosActivos[0] + "']\"";
            else {
                json += "\"[";
                for (int i=0; i<medicina.PrincipiosActivos.Length-1; i++)
                {
                    json += "'" + medicina.PrincipiosActivos[i] + "',";
                }
                json +="'" +medicina.PrincipiosActivos[medicina.PrincipiosActivos.Length-1]+ "']\"";

            }
            json += ",\"CodigoNacional\":" + medicina.CodigoNacional + ",\"Contenido\":" + medicina.Contenido + ",\"UnidadContenido\":\"" + medicina.UnidadContenido + "\"}";
            System.Diagnostics.Debug.WriteLine(json);
            byte[] bytes = Encoding.Default.GetBytes(json);
            json = Encoding.UTF8.GetString(bytes);
            //request.ContentLength = json.Length;
            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.UTF8);
            requestWriter.Write(json);
            requestWriter.Close();

            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(response);
                responseReader.Close();
            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine("----------------");
                System.Diagnostics.Debug.WriteLine(e.Message);

            
            }

        }


    }
}

public class Medicina
{
    public int MedicinaId { get; set; }
    public string NombreMedicina { get; set; }
    public string[] PrincipiosActivos { get; set; }
    public float CodigoNacional { get; set; }
    public string Contenido { get; set; }
    public string UnidadContenido { get; set; }
    public Medicina()
    {
        this.NombreMedicina = "";
        this.PrincipiosActivos = new string[1];
        this.CodigoNacional = 0.0F;
        this.Contenido = "1";
        this.UnidadContenido = "";
    }
}
