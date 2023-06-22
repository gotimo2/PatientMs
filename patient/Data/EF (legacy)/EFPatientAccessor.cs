
namespace PatientMs.Data.EF
{



    #region EF-based patientAccessor

    public class EFPatientAccessor //: IPatientAccessor
    {
        //private PatientDbContext dbContext { get; init; }
        //
        //public EFPatientAccessor(PatientDbContext dbContext)
        //{
        //    this.dbContext = dbContext;
        //}
        //
        //public async Task<Patient> GetPatient(long id)
        //{
        //    var p = await dbContext.patients.Where(p => p.id == id)
        //                                    .Include(p => p.Adress)
        //                                    .Include(p => p.MedicalProfile)
        //                                    .Include(p => p.InsurancePolicies)
        //                                    .Include(p => p.MedicalConditions)
        //                                    .FirstOrDefaultAsync();
        //
        //    if (p == null)
        //    {
        //        throw new KeyNotFoundException($"Patient with id {id} not found");
        //    }
        //    return p;
        //}
        //
        //public async Task<List<Patient>> GetByName(string firstName, string lastName)
        //{
        //    return await dbContext.patients.Where(p =>
        //     p.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
        //        p.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)
        //    ).ToListAsync();
        //}
        //
        //public async Task AddPatient(Patient patient)
        //{
        //    dbContext.Add(patient);
        //    await Save();
        //}
        //
        //public async Task UpdatePatient(Patient patient)
        //{
        //    dbContext.Update(patient);
        //    await Save();
        //}
        //
        //public async Task DeletePatient(Patient patient)
        //{
        //    dbContext.Remove(patient);
        //    await Save();
        //}
        //
        //public async Task Save()
        //{
        //    await dbContext.SaveChangesAsync();
        //}
        //

    }
    #endregion
}
