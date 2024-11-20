using UniversiteDomain.DataAdapters;
using UniversiteDomain.DataAdapters.DataAdaptersFactory;
using UniversiteDomain.Entities;
using UniversiteDomain.Exceptions.EtudiantExceptions;
using UniversiteDomain.Exceptions.UeExceptions;
using UniversiteDomain.Util;

namespace UniversiteDomain.UseCases.UeUseCases.Create;

public class CreateUeUseCase(IRepositoryFactory UeRepository)
{
    public async Task<Ue> ExecuteAsync(string numeroUe, string intitule)
    {
        var ue = new Ue{NumeroUe = numeroUe, Intitule = intitule};
        return await ExecuteAsync(ue);
    }
    public async Task<Ue> ExecuteAsync(Ue ue)
    {
        await CheckBusinessRules(ue);
        Ue uee = await UeRepository.UeRepository().CreateAsync(ue);
        UeRepository.SaveChangesAsync().Wait();
        return uee;
    }
    private async Task CheckBusinessRules(Ue ue)
    {
        ArgumentNullException.ThrowIfNull(ue);
        ArgumentNullException.ThrowIfNull(ue.NumeroUe);
        ArgumentNullException.ThrowIfNull(ue.Intitule);
        ArgumentNullException.ThrowIfNull(UeRepository);
        
        // On recherche une Ue avec le même numéro d'UE
        List<Ue> existe = await UeRepository.UeRepository().FindByConditionAsync(e=>e.NumeroUe.Equals(ue.NumeroUe));

        // Si une Ue avec le même numéro d'Ue existe déjà, on lève une exception personnalisée
       if (existe .Any()) throw new DuplicateNumUeException(ue.NumeroUe + " - ce numéro d'Ue est déjà affecté à une Ue");
        
       if (ue.Intitule.Length < 3) throw new InvalidNomUeException(ue.Intitule +" incorrect - L'intitulé d'une Ue doit contenir plus de 3 caractères");
        
    }
}