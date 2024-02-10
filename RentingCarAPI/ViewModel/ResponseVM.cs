namespace RentingCarAPI.ViewModel
{
    public class ResponseVMWithEntity<T> where T : class
    {
        public string? Message { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public T? Entity { get; set; }
    }
    public class ResponseVM
    {
        public string? Message { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
