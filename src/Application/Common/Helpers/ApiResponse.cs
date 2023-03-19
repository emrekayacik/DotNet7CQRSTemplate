namespace Application.Common.Helpers;
public enum ApiResponseType
{
    Success = 200,
    ValidationError = 400,
    NotFoundError = 404,
    UnhandledError = 500,
    Custom = 300,
    FileFormatError = 301,
    VehicleMaintenanceError = 302,
    TripPlanningError = 303
}
public class ApiResponse
{
    /// <summary>
    /// Başarısız işlemler için hata kodu. Başarılı işlemlerde 0
    /// </summary>
    public ApiResponseType Code { get; set; }

    /// <summary>
    /// True ise işlem başarılıdır.
    /// </summary>
    public bool Ok { get; set; }

    /// <summary>
    /// İşlem açıklaması.
    /// Başarısız veya uyarı olan işlemlerde hata veya uyarı mesajını barındırır.
    /// Başarılı işlemlerde genellikle null değerini alır.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gönderilen datalar
    /// </summary>
    public object Data { get; set; }

    public ApiResponse(bool ok, ApiResponseType code, string message, object data)
    {
        Code = code;
        Ok = ok;
        Message = message;
        Data = data;
    }

    public ApiResponse()
    {
    }

    public static ApiResponse Success()
    {
        return new ApiResponse(true, ApiResponseType.Success, "Success", null);
    }

    public static ApiResponse Failure(IEnumerable<string> errors)
    {
        return new ApiResponse(false, ApiResponseType.UnhandledError, "Failed", errors);
    }
}