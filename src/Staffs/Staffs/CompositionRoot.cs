﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Staffs;

[ExcludeFromCodeCoverage]
internal static class CompositionRoot
{
    private static IServiceProvider? _provider;

    public static void SetProvider(IServiceProvider provider)
    {
        _provider = provider;
    }

    public static IServiceScope BeginLifetimeScope() =>
        _provider?.CreateScope() ?? throw new Exception("Service provider not set.");
}