using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using Microsoft.EntityFrameworkCore;


namespace courseProject.Repository.GenericRepository
{
    public class MaterialRepository : GenericRepository1<CourseMaterial>, IMaterialRepository
    {
        private readonly projectDbContext dbContext;

        public MaterialRepository(projectDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CourseMaterial> GetMaterialByIdAsync(Guid id)
        {
           
           return await dbContext.courseMaterials.Include(x=>x.Student_Task_Submissions).Include(x=>x.MaterialFiles).FirstOrDefaultAsync(x => x.Id == id);
                        
        }

        public async Task<IReadOnlyList<CourseMaterial>> GetAllMaterial(Guid? Courseid, Guid? consultationId ,string userType)
        {
            IReadOnlyList<CourseMaterial>? materials = null;
            if (userType.ToLower() == "student")
            {
                materials = await dbContext.courseMaterials.Include(x => x.MaterialFiles).Where(x => (x.courseId == Courseid && x.consultationId == consultationId) && x.isHidden == false).ToListAsync();
            }
            else if (userType.ToLower() == "instructor")
            {
                materials = await dbContext.courseMaterials.Include(x => x.MaterialFiles).Where(x => (x.courseId == Courseid && x.consultationId == consultationId) ).ToListAsync();
            }
            return materials;
        }

        public async Task<IEnumerable<CourseMaterial>> GetAllMaterialInSameCourse(Guid courseId)
        {
            
                return (IEnumerable<CourseMaterial>)
                await dbContext.courseMaterials                                                 
                .Where(a => a.courseId == courseId).ToListAsync();
           
        }


        public async Task AddMaterialFiles(MaterialFiles materialFiles)
        {
            await dbContext.MaterialFiles.AddAsync(materialFiles);
        }


        public async Task<IReadOnlyList<MaterialFiles>> GetMaterialFilesByMaterialId(Guid materialId)
        {
            return await dbContext.MaterialFiles
                                   .Where(mf => mf.materialId == materialId)
                                   .ToListAsync();
        }


        public async Task DeleteFilesById(MaterialFiles file)
        {
            dbContext.Entry(file).State = EntityState.Deleted;
            await dbContext.SaveChangesAsync();
        }


        public async Task AddMaterial(CourseMaterial courseMaterial)
        {
            await dbContext.Set<CourseMaterial>().AddAsync(courseMaterial);
        }

        public async Task DeleteMaterial(Guid id)
        {
            var materail = await dbContext.courseMaterials.FirstOrDefaultAsync(x => x.Id == id);
            dbContext.courseMaterials.Remove(materail);
        }

        public async Task EditMaterial(CourseMaterial courseMaterial)
        {
            dbContext.Set<CourseMaterial>().Update(courseMaterial);
        }


    }
}
