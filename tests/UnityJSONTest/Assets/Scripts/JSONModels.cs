using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class SegmentContainer
{
	[JsonProperty ("segments")]
	public List<Segment> Segments { get; set; }
}

public class Segment
{
	[JsonProperty ("begin")]
	public float Begin { get; set; }

	[JsonProperty ("end")]
	public float End { get; set; }

	[JsonProperty ("text")]
	public string Text { get; set; }
}
