using courseProject.Core.IGenericRepository;
using courseProject.MappingProfile;
using courseProject.Repository.GenericRepository;

namespace courseProject.Configuration
{
    public static class MapperDependency
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped(typeof(IGenericRepository1<>), typeof(GenericRepository1<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddAutoMapper(typeof(MappingForStudents));
            services.AddAutoMapper(typeof(MappingForCourse));
            services.AddAutoMapper(typeof(MappingForEmployee));
            services.AddAutoMapper(typeof(MappingForEvents));
            services.AddAutoMapper(typeof(MappingForContact));
            services.AddAutoMapper(typeof(MappingForFeedback));
            services.AddAutoMapper(typeof(MappingForMaterial));
            services.AddAutoMapper(typeof(MappingForStudentCourses));




            return services;
        }

    }
}
