public class HttpResponseBase<T> where T: class{
    public int statusCode { get; set; }
    public string message { get; set; }
    public T content { get; set; }
    public DateTime dateTime { get; set; }
}