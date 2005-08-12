using System;

namespace OCR
{
    class InvalidEngineException : System.Exception
    {
        public InvalidEngineException(string message)
            : base(message)
        {
            this.Message = message;
        }
    }
}
