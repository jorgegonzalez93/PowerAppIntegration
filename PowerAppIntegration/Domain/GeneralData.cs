using Migration.Domain.Domain.DTOs.MigracionUsuarios;
using System.Data;
using System.Text.RegularExpressions;

namespace Migration.Domain.Domain
{
    public static class GeneralData
    {
        public static List<RegistryCompanyDto?> REGISTRY_COMPANYS { get; set; } = default!;

        public static int MAX_USER_BY_PACKAGE { get; set; } = 3;
        public static int MAX_CHARACTER_USER_NICK_NAME { get; set; } = 20;
        public static string AdminUser { get; set; } = "xm_e_jherrera@xm.com.co";
        public static DataTable DT_CONTACT { get; set; } = default!;
        public static DataTable DT_COMPANY { get; set; } = default!;

        public const string NATURAL_PERSON = "Natural";
        public const string ENTITY_PERSON = "Juridica";

        public const string BASIC_STATUS = "En revisión";

        public const string PERSON_REPRESENTATIVE_ROL = "Representante legal";

        public const string GENERIC_TELEPHONE_NUMBER = "0000000";
        public const string DEFAULT_SUFIX_USER_NAME = "2023";

        public const string DEFAULT_USER_ROL = "Otro cargo";

        public const string PRIVATE_COMPANY = "Privada";
        public const string PUBLIC_COMPANY = "Pública";

        public const bool DEFAULT_ISMARKET_AGENT = false;
        public const bool CREATEB2CUSER_FLAG = true;
        public const bool CREATE_DUMMY_DOCUMENT = false;

        public const string TYPE_CEDULA_EXTRANJERIA = "Cédula de extranjería";
        public const string TYPE_CEDULA_CIUDADANIA = "Cédula de ciudadanía";
        public const string TYPE_PASAPORTE = "Pasaporte";
        public const string NIT = "NIT";

        public const string DOCUMENTS_PATH = @"C:\Users\jorge.gonzalez\Desktop\SoportesReales\Migracion Actividad";
        public const string LABORATOTY_PATH = @"C:\Users\jorge.gonzalez\Desktop\SoportesReales\Laboratorio";
        //public const string DOCUMENTS_PATH = "C:\\Users\\jorge.gonzalez\\Desktop\\SoportesReales";
        public const string DUMMY_PDF = "C:\\Users\\jorge.gonzalez\\Desktop\\DocumentosMigracion\\Dummy_PDF";
        public const string PDF_EN_BLANCO = @"C:\Users\jorge.gonzalez\source\repos\Aplicacion para organizar datos MVM\Insumos\blank.pdf";

        public const string CERTIFICADO_EXISTENCIA = "Existencia";
        public const string RUT = "RUT";
        public const string DOCUMENTOIDENTIDAD = "Identidad";
        public const string SARLAFT = "Sarlaft";

        public static readonly List<string> REQUIRED_DOCUMENTS_LEGAL_ENTITY_REPRESENTATIVE = new() {
            SARLAFT,
            DOCUMENTOIDENTIDAD,
            CERTIFICADO_EXISTENCIA
        };

        public static readonly List<string> REQUIRED_DOCUMENT_APODERA = new() {
            "Autorizacion",
            "FormatoESP",
            "Poder",
            "Opcionales"
        };


        public static readonly List<string> REQUIRED_DOCUMENT_NO_APODERA = new() {
            "Autorizacion",
            "FormatoESP",
            "Opcionales"
        };

        public static readonly List<string> INVALID_CHARACTER_USER_NICK_NAME = new()
        {
            "/",
            @"\",
            "@",
            "*",
            "."
        };

        public static Regex REGEX_USER_IDENTITY = new Regex(@"[^\w\d\s]|_{6,20}$");

        public static List<DataRow> CONTACT_LIST { get; set; }
    }
}
