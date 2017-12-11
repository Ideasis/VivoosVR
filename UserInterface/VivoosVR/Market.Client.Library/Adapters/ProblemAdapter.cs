using Core.Common.Converters;
using Core.Common.DataModel;
using Core.Common.DataModel.Market;
using Market.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Client.Adapters
{
    public class ProblemAdapter : IAdapter<ProblemModel, Problem>, IAdapter<Problem, ProblemModel>
    {
        public Problem Convert(ProblemModel e)
        {
            throw new NotImplementedException();
        }

        public ProblemModel Convert(Problem e)
        {
            throw new NotImplementedException();
        }
    }
}
