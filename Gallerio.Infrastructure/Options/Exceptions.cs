using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerio.Infrastructure.Options
{
    internal class OptionsNotFoundException : ApplicationException
    {
        public OptionsNotFoundException(Type optionsType) : base($"Requested options not found ({optionsType.Name}).")
        {
        }
    }

    internal class OptionsMemberNotFoundException : ApplicationException
    {
        public OptionsMemberNotFoundException(Type optionsType, string member) : base($"Requested options member {member} not found ({optionsType.Name}).")
        {
        }
    }
}
