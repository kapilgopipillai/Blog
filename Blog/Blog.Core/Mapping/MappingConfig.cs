using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Mapping
{
    public static class MappingConfig
    {
        public static void AddMapping(this IServiceCollection serviceCollection)
        {
            var cfg = new MapperConfiguration(x =>
            {
                x.AddProfile<MappingProfile>();
            });

            var mapper = cfg.CreateMapper();

            serviceCollection.AddSingleton(mapper);
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
