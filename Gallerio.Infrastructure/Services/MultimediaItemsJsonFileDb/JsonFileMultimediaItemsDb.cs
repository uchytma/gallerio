using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Infrastructure.Services.MultimediaItemsJsonFileDb
{
    public class JsonFileMultimediaItemsDb : BaseJsonFileDb<DbModel>
    {
        public JsonFileMultimediaItemsDb(string pathToJsonFile) : base(pathToJsonFile, DbModel.Empty)
        {
        }
    }
}
