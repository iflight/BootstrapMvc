﻿using System;
using System.Collections.Generic;
using System.IO;

namespace BootstrapMvc.Core
{
    public interface IBootstrapContext
    {
        TextWriter Writer { get; }

        void Write(object value);

        ITagBuilder CreateTagBuilder(string tagName);

        string CreateUrl(IDictionary<string, object> routeValues);

        string CreateUrl(IDictionary<string, object> routeValues, string protocol, string hostName);

        string HtmlEncode(string value);

        string GetMessage(int id);

        void Push(DisposableContext value);

        T Peek<T>() where T : DisposableContext;

        DisposableContext Pop();
    }
}
