using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Crestron.SimplSharp;
using Crestron.SimplSharpPro.DeviceSupport;

using PepperDash.Core;
using PepperDash.Essentials.Core;
using PepperDash.Essentials.Bridges;
using Crestron.SimplSharp.Reflection;
using Newtonsoft.Json;

namespace FlexActionsEPI
{
	public static class FlexActionsBridge
	{

		public static void LinkToApiExt(this FlexActions FlexActionsEpi, BasicTriList trilist, uint joinStart, string joinMapKey)
		{
			FlexActionsBridgeJoinMap joinMap = new FlexActionsBridgeJoinMap(joinStart);

			var JoinMapSerialized = JoinMapHelper.GetJoinMapForDevice(joinMapKey);
			trilist.SetSigTrueAction(1, () => FlexActionsEpi.tryThis());
			if (!string.IsNullOrEmpty(JoinMapSerialized))
				joinMap = JsonConvert.DeserializeObject<FlexActionsBridgeJoinMap>(JoinMapSerialized);


		}
	}
	public class FlexActionsBridgeJoinMap : JoinMapBase
	{
		
		public FlexActionsBridgeJoinMap(uint joinStart) 
		{
			OffsetJoinNumbers(joinStart);
		}

		public override void OffsetJoinNumbers(uint joinStart)
		{
            GetType()
                .GetCType()
                .GetProperties()
                .Where(x => x.PropertyType == typeof(uint))
                .ToList()
                .ForEach(prop => prop.SetValue(this, (uint)prop.GetValue(this, null) + joinStart - 1, null));
		}

	}
}