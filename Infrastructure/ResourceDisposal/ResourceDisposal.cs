using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ResourceDisposal
{
    public class ResourceDisposal : IDisposable
    {
        private bool _disposed = false;
        public void Dispose()
        {
            Cleanup(false);
            GC.SuppressFinalize(this);
        }
        private void Cleanup(bool calledFromFinalizer)
        {
            if (this._disposed)
                return;
            if (!calledFromFinalizer) {}

            //Dispose Unmanaged resources
            _disposed = true;
        }
    }
}
