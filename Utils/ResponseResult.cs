using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs;

namespace go_han.Utils
{
    public class ResponseResult
    {
        public static ApiResponse<T> Success<T>(T data, string message = "Operation successful")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> Fail<T>(string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }
}