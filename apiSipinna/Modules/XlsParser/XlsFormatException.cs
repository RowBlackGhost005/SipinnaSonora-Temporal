using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace apiSipinna.Modules.XlsParser
{
    /// <summary>
    /// Clase <c>XlsFormatException<c/> representa una excepción dentro del módulo XlsParser
    /// </summary>
    public class XlsFormatException : Exception
    {
        /// <summary>
        /// Crea una XlsFormatException vacía.
        /// </summary>
        public XlsFormatException()
        {

        }

        /// <summary>
        /// Crea una XlsFormatException con el mensaje dado como parámetro.
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción</param>
        public XlsFormatException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Crea una XlsFormatException con el mensaje y excepción dada como parámetro
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción</param>
        /// <param name="inner">Excepción original</param>
        public XlsFormatException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}