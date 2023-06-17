namespace Migration.Domain.Domain.DTOs.MigracionActividad
{
    public class RequisitosLegalesDocuments
    {
        public string NIT { get; set; }
        public bool Apodera { get; set; }
        public bool Esp { get; set; }
        public string Estado { get; set; }

        public string Correo { get; set; } //Quien se va a apoderar
        public ParticipationDocumentDataDto Poder { get; set; }
        public ParticipationDocumentDataDto CertificadoExistencia { get; set; }
        public ParticipationDocumentDataDto AutorizacionCuantia { get; set; }
        public ParticipationDocumentDataDto ConstitucionFuturaESP { get; set; } // Pdf en blanco

        public IEnumerable<ParticipationDocumentDataDto> DocumentosOpcionales { get; set; }

        public IEnumerable<string> CorreosContactos { get; set; }
    }
}
