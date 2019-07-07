namespace SqlToCSharp.Classes
{
    using System;
    using System.Xml.Linq;

    public class ExceptionWrapper
    {
        private readonly string _stringFormat = "Message: {0}" +
        Environment.NewLine +
        "Source: {1}" +
        Environment.NewLine +
        "Help Link: {2}" +
        Environment.NewLine +
        "Stack Trace: {3}";

        public ExceptionWrapper(Exception ex)
        {
            Message = ex.Message;
            Helplink = ex.HelpLink;
            StackTrace = ex.StackTrace;
            Source = ex.Source;

            if (ex.InnerException != null)
                InnerException = new ExceptionWrapper(ex.InnerException);
        }

        public string Helplink { get; }

        public ExceptionWrapper InnerException { get; }
        public string Message { get; }

        public string Source { get; }

        public string StackTrace { get; }

        public override string ToString()
            => string.Format(_stringFormat, Message, Source, Helplink, StackTrace);

        public string ToXmlString()
            => ConvertToXml();

        private string ConvertToXml()
        {
            var doc = new XDocument(GetXElement());
            var s = doc.ToString();

            return s;
        }

        private XElement GetXElement()
            => new XElement
            (
                "Exception",
                new XElement(nameof(Message), Message),
                new XElement(nameof(Source), Source),
                new XElement(nameof(Helplink), Helplink),
                new XElement(nameof(StackTrace), StackTrace),
                InnerException?.GetXElement()
            );
    }
}