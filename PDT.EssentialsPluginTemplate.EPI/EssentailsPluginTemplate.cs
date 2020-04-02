using System;
using Crestron.SimplSharp;                          				// For Basic SIMPL# Classes
using Crestron.SimplSharpPro;                       				// For Basic SIMPL#Pro classes

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using PepperDash.Essentials;
using PepperDash.Essentials.Core;
using PepperDash.Essentials.Core.Config;
using PepperDash.Core;
using PepperDash.Essentials.Bridges;

namespace FlexActionsEPI 
{
	public class FlexActions : Device, IBridge

	{
		public static void LoadPlugin()
		{
			PepperDash.Essentials.Core.DeviceFactory.AddFactoryForType("FlexActions", FlexActions.BuildDevice);	
		}

		public static FlexActions BuildDevice(DeviceConfig dc)
		{
			var config = JsonConvert.DeserializeObject<FlexActionsConfigObject>(dc.Properties.ToString());
			var newMe = new FlexActions(dc.Key, dc.Name, config);
			return newMe;
		}


		GenericSecureTcpIpClient_ForServer Client;
		int temp = 0;

		public FlexActions(string key, string name, FlexActionsConfigObject config)
			: base(key, name)
		{
			
		}

		public void tryThis()
		{
			try
			{
				Debug.Console(0, this, "Trying This");
				if (temp < 17)
				{
					temp++;
				}
				else
				{
					temp = 1;
				}
				JArray a = new JArray();
				a.Add(temp);
				a.Add(1);
				a.Add(eRoutingSignalType.Video);
				JObject o = JObject.FromObject(new
				{
					DeviceKey = "Xtp01Chassis",
					MethodName = "ExecuteSwitchInt",
					

				});
				o["Params"] = a;
				Debug.Console(0, this, "Object {0}", o);
				DeviceJsonApi.DoDeviceActionWithJson(o.ToString());
			}
			catch (Exception ex)
			{
				



			}
		}	        
		
		#region IBridge Members

        public void LinkToApi(Crestron.SimplSharpPro.DeviceSupport.BasicTriList trilist, uint joinStart, string joinMapKey)
        {
            this.LinkToApiExt(trilist, joinStart, joinMapKey);
		}

		#endregion
	}

}

