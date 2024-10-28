namespace BusinessCardApi.Model
{
    public class PaginationResult<T> 
    {
        public IEnumerable<T> Data { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
    }
}
