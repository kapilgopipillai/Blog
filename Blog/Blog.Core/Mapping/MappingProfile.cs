
using Blog.Entity;
using Blog.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public override string ProfileName => GetType().FullName;

        public MappingProfile()
        {
            CreateMap<RegistrationModel, RegistrationEnity>();
            CreateMap<RegistrationEnity, RegistrationModel>();


            CreateMap<PostModel, PostEntity>();
            CreateMap<PostEntity, PostModel>();

            CreateMap<ListQueryResult<PostEntity>, ListQueryResult<PostModel>>();
            CreateMap<ListQueryResult<PostModel>, ListQueryResult<PostEntity>>();
        }

    }
}
