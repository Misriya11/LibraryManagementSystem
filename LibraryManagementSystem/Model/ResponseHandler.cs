using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Model
{
    public class ResponseHandler
    {
        public static ApiResponse<String> GetExceptionResponse(Exception exception)
        {
            ApiResponse<String> response = new ApiResponse<String>();
            response.Code = "400";
            response.ResponseData = exception.Message;   
            response.Message = "error";
            return response;    
        }

        public static ApiResponse<String> GetCustomExceptionResponse(CustomError exception)
        {
            ApiResponse<String> response = new ApiResponse<String>();
            response.Code = exception.code.ToString();
            response.ResponseData = exception.Message;
            response.Message = "error";
            return response;
        }
        public static ApiResponse<T> GetAppResponse<T>(ResponseType responseType,T? contract) {
            ApiResponse<T> response;
            response = new ApiResponse<T> { ResponseData = contract };
            switch(responseType)
            {
                case ResponseType.success:
                    response.Code="200";
                    response.Message = "Success";

                    break;
                case ResponseType.NotFound: 
                    response.Code="0";  
                    response.Message = "No record available";
                    break;
            }
            return response;
        }
    }
}
