﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DotNet.Tools.Uninstall.Shared.SdkInfo;
using Microsoft.DotNet.Tools.Uninstall.Shared.Utils;

namespace Microsoft.DotNet.Tools.Uninstall.Shared.Filterers
{
    class AllButOptionFilterer : ArgFilterer<IEnumerable<string>>
    {
        public override IEnumerable<ISdkInfo> Filter(IEnumerable<string> argValue, IEnumerable<ISdkInfo> sdks)
        {
            var excludedVersions = argValue.Select(versionString => VersionRegex.ParseVersionString(versionString));
            return sdks.Where(sdk => !excludedVersions.Contains(sdk.Version));
        }
    }
}
