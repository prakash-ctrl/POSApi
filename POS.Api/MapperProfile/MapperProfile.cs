using AutoMapper;
using POS.Api.Models.Authenticate;
using POS.Entity;


namespace POS.Api.MapperProfile
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            #region UserModel to UserEntity
            CreateMap<RegisterModel, UserEntity>();
            #endregion
        }
    }
}
