using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InterfazEquationFiduciario.Helpers
{
    public class Funciones
    {
        public string strLogPath;
        public bool EscribirLog;

        public Funciones()
        {
            String CadenaRsultante, strPathServicio;
            strPathServicio = System.Reflection.Assembly.GetEntryAssembly().Location;
            CadenaRsultante = strPathServicio.Substring(strPathServicio.LastIndexOf("\\", strPathServicio.Length) + 1);

            strLogPath = strPathServicio.Substring(0, strPathServicio.Length - CadenaRsultante.Length) + "Logs\\";
            EscribirLog = true;
        }

        public string obtenParametroINI(string key, string section = "")
        {
            if (section.Length >= 1)
            {
                return ConfigurationManager.AppSettings[$"{section}.{key}"];
            }
            else
            {
                return ConfigurationManager.AppSettings[$"{key}"];
            }

        }

        public bool EscribeParametroINI(string key, string value, string section = "")
        {
            //string nombre_appconfig = "MonitorMQTKT.exe.config";
            string nombre_appconfig = "App.config";

            bool bandera_archivo_existe = false;
            try
            {
                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                writeToLog("Split aplicado a:   " + appPath);
                string[] appPath_arr = appPath.Split('\\');

                appPath = "";
                for (int i = 0; i < (appPath_arr.Length); i++)
                {
                    appPath = (i > 0) ? appPath + "\\" + appPath_arr[i] : appPath + appPath_arr[i];
                    string busqueda = $"{appPath}\\{nombre_appconfig}";
                    writeToLog("Buscando:    " + busqueda);
                    bandera_archivo_existe = File.Exists(busqueda);
                    if (bandera_archivo_existe) break;
                }
                if (bandera_archivo_existe)
                {
                    appPath = appPath.Substring(1, appPath.Length - 1);
                    string configFile = System.IO.Path.Combine(appPath, nombre_appconfig);
                    ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                    configFileMap.ExeConfigFilename = configFile;
                    System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
                    if (section.Length > 0)
                    {
                        config.AppSettings.Settings[$"{section}.{key}"].Value = value;
                    }
                    else
                    {
                        config.AppSettings.Settings[key].Value = value;
                    }
                    config.Save();
                    return true;
                }
                else
                {
                    writeToLog("No se encontro el archivo", "Error");
                    return false;
                }

            }
            catch (Exception ex)
            {
                writeToLog(ex, "Error");
                return false;
            }
        }


        public void writeToLog(string vData, string tipo = "Mensaje")
        {
            StackTrace trace = new StackTrace(StackTrace.METHODS_TO_SKIP + 2);
            StackFrame frame = trace.GetFrame(0);
            MethodBase caller = frame.GetMethod();

            string clase = caller.ReflectedType.Name;
            string funcion = caller.Name;
            clase = "MQTKT";

            string seccion = "escribeArchivoLOG";
            string nombre_archivo = DateTime.Now.ToString("ddMMyyyy") + "-" + this.obtenParametroINI("logFileName", seccion);
            nombre_archivo = nombre_archivo.Replace("@clase", clase);

            if (EscribirLog)
            {
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(strLogPath, nombre_archivo), append: true))
                {
                    vData = $"[{DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss")}]  {tipo} desde {funcion}:  {vData}";
                    Console.WriteLine(vData);
                    outputFile.WriteLine(vData);
                }

            }
        }


        public void writeToLog(Exception ex, string tipo = "Error")
        {
            StackTrace trace = new StackTrace(StackTrace.METHODS_TO_SKIP + 2);
            StackFrame frame = trace.GetFrame(0);
            MethodBase caller = frame.GetMethod();

            string clase = caller.ReflectedType.Name;
            string funcion = caller.Name;
            clase = "MQTKT";

            string vData;
            string seccion = "escribeArchivoLOG";
            string nombre_archivo = DateTime.Now.ToString("ddMMyyyy") + "-" + this.obtenParametroINI("logFileName", seccion);
            nombre_archivo = nombre_archivo.Replace("@clase", clase);

            if (EscribirLog)
            {
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(strLogPath, nombre_archivo), append: true))
                {
                    vData = $"[{DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss")}] {(char)13}" +
                        $"*{tipo} desde {funcion}:  {ex.Message} {(char)13}" +
                        $"*InnerException: {ex.InnerException} {(char)13}" +
                        $"*Source: {ex.Source}  {(char)13}" +
                        $"*Data: {ex.Data}  {(char)13}" +
                        $"*HelpLink: {ex.HelpLink}  {(char)13}" +
                        $"*StackTrace: {ex.StackTrace}  {(char)13}" +
                        $"*HResult: {ex.HResult}  {(char)13}" +
                        $"*TargetSite: {ex.TargetSite}  {(char)13}";
                    Console.Write(vData);
                    outputFile.WriteLine(vData);
                }

            }
        }
    }
}
