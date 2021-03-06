﻿#region using

using System;
using Skyland.Pipeline.Components;

#endregion

namespace Skyland.Pipeline.Internal.Components
{
    internal class InlineJobComponent<TInput, TOutput> : IJobComponent<TInput, TOutput>
    {
        private readonly Func<TInput, TOutput> _function;

        public InlineJobComponent(Func<TInput, TOutput> function)
        {
            _function = function;
        }  

        public TOutput Execute(TInput input)
        {
            return _function(input);
        }
    }
}
