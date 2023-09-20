using System.Collections.Generic;
using Tanulok.DTO;
using Tanulok.Models;
using Tanulok.Repository;

namespace Tanulok.Mapper
{
    public class AutoSzervizMapper
    {
        private IAutoEFRepository repository;
        public AutoSzervizMapper(IAutoEFRepository autoEFRepository)
        { 
            repository= autoEFRepository;
        }
        public AutoSzervizDTO AutoToAutoSzervizDTO(Autok autok)
        {
            AutoSzervizDTO autoSzervizDTO = new AutoSzervizDTO();
            autoSzervizDTO.Beosztas = autok.Alkalmazott.Beosztas;
            autoSzervizDTO.SzervizKm = autok.Tipusok.SzervizKm;
            autoSzervizDTO.AlkalmazottId = autok.Alkalmazott.Id;
            autoSzervizDTO.Rendszam = autok.Rendszam;
            autoSzervizDTO.AlkNev = autok.Alkalmazott.AlkNev;
            autoSzervizDTO.TipusNev=autok.Tipusok.TipusNev;
            autoSzervizDTO.FutottKm = autok.FutottKm;
            return autoSzervizDTO;
        }
        public List<AutoSzervizDTO> AutoToAutoSzervizDTOs(List<Autok> autok) 
        {
            List < AutoSzervizDTO > result = new List < AutoSzervizDTO >();
            foreach (Autok item in autok)
            {
                result.Add(AutoToAutoSzervizDTO(item));
            }
            return result;
        }
    }
}
