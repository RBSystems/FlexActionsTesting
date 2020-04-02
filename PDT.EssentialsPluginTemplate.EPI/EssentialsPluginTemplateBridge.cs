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

namespace EssentialsPluginTemplateEPI
{
	public static class EssentialsPluginTemplateBridge
	{

		public static void LinkToApiExt(this EssentialsPluginTemplate DspDevice, BasicTriList trilist, uint joinStart, string joinMapKey)
		{
			EssentialsPluginTemplateBridgeJoinMap joinMap = new EssentialsPluginTemplateBridgeJoinMap(joinStart);

			var JoinMapSerialized = JoinMapHelper.GetJoinMapForDevice(joinMapKey);
			
			if (!string.IsNullOrEmpty(JoinMapSerialized))
				joinMap = JsonConvert.DeserializeObject<EssentialsPluginTemplateBridgeJoinMap>(JoinMapSerialized);


		}
	}
	public class EssentialsPluginTemplateBridgeJoinMap : JoinMapBase
	{
		public EssentialsPluginTemplateBridgeJoinMap(uint joinStart) 
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