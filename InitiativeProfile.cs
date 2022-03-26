using AutoMapper;
using TaskManager.Entities;
using TaskManager.Models;
using Task = TaskManager.Entities.Task;

namespace TaskManager
{
    public class InitiativeProfile : Profile
    {
        public InitiativeProfile()
        {
            CreateMap<Initiative, InitiativeDto>()
                .ForMember(i => i.Name, map => map.MapFrom(initiative => initiative.Name))
                .ForMember(i => i.Epics, map => map.MapFrom(initiative => initiative.Epics));

            CreateMap<Epic, EpicDto>()
                .ForMember(e => e.Name, map => map.MapFrom(epic => epic.Name))
                .ForMember(e => e.Initiative, map => map.MapFrom(epic => epic.Initiative))
                .ForMember(e => e.Tasks, map => map.MapFrom(epic => epic.Tasks));

            CreateMap<Task, TaskDto>()
                .ForMember(t => t.Name, map => map.MapFrom(task => task.Name))
                .ForMember(t => t.Signature, map => map.MapFrom(task => task.Signature))
                .ForMember(t => t.Description, map => map.MapFrom(task => task.Description))
                .ForMember(t => t.Epic, map => map.MapFrom(task => task.Epic))
                .ForMember(t => t.Status, map => map.MapFrom(task => task.Status));
        }
    }
}
