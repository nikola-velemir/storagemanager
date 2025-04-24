using StoreManager.Infrastructure.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManager.Application.Auth.DTO;
using StoreManager.Domain.Auth.Tokens.RefreshToken.Model;
using StoreManager.Domain.User.Model;

namespace StoreManager.Tests.Auth.Service
{
    public static class AuthServiceTestData
    {


        public static readonly string VALID_USERNAME = "TEST";
        public static readonly string VALID_PASSWORD = "TEST";
        public static readonly string INVALID_USERNAME = "INVALID";
        public static readonly string INVALID_PASSWORD = "INVALID";

        public static readonly Domain.User.Model.User VALID_USER = new(1, VALID_USERNAME, VALID_PASSWORD, "TEST", "TEST", UserRole.ADMIN);
        public static readonly string VALID_JWT_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIyYTA4ODNkOS01ODNhLTQ3MTctYWNmMC1kOWFmMGJjZWQwYzYiLCJuYW1lIjoidXNlcm5hbWUiLCJyb2xlIjoiTUFOQUdFUiIsIm5iZiI6MTc0MTQzMzA2MCwiZXhwIjoxNzQxNDMzMTIwLCJpYXQiOjE3NDE0MzMwNjAsImlzcyI6InN0b3JhZ2VfbWFuYWdlciIsImF1ZCI6ImFwcF9zdG9yYWdlX21hbmFnZXIifQ.lWEZGkELFmIj3uGaFhmAWVmVyD3K5VdyAGN4CfBLKmA";
        public static readonly string VALID_JTI = "2a0883d9-583a-4717-acf0-d9af0bced0c6";
        public static readonly string VALID_REFRESH_TOKEN = "/ZvvyVtCy+4O5hH5HdgJcYAApL6D1LZpArL8GaO+E6Y=";
        public static readonly string VALID_REFRESH_TOKEN_EXPIRED = "/ZvvyVtCy+4O5hH5HdgJcYAApL6D1LZpArL8GaO+E617";

        public static readonly string INVALID_REFRESH_TOKEN = "INVALID";
        public static readonly string INVALID_JWT_TOKEN = "INVALID";
        public static readonly string INVALID_JWT_TOKEN_NO_JTI = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoidXNlcm5hbWUiLCJyb2xlIjoiTUFOQUdFUiIsIm5iZiI6MTc0MTQzMzA2MCwiZXhwIjoxNzQxNDMzMTIwLCJpYXQiOjE3NDE0MzMwNjAsImlzcyI6InN0b3JhZ2VfbWFuYWdlciIsImF1ZCI6ImFwcF9zdG9yYWdlX21hbmFnZXIifQ.1jmTMSeYJNyU7qs4XpmBxzTjvY-bGy6VsGPrNc8g6_k";
        public static readonly LoginResponseDto VALID_LOGIN_RESPONSE = new LoginResponseDto(VALID_JWT_TOKEN, VALID_REFRESH_TOKEN, VALID_USER.Role.ToString());

        public static readonly RefreshTokenModel VALID_REFRESH_TOKEN_MODEL = new RefreshTokenModel
        {
            Token = VALID_REFRESH_TOKEN,
            User = VALID_USER,
            ExpiresOnUtc = DateTime.UtcNow.AddHours(4),
            Id = Guid.NewGuid(),
            UserId = VALID_USER.Id
        };
        public static readonly RefreshTokenModel EXPIRED_REFRESH_TOKEN_MODEL = new RefreshTokenModel
        {
            Token = VALID_REFRESH_TOKEN,
            User = VALID_USER,
            ExpiresOnUtc = DateTime.UtcNow.AddDays(-4),
            Id = Guid.NewGuid(),
            UserId = VALID_USER.Id
        };
        public static readonly RefreshRequestDto INVALID_REFRESH_REQUEST = new(INVALID_REFRESH_TOKEN);
        public static readonly RefreshRequestDto EXPIRED_REFRESH_REQUEST = new(VALID_REFRESH_TOKEN_EXPIRED);
        public static readonly RefreshRequestDto VALID_REFRESH_REQUEST = new(VALID_REFRESH_TOKEN);
        public static readonly LoginResponseDto VALID_RESPONSE = new(VALID_JWT_TOKEN, VALID_REFRESH_TOKEN, UserRole.ADMIN.ToString());
        public static readonly LoginRequestDto VALID_REQUEST = new(VALID_USERNAME, VALID_PASSWORD);
        public static readonly LoginRequestDto INVALID_REQUEST_INVALID_USERNAME = new(INVALID_USERNAME, VALID_PASSWORD);
        public static readonly LoginRequestDto INVALID_REQUEST_INVALID_PASSWORD = new(VALID_USERNAME, INVALID_PASSWORD);

    }
}
