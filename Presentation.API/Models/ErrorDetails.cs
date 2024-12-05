using System.Text.Json;
using System.Text.Json.Serialization;

namespace Presentation.API.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        //public string? Detailed { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);//, new JsonSerializerOptions
            //{
            //    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            //});
            //JsonSerializer sẽ bỏ qua các thuộc tính có giá trị null khi chuyển thành chuỗi JSON.
        }
        //JsonSerializer.Serialize(this): Chuyển đổi toàn bộ dữ liệu của đối tượng this (bao gồm các thuộc tính công khai - public) thành định dạng JSON.
    }
}
