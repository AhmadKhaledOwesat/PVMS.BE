
using AutoMapper;
using PVMS.Application.Dto;
using PVMS.Domain.Entities;
using PVMS.Domain.Entities.Filters;
using PVMS.Domain.Interfaces;
using Profile = AutoMapper.Profile;

namespace PVMS.Application.Mapper
{
    public class InnovaMapper : Profile, IInnovaMapper
    {
        private readonly IMapper _mapper;
        public InnovaMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        public InnovaMapper()
        {
            CreateMap<TypeCategory, TypeCategoryDto>().ReverseMap();
            CreateMap<PageResult<TypeCategory>, PageResult<TypeCategoryDto>>().ReverseMap();
            CreateMap<TicketCategory, TicketCategoryDto>().ReverseMap();
            CreateMap<PageResult<TicketCategory>, PageResult<TicketCategoryDto>>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<PageResult<Location>, PageResult<LocationDto>>().ReverseMap();
            CreateMap<WorkFlowHistory, WorkFlowHistoryDto>().ReverseMap();
            CreateMap<PageResult<WorkFlowHistory>, PageResult<WorkFlowHistoryDto>>().ReverseMap();
            CreateMap<RoleStatus, RoleStatusDto>().ReverseMap();
            CreateMap<PageResult<RoleStatus>, PageResult<RoleStatusDto>>().ReverseMap();
            CreateMap<ProcedureType, ProcedureTypeDto>().ReverseMap();
            CreateMap<PageResult<ProcedureType>, PageResult<ProcedureTypeDto>>().ReverseMap();
            CreateMap<Nationality, NationalityDto>().ReverseMap();
            CreateMap<PageResult<Nationality>, PageResult<NationalityDto>>().ReverseMap();
            CreateMap<TicketViolation, TicketViolationDto>().ReverseMap();
            CreateMap<PageResult<TicketViolation>, PageResult<TicketViolationDto>>().ReverseMap();
            CreateMap<TicketAttachment, TicketAttachmentDto>().ReverseMap();
            CreateMap<PageResult<TicketAttachment>, PageResult<TicketAttachmentDto>>().ReverseMap();
            CreateMap<Users, UsersDto>().ReverseMap();
            CreateMap<PageResult<Users>, PageResult<UsersDto>>().ReverseMap();
            CreateMap<TicketTypePrice, TicketTypePriceDto>().ReverseMap();
            CreateMap<PageResult<TicketTypePrice>, PageResult<TicketTypePriceDto>>().ReverseMap();
            CreateMap<Citizen, CitizenDto>().ReverseMap();
            CreateMap<PageResult<Citizen>, PageResult<CitizenDto>>().ReverseMap();
            CreateMap<PageResult<Statistic>, PageResult<StatisticDto>>().ReverseMap();
            CreateMap<Statistic, StatisticDto>().ReverseMap();
            CreateMap<PageResult<Ticket>, PageResult<TicketDto>>().ReverseMap();
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<TicketType, TicketTypeDto>().ReverseMap();
            CreateMap<PageResult<TicketType>, PageResult<TicketTypeDto>>().ReverseMap();
            CreateMap<Setting, SettingDto>().ReverseMap();
            CreateMap<PageResult<Setting>, PageResult<SettingDto>>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<PageResult<Role>, PageResult<RoleDto>>().ReverseMap();
            CreateMap<Privilege, PrivilegeDto>()
             .ForMember(dest => dest.ParentName, src => src.MapFrom(a => a.Parent == null ? string.Empty : a.Parent.PrivilegeName))
            .ReverseMap();
            CreateMap<PageResult<Privilege>, PageResult<PrivilegeDto>>().ReverseMap();
            CreateMap<UserRole, UserRoleDto>()
             .ForMember(dest => dest.UserName, src => src.MapFrom(a => a.User == null ? string.Empty : a.User.FullName))
             .ForMember(dest => dest.RoleName, src => src.MapFrom(a => a.Role == null ? string.Empty : a.Role.NameAr))
             .ReverseMap();
            CreateMap<PageResult<UserRole>, PageResult<UserRoleDto>>().ReverseMap();
            CreateMap<UserWorkFlowDefinition, UserWorkFlowDefinitionDto>().ReverseMap();
            CreateMap<PageResult<UserWorkFlowDefinition>, PageResult<UserWorkFlowDefinitionDto>>().ReverseMap();

            CreateMap<RolePrivilege, RolePrivilegeDto>()
          .ForMember(dest => dest.PrivilegeName, src => src.MapFrom(a => a.Privilege == null ? string.Empty : a.Privilege.PrivilegeName))
          .ForMember(dest => dest.RoleName, src => src.MapFrom(a => a.Role == null ? string.Empty : a.Role.NameAr))
          .ReverseMap();
            CreateMap<PageResult<RolePrivilege>, PageResult<RolePrivilegeDto>>().ReverseMap();
            CreateMap<Report, ReportDto>().ReverseMap();
            CreateMap<PageResult<Report>, PageResult<ReportDto>>().ReverseMap();
            CreateMap<ReportParameter, ReportParameterDto>().ReverseMap();
            CreateMap<PageResult<ReportParameter>, PageResult<ReportParameterDto>>().ReverseMap();
            CreateMap<Notifications, NotificationDto>();
            CreateMap<PageResult<Notifications>, PageResult<NotificationDto>>().ReverseMap();
            CreateMap<AditLog, AditLogDto>()
                .ForMember(d => d.CreatedByName, o => o.MapFrom(s => s.Creator != null ? s.Creator.FullName : null))
                .ForMember(d => d.ModifiedByName, o => o.MapFrom(s => s.Modifier != null ? s.Modifier.FullName : null))
                .ReverseMap();
            CreateMap<PageResult<AditLog>, PageResult<AditLogDto>>().ReverseMap();

            CreateMap<WorkFlowStep, WorkFlowStepDto>()
                .ForMember(d => d.Order, o => o.MapFrom(s => s.StepOrder))
                .ForMember(d => d.ApproveRoleIds, o => o.MapFrom(s => s.ApproveRoles.Select(a => a.RoleId).ToList()))
                .ForMember(d => d.RejectRoleIds, o => o.MapFrom(s => s.RejectRoles.Select(a => a.RoleId).ToList()))
                .ForMember(d => d.SkipRoleIds, o => o.MapFrom(s => s.SkipRoles.Select(a => a.RoleId).ToList()))
                .ForMember(d => d.Id, o => o.MapFrom(s => (Guid?)s.Id));
            CreateMap<WorkFlowDefinition, WorkFlowDefinitionDto>()
                .ForMember(d => d.TicketTypeIds, o => o.MapFrom(s => s.TicketTypes.Select(x => x.TicketTypeId).ToList()));
            CreateMap<PageResult<WorkFlowDefinition>, PageResult<WorkFlowDefinitionDto>>();
        }

        public TDestination Map<TSource, TDestination>(TSource source) => _mapper.Map<TSource, TDestination>(source);

        public void Map<TSource, TDestination>(TSource source, TDestination destination) => Map(source, destination);

        public TDestination Map<TDestination>(object source) => _mapper.Map<TDestination>(source);
    }
}
