namespace Tanulok.Helper
{
    public class ValidationResult<T>
    {
        public bool isValid { get; set; }
        public List<string> Errors { get; set; }
        public T result {get;set;}

    }
}
