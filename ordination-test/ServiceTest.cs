namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

[TestClass]
public class ServiceTest
{
    private DataService service;

    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    public void PatientsExist()
    {
        Assert.IsNotNull(service.GetPatienter());
    }
    
    [TestMethod]
    public void getPatientDosisLet()
    {
        int laegemiddelId = 4;
        int patientId = 7;

        double anbefaletDosis = service.GetAnbefaletDosisPerDøgn(patientId, laegemiddelId);
        Assert.AreEqual(anbefaletDosis, 0.01);
    }

    [TestMethod]
    public void getPatientDosisMiddel()
    {
        int laegemiddelId = 4;
        int patientId = 5;

        double anbefaletDosis = service.GetAnbefaletDosisPerDøgn(patientId, laegemiddelId);
        Assert.AreEqual(anbefaletDosis, 0.015);
    }

    [TestMethod]
    public void getPatientDosisTung()
    {
        int laegemiddelId = 4;
        int patientId = 6;

        double anbefaletDosis = service.GetAnbefaletDosisPerDøgn(patientId, laegemiddelId);
        Assert.AreEqual(anbefaletDosis, 0.02); 
    }

    [TestMethod]
    public void PNOprettelse()
    {
        Patient p = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        service.OpretPN(p.PatientId, lm.LaegemiddelId, 5, DateTime.Now, DateTime.Now.AddDays(10));
        Assert.AreEqual(5, service.GetPNs().Count());
    }
}