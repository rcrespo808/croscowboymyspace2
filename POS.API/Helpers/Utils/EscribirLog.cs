using System;
using System.IO;

namespace POS.API.Helpers.Utils;

public class EscribirLog
{
    public string mensajeLog { get; set; }
    public Boolean mostrarConsola { get; set; }

    public EscribirLog(string mensajeEnviar, Boolean mostrarConsola = true)
    {
        mensajeLog = mensajeEnviar;
        if (mostrarConsola)
            monstrarMensajeConsola();
        escribirLineaFichero();
    }
    public EscribirLog()
    {
        if (mostrarConsola)
            monstrarMensajeConsola();
        escribirLineaFichero();
    }

    public void monstrarMensajeConsola()
    {
        mensajeLog = mensajeLog.Replace(Environment.NewLine, " | ");
        mensajeLog = mensajeLog.Replace("\r\n", " | ").Replace("\n", " | ").Replace("\r", " | ");
        Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " " + mensajeLog);
    }
    public void escribirLineaFichero()
    {
        try
        {
            string Date = DateTime.Now.ToString("dd-MM-yyyy");
            FileStream fs = new FileStream(@AppDomain.CurrentDomain.BaseDirectory +
                                           $"Estados-{Date}.log", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            mensajeLog = mensajeLog.Replace(Environment.NewLine, " | ");
            mensajeLog = mensajeLog.Replace("\r\n", " | ").Replace("\n", " | ").Replace("\r", " | ");
            m_streamWriter.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " " + mensajeLog);
            m_streamWriter.Flush();
            m_streamWriter.Close();
        }
        catch
        {

        }
    }
}