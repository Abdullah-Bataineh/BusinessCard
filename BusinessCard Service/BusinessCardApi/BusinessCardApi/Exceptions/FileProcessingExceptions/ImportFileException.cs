namespace BusinessCardApi.Exceptions.FileProcessingExceptions
{
    public class ImportFileException:Exception
    {
        public ImportFileException(string message) : base(message) { }
    }
}
