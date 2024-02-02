namespace RentingCarAPI.ViewModel
{
    public class ResponseWithEntity<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public T? Entity { get; set; }
    }
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
