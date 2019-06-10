﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.CommandLine.Invocation;
using Microsoft.DotNet.Tools.Uninstall.Shared.Configs;

namespace Microsoft.DotNet.Tools.Uninstall
{
    internal class Program
    {
        static int Main(string[] args)
        {
            return CommandLineConfigs.UninstallRootCommand.InvokeAsync(args).Result;
        }
    }
}
