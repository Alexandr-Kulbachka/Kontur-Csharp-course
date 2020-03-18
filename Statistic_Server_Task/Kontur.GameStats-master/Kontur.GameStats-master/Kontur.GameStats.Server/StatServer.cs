using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kontur.GameStats.Server
{
    internal class StatServer : IDisposable
    {

        private readonly HttpListener _listener;

        private RequestHandler _requestHandler;
        private Thread _listenerThread;
        private bool _disposed;
        private volatile bool _isRunning;
        private readonly char[] _splitSymbol = new[] { '/' };

        public StatServer()
        {
            _listener = new HttpListener();
            _requestHandler = new RequestHandler();
        }

        public void Start(string prefix)
        {
            lock (_listener)
            {
                if (!_isRunning)
                {
                    _listener.Prefixes.Clear();
                    _listener.Prefixes.Add(prefix);
                    _listener.Start();

                    _listenerThread = new Thread(Listen)
                    {
                        IsBackground = true,
                        Priority = ThreadPriority.Highest
                    };
                    _listenerThread.Start();

                    _isRunning = true;
                }
            }
        }

        public void Stop()
        {
            lock (_listener)
            {
                if (!_isRunning)
                    return;

                _listener.Stop();

                _listenerThread.Abort();
                _listenerThread.Join();

                _isRunning = false;
            }
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;

            Stop();

            _listener.Close();
        }

        private void Listen()
        {
            while (true)
            {
                try
                {
                    if (_listener.IsListening)
                    {
                        var context = _listener.GetContext();
                        Task.Run(() => HandleContextAsync(context));
                    }
                    else Thread.Sleep(0);
                }
                catch (ThreadAbortException)
                {
                    return;
                }
                catch (Exception error)
                {
                    // TODO: log errors
                }
            }
        }

        private async Task HandleContextAsync(HttpListenerContext listenerContext)
        {            
            var incomingRequest = listenerContext.Request.RawUrl.Split(_splitSymbol, StringSplitOptions.RemoveEmptyEntries);
            _requestHandler.ExecuteRequestProcessing(listenerContext, incomingRequest);
        }
    }
}