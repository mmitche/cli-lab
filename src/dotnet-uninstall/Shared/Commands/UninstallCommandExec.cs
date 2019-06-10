﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.CommandLine;
using Microsoft.DotNet.Tools.Uninstall.Shared.Configs;
using Microsoft.DotNet.Tools.Uninstall.Shared.Filterers;
using Microsoft.DotNet.Tools.Uninstall.Shared.SdkInfo;
using Microsoft.DotNet.Tools.Uninstall.Shared.Utils;
using Microsoft.DotNet.Tools.Uninstall.Windows;
using static Microsoft.DotNet.Tools.Uninstall.Shared.Exceptions.Exceptions;

namespace Microsoft.DotNet.Tools.Uninstall.Shared.Commands
{
    static class UninstallCommandExec
    {
        private static readonly ParseResult CommandLineParseResult
            = CommandLineConfigs.UninstallRootCommand.Parse(Environment.GetCommandLineArgs());

        public static void Execute()
        {
            if (RuntimeInfo.RunningOnWindows)
            {
                Execute(SdkRegistryQuery.GetInstalledSdks());
            }
            else if (RuntimeInfo.RunningOnOSX)
            {
                throw new NotImplementedException();
            }
            else if (RuntimeInfo.RunningOnLinux)
            {
                throw new LinuxNotSupportedException();
            }
        }

        private static void Execute(IEnumerable<ISdkInfo> sdks)
        {
            var option = GetOption();

            if (option == CommandLineConfigs.UninstallVerbosityOption)
            {
                throw new NotImplementedException();
            }

            var filteredSdks = option == null ?
                OptionFilterers.UninstallNoOptionFilterer.Filter(CommandLineParseResult.CommandResult.Arguments, sdks) :
                OptionFilterers.OptionFiltererDictionary[option].Filter(CommandLineParseResult, option, sdks);

            ExecuteUninstall(filteredSdks);
        }

        private static Option GetOption()
        {
            Option specifiedOption = null;

            foreach (var option in CommandLineConfigs.Options)
            {
                if (CommandLineParseResult.HasOption(option))
                {
                    if (specifiedOption == null)
                    {
                        specifiedOption = option;
                    }
                    else
                    {
                        throw new OptionsConflictException();
                    }
                }
            }

            if (specifiedOption != null && CommandLineParseResult.RootCommandResult.Arguments.Count > 0)
            {
                throw new OptionsConflictException();
            }

            return specifiedOption;
        }

        private static void ExecuteUninstall(IEnumerable<ISdkInfo> sdks)
        {
            foreach (var sdk in sdks)
            {
                // TODO: replace this
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(string.Format("Uninstalling: {0}", sdk.UninstallCommand));
                Console.ResetColor();
            }
        }
    }
}
