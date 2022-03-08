using InterfazEquationFiduciario;
using InterfazEquationFiduciario.Data;
using InterfazEquationFiduciario.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Consumir_InterfazEquationFiduciario
{
    public partial class Form1 : Form
    {

        

        Miscelaneas miscelaneas;
        Encriptacion encriptacion;
        FuncionesBd bd;

    

        public Form1()
        {
            InitializeComponent();

            miscelaneas = new Miscelaneas();
            encriptacion = new Encriptacion();
            Log.EscribeLog = true;

            miscelaneas.paso1 = (Funciones.getValueAppConfig("1", "paso") == "1" ) ? true : false;
            miscelaneas.paso2 = (Funciones.getValueAppConfig("2", "paso") == "1" ) ? true : false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                
                EstablecerParametros();

                if (miscelaneas.paso1)
                {
                    ValidarCarpeta(miscelaneas.rutaPathModelos);
                    ValidarCarpeta(miscelaneas.rutaPathTransfer);
                    ValidarCarpeta(miscelaneas.rutaPathDatos);

                    miscelaneas.nombreArchivo = miscelaneas.rutaPathDatos + "EQFIDTKT" + DateTime.Now.ToString("ddMMyy") + ".TXT";
                    miscelaneas.nombreArchivoDtt = miscelaneas.rutaPathModelos + "EQFIDTKT.DTT";
                    miscelaneas.nombreArchivoDttDestino = miscelaneas.rutaPathTransfer + "EQFIDTKT" + DateTime.Now.ToString("ddMMyy") + ".DTT";
                    miscelaneas.nombreArchivoFDF = miscelaneas.rutaPathModelos + "EQFIDTKTF.FDF";
                    miscelaneas.nombreArchivoFDFDestino = miscelaneas.rutaPathTransfer + "EQFIDTKTF.FDF";


                    if (ConectDB())
                    {
                        miscelaneas.gs_sql = @"
                        select 
	                        DISTINCT contrato=convert(varchar,ag.prefijo_agencia) + cf.CUENTA_CLIENTE,
	                        GETDATE() [fecha],
	                        TIPO_OPERACION  
                        from 
	                        catalogos..CLIENTE_FIDEICOMISO cf with (nolock) 
	                        join catalogos..cliente cl with (nolock) on cf.CUENTA_CLIENTE=cl.CUENTA_CLIENTE AND CF.AGENCIA=CL.AGENCIA  
	                        join catalogos..AGENCIA ag with (nolock) on cf.agencia=ag.agencia 
	                        join ticket..producto_contratado PC with (nolock) on pc.CUENTA_CLIENTE=cl.CUENTA_CLIENTE 
	                        join ticket..STATUS_PRODUCTO sp with (nolock) on pc.status_producto=sp.status_producto 
                        where 
	                        ESTATUS in (1,4) 
	                        and 
	                        ( TIPO_OPERACION in ('A','M')) 
	                        and 
	                        pc.status_producto not in (8039) 
	                        and 
	                        pc.producto in (8009) 
                        order by 1 desc;
                    ";

                        string vData = "";
                        SqlDataReader dr = bd.ejecutarConsulta(miscelaneas.gs_sql);

                        using (StreamWriter outputFile = new StreamWriter(miscelaneas.nombreArchivo, append: true))
                        {
                            if (dr != null)
                            {
                                while (dr.Read())
                                {
                                    vData = dr.GetString(0) + "-" + dr.GetDateTime(1).ToString("ddMMyyyy") + dr.GetString(2);
                                    outputFile.WriteLine(vData);
                                }
                                dr.Close();
                            }

                        }
                    }

                    if (!File.Exists(miscelaneas.nombreArchivoDttDestino))
                    {
                        File.Copy(miscelaneas.nombreArchivoDtt, miscelaneas.nombreArchivoDttDestino);
                    }
                    if (!File.Exists(miscelaneas.nombreArchivoFDFDestino))
                    {
                        File.Copy(miscelaneas.nombreArchivoFDF, miscelaneas.nombreArchivoFDFDestino);
                    }

                    miscelaneas.EquipoAS = encriptacion.Decrypt(Funciones.getValueAppConfig("Equipo", "AS400"));
                    miscelaneas.LibreriaAS = encriptacion.Decrypt(Funciones.getValueAppConfig("Libreria", "AS400"));
                    miscelaneas.ArchivoAS = encriptacion.Decrypt(Funciones.getValueAppConfig("Archivo", "AS400"));


                    EscribirParametroDtt("HostName", miscelaneas.nombreArchivoDttDestino, miscelaneas.EquipoAS, "HostInfo");
                    EscribirParametroDtt("HostFile", miscelaneas.nombreArchivoDttDestino, miscelaneas.LibreriaAS + "/" + miscelaneas.ArchivoAS, "HostInfo");
                    EscribirParametroDtt("PCFile", miscelaneas.nombreArchivoDttDestino, miscelaneas.nombreArchivo, "ClientInfo");
                    EscribirParametroDtt("FDFFile", miscelaneas.nombreArchivoDttDestino, miscelaneas.nombreArchivoFDFDestino, "ClientInfo");

                }
                if (miscelaneas.paso2)
                {
                    EjecutaTransfer(miscelaneas.msPathFTPApp, miscelaneas.nombreArchivoDttDestino);
                }
            }
            catch (Exception ex)
            {
                Log.Escribe(ex);
            }
        }

        private void EstablecerParametros()
        {
            try
            {
                miscelaneas.ApliPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                if (Funciones.Right(miscelaneas.ApliPath, 1) == "\\")
                {
                    miscelaneas.ApliPath = Funciones.Left(miscelaneas.ApliPath, miscelaneas.ApliPath.Length - 1);
                }
              
                string section = "conexion";

                string a = Funciones.getValueAppConfig("DBCata", section);
                miscelaneas.gsCataDB = encriptacion.Decrypt(a);
                miscelaneas.gsDSNDB = encriptacion.Decrypt(Funciones.getValueAppConfig("DBDSN", section));
                miscelaneas.gsSrvr = encriptacion.Decrypt(Funciones.getValueAppConfig("DBSrvr", section));
                miscelaneas.gsUserDB = encriptacion.Decrypt(Funciones.getValueAppConfig("DBUser", section));
                miscelaneas.gsPswdDB = encriptacion.Decrypt(Funciones.getValueAppConfig("DBPswd", section));
                miscelaneas.gsNameDB = encriptacion.Decrypt(Funciones.getValueAppConfig("DBName", section));

                miscelaneas.rutaArchivo = Funciones.getValueAppConfig("RutaArchivo", "Envfideicomiso");

                miscelaneas.rutaPathModelos = Funciones.getValueAppConfig("PathModelos", "RUTAS");
                miscelaneas.rutaPathTransfer = Funciones.getValueAppConfig("PathTransfer", "RUTAS");
                miscelaneas.rutaPathDatos = Funciones.getValueAppConfig("PathDatos", "RUTAS");

                miscelaneas.GsUSerAS400 = Funciones.getValueAppConfig("Usuario", "AS400");
                miscelaneas.GsPasswordAS400 = Funciones.getValueAppConfig("PSW", "AS400");

                miscelaneas.msPathFTPApp = Funciones.getValueAppConfig("ClientAccess", "RUTAS");
                
            }
            catch (Exception ex)
            {
                Log.Escribe(ex);
            }
        }

        private bool EscribirParametroDtt(string key, string path, string value, string section)
        {
            try
            {

                if (File.Exists(path))
                {
                    string buscar = $"{key}=";
                    string remplazar = $"{key}={value}";
                    string text = File.ReadAllText(path).Replace(buscar, remplazar);
                    File.WriteAllText(path, text);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Escribe(ex);
                return false;
            }
        }

        private void EjecutaTransfer(string path,string nombreArchivoDttDestino)
        {
            try
            {
                Process p = new Process();
                p.EnableRaisingEvents = false;
                p.StartInfo.FileName = $"{path}cwbtf.exe";
                p.StartInfo.Arguments = nombreArchivoDttDestino;
                p.StartInfo.CreateNoWindow = false;
                p.Start();
                p.WaitForExit();
                Thread.Sleep(5000);

            }
            catch (Exception ex)
            {
                Log.Escribe("Error al abrir el ejecutable ", "Error");
                Log.Escribe(ex);
            }
        }


        private void Erase(ref string[] Parametros)
        {
            int tam = Parametros.Length;

            for(int i = 0; i < tam; i++)
            {
                Parametros[i] = String.Empty;
            }
        }

        public bool ConectDB()
        {
            bool ConectDB;
            try
            {
                string conn_str = $"Data source ={miscelaneas.gsSrvr}; uid ={miscelaneas.gsUserDB}; PWD ={miscelaneas.gsPswdDB}; initial catalog = {miscelaneas.gsNameDB}";
                bd = new FuncionesBd(conn_str);

                ConectDB = true;

            }
            catch (Exception ex)
            {
                ConectDB = false;
                Log.Escribe(ex, "Error");
            }
            return ConectDB;
        }

        public bool ValidarCarpeta(string ruta)
        {
            try
            {
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                return true;
            }
            catch(Exception ex)
            {
                Log.Escribe(ex);
                return false;
            }
        }

    }
}
