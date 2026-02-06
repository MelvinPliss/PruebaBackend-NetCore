namespace PruebaBackendAPI.Models
{
    public class ApiResponse<T>
    {
        public bool Status { get; set; }
        public T Value { get; set; }
        public string Msg { get; set; }
    }
}
