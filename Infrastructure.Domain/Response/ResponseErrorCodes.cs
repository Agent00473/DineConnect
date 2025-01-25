
namespace Infrastructure.Domain.Response
{
    public enum HttpResponseErrorCode
    {
        // Client error responses (4xx)
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        NotAcceptable = 406,
        ProxyAuthenticationRequired = 407,
        RequestTimeout = 408,
        Conflict = 409,
        Gone = 410,
        LengthRequired = 411,
        PreconditionFailed = 412,
        PayloadTooLarge = 413,
        URITooLong = 414,
        UnsupportedMediaType = 415,
        RangeNotSatisfiable = 416,
        ExpectationFailed = 417,
        MisdirectedRequest = 421,
        UnprocessableEntity = 422,
        Locked = 423,
        FailedDependency = 424,
        TooManyRequests = 429,
        RequestHeaderFieldsTooLarge = 431,
        UnavailableForLegalReasons = 451,

        // Server error responses (5xx)
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GatewayTimeout = 504,
        HTTPVersionNotSupported = 505,
        InsufficientStorage = 507,
        LoopDetected = 508,
        NotExtended = 510,
        NetworkAuthenticationRequired = 511
    }


}
