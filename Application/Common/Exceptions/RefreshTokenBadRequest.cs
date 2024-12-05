using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class RefreshTokenBadRequest:Exception
    {
        public RefreshTokenBadRequest(string msg) : base(msg) { }
        // Constructor với thông điệp lỗi và InnerException
        public RefreshTokenBadRequest(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
