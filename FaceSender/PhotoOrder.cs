using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceSender
{
    public class PhotoOrder : TableEntity
    {
        public string Name { get; set; }
    }
}
