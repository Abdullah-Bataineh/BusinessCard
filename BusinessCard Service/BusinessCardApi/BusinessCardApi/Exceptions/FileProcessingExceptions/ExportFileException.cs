namespace BusinessCardApi.Exceptions.FileProcessingExceptions
{
    public class ExportFileException:Exception
    {
        public ExportFileException(string message):base(message) { }
    }
}
