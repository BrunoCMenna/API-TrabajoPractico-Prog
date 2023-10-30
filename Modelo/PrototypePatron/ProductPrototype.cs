using Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.PrototypePatron
{
    internal interface ProductPrototype
    {
        HistoricProduct Clone();
    }
}
