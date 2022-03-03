using InterfazEquationFiduciario;
using InterfazEquationFiduciario.Data;
using InterfazEquationFiduciario.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Consumir_InterfazEquationFiduciario
{
    public partial class Form1 : Form
    {

        string lsCommandLine;
        int lnSpacePoint;
        string[] Parametros;
        string rutaIni;
        string rutaArchivo;
        string nombreArchivo;
        string nombreArchivoRes;
        string nombreArchivoDtt;
        string nombreArchivoDttDestino;
        string nombreArchivoFDF;
        string nombreArchivoFDFDestino;

        object fs;
        object a;

        int iniErr;
        string EquipoAS;
        string LibreriaAS;
        string ArchivoAS;
        string UsuarioAS;
        string PswAS;

        long lnAnswer;
        string msPathFTPApp;

        Miscelaneas miscelaneas;
        Encriptacion encriptacion;
        FuncionesBd bd;

        public string gsPswdDB;
        public string gsUserDB;
        public string gsNameDB;
        public string gsCataDB;
        public string gsDSNDB;
        public string gsSrvr;

        public Form1()
        {
            InitializeComponent();

            miscelaneas = new Miscelaneas();
            encriptacion = new Encriptacion();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if(lsCommandLine != null)
                {
                    Erase(ref Parametros);
                    Parametros = lsCommandLine.Split('-');

                    miscelaneas.DEFAULT_SRVR = Parametros[0].Trim();
                    miscelaneas.GsUSer = Parametros[1].Trim();
                    miscelaneas.GsPassword = Parametros[2].Trim();
                    miscelaneas.DBDESARROLLO = Parametros[3].Trim();
                    rutaArchivo = Parametros[4].Trim();
                }
                else
                {
                    string section = "conexion";

                    string a = Funciones.getValueAppConfig("DBCata", section);
                    this.gsCataDB = encriptacion.Decrypt(a);
                    this.gsDSNDB = encriptacion.Decrypt(Funciones.getValueAppConfig("DBDSN", section));
                    this.gsSrvr = encriptacion.Decrypt(Funciones.getValueAppConfig("DBSrvr", section));
                    this.gsUserDB = encriptacion.Decrypt(Funciones.getValueAppConfig("DBUser", section));
                    this.gsPswdDB = encriptacion.Decrypt(Funciones.getValueAppConfig("DBPswd", section));
                    this.gsNameDB = encriptacion.Decrypt(Funciones.getValueAppConfig("DBName", section));

                    rutaArchivo = Funciones.getValueAppConfig("RutaArchivo", "Envfideicomiso");

                    miscelaneas.GsUSerAS400 = Funciones.getValueAppConfig("Usuario", "AS400");
                    miscelaneas.GsPasswordAS400 = Funciones.getValueAppConfig("PSW", "AS400");
                }

                if(rutaArchivo == "")
                {
                    Log.Escribe("No existe el valor de configuracion para generación de archivos", "Error");
                }

                if(this.ConectDB())
                {
                    String gs_sql = @"
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

                    //SqlDataReader dr =this.bd.ejecutarConsulta(gs_sql);
                    //DataTable dt = new DataTable();
                    //dt.Clear();
                    //dt.Columns.Add("contrato");
                    //dt.Columns.Add("fecha");
                    //dt.Columns.Add("tipo_operacion");


                    
                }
            }
            catch (Exception ex)
            {
                Log.Escribe(ex);
            }
        }

        private void Erase(ref string[] Parametrod)
        {
            int tam = Parametros.Length;

            for(int i = 0; i < tam; i++)
            {
                Parametros[i] = String.Empty;
            }
        }

        public bool ConectDB()
        {
            bool ConectDB = false;
            string section = "conexion";
            try
            {
                string conn_str = $"Data source ={this.gsSrvr}; uid ={this.gsUserDB}; PWD ={this.gsPswdDB}; initial catalog = {this.gsNameDB}";
                this.bd = new FuncionesBd(conn_str);

                ConectDB = true;

            }
            catch (Exception ex)
            {
                ConectDB = false;
                Log.Escribe(ex, "Error");
            }
            return ConectDB;
        }

    }
}
