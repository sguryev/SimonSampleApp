namespace SimonSampleApp.Web
{
    using AutoMapper;
    using Models;
    using Services.OneSignal.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapOneSignalApp();
        }

        private void MapOneSignalApp()
        {
            CreateMap<AppModel, OneSignalAppModel>();
            
            CreateMap<OneSignalAppPostModel, AppPostModel>();
            
            CreateMap<OneSignalAppPutModel, AppPutModel>();
        }
    }
}

