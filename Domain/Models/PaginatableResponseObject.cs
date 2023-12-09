namespace Psp.Pos.Api.Models
{
    public class PaginatableResponseObject<T>
    {
        public T Data { get; set; }
        public string nextPage { get; set; }
    }
}
