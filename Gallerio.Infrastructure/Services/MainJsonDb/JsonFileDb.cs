using Gallerio.Api.Options;
using Gallerio.Infrastructure.Options;
using Gallerio.Infrastructure.Services.MultimediaItemsJsonFileDb;
using Gallerio.Infrastructure.Services.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Infrastructure.Services.MainJsonDb
{

    public class JsonFileDb : BaseJsonFileDb<DbModel>
    {
        public JsonFileDb(IOptions<ApplicationOptions> applicationOptions) 
            : base(GetDbPath(applicationOptions), DbModel.Empty)
        {
        }

        private static string GetDbPath(IOptions<ApplicationOptions> applicationOptions)
        {
            var opt = applicationOptions.Value ?? throw new OptionsNotFoundException(typeof(ApplicationOptions));
            return opt.DatabasePath ?? throw new OptionsMemberNotFoundException(typeof(ApplicationOptions), nameof(opt.DatabasePath));
        }
    }
}
