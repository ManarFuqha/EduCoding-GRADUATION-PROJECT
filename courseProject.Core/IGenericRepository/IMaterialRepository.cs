using courseProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Core.IGenericRepository
{
    public interface IMaterialRepository :IGenericRepository1<CourseMaterial>
    {
        public Task<CourseMaterial> GetMaterialByIdAsync(Guid materialId);
        public Task<IEnumerable<CourseMaterial>> GetAllMaterialInSameCourse(Guid CourseId);

        public Task<IReadOnlyList<CourseMaterial>> GetAllMaterial(Guid? Courseid, Guid? consultationId, string userType);
        public  Task AddMaterialFiles(MaterialFiles materialFiles);
        public Task<IReadOnlyList<MaterialFiles>> GetMaterialFilesByMaterialId(Guid materialId);      

        public  Task DeleteFilesById(MaterialFiles file);

        public Task AddMaterial(CourseMaterial courseMaterial);
        public Task DeleteMaterial(Guid id);
        public Task EditMaterial(CourseMaterial courseMaterial);
    }
}
