namespace UniversityApiBackend.Models.DataModels
{
    public class JwtSettings
    {
        //estas validaciones sirven para todo el proyecto, se repueden reutilizar
        public bool ValidateIssuerSigninKey { get; set; }
        public string? IssuerSigninKey { set; get; } = string.Empty;

        public bool validateIssuer { get; set; } = true;
        public string? ValidIssuer { set; get; }

        public bool ValidateAudience { get; set; } = true;
        public string? ValidAudience { get; set; }

        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifetime { get; set; } = true;
    }
}
