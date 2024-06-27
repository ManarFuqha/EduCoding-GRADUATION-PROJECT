using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;


namespace courseProject.Repository.GenericRepository
{
    public class SubAdminRepository /*: GenericRepository1<SubAdmin>, ISubAdminRepository*/
    {
        private readonly projectDbContext dbContext;
       

        //public SubAdminRepository(projectDbContext dbContext) : base(dbContext)
        //{
        //    this.dbContext = dbContext;
         
        //}





        //public async Task createSubAdminAccountAsync(SubAdmin entity)
        //{
        //    await dbContext.Set<SubAdmin>().AddAsync(entity);
        //}


       

      

      

        //public async Task<SubAdmin> GetSubAdminByIdAsync(Guid id)
        //{
        //   return await dbContext.subadmins.Include(x=>x.user).FirstOrDefaultAsync(x => x.SubAdminId == id);
 
        //}

       

        //public async Task<SubAdmin> getSubAdminByIdAsync(Guid subAdminId)
        //{
        //    return await dbContext.subadmins.FirstOrDefaultAsync(x => x.SubAdminId == subAdminId);
        //}

       

        //public async Task RemoveSubAdmin(SubAdmin subAdmin)
        //{
        //    dbContext.subadmins.Remove(subAdmin);
        //}

       

        
    }
}
