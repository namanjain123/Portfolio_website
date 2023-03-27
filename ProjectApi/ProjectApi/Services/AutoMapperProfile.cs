using AutoMapper;
using ProjectApi.DTOs;
using ProjectApi.Model;

namespace ProjectApi.Client
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Define your mappings here
            CreateMap<Jobs, JobsDTO>();
            CreateMap<JobsDTO, Jobs>();
            CreateMap<Projects, ProjectsDTO>();
            CreateMap<ProjectsDTO, Projects>();
            CreateMap<Skills, SkillsDTO>();
            CreateMap<SkillsDTO, Skills>();
        }
    }
    
}
