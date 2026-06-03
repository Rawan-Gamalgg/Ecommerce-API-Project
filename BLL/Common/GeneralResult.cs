namespace Ecommerce.BLL
{
    public class GeneralResult<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public static GeneralResult<T> Ok(T data, string message = "Success")
            => new() { Success = true, Data = data, Message = message };

        public static GeneralResult<T> Fail(string error)
            => new() { Success = false, Errors = new List<string> { error } };

        public static GeneralResult<T> Fail(List<string> errors)
            => new() { Success = false, Errors = errors };
    }
}
