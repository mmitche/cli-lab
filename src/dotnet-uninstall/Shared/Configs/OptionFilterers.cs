﻿using System.Collections;
using System.Collections.Generic;
using System.CommandLine;
using Microsoft.DotNet.Tools.Uninstall.Shared.Filterers;

namespace Microsoft.DotNet.Tools.Uninstall.Shared.Configs
{
    static class OptionFilterers
    {
        public static readonly IDictionary<Option, IFilterer> OptionFiltererDictionary
            = new Dictionary<Option, IFilterer>
            {
                {
                    CommandLineConfigs.UninstallAllOption,
                    new AllOptionFilterer()
                },
                {
                    CommandLineConfigs.UninstallAllLowerPatchesOption,
                    new AllLowerPatchesOptionFilterer()
                },
                {
                    CommandLineConfigs.UninstallAllButLatestOption,
                    new AllButLatestOptionFilterer()
                },
                {
                    CommandLineConfigs.UninstallAllButOption,
                    new AllButOptionFilterer()
                },
                {
                    CommandLineConfigs.UninstallAllBelowOption,
                    new AllBelowOptionFilterer()
                },
                {
                    CommandLineConfigs.UninstallAllPreviewsOption,
                    new AllPreviewsOptionFilterer()
                },
                {
                    CommandLineConfigs.UninstallAllPreviewsButLatestOption,
                    new AllPreviewsButLatestOptionFilterer()
                },
                {
                    CommandLineConfigs.UninstallMajorMinorOption,
                    new MajorMinorOptionFilterer()
                }
            };

        public static readonly ArgFilterer<IEnumerable<string>> UninstallNoOptionFilterer = new NoOptionFilterer();
    }
}
