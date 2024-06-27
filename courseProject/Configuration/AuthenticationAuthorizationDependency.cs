using courseProject.Authentication.CourseParticipantsAuthorize;
using courseProject.Authentication.EnrolledInCourse;
using courseProject.Authentication.MaterialInEnrolledCourse;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace courseProject.Configuration
{
    public static class AuthenticationAuthorizationDependency
    {

        public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            // Retrieve the secret key used for JWT authentication from the configuration
            var Key = configuration.GetValue<string>("Authentication:SecretKey");



            services.AddAuthentication(x =>
            {
                // Set the default authentication scheme to JWT Bearer
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(a =>
            {
                a.RequireHttpsMetadata = false;
                a.SaveToken = true;
                a.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
                options.AddPolicy("subAdmin", policy => policy.RequireRole("subadmin"));

                options.AddPolicy("Instructor", policy => policy.RequireRole("instructor"));
                options.AddPolicy("Student", policy => policy.RequireRole("student"));
                options.AddPolicy("MainSubAdmin", policy => policy.RequireRole("main-subadmin"));
                options.AddPolicy("SubAdmin , Main-SubAdmin", policy => policy.RequireRole("subadmin", "main-subadmin"));
                options.AddPolicy("Admin&subAdmin", policy =>
                {
                    policy.RequireAssertion(a =>

                        a.User.IsInRole("admin") ||
                        a.User.IsInRole("subadmin")

                    );

                });
                options.AddPolicy("Main-SubAdmin , SubAdmin", policy =>
                {
                    policy.RequireAssertion(a =>


                        a.User.IsInRole("subadmin") ||
                        a.User.IsInRole("main-subadmin")

                    );

                });
                options.AddPolicy("Admin, Main-SubAdmin , SubAdmin", policy =>
                {
                    policy.RequireAssertion(a =>

                        a.User.IsInRole("admin") ||
                        a.User.IsInRole("subadmin") ||
                        a.User.IsInRole("main-subadmin")

                    );

                });
                options.AddPolicy("Admin , Instructor", policy =>
                {
                    policy.RequireAssertion(a =>

                    a.User.IsInRole("admin") ||
                    a.User.IsInRole("instructor"));
                });

                options.AddPolicy("Admin , Main_Sub-Admin", policy =>
                {
                    policy.RequireAssertion(a =>

                    a.User.IsInRole("admin") ||
                    a.User.IsInRole("main-subadmin"));
                });

                options.AddPolicy("Admin , Student", policy =>
                {
                    policy.RequireAssertion(a =>

                    a.User.IsInRole("admin") ||
                    a.User.IsInRole("student"));
                });


                options.AddPolicy("Main-SubAdmin , Student", policy =>
                {
                    policy.RequireAssertion(a =>
                    a.User.IsInRole("main-subadmin") ||
                    a.User.IsInRole("student"));
                });

                options.AddPolicy("Main-SubAdmin ,Instructor , Student", policy =>
                {
                    policy.RequireAssertion(a =>
                    a.User.IsInRole("main-subadmin") ||
                    a.User.IsInRole("instructor") ||
                    a.User.IsInRole("student"));
                }
               );


                options.AddPolicy("Admin , EnrolledInCourse", policy =>
                 policy.Requirements.Add(new CourseParticipantsAuthorizeRequirement()));


                options.AddPolicy("EnrolledInCourse", policy =>
                policy.Requirements.Add(new EnrolledInCourseRequirement()));

                options.AddPolicy("MaterialInEnrolledCourse", policy =>
                policy.Requirements.Add(new MaterialInEnrolledCourseRequeriment()));

                options.AddPolicy("InstructorGiveTheCourse", policy =>
                {
                    policy.Requirements.Add(new EnrolledInCourseRequirement());
                    policy.RequireRole("instructor");
                }


                );

                options.AddPolicy("InstructorwhoGiveTheMaterial", policy =>
                {
                    policy.Requirements.Add(new MaterialInEnrolledCourseRequeriment());
                    policy.RequireRole("instructor");
                }
                );

                options.AddPolicy("MaterialInEnrolledCourseForStudent", policy =>
                {
                    policy.Requirements.Add(new MaterialInEnrolledCourseRequeriment());
                    policy.RequireRole("student");
                }
                );








            });




            services.AddScoped<IAuthorizationHandler, GetMaterialForEnrolledCourseHandler>();
            services.AddScoped<IAuthorizationHandler, EnrolledInCourseHandler>();
            services.AddScoped<IAuthorizationHandler, CourseParticipantsAuthorizeHandler>();

            return services;
        }

    }
}
