namespace Core.Response
{
    public class ResponseService<T>
    {
        public string? Error { get; set; }
        public T? Value { get; set; }
        public bool Success { get; set; }
        public EStatusErrors Status { get; set; }
    }
}
