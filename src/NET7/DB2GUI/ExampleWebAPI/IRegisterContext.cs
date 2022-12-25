using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal interface IRegisterContext
{
    IServiceCollection AddDbContextFactory(IServiceCollection services, ConfigurationManager configuration);

}
