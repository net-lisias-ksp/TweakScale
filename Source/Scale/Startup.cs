﻿/*
		This file is part of TweakScale /L
		© 2018-2021 LisiasT
		© 2015-2018 pellinor
		© 2014 Gaius Godspeed and Biotronic

		TweakScale /L is double licensed, as follows:

		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

		And you are allowed to choose the License that better suit your needs.

		TweakScale /L is distributed in the hope that it will be useful,
		but WITHOUT ANY WARRANTY; without even the implied warranty of
		MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

		You should have received a copy of the SKL Standard License 1.0
		along with TweakScale /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

		You should have received a copy of the GNU General Public License 2.0
		along with TweakScale /L If not, see <https://www.gnu.org/licenses/>.
*/
using System;

using UnityEngine;

using TweakScale.Annotations;

namespace TweakScale
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    internal class Startup : MonoBehaviour
	{
        [UsedImplicitly]
        private void Start()
        {
            Log.force("Version {0}", Version.Text);

            try
            {
                KSPe.Util.Compatibility.Check<Startup>(typeof(Version), typeof(Configuration));
                KSPe.Util.Installation.Check<Startup>("Scale", "TweakScale", null, true);

                if (KSPe.Util.KSP.Version.Current >= KSPe.Util.KSP.Version.FindByVersion(1,9,0))
                {
                    Type calledType = Type.GetType("KSP_Recall.Version, KSP-Recall", false, false);
                    if (null == calledType) GUI.NoRecallAlertBox.Show();
                }
                else if (KSPe.Util.KSP.Version.Current > KSPe.Util.KSP.Version.FindByVersion(1,12,0))
                {
                    GUI.UnsupportedKSPAdviseBox.Show(KSPe.Util.KSP.Version.Current.ToString());
                }
            }
            catch (KSPe.Util.InstallmentException e)
            {
                Log.error(e.ToShortMessage());
                KSPe.Common.Dialogs.ShowStopperAlertBox.Show(e);
            }
        }
	}

    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    internal class ModuleManagerListener : MonoBehaviour
    {
        internal static bool shouldShowWarnings = true;

        [UsedImplicitly]
        public static void ModuleManagerPostLoad()
        {
            shouldShowWarnings = !KSPe.Util.ModuleManagerTools.IsLoadedFromCache;
            Log.detail("ModuleManagerPostLoad handled! shouldShowWarnings is {0}", shouldShowWarnings);
        }
    }

}
