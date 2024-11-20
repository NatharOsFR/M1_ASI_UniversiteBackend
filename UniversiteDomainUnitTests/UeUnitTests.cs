using Moq;
using NUnit.Framework;
using UniversiteDomain.DataAdapters;
using UniversiteDomain.DataAdapters.DataAdaptersFactory;
using UniversiteDomain.Entities;
using UniversiteDomain.Exceptions.UeExceptions;
using UniversiteDomain.UseCases.UeUseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UniversiteDomain.Exceptions.EtudiantExceptions;
using UniversiteDomain.UseCases.UeUseCases.Create;

namespace UniversiteDomainUnitTests;

public class UeUnitTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CreateUeUseCase_ValidUe()
    {
        string numeroUe = "UE101";
        string intitule = "Introduction à la programmation";
        Ue ueAvant = new Ue { NumeroUe = numeroUe, Intitule = intitule };

        var mockUeRepository = new Mock<IUeRepository>();
        mockUeRepository
            .Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Ue, bool>>>()))
            .ReturnsAsync(new List<Ue>()); // Pas de conflit existant

        Ue ueApres = new Ue { Id = 1, NumeroUe = numeroUe, Intitule = intitule };
        mockUeRepository
            .Setup(repo => repo.CreateAsync(ueAvant))
            .ReturnsAsync(ueApres);

        var mockFactory = new Mock<IRepositoryFactory>();
        mockFactory.Setup(facto => facto.UeRepository()).Returns(mockUeRepository.Object);

        CreateUeUseCase useCase = new CreateUeUseCase(mockFactory.Object);
        
        var resultat = await useCase.ExecuteAsync(ueAvant);
        
        Assert.That(resultat.Id, Is.EqualTo(ueApres.Id));
        Assert.That(resultat.NumeroUe, Is.EqualTo(ueApres.NumeroUe));
        Assert.That(resultat.Intitule, Is.EqualTo(ueApres.Intitule));
    }
}