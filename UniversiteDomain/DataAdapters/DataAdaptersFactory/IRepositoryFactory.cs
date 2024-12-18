﻿using UniversiteDomain.DataAdapters;
using UniversiteDomain.Entities;

namespace UniversiteDomain.DataAdapters.DataAdaptersFactory;

public interface IRepositoryFactory
{
    IEtudiantRepository EtudiantRepository();
    IParcoursRepository ParcoursRepository();
    IUeRepository UeRepository();
    // Méthodes de gestion de la dadasource
    // Ce sont des méthodes qui permettent de gérer l'ensemble du data source
    // comme par exemple tout supprimer ou tout créer
    Task EnsureDeletedAsync();
    Task EnsureCreatedAsync();
    Task SaveChangesAsync();
    Task<Parcours> CreateAsync(Parcours parcours);
}