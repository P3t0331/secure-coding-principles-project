namespace Panbyte.Exceptions;
using System;
using System.Runtime.Serialization;

[Serializable]
public class HelpException : Exception
{
    public HelpException() : base("Help")
    {
    }

    protected HelpException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}