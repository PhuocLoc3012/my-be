using System.Text.Json;

namespace Presentation.API.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Detailed { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        //JsonSerializer.Serialize(this): Chuyển đổi toàn bộ dữ liệu của đối tượng this (bao gồm các thuộc tính công khai - public) thành định dạng JSON.
    }
}
