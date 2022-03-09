using InterfazEquationFiduciario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfazEquationFiduciario
{
    public class Miscelaneas
    {
        private bool TRADUCE;
        const string gsUSUARIO = "USUARIO";
        public const string Aplicacion = "BackLA";


        public string DEFAULT_SRVR;   // Variable para almacenar el nombre del Servidor a Utilizar
        
        public string DBDESARROLLO;   // Variable para almacenar el nombre de la Base de Datos de DESARROLLO
        public string DBCATALOGOS;   // Variable para almacenar el nombre de la Base de Datos de CATALOGOS
        public string DBFUNCS;   // Variable para almacenar el nombre de la Base de Datos de FUNCIONARIOS
        public string DEFAULT_SRVRKYC;   // Variable para almacenar el nombre del Servidor de KYC
        public string GPATH;   // Variable para almacenar la ruta de los Reportes del Sistema
        public string GsPassword;   // Variable para almacenar el password de usuario SQL
        public string GsUSer;   // Variable para almacenar el usuario de usuario SQL
        public string GsPasswordAS400;   // Variable para almacenar el password de As400
        public string GsUSerAS400;   // Variable para almacenar el usuario de AS400
        public string CNNAME;   // Variable para almacenar el nombre que se le asigna a la conexion
        public string DBDSN;   // Variable para almacenar el DSN utilizado para reportes
        public string DEFAULT_SRVROFAC;   // Variable para almacenar el nombre del Servidor de OFAC
        public string DBOFAC;   // Variable para almacenar el nombre de la Base de Datos de OFAC
        public string GsPasswordOFAC;   // Variable para almacenar el password de usuario SQL de OFAC
        public string GsUSerOFAC;   // Variable para almacenar el usuario de usuario SQL de OFAC

        public string ApliPath;   // Ruta donde se ejecuta la aplicacion

        public string gs_sql;   // Variable para sentencias en SQL
        public int gn_LineaTel;  // Numero de linea con la que firma el usuario
        public string gs_FechaHoy;   // Variable que guarda la fecha del sistema en formato 'mm-dd-yy'
        public int gn_NumUnidOrg;  // Variable para gardar el numero de unidad organizacional
        public string gs_NumCuenta;   // Se utiliza para las formas de Cotitulares y Beneficiarios
        public bool gb_TreeCharged;  // Bandera que indica cuando ya se cargaron los datos del arbol de UnOrg
        public long[] ga_Aperturas;     // Arreglo para guardar operaciones por validar en aperturas por excepcion
        public string sHoraValida;
        public int GnAgencia;  // Se utiliza para las formas de Cotitulares y Beneficiarios
        public long GnProductoContratado;
        public string sMsg;   // Variable para el manejo de mensajes
        public string sMsg2;   // Variable para el manejo de mensajes
        public string GsCuenta;
        public int gAgencia;
        public string GsNombreCliente;
        public string GsPermisoAgencia;   // Variable utilizada para Permiso Agencias
        public long GnOper;
        public long siManc;     // Variable para saber si la cuenta es mancomunada
        public long GnTipoCuenta;     // Variable para tipo de cuenta en Captura Ordenes de Pago
        public bool bCapOrdPago;  // Variable que se utiliza para pedir password y login en Captura Ordenes de pago y CaptPassw.
        public bool bComision;  // Variable que se utiliza para pedir password y login en Captura Ordenes de pago y CaptPassw
        public string GsHoraCierreCHASE;   // Variable para el manejo de mensajes
        public string GSHoraCierreBack;
        public string GSHoraLimiteBack;
        public string GsRepTDOver;
        public bool bCambioSaldos;  // Variable para validacion de usuario (Cambio de Saldos)
        public long GnProductoContratadoH;     // Variable para el producto Contratado de la cuenta eje harris
        public string sCausa;   // Variable para manejar la OP
        public string payTO;   // Variable para manejar la OP
        public string sLocation;   // Variable para manejar la OP
        public string sAba;   // Variable para manejar la OP
        public string sFavorOf;   // Variable para manejar la OP
        public string sFavorOfAccount;   // Variable para manejar la OP
        public string sForFurther;   // Variable para manejar la OP
        public string sReference;   // Variable para manejar la OP
        public string sfechaCapturaOp;   // Variable para pasar fecha de captura a la pantalla de Genera OP
        public string sfechaOperacionOp;   // Variable para pasar fecha de operacion a la pantalla de Genera OP
        public decimal nMontoOP; // Variable para pasar el monto de OP a Genera OP
        public long nRespuesta;
        public OperacionOvernight[] gaOperacionOvernight;  // Arreglo para validar todas las operaciones de TD Overnight
        public int gn_ProcessID;
        public int gn_DBSwapNum;  // Numero de referencia a archivo Swap MDB
        public string[] ga_DBSwapFiles;   // Arreglo de almacenamiento de archivos Swap
        public int GnAccion;  // Variable que indica si se va a dar Mantenimiento o a Cancelar un Hold
        public int gnErrorReporte;  // Variable para manejar los errores de los reportes

        public string lsCommandLine;
        public string[] Parametros;
        public string rutaArchivo;
        public string nombreArchivo;
        public string nombreArchivoDtt;
        public string nombreArchivoDttDestino;
        public string nombreArchivoFDF, ArchivoFDF;
        public string nombreArchivoFDFDestino;
         
        public string rutaPathModelos;
        public string rutaPathDatos;
        public string rutaPathTransfer;
         
         
        public string EquipoAS;
        public string LibreriaAS;
        public string ArchivoAS;
         
        public string msPathFTPApp;

        public string gsPswdDB;
        public string gsUserDB;
        public string gsNameDB;
        public string gsCataDB;
        public string gsDSNDB;
        public string gsSrvr;

        public bool paso1, paso2;

        //    Declare Function GetComputerName Lib "kernel32" Alias "GetComputerNameA" (ByVal lpBuffer As String, nSize As Long) As Long
        //    Declare Function GetTempPath Lib "kernel32" Alias "GetTempPathA" (ByVal nBufferLength As Long, ByVal lpBuffer As String) As Long
        //    Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Long) As Long
        //    Public Declare Function GetPrivateProfileString% Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal AppName$, ByVal KeyName$, ByVal keydefault$, ByVal ReturnString$, ByVal NumBytes As Integer, ByVal FileName$)
        //    Public Declare Function WritePrivateProfileString% Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal AppName$, ByVal KeyName$, ByVal KeyValue$, ByVal FileName$)


    }
}
