namespace LibraryManagementSystem.Model
{
    public class ApiResponse<T>
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public T? ResponseData { get; set; }
    }
    public enum ResponseType
        {
        success,
        NotFound, 
        Failure
        }

}
