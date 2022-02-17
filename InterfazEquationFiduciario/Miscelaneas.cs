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


        private string DEFAULT_SRVR;   // Variable para almacenar el nombre del Servidor a Utilizar
        
        private string DBDESARROLLO;   // Variable para almacenar el nombre de la Base de Datos de DESARROLLO
        private string DBCATALOGOS;   // Variable para almacenar el nombre de la Base de Datos de CATALOGOS
        private string DBFUNCS;   // Variable para almacenar el nombre de la Base de Datos de FUNCIONARIOS
        private string DEFAULT_SRVRKYC;   // Variable para almacenar el nombre del Servidor de KYC
        private string GPATH;   // Variable para almacenar la ruta de los Reportes del Sistema
        private string GsPassword;   // Variable para almacenar el password de usuario SQL
        private string GsUSer;   // Variable para almacenar el usuario de usuario SQL
        private string GsPasswordAS400;   // Variable para almacenar el password de As400
        private string GsUSerAS400;   // Variable para almacenar el usuario de AS400
        private string CNNAME;   // Variable para almacenar el nombre que se le asigna a la conexion
        private string DBDSN;   // Variable para almacenar el DSN utilizado para reportes
        private string DEFAULT_SRVROFAC;   // Variable para almacenar el nombre del Servidor de OFAC
        private string DBOFAC;   // Variable para almacenar el nombre de la Base de Datos de OFAC
        private string GsPasswordOFAC;   // Variable para almacenar el password de usuario SQL de OFAC
        private string GsUSerOFAC;   // Variable para almacenar el usuario de usuario SQL de OFAC

        private string ApliPath;   // Ruta donde se ejecuta la aplicacion

        private string gs_sql;   // Variable para sentencias en SQL
        private int gn_LineaTel;  // Numero de linea con la que firma el usuario
        private string gs_FechaHoy;   // Variable que guarda la fecha del sistema en formato 'mm-dd-yy'
        private int gn_NumUnidOrg;  // Variable para gardar el numero de unidad organizacional
        private string gs_NumCuenta;   // Se utiliza para las formas de Cotitulares y Beneficiarios
        private bool gb_TreeCharged;  // Bandera que indica cuando ya se cargaron los datos del arbol de UnOrg
        private long[] ga_Aperturas;     // Arreglo para guardar operaciones por validar en aperturas por excepcion
        private string sHoraValida;
        private int GnAgencia;  // Se utiliza para las formas de Cotitulares y Beneficiarios
        private long GnProductoContratado;
        private string sMsg;   // Variable para el manejo de mensajes
        private string sMsg2;   // Variable para el manejo de mensajes
        private string GsCuenta;
        private int gAgencia;
        private string GsNombreCliente;
        private string GsPermisoAgencia;   // Variable utilizada para Permiso Agencias
        private long GnOper;
        private long siManc;     // Variable para saber si la cuenta es mancomunada
        private long GnTipoCuenta;     // Variable para tipo de cuenta en Captura Ordenes de Pago
        private bool bCapOrdPago;  // Variable que se utiliza para pedir password y login en Captura Ordenes de pago y CaptPassw.
        private bool bComision;  // Variable que se utiliza para pedir password y login en Captura Ordenes de pago y CaptPassw
        private string GsHoraCierreCHASE;   // Variable para el manejo de mensajes
        private string GSHoraCierreBack;
        private string GSHoraLimiteBack;
        private string GsRepTDOver;
        private bool bCambioSaldos;  // Variable para validacion de usuario (Cambio de Saldos)
        private long GnProductoContratadoH;     // Variable para el producto Contratado de la cuenta eje harris
        private string sCausa;   // Variable para manejar la OP
        private string payTO;   // Variable para manejar la OP
        private string sLocation;   // Variable para manejar la OP
        private string sAba;   // Variable para manejar la OP
        private string sFavorOf;   // Variable para manejar la OP
        private string sFavorOfAccount;   // Variable para manejar la OP
        private string sForFurther;   // Variable para manejar la OP
        private string sReference;   // Variable para manejar la OP
        private string sfechaCapturaOp;   // Variable para pasar fecha de captura a la pantalla de Genera OP
        private string sfechaOperacionOp;   // Variable para pasar fecha de operacion a la pantalla de Genera OP
        private decimal nMontoOP; // Variable para pasar el monto de OP a Genera OP
        private long nRespuesta;
        private OperacionOvernight[] gaOperacionOvernight;  // Arreglo para validar todas las operaciones de TD Overnight
        private int gn_ProcessID;
        private int gn_DBSwapNum;  // Numero de referencia a archivo Swap MDB
        private string[] ga_DBSwapFiles;   // Arreglo de almacenamiento de archivos Swap
        private int GnAccion;  // Variable que indica si se va a dar Mantenimiento o a Cancelar un Hold
        private int gnErrorReporte;  // Variable para manejar los errores de los reportes

    //    Declare Function GetComputerName Lib "kernel32" Alias "GetComputerNameA" (ByVal lpBuffer As String, nSize As Long) As Long
    //    Declare Function GetTempPath Lib "kernel32" Alias "GetTempPathA" (ByVal nBufferLength As Long, ByVal lpBuffer As String) As Long
    //    Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Long) As Long
    //    Public Declare Function GetPrivateProfileString% Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal AppName$, ByVal KeyName$, ByVal keydefault$, ByVal ReturnString$, ByVal NumBytes As Integer, ByVal FileName$)
    //    Public Declare Function WritePrivateProfileString% Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal AppName$, ByVal KeyName$, ByVal KeyValue$, ByVal FileName$)


    }
}
