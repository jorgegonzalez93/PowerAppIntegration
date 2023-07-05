using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migration.Domain.Domain.DTOs.MigracionActividad
{
    public class DeclaracionInteresDto
    {
        public string NIT { get; set; }

        public string Estado { get; set; }

        public  IEnumerable<ParticipationDocumentDataDto> documents { get; set; }   

    }
}
