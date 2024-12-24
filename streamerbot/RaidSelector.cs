namespace streamerbot;
using static CPHNameSpace.CPHArgs; // Mimic arguments for inline CPH

/************************************************************************
* COPY AND PASTE BELOW CLASS INTO STREAMER.BOT 
* DO NOT INCLUDE ABOVE CODE!
************************************************************************/
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Collections;
using System.Text.RegularExpressions;

public class CPHInline
{
	public bool Execute()
	{
		// your main code goes here
		return true;
	}

	/**
	 * HELPER FUNCTIONS BELOW
	 **/
	public void PrintArgsVerbose()
    {
        CPH.LogVerbose($"Arguments being passed in...");
        foreach (var arg in args)
        {
            CPH.LogVerbose($"{arg.Key} :: {arg.Value}");
        }
    }

	public string ObjToString(object obj)
    {
        return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonConverter[] { new StringEnumConverter() });
    }

}