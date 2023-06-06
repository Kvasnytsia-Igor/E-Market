namespace Application.Common.Models;

public record class ApiResponse(int StatusCode, dynamic Data);
